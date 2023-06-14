using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvancedControls.AdvancedCombobox {
    public partial class AdvancedCombobox<T> : UserControl where T : class {
        private Tuple<int, T> _selectedItemPair = null;
        private Tuple<int, T> _previousSelectedItemPair = null;
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
                base.Enabled = _enabled;
            }
        }

        /// <summary>
        /// Determines whether the control only fires on leave event.
        /// The selected item is however always synchronized with the view
        /// so even though the event was not raised the properties will return the current visible value.
        /// </summary>
        public bool FireOnLeaveOnly { get; set; }
        #endregion

        #region Events
        public event EventHandler<SelectedItemChangedEventArgs<T>> SelectedItemChanged;
        #endregion



        public AdvancedCombobox() {
            InitializeComponent();
        }

        public void ClearValidity() {
            SetValidityState(ValidityState.None, null);
        }
        public void SetValidityState(ValidityState state, string message) {
            comboBox1.SetValidityState(state, message);
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
                var args = new SelectedItemChangedEventArgs<T>(_selectedItemPair?.Item2, newSelectedItem);

                // Assign the new selected item and raise the event
                _selectedItemPair = newSelectedItemPair;

                await OnSelectedItemChangedAsync(args);
            }
        }
        public async Task SetSelectedItem(T item) {
            T newSelectedItem;
            int newSelectedIndex;
            Tuple<int, T> newSelectedItemPair;

            comboBox1.SelectedItem = item;

            newSelectedItem = (T)comboBox1.SelectedItem;
            newSelectedIndex = comboBox1.SelectedIndex;
            newSelectedItemPair = Tuple.Create(newSelectedIndex, newSelectedItem);

            if (!Compare(newSelectedItem, _selectedItemPair?.Item2)) {
                var args = new SelectedItemChangedEventArgs<T>(newSelectedItem, _selectedItemPair?.Item2);

                // Assign the new selected item and raise the event
                _selectedItemPair = newSelectedItemPair;

                await OnSelectedItemChangedAsync(args);
            }
        }

        /// <summary>
        /// Compile-friendly compare procedure.
        /// This wrapper is needed to avoid possible bugs when the generic variable is compared to another type
        /// which with other methods is automatically upcasted to object (.Equals, ==)
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



        private async Task OnSelectedItemChangedAsync(SelectedItemChangedEventArgs<T> args) {
            if (SelectedItemChanged != null) {
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

        private async void comboBox1_SelectionChangeCommitted(object sender, EventArgs e) {
            var newSelectedItem = (T)comboBox1.SelectedItem;
            var newSelectedIndex = comboBox1.SelectedIndex;
            var newSelectedItemPair = Tuple.Create(newSelectedIndex, newSelectedItem);
            var args = new SelectedItemChangedEventArgs<T>(newSelectedItem, _selectedItemPair?.Item2);

            if (FireOnLeaveOnly) _previousSelectedItemPair = _selectedItemPair;

            // Assign the new selected item and raise the event
            _selectedItemPair = newSelectedItemPair;

            if (!FireOnLeaveOnly) {
                await OnSelectedItemChangedAsync(args);
            }
        }

        private async void comboBox1_Leave(object sender, EventArgs e) {
            var tempPreviousItem = _previousSelectedItemPair;

            if (tempPreviousItem == null) return;

            // Reset previous value so that this case is identified next time
            _previousSelectedItemPair = null;

            var args = new SelectedItemChangedEventArgs<T>(_selectedItemPair?.Item2, tempPreviousItem.Item2);
            await OnSelectedItemChangedAsync(args);
        }
    }
}
