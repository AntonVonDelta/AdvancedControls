namespace AdvancedControls.AdvancedCombobox {
    partial class AdvancedCombobox<T> {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.comboBox1 = new AdvancedControls.ValidatedCombobox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = null;
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndex = -1;
            this.comboBox1.SelectedItem = null;
            this.comboBox1.Size = new System.Drawing.Size(388, 25);
            this.comboBox1.StateToolTip = null;
            this.comboBox1.TabIndex = 0;
            this.comboBox1.ValidityBorderSize = 4;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            this.comboBox1.Leave += new System.EventHandler(this.comboBox1_Leave);
            // 
            // AdvancedCombobox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox1);
            this.Name = "AdvancedCombobox";
            this.Size = new System.Drawing.Size(388, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private ValidatedCombobox comboBox1;
    }
}
