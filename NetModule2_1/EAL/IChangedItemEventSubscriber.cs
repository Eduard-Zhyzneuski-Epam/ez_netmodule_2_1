using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_1.EAL
{
    public interface IChangedItemEventSubscriber : IDisposable
    {
        void Subscribe();
    }
}
