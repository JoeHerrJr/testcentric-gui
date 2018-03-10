﻿// ***********************************************************************
// Copyright (c) 2018 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using NUnit.Engine;

namespace TestCentric.Gui.Model
{
    public class TestModel : ITestModel
    {
        private const string PROJECT_LOADER_EXTENSION_PATH = "/NUnit/Engine/TypeExtensions/IProjectLoader";
        private const string NUNIT_PROJECT_LOADER = "NUnit.Engine.Services.ProjectLoaders.NUnitProjectLoader";
        private const string VISUAL_STUDIO_PROJECT_LOADER = "NUnit.Engine.Services.ProjectLoaders.VisualStudioProjectLoader";

        private TestEventDispatcher _events;

        #region Constructor

        public TestModel(ITestEngine testEngine)
        {
            TestEngine = testEngine;
            RecentFiles = GetService<IRecentFiles>();

            foreach (var node in GetService<IExtensionService>().GetExtensionNodes(PROJECT_LOADER_EXTENSION_PATH))
            {
                if (node.TypeName == NUNIT_PROJECT_LOADER)
                    NUnitProjectSupport = true;
                else if (node.TypeName == VISUAL_STUDIO_PROJECT_LOADER)
                    VisualStudioSupport = true;
            }

            _events = new TestEventDispatcher(this);
        }

        #endregion

        #region ITestModel Members

        #region General Properties

        // Event Dispatcher
        public ITestEvents Events { get { return _events; } }

        // Project Support
        public bool NUnitProjectSupport { get; }
        public bool VisualStudioSupport { get; }

        // Runtime Support
        private List<IRuntimeFramework> _runtimes;
        public IList<IRuntimeFramework> AvailableRuntimes
        {
            get
            {
                if (_runtimes == null)
                    _runtimes = GetAvailableRuntimes();

                return _runtimes;
            }
        }

        // Result Format Support
        private List<string> _resultFormats;
        public IEnumerable<string> ResultFormats
        {
            get
            {
                if (_resultFormats == null)
                {
                    _resultFormats = new List<string>();
                    foreach (string format in GetService<IResultService>().Formats)
                        _resultFormats.Add(format);
                }

                return _resultFormats;
            }
        }

        #endregion

        #region Current State of the Model

        public bool IsPackageLoaded { get { return TestPackage != null; } }

        public List<string> TestFiles { get; } = new List<string>();

        public IDictionary<string, object> PackageSettings { get; } = new Dictionary<string, object>();

        public TestNode Tests { get; private set; }
        public bool HasTests { get { return Tests != null; } }

        public IList<string> AvailableCategories { get; private set; }

        public bool IsTestRunning
        {
            get { return Runner != null && Runner.IsTestRunning;  }
        }

        public bool HasResults
        {
            get { return Results.Count > 0; }
        }

        public string[] SelectedCategories { get; private set; }

        public bool ExcludeSelectedCategories { get; private set; }

        public TestFilter CategoryFilter { get; private set; } = TestFilter.Empty;

        #endregion

        #region Methods

        public void NewProject()
        {
            NewProject("Dummy");
        }

        public const string PROJECT_SAVE_MESSAGE =
            "It's not yet decided if we will implement saving of projects. The alternative is to require use of the project editor, which already supports this.";

        public void NewProject(string filename)
        {
            throw new NotImplementedException(PROJECT_SAVE_MESSAGE);
        }

        public void SaveProject()
        {
            throw new NotImplementedException(PROJECT_SAVE_MESSAGE);
        }

        public void LoadTests(IList<string> files)
        {
            if (IsPackageLoaded)
                UnloadTests();

            TestFiles.Clear();
            TestFiles.AddRange(files);
            _events.FireTestsLoading();

            TestPackage = MakeTestPackage(files);

            Runner = TestEngine.GetRunner(TestPackage);

            Tests = new TestNode(Runner.Explore(TestFilter.Empty));
            AvailableCategories = GetAvailableCategories();

            Results.Clear();

            _events.FireTestLoaded(Tests);

            foreach (var subPackage in TestPackage.SubPackages)
                RecentFiles.SetMostRecent(subPackage.FullName);
        }

        public void UnloadTests()
        {
            _events.FireTestsUnloading();

            Runner.Unload();
            Tests = null;
            AvailableCategories = null;
            TestPackage = null;
            TestFiles.Clear();
            Results.Clear();

            _events.FireTestUnloaded();
        }

        public void ReloadTests()
        {
            Runner.Unload();
            Results.Clear();
            Tests = null;

            TestPackage = MakeTestPackage(TestFiles);

            Runner = TestEngine.GetRunner(TestPackage);

            Tests = new TestNode(Runner.Explore(TestFilter.Empty));
            AvailableCategories = GetAvailableCategories();

            Results.Clear();

            _events.FireTestReloaded(Tests);
        }

        public void RunAllTests()
        {
            RunTests(CategoryFilter);
        }

        public void RunTests(ITestItem testItem)
        {
            if (testItem == null)
                throw new ArgumentNullException("testItem");

            var filter = testItem.GetTestFilter();

            if (!CategoryFilter.IsEmpty())
                filter = Filters.MakeAndFilter( filter, CategoryFilter );

            RunTests(filter);
        }

        private void RunTests(TestFilter filter)
        {
            Runner.RunAsync(_events, filter);
        }

        public void CancelTestRun()
        {
            Runner.StopRun(false);
        }

        public void SaveResults(string filePath, string format = "nunit3")
        {
            var resultService = GetService<IResultService>();
            var resultWriter = resultService.GetResultWriter(format, new object[0]);
            var results = GetResultForTest(Tests);
            resultWriter.WriteResultFile(results.Xml, filePath);
        }

        public ResultNode GetResultForTest(TestNode testNode)
        {
            if (testNode != null)
            {
                ResultNode result;
                if (Results.TryGetValue(testNode.Id, out result))
                    return result;
            }

            return null;
        }

        public void SelectCategories(string[] categories, bool exclude)
        {
            SelectedCategories = categories;
            ExcludeSelectedCategories = exclude;

            UpdateCategoryFilter();

            _events.FireCategorySelectionChanged();
        }

        private void UpdateCategoryFilter()
        {
            var catFilter = TestFilter.Empty;

            if (SelectedCategories != null && SelectedCategories.Length > 0)
            {
                catFilter = Filters.MakeCategoryFilter(SelectedCategories);

                if (ExcludeSelectedCategories)
                    catFilter = Filters.MakeNotFilter(catFilter);
            }

            CategoryFilter = catFilter;
        }

        public void NotifySelectedItemChanged(ITestItem testItem)
        {
            _events.FireSelectedItemChanged(testItem);
        }

        #endregion

        #endregion

        #region IServiceLocator Implementation

        public T GetService<T>() where T: class
        {
            return TestEngine.Services.GetService<T>();
        }

        public object GetService(Type serviceType)
        {
            return TestEngine.Services.GetService(serviceType);
        }

        #endregion

        #region ITestEventListener Implementation


        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            if (IsPackageLoaded)
                UnloadTests();

            if (Runner != null)
                Runner.Dispose();
        }

        #endregion

        #region Private Properties

        private IRecentFiles RecentFiles { get; }

        private ITestEngine TestEngine { get; set; }
        
        private ITestRunner Runner { get; set; }
        
        private TestPackage TestPackage { get; set; }

        internal IDictionary<string, ResultNode> Results { get; } = new Dictionary<string, ResultNode>();

        #endregion

        #region Helper Methods

        // Public for testing only
        public TestPackage MakeTestPackage(IList<string> testFiles)
        {
            var package = new TestPackage(testFiles);

            // We use AddSetting rather than just setting the value because
            // it propagates the setting to all subprojects.

            //if (Options.InternalTraceLevel != null)
            //    package.AddSetting(EnginePackageSettings.InternalTraceLevel, Options.InternalTraceLevel);

            // We use shadow copy so that the user may re-compile while the gui is running.
            package.AddSetting(EnginePackageSettings.ShadowCopyFiles, true);

            // Temp settings for testing
            package.AddSetting(EnginePackageSettings.ProcessModel, "Single");
            package.AddSetting(EnginePackageSettings.InternalTraceLevel, "Debug");

            foreach (var entry in PackageSettings)
                package.AddSetting(entry.Key, entry.Value);

            return package;
        }

        // The engine returns more values than we really want.
        // For example, we don't currently distinguish between
        // client and full profiles when executing tests. We
        // drop unwanted entries here. Even if some of these items
        // are removed in a later version of the engine, we may
        // have to retain this code to work with older engines.
        private List<IRuntimeFramework> GetAvailableRuntimes()
        {
            var runtimes = new List<IRuntimeFramework>();

            foreach (var runtime in GetService<IAvailableRuntimes>().AvailableRuntimes)
            {
                // Nothing below 2.0
                if (runtime.ClrVersion.Major < 2)
                    continue;

                // Remove erroneous entries for 4.5 Client profile
                if (IsErroneousNet45ClientProfile(runtime))
                    continue;

                // Skip duplicates
                var duplicate = false;
                foreach (var rt in runtimes)
                    if (rt.DisplayName == runtime.DisplayName)
                    {
                        duplicate = true;
                        break;
                    }

                if (!duplicate)
                    runtimes.Add(runtime);
            }

            runtimes.Sort((x, y) => x.DisplayName.CompareTo(y.DisplayName));

            // Now eliminate client entries where full entry follows
            for (int i = runtimes.Count - 2; i >= 0; i--)
            {
                var rt1 = runtimes[i];
                var rt2 = runtimes[i + 1];

                if (rt1.Id != rt2.Id)
                    continue;

                if (rt1.Profile == "Client" && rt2.Profile == "Full")
                    runtimes.RemoveAt(i);
            }

            return runtimes;
        }

        // Some early versions of the engine return .NET 4.5 Client profile, which doesn't really exist
        private static bool IsErroneousNet45ClientProfile(IRuntimeFramework runtime)
        {
            return runtime.FrameworkVersion.Major == 4 && runtime.FrameworkVersion.Minor > 0 && runtime.Profile == "Client";
        }

        public IList<string> GetAvailableCategories()
        {
            var categories = new Dictionary<string, string> ();
            CollectCategories(Tests, categories);

            var list = new List<string>(categories.Values);
            list.Sort();
            return list;
        }

        private void CollectCategories(TestNode test, Dictionary<string, string> categories)

        {
            foreach (string name in test.GetPropertyList("Category").Split(new[] { ',' }))
                if (IsValidCategoryName(name))
                    categories[name] = name;

            if (test.IsSuite)
                foreach (TestNode child in test.Children)
                    CollectCategories(child, categories);
        }

        public static bool IsValidCategoryName(string name)
        {
            return name.Length > 0 && name.IndexOfAny(new char[] { ',', '!', '+', '-' }) < 0;
        }

        #endregion
    }
}