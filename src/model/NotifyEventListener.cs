using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAdobe.src.model
{
    public interface NotifyEventListener
    {
        void onOkClicked(object msg);
        void onCancelClicked(object msg);
    }
}
