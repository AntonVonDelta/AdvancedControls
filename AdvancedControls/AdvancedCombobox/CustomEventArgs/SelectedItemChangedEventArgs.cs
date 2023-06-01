using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedControls.AdvancedCombobox.CustomEventArgs {
    public class SelectedItemChangedEventArgs<T> : EventArgs {
        public T NewValue { get; set; }

        public T OldValue { get; set; }

        public SelectedItemChangedEventArgs(T newValue, T oldValue) {
            NewValue = newValue;
            OldValue = oldValue;
        }
    }
}
