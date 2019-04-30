using iS3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3_Controls
{
    public class DemoControl:BaseView
    {
        public override string ViewID
        {
            get { return "DemoControl"; }
        }
        public override string ViewName
        {
            get { return "测试外部窗口"; }
        }

    }
}
