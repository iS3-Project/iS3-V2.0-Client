using iS3.Core.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.Core.Repository
{
    public class RepositoryForProject
    {
        //public async Task<ObservableCollection<ProjectLocation>> RetrieveProjectList()
        //{
        //    //网络请求 
        //    string result = await Task.Run(() =>
        //       WebApiCaller.HttpGet(ServiceConfig.BaseURL +string.Format(ServiceConfig.ProjectListFormat)));
        //    //
        //    JObject obj = JObject.Parse(result);
        //    string data = obj["data"].ToString();
        //    //
        //    ObservableCollection<ProjectLocation> _obj = JsonConvert.DeserializeObject<ObservableCollection<ProjectLocation>>(data);
        //    return _obj;
        //}
        public async Task<List<ProjectLocation>> RetrieveProjectList()
        {
            //网络请求 
            string result =await Globals.oAuthClient.GetByAuth(ServiceConfig.BaseURL +ServiceConfig.ProjectListFormat);
            //
            List<ProjectLocation> _obj = JsonConvert.DeserializeObject<List<ProjectLocation>>(result);
            return _obj;
        }
    }
}
