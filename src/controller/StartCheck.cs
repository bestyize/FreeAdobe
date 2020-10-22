using FreeAdobe.src.util;
using FreeAdobe.src.view;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FreeAdobe.src.model
{
    class StartCheck
    {
        StartConfigBean bean;
        public static StartConfigBean doCheck() {
            StartConfigBean startConfigBean = null;
            try {
                string response = HttpHelper.downloadWebSiteUseGet("https://bestyize.github.io/adobe/freeadobe/config/config.json");
                JObject jo = (JObject)JsonConvert.DeserializeObject(response);
                string version = jo["version"].ToString();
                string download = jo["download"].ToString();
                string title = jo["title"].ToString();
                string notice= jo["notice"].ToString();
                string verify = jo["verify"].ToString();
                string enable = jo["enable"].ToString();
                startConfigBean = new StartConfigBean(version, download, title,notice,verify,enable);
            } catch(Exception e) { }
            return startConfigBean;
            
        }



        

        

    }
}
