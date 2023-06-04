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
    public partial class ValidatedButton : UserControl {
        private ToolTip _toolTip;
        private ValidityState _validityState = ValidityState.None;
        private int _validityBorderSize = 4;

        #region Properties
        public bool UseVisualStyleBackColor {
            get => button1.UseVisualStyleBackColor;
            set => button1.UseVisualStyleBackColor = value;
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

            _toolTip?.SetToolTip(button1, message);
        }



        private void Button1_Click(object sender, EventArgs e) {
            if (Click != null) Click(this, e);
        }
    }
}
