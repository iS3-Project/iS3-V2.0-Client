using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace iS3.Core
{
    public class BaseView : UserControl, IBaseView
    {
        public virtual string ViewName { get; }
        public virtual string ViewID { get;  }
        public virtual bool DefaultShow { get; }

        public ViewLocation ViewPos => throw new NotImplementedException();

        public bool SetData(params object[] objs)
        {
            return true;
        }
    }
}
