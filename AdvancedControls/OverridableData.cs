using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedControls {
    internal class OverridableData<T> {
        private bool _overloaded;
        private T _overloadData;

        public T Initial { get; set; }


        public OverridableData(T initial) {
            Initial = initial;
        }

        public OverridableData() {
        }

        public void RemoveOverload() {
            _overloaded = false;
        }

        public void SetOverload(T newData) {
            _overloaded = true;
            _overloadData = newData;
        }

        public static implicit operator T(OverridableData<T> d) {
            if (d._overloaded) return d._overloadData;
            return d.Initial;
        }
    }
}
