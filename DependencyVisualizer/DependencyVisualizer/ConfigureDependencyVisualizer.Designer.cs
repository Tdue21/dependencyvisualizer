namespace DependencyVisualizer
{
    /// <summary>
    /// Configuration UI for Dependency visualizer
    /// </summary>
    partial class ConfigureDependencyVisualizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureDependencyVisualizer));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbHideNet2 = new System.Windows.Forms.CheckBox();
            this.cbHideNet3 = new System.Windows.Forms.CheckBox();
            this.cbGeneratePng = new System.Windows.Forms.CheckBox();
            this.cbGenerateSvg = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.cbHideNet2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbHideNet3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbGeneratePng, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbGenerateSvg, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // cbHideNet2
            // 
            resources.ApplyResources(this.cbHideNet2, "cbHideNet2");
            this.cbHideNet2.Checked = global::DependencyVisualizer.Properties.Settings.Default.HideNet2Assemblies;
            this.cbHideNet2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideNet2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DependencyVisualizer.Properties.Settings.Default, "HideNet2Assemblies", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbHideNet2.Name = "cbHideNet2";
            this.toolTip.SetToolTip(this.cbHideNet2, resources.GetString("cbHideNet2.ToolTip"));
            this.cbHideNet2.UseVisualStyleBackColor = true;
            // 
            // cbHideNet3
            // 
            resources.ApplyResources(this.cbHideNet3, "cbHideNet3");
            this.cbHideNet3.Checked = global::DependencyVisualizer.Properties.Settings.Default.HideNet3Assemblies;
            this.cbHideNet3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHideNet3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DependencyVisualizer.Properties.Settings.Default, "HideNet3Assemblies", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbHideNet3.Name = "cbHideNet3";
            this.toolTip.SetToolTip(this.cbHideNet3, resources.GetString("cbHideNet3.ToolTip"));
            this.cbHideNet3.UseVisualStyleBackColor = true;
            // 
            // cbGeneratePng
            // 
            resources.ApplyResources(this.cbGeneratePng, "cbGeneratePng");
            this.cbGeneratePng.Checked = global::DependencyVisualizer.Properties.Settings.Default.GeneratePng;
            this.cbGeneratePng.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGeneratePng.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DependencyVisualizer.Properties.Settings.Default, "GeneratePng", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbGeneratePng.Name = "cbGeneratePng";
            this.toolTip.SetToolTip(this.cbGeneratePng, resources.GetString("cbGeneratePng.ToolTip"));
            this.cbGeneratePng.UseVisualStyleBackColor = true;
            // 
            // cbGenerateSvg
            // 
            resources.ApplyResources(this.cbGenerateSvg, "cbGenerateSvg");
            this.cbGenerateSvg.Checked = global::DependencyVisualizer.Properties.Settings.Default.GenerateSvg;
            this.cbGenerateSvg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGenerateSvg.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DependencyVisualizer.Properties.Settings.Default, "GenerateSvg", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbGenerateSvg.Name = "cbGenerateSvg";
            this.toolTip.SetToolTip(this.cbGenerateSvg, resources.GetString("cbGenerateSvg.ToolTip"));
            this.cbGenerateSvg.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.btnOk);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ConfigureDependencyVisualizer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConfigureDependencyVisualizer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox cbHideNet2;
        private System.Windows.Forms.CheckBox cbHideNet3;
        private System.Windows.Forms.CheckBox cbGeneratePng;
        private System.Windows.Forms.CheckBox cbGenerateSvg;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ToolTip toolTip;
    }
}