using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace AdvancedControls.AdvancedButton {
    public partial class AdvancedButton : UserControl {
        private ValidityState _validityState = ValidityState.None;
        private bool _applyValidityBorder;
        private Color _validityBorderColor;
        private string _validityMessage;
        private ToolTip _toolTip;

        /// <summary>
        /// Hide BorderStyle because the usercontrol will have the button's border
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new BorderStyle BorderStyle { get; set; }
        public int ValidityBorderSize { get; set; } = 4;
        public ValidityState ValidityState => _validityState;
        public ToolTip StateToolTip {
            get => _toolTip;
            set => _toolTip = value;
        }


        public new event EventHandler<DeferralEventArgs> Click;


        public AdvancedButton() {
            InitializeComponent();
            button1.Paint += HandleButtonPaint;
        }

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

            _toolTip?.SetToolTip(button1, message);

            InvalidateAll();
        }

        private void InvalidateAll() {
            button1.Invalidate();
            Invalidate();
        }

        private void DrawBorder(Graphics graphics, Color color, int thickness) {
            ControlPaint.DrawBorder(graphics, ClientRectangle,
                color, thickness, ButtonBorderStyle.Solid,
                color, thickness, ButtonBorderStyle.Solid,
                color, thickness, ButtonBorderStyle.Solid,
                color, thickness, ButtonBorderStyle.Solid);
        }

        private async Task OnClick() {
            if (Click != null) {
                var args = new DeferralEventArgs();

                Enabled = false;

                Click(this, args);
                await args.WaitForDeferralsAsync();

                Enabled = true;
            }
        }

        private void HandleButtonPaint(object sender, PaintEventArgs e) {
            base.OnPaint(e);

            if (_applyValidityBorder)
                DrawBorder(e.Graphics, _validityBorderColor, ValidityBorderSize);
        }

        private async void button1_Click(object sender, EventArgs e) {
            await OnClick();
        }
    }
}
