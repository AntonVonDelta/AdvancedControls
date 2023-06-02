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
        private OverridableData<int> _borderSize = new OverridableData<int>();
        private OverridableData<Color> _borderColor = new OverridableData<Color>();
        private OverridableData<FlatStyle> _borderFlatStyle = new OverridableData<FlatStyle>(FlatStyle.Standard);

        /// <summary>
        /// Hide BorderStyle because the usercontrol will have the button's border
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new BorderStyle BorderStyle { get; set; }

        public int BorderSize {
            get => _borderSize;
            set {
                _borderSize.Initial = value;
                button1.FlatAppearance.BorderSize = _borderSize;
                UpdateBorders();
            }
        }

        public Color BorderColor {
            get => _borderColor;
            set {
                _borderColor.Initial = value;
                UpdateBorders();
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
                    _borderSize.RemoveOverload();
                    _borderFlatStyle.RemoveOverload();
                    break;

                case ValidityState.Error:
                    _borderColor.SetOverload(Color.DarkRed);
                    _borderSize.SetOverload(2);
                    _borderFlatStyle.SetOverload(FlatStyle.Flat);
                    break;

                case ValidityState.Information:
                    _borderColor.SetOverload(Color.Blue);
                    _borderSize.SetOverload(2);
                    _borderFlatStyle.SetOverload(FlatStyle.Flat);
                    break;
            }

            UpdateBorders();
        }

        private void UpdateBorders() {
            button1.FlatAppearance.BorderSize = _borderSize;
            button1.FlatAppearance.BorderColor = _borderColor;
            button1.FlatStyle = _borderFlatStyle;
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
