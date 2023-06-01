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
        public new event EventHandler<DeferralEventArgs> Click;


        public AdvancedButton() {
            InitializeComponent();
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
