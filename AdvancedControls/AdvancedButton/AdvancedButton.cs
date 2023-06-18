using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls.AdvancedButton {
    [DefaultEvent(nameof(AdvancedButton.Click))]
    public partial class AdvancedButton : UserControl {
        private OverridableData<bool> _enabled = new OverridableData<bool>(true);

        #region Properties
        public ValidityState ValidityState => button1.ValidityState;

        public int ValidityBorderSize {
            get => button1.ValidityBorderSize;
            set => button1.ValidityBorderSize = value;
        }
        public ToolTip StateToolTip {
            get => button1.StateToolTip;
            set => button1.StateToolTip = value;
        }

        /// <summary>
        /// Enables or disables the control.
        /// This value does not supersede the internal mechanism for reentrancy-avoidance
        /// (aka disabling the button when an async operation is in execution).
        /// Issues may appear though when this control is disabled through its base Enabled property
        /// </summary>
        public new bool Enabled {
            get => _enabled;
            set {
                _enabled.Initial = value;
                UpdateEnabled();
            }
        }
        #endregion

        #region Events
        public new event EventHandler<DeferralEventArgs> Click;
        #endregion


        public AdvancedButton() {
            InitializeComponent();
        }

        public void ClearValidity() {
            SetValidityState(ValidityState.None, null);
        }
        public void SetValidityState(ValidityState state, string message) {
            button1.SetValidityState(state, message);
        }
        private void UpdateEnabled() {
            button1.Enabled = _enabled;
        }



        private async Task OnClick() {
            if (Click != null) {
                var args = new DeferralEventArgs();

                _enabled.SetOverload(false);
                UpdateEnabled();

                Click(this, args);
                await args.WaitForDeferralsAsync();

                _enabled.RemoveOverload();
                UpdateEnabled();
            }
        }

        private async void button1_Click(object sender, EventArgs e) {
            await OnClick();
        }
    }
}
