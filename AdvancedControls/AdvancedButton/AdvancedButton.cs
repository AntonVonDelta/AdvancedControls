using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls.AdvancedButton {
    public partial class AdvancedButton : UserControl {
        private ValidityState _validityState = ValidityState.None;
        private OverridableData<BorderStyle> _borderStyle = new OverridableData<BorderStyle>();
        private OverridableData<Color> _borderColor = new OverridableData<Color>();

        public new BorderStyle BorderStyle {
            get => _borderStyle;
            set {
                _borderStyle.Initial = value;
                Invalidate();
            }
        }

        public Color BorderColor {
            get => _borderColor;
            set {
                _borderColor.Initial = value;
                Invalidate();
            }
        }

        public ToolTip StateToolTip { get; set; }
        public ValidityState ValidityState => _validityState;

        public new event EventHandler<DeferralEventArgs> Click;


        public AdvancedButton() {
            InitializeComponent();
        }

        public void SetValidityState(ValidityState state, string message) {
            _validityState = state;

            if (StateToolTip != null) {
                StateToolTip.SetToolTip(this, message);
            }

            switch (state) {
                case ValidityState.None:
                    _borderColor.RemoveOverload();
                    _borderStyle.RemoveOverload();
                    break;

                case ValidityState.Error:
                    _borderColor.SetOverload(Color.DarkRed);
                    _borderStyle.SetOverload(BorderStyle.FixedSingle);
                    break;

                case ValidityState.Information:
                    _borderColor.SetOverload(Color.Blue);
                    _borderStyle.SetOverload(BorderStyle.FixedSingle);
                    break;
            }
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

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if (_borderStyle == BorderStyle.FixedSingle)
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _borderColor, ButtonBorderStyle.Solid);
        }

        private async void button1_Click(object sender, EventArgs e) {
            await OnClick();
        }
    }
}
