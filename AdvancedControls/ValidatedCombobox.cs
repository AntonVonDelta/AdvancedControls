using System;
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
        private ValidityState _validityState = ValidityState.None;
        private int _validityBorderSize = 4;
        private ToolTip _toolTip;

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
        #endregion



        public ValidatedCombobox() {
            InitializeComponent();

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
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

            _toolTip?.SetToolTip(this, message);
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



        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, e);
        }
    }
}
