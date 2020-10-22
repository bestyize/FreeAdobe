using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAdobe.src
{
    public class PatchInfo
    {
        private AdobeProduct product;
        private string productName;
        private string version;
        private string installPath;
        private string launchPath;
        private string fileName;
        private string targetByteStr;
        private string newByteStr;
        
        

        public AdobeProduct Product { get => product; set => product = value; }

        public string ProductName { get => productName; set => productName = value; }

        public string Version { get => version; set => version = value; }
        public string InstallPath { get => installPath; set => installPath = value; }


        public string FileName { get => fileName; set => fileName = value; }
        public string TargetByteStr { get => targetByteStr; set => targetByteStr = value; }
        public string NewByteStr { get => newByteStr; set => newByteStr = value; }
        public string LaunchPath { get => launchPath; set => launchPath = value; }

        public PatchInfo()
        {
        }

        public PatchInfo(AdobeProduct product,string productName, string version, string installPath, string launchPath, string fileName, string targetByteStr, string newByteStr)
        {
            this.Product = product;
            this.productName = productName;
            this.Version = version;
            this.InstallPath = installPath;
            this.launchPath = launchPath;
            this.FileName = fileName;
            this.TargetByteStr = targetByteStr;
            this.NewByteStr = newByteStr;
        }
    }
}
