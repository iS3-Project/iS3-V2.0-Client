using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iS3.Monitoring;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            //StreamReader sr = new StreamReader("D:/1.txt", Encoding.Default);
            //String line = sr.ReadToEnd();
            //JObject jo = JObject.Parse(line);
            //JArray ja = JArray.Parse(jo["monComponentList"].ToString());
            //foreach (JToken jt in ja)
            //{
            //    string name = jt["componentName"].ToString();
            //}
            //MonPoint mp = JsonConvert.DeserializeObject<MonPoint>(line);
            //Console.ReadLine();



            MonPoint mp = new MonPoint();
            mp.monComponentList = new List<MonComponent>()
            {
                new MonComponent(){componentName="1"},
                new MonComponent(){componentName="2"}
            };
            string st = JsonConvert.SerializeObject(mp);
            MonPoint mp1 = JsonConvert.DeserializeObject<MonPoint>(st);
            Console.ReadLine();

        }
    }
}
