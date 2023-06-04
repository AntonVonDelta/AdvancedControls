﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls.AdvancedCombobox {
    public partial class AdvancedCombobox<T> : UserControl where T : class {
        private Tuple<int, T> _selectedItemPair = null;
        private OverridableData<bool> _enabled = new OverridableData<bool>(true);
        private int _selectedItemChangedStack;

        #region Properties
        public ValidityState ValidityState => comboBox1.ValidityState;
        public int ValidityBorderSize {
            get => comboBox1.ValidityBorderSize;
            set => comboBox1.ValidityBorderSize = value;
        }
        public ToolTip StateToolTip {
            get => comboBox1.StateToolTip;
            set => comboBox1.StateToolTip = value;
        }

        public new bool Enabled {
            get => _enabled;
            set {
                _enabled.Initial = value;
                base.Enabled = _enabled;
            }
        }
        #endregion

        public event EventHandler<SelectedItemChangedEventArgs<T>> SelectedItemChanged;


        public AdvancedCombobox() {
            InitializeComponent();
        }

        public void ClearValidity() {
            SetValidityState(ValidityState.None, null);
        }
        public void SetValidityState(ValidityState state, string message) {
            comboBox1.SetValidityState(state, message);
            Invalidate();
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



        private async Task OnSelectedItemChangedAsync(Tuple<int, T> newSelectedItem) {
            if (SelectedItemChanged != null) {
                var args = new SelectedItemChangedEventArgs<T>(_selectedItemPair?.Item2, newSelectedItem.Item2);

                _selectedItemChangedStack++;
                if (_selectedItemChangedStack == 1) {
                    _enabled.SetOverload(false);
                    base.Enabled = _enabled;
                }

                SelectedItemChanged(this, args);
                await args.WaitForDeferralsAsync();

                _selectedItemChangedStack--;
                if (_selectedItemChangedStack == 0) {
                    _enabled.RemoveOverload();
                    base.Enabled = _enabled;
                }
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            var newSelectedItem = (T)comboBox1.SelectedItem;
            var newSelectedIndex = comboBox1.SelectedIndex;
            var newSelectedItemPair = Tuple.Create(newSelectedIndex, newSelectedItem);

            await OnSelectedItemChangedAsync(newSelectedItemPair);
        }
    }
}
