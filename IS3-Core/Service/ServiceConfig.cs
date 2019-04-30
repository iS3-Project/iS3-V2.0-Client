using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.Core.Service
{
    public static class ServiceConfig
    {
        public static bool NeedCache = true;
        //http://139.196.73.142:8011/api/geology/borehole?project=SHML12
        public static string IP = "";
        public static string PortNum = "";

        public static string BaseURL { get { return string.Format("http://{0}:{1}/", IP, PortNum); } }
        //获取Token
        public static string TokenURL { get { return BaseURL + "/token"; } }
        //获取工程列表
        public static string ProjectListFormat = "api/project/ProjectLocation";

        //获取对象列表格式
        public static string DGObjectListFormat = "api/{0}/{1}?project={2}&filter={3}";

        //获取单个对象格式
        public static string DGObjectByIDFormat = "api/{0}/{1}?project={2}&id={3}";

        //文件服务格式
        public static string FileGetFormat = "api/file/{0}?file={1}";
    }
}
