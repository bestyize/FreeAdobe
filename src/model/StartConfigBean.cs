using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAdobe.src.model
{
    class StartConfigBean
    {
        string version;
        string download;
        string title;
        string notice;
        string verify;
        string enable;

        public StartConfigBean(string version, string download, string title, string notice, string verify, string enable)
        {
            this.version = version;
            this.download = download;
            this.title = title;
            this.notice = notice;
            this.verify = verify;
            this.enable = enable;
        }

        public string Version { get => version; set => version = value; }
        public string Download { get => download; set => download = value; }
        public string Title { get => title; set => title = value; }
        public string Notice { get => notice; set => notice = value; }
        public string Verify { get => verify; set => verify = value; }
        public string Enable { get => enable; set => enable = value; }
    }
}
