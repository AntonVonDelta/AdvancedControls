using AdvancedControls.AdvancedCombobox.CustomEventArgs;
using System;
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

        private async void button1_Click(object sender, EventArgs e) {
            Test();

            await source.Task;

            Debug.WriteLine("after");
        }

        async void Test() {
            await Task.Delay(2000);
            source.SetResult(true);
            Debug.WriteLine("test");
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private Task comboBox1_SelectedItemChanged(object sender, SelectedItemChangedEventArgs<string> e) {
            return Task.CompletedTask;
        }
    }
}
