using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedControls.AdvancedCombobox {
    public class SelectedItemChangedEventArgs<T> : DeferralEventArgs {
        public T NewValue { get; set; }

        public T OldValue { get; set; }

        public SelectedItemChangedEventArgs(T newValue, T oldValue) {
            NewValue = newValue;
            OldValue = oldValue;
        }
    }
}
