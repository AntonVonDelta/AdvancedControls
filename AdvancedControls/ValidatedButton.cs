﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls {
    public partial class ValidatedButton : UserControl {
        private ToolTip _toolTip;
        private string _validityMessage = "";
        private ValidityState _validityState = ValidityState.None;
        private int _validityBorderSize = 4;
        private Control _currentToolTipControl;

        #region Properties
        public bool UseVisualStyleBackColor {
            get => button1.UseVisualStyleBackColor;
            set => button1.UseVisualStyleBackColor = value;
        }

        /// <summary>
        /// Issues may appear when this property is set through
        /// the base class.
        /// </summary>
        public new bool Enabled {
            get => button1.Enabled;
            set => button1.Enabled = value;
        }
        #endregion

        #region ValidationProperties
        public ValidityState ValidityState => _validityState;
        public int ValidityBorderSize {
            get => _validityBorderSize;
            set {
                _validityBorderSize = value;
                panelBorder.Height = value;

                Height = Padding.Vertical +
                    (button1.Height + button1.Margin.Vertical) +
                    _validityBorderSize;
            }
        }
        public ToolTip StateToolTip {
            get => _toolTip;
            set => _toolTip = value;
        }
        #endregion

        #region Events
        public new event EventHandler Click;
        #endregion



        public ValidatedButton() {
            InitializeComponent();

            button1.Click += Button1_Click;
        }

        public void ClearValidity() {
            SetValidityState(ValidityState.None, null);
        }
        public void SetValidityState(ValidityState state, string message) {
            _validityState = state;

            switch (state) {
                case ValidityState.None:
                    panelBorder.Visible = false;
                    break;

                case ValidityState.Error:
                    panelBorder.BackColor = Color.Red;
                    panelBorder.Visible = true;
                    break;

                case ValidityState.Information:
                    panelBorder.BackColor = Color.RoyalBlue;
                    panelBorder.Visible = true;
                    break;
            }

            _validityMessage = message;
            _toolTip?.SetToolTip(button1, _validityMessage);
        }



        private void Button1_Click(object sender, EventArgs e) {
            if (Click != null) Click(this, e);
        }

        private async void ValidatedButton_MouseMove(object sender, MouseEventArgs e) {
            Control control = GetChildAtPoint(e.Location);

            if (control == null) return;

            // Show the tooltip regardless of whether the control is enabled or not
            // Tooltip has serious issues - it seems its dependednt on messages being pumped
            // after SetToolTip is called. Solved only by calling SetToolTip twice.
            if (!control.Enabled && _currentToolTipControl == null) {
                _currentToolTipControl = control;

                _toolTip?.SetToolTip(this, _validityMessage);
                await Task.Delay(1);

                _toolTip?.SetToolTip(this, _validityMessage);
                await Task.Delay(_toolTip.AutoPopDelay);

                _currentToolTipControl = null;
            }
        }

        private void ValidatedButton_MouseEnter(object sender, EventArgs e) {
            _toolTip?.SetToolTip(this, _validityMessage);
        }
    }
}
