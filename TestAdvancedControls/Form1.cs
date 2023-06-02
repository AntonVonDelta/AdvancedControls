using AdvancedControls.AdvancedCombobox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAdvancedControls {
    public partial class Form1 : Form {
        TaskCompletionSource<object> source = new TaskCompletionSource<object>();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            button1.StateToolTip = toolTip1;
        }

        private async void comboBox1_SelectedItemChanged(object sender, SelectedItemChangedEventArgs<string> e) {
            using (e.GetDeferral()) {
                await Task.Delay(5000);
            }
        }

        private async void button1_Click(object sender, AdvancedControls.DeferralEventArgs e) {
            using (e.GetDeferral()) {
                var llist = new List<string>();

                button1.SetValidityState(AdvancedControls.ValidityState.Error, "Error sir!");

                return;

                for (int i = 0; i < 100; i++) {
                    llist.Add($"{i + 1} si {i + 2}");
                    await Task.Yield();
                }
                await Task.Delay(2000);
                await comboBox1.SetDataSourceAsync(new BindingList<string>(llist));
            }
        }
    }
}
