using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls.AdvancedButton {
    public partial class AdvancedButton : UserControl {
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
            Invalidate();
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

        private async void button1_Click(object sender, EventArgs e) {
            await OnClick();
        }
    }
}
