﻿using AdvancedControls.AdvancedCombobox;
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new AdvancedControls.AdvancedButton.AdvancedButton();
            this.comboBox2 = new TestAdvancedControls.CustomControls.CastedAdvancedCombobox();
            this.comboBox1 = new TestAdvancedControls.CustomControls.CastedAdvancedCombobox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(200, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 34);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 36);
            this.button1.StateToolTip = null;
            this.button1.TabIndex = 4;
            this.button1.ValidityBorderSize = 6;
            this.button1.Click += new System.EventHandler<AdvancedControls.DeferralEventArgs>(this.button1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.AutoSize = true;
            this.comboBox2.Location = new System.Drawing.Point(88, 252);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(373, 25);
            this.comboBox2.StateToolTip = null;
            this.comboBox2.TabIndex = 3;
            this.comboBox2.ValidityBorderSize = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.AutoSize = true;
            this.comboBox1.Location = new System.Drawing.Point(40, 95);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(343, 25);
            this.comboBox1.StateToolTip = null;
            this.comboBox1.TabIndex = 2;
            this.comboBox1.ValidityBorderSize = 2;
            this.comboBox1.SelectedItemChanged += new System.EventHandler<AdvancedControls.AdvancedCombobox.SelectedItemChangedEventArgs<string>>(this.comboBox1_SelectedItemChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomControls.CastedAdvancedCombobox comboBox1;
        private CustomControls.CastedAdvancedCombobox comboBox2;
        private AdvancedControls.AdvancedButton.AdvancedButton button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button2;
    }
}