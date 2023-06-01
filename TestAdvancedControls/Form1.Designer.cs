using AdvancedControls.AdvancedCombobox;
using Microsoft.VisualStudio.Threading;

namespace TestAdvancedControls {
    partial class Form1 {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.comboBox1 = new TestAdvancedControls.CustomControls.CastedAdvancedCombobox();
            this.comboBox2 = new TestAdvancedControls.CustomControls.CastedAdvancedCombobox();
            this.button1 = new AdvancedControls.AdvancedButton.AdvancedButton();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(40, 107);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(316, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedItemChanged += new System.EventHandler<AdvancedControls.AdvancedCombobox.SelectedItemChangedEventArgs<string>>(this.comboBox1_SelectedItemChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(86, 152);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(373, 21);
            this.comboBox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(298, 65);
            this.button1.TabIndex = 4;
            this.button1.Click += new System.EventHandler<AdvancedControls.DeferralEventArgs>(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private CustomControls.CastedAdvancedCombobox comboBox1;
        private CustomControls.CastedAdvancedCombobox comboBox2;
        private AdvancedControls.AdvancedButton.AdvancedButton button1;
    }
}