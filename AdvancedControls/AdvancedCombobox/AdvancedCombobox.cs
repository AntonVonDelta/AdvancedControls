using AdvancedControls.AdvancedCombobox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls.AdvancedCombobox {
    public partial class AdvancedCombobox<T> : UserControl where T : class {
        private Tuple<int, T> _selectedItemPair = null;

        public event EventHandler<SelectedItemChangedEventArgs<T>> SelectedItemChanged;


        public AdvancedCombobox() {
            InitializeComponent();
        }

        private async Task OnSelectedItemChangedAsync(Tuple<int, T> newSelectedItem) {
            if (SelectedItemChanged != null) {
                var args = new SelectedItemChangedEventArgs<T>(_selectedItemPair?.Item2, newSelectedItem.Item2);

                Enabled = false;
                SelectedItemChanged(this, args);
                await args.WaitForDeferralsAsync();
                Enabled = true;
            }
        }

        public async Task SetDataSourceAsync(BindingList<T> data) {
            T newSelectedItem;
            int newSelectedIndex;
            Tuple<int, T> newSelectedItemPair;

            comboBox1.DataSource = data;
            newSelectedItem = (T)comboBox1.SelectedItem;
            newSelectedIndex = comboBox1.SelectedIndex;
            newSelectedItemPair = Tuple.Create(newSelectedIndex, newSelectedItem);

            if (!Compare(newSelectedItem, _selectedItemPair?.Item2)) {
                // Assign the new selected item and raise the event
                _selectedItemPair = newSelectedItemPair;
                await OnSelectedItemChangedAsync(newSelectedItemPair);
            }
        }

        /// <summary>
        /// Compile-friendly compare procedure.
        /// This wrapper is needed to avoid possible bugs when the generic variable is compared to another type
        /// which with other methods is automatically upcasted to object (.Eqauls, ==)
        /// </summary>
        private bool Compare(T val1, T val2) {
            return val1 == val2;
        }

        /// <summary>
        /// Prevents the control from being resized vertically
        /// </summary>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified) {
            height = comboBox1.Height;
            base.SetBoundsCore(x, y, width, height, specified);
        }
    }
}
