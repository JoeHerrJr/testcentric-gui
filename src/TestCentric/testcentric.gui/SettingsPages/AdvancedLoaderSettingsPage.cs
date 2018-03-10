// ***********************************************************************
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using NUnit.Engine;

namespace TestCentric.Gui.SettingsPages
{
	public class AdvancedLoaderSettingsPage : SettingsPage
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox disableShadowCopyCheckBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.HelpProvider helpProvider1;
        private CheckBox principalPolicyCheckBox;
        private Label label7;
        private Label label6;
        private GroupBox groupBox1;
        private ListBox principalPolicyListBox;
        private Label label1;
        private ComboBox traceLevelComboBox;
        private Label label4;
        private Label label5;
        private GroupBox groupBox2;
        private System.ComponentModel.IContainer components = null;

		public AdvancedLoaderSettingsPage( string key ) : base( key )
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedLoaderSettingsPage));
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.disableShadowCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.principalPolicyCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.principalPolicyListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.traceLevelComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Shadow Copy";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(139, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 8);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // disableShadowCopyCheckBox
            // 
            this.disableShadowCopyCheckBox.AutoSize = true;
            this.helpProvider1.SetHelpString(this.disableShadowCopyCheckBox, resources.GetString("disableShadowCopyCheckBox.HelpString"));
            this.disableShadowCopyCheckBox.Location = new System.Drawing.Point(24, 32);
            this.disableShadowCopyCheckBox.Name = "disableShadowCopyCheckBox";
            this.helpProvider1.SetShowHelp(this.disableShadowCopyCheckBox, true);
            this.disableShadowCopyCheckBox.Size = new System.Drawing.Size(130, 17);
            this.disableShadowCopyCheckBox.TabIndex = 2;
            this.disableShadowCopyCheckBox.Text = "Disable Shadow Copy";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(139, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 59);
            this.label2.TabIndex = 6;
            this.label2.Text = "Shadow copy should normally be enabled. If it is disabled, the TestCentric Gui may not " +
    "function correctly.";
            // 
            // principalPolicyCheckBox
            // 
            this.principalPolicyCheckBox.AutoSize = true;
            this.principalPolicyCheckBox.Location = new System.Drawing.Point(24, 145);
            this.principalPolicyCheckBox.Name = "principalPolicyCheckBox";
            this.principalPolicyCheckBox.Size = new System.Drawing.Size(214, 17);
            this.principalPolicyCheckBox.TabIndex = 9;
            this.principalPolicyCheckBox.Text = "Set Principal Policy for test AppDomains";
            this.principalPolicyCheckBox.UseVisualStyleBackColor = true;
            this.principalPolicyCheckBox.CheckedChanged += new System.EventHandler(this.principalPolicyCheckBox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Policy:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Principal Policy";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(139, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 8);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // principalPolicyListBox
            // 
            this.principalPolicyListBox.FormattingEnabled = true;
            this.principalPolicyListBox.Items.AddRange(new object[] {
            "UnauthenticatedPrincipal",
            "NoPrincipal",
            "WindowsPrincipal"});
            this.principalPolicyListBox.Location = new System.Drawing.Point(139, 171);
            this.principalPolicyListBox.Name = "principalPolicyListBox";
            this.principalPolicyListBox.Size = new System.Drawing.Size(241, 69);
            this.principalPolicyListBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Warning:";
            // 
            // traceLevelComboBox
            // 
            this.traceLevelComboBox.FormattingEnabled = true;
            this.traceLevelComboBox.Items.AddRange(new object[] {
            "Default",
            "Off",
            "Error",
            "Warning",
            "Info",
            "Verbose"});
            this.traceLevelComboBox.Location = new System.Drawing.Point(137, 298);
            this.traceLevelComboBox.Name = "traceLevelComboBox";
            this.traceLevelComboBox.Size = new System.Drawing.Size(89, 21);
            this.traceLevelComboBox.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Trace Level:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 267);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Internal Trace";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(139, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 8);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // AdvancedLoaderSettingsPage
            // 
            this.Controls.Add(this.traceLevelComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.principalPolicyListBox);
            this.Controls.Add(this.principalPolicyCheckBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.disableShadowCopyCheckBox);
            this.Name = "AdvancedLoaderSettingsPage";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public override void LoadSettings()
		{
            disableShadowCopyCheckBox.Checked = !Settings.GetSetting("Options.TestLoader.ShadowCopyFiles", true);

            principalPolicyCheckBox.Checked = principalPolicyListBox.Enabled =
                Settings.GetSetting("Options.TestLoader.SetPrincipalPolicy", false);
            principalPolicyListBox.SelectedIndex = (int)(PrincipalPolicy)Settings.GetSetting("Options.TestLoader.PrincipalPolicy", PrincipalPolicy.UnauthenticatedPrincipal);

            traceLevelComboBox.SelectedIndex = (int)(InternalTraceLevel)Settings.GetSetting("Options.InternalTraceLevel", InternalTraceLevel.Default);
        }

        public override void ApplySettings()
		{
			Settings.SaveSetting( "Options.TestLoader.ShadowCopyFiles", !disableShadowCopyCheckBox.Checked );

            Settings.SaveSetting("Options.TestLoader.SetPrincipalPolicy", principalPolicyCheckBox.Checked);

            if (principalPolicyCheckBox.Checked)
                Settings.SaveSetting("Options.TestLoader.PrincipalPolicy", (PrincipalPolicy)principalPolicyListBox.SelectedIndex);
            else
                Settings.RemoveSetting("Options.TestLoader.PrincipalPolicy");

            InternalTraceLevel level = (InternalTraceLevel)traceLevelComboBox.SelectedIndex;
            Settings.SaveSetting("Options.InternalTraceLevel", level);
            //InternalTrace.Level = level;
        }

        public override bool HasChangesRequiringReload
		{
			get
			{
				bool oldShadowCopyFiles = Settings.GetSetting( "Options.TestLoader.ShadowCopyFiles", true );
                bool oldSetPrincipalPolicy = Settings.GetSetting("Options.TestLoader.SetPrincipalPolicy", false);
                PrincipalPolicy oldPrincipalPolicy = (PrincipalPolicy)Settings.GetSetting("Options.TestLoader.PrincipalPolicy", PrincipalPolicy.UnauthenticatedPrincipal);

                return disableShadowCopyCheckBox.Checked == oldShadowCopyFiles // Use == because the checkbox disables
                    || principalPolicyCheckBox.Checked != oldSetPrincipalPolicy
                    || principalPolicyListBox.SelectedIndex != (int)oldPrincipalPolicy;

			}
		}

        private void principalPolicyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            principalPolicyListBox.Enabled = principalPolicyCheckBox.Checked;
        }
	}
}
