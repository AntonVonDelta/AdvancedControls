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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestAdvancedControls {
    public partial class Form1 : Form {
        TaskCompletionSource<object> source = new TaskCompletionSource<object>();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 250;

            button1.StateToolTip = toolTip1;
            comboBox1.StateToolTip = toolTip1;
            comboBox1.SetValidityState(AdvancedControls.ValidityState.Information, "Tst infoirmation here");
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

                for (int i = 0; i < 100; i++) {
                    llist.Add($"{i + 1} si {i + 2}");
                }

                await Task.Delay(2000);

                await comboBox1.SetDataSourceAsync(new BindingList<string>(llist));
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            button1.ClearValidity();
        }

        private void comboBox1_SelectedItemChanged(object sender, SelectedItemChangedEventArgs<T> e) {

        }
    }
}
