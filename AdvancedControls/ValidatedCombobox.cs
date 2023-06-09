﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls {
    public partial class ValidatedCombobox : UserControl {
        private ToolTip _toolTip;
        private string _validityMessage = "";
        private ValidityState _validityState = ValidityState.None;
        private int _validityBorderSize = 4;
        private Control _currentToolTipControl;

        #region Properties
        public bool FormattingEnabled {
            get => comboBox1.FormattingEnabled;
            set => comboBox1.FormattingEnabled = value;
        }
        public object DataSource {
            get => comboBox1.DataSource;
            set => comboBox1.DataSource = value;
        }
        public object SelectedItem {
            get => comboBox1.SelectedItem;
            set => comboBox1.SelectedItem = value;
        }
        public int SelectedIndex {
            get => comboBox1.SelectedIndex;
            set => comboBox1.SelectedIndex = value;
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
                    (comboBox1.Height + comboBox1.Margin.Vertical) +
                    _validityBorderSize;
            }
        }
        public ToolTip StateToolTip {
            get => _toolTip;
            set => _toolTip = value;
        }
        #endregion

        #region Events
        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectionChangeCommitted;
        #endregion



        public ValidatedCombobox() {
            InitializeComponent();
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

            _toolTip?.SetToolTip(comboBox1, message);
        }

        /// <summary>
        /// Corrects the height dimension.
        /// </summary>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) {
            height = Padding.Vertical +
                    (comboBox1.Height + comboBox1.Margin.Vertical) +
                    _validityBorderSize;

            base.SetBoundsCore(x, y, width, height, specified);
        }



        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e) {
            if (SelectionChangeCommitted != null) SelectionChangeCommitted(this, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, e);
        }

        private async void ValidatedCombobox_MouseMove(object sender, MouseEventArgs e) {
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

        private void ValidatedCombobox_MouseEnter(object sender, EventArgs e) {
            _toolTip?.SetToolTip(this, _validityMessage);
        }
    }
}
