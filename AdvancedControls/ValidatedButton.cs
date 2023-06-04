using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls {
    public class ValidatedButton : Button {
        private ValidityState _validityState = ValidityState.None;
        private bool _applyValidityBorder;
        private Color _validityBorderColor;
        private int _validityBorderSize = 4;
        private ToolTip _toolTip;

        public ValidityState ValidityState => _validityState;
        public int ValidityBorderSize {
            get => _validityBorderSize;
            set {
                _validityBorderSize = value;
                Margin = new Padding(_validityBorderSize);
            }
        }
        public ToolTip StateToolTip {
            get => _toolTip;
            set => _toolTip = value;
        }


        public ValidatedButton() { }

        public void ClearValidity() {
            SetValidityState(ValidityState.None, null);
        }
        public void SetValidityState(ValidityState state, string message) {
            _validityState = state;

            switch (state) {
                case ValidityState.None:
                    _applyValidityBorder = false;
                    break;

                case ValidityState.Error:
                    _applyValidityBorder = true;
                    _validityBorderColor = Color.Red;
                    break;

                case ValidityState.Information:
                    _applyValidityBorder = true;
                    _validityBorderColor = Color.DodgerBlue;
                    break;
            }

            _toolTip?.SetToolTip(this, message);

            Invalidate();
        }

        private void DrawBorder(Graphics graphics, Color color, int thickness) {
            ControlPaint.DrawBorder(graphics, ClientRectangle,
                color, thickness, ButtonBorderStyle.None,
                color, thickness, ButtonBorderStyle.None,
                color, thickness, ButtonBorderStyle.None,
                color, thickness, ButtonBorderStyle.Solid);
        }


        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if (_applyValidityBorder)
                DrawBorder(e.Graphics, _validityBorderColor, _validityBorderSize);
        }
    }
}
