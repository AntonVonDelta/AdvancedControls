using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedControls {
    public class DeferralEventArgs {
        private readonly DeferralManager _manager = new DeferralManager();

        public IDisposable GetDeferral() {
            return _manager.DeferralSource.GetDeferral();
        }

        internal Task WaitForDeferralsAsync() {
            return _manager.WaitForDeferralsAsync();
        }
    }
}
