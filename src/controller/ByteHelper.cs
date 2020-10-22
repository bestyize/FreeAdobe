using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAdobe.src
{
    public class ByteHelper
    {
        public static bool hasMatch(string fileName, string pattern) {
            return searchIndex(fileName,pattern).Count>0;
        }
        /*
         *转换Byte
         */
        private static byte[] convertToByte(string text) {
            try {
                string[] byteStr = text.Split(' ');
                byte[] b = new byte[byteStr.Length];
                for (int i = 0; i < b.Length; i++)
                {
                    b[i] = Convert.ToByte(byteStr[i],16);
                }
                return b;
            } catch(Exception e) {
                string s=e.ToString();
                return new byte[0];
            }
        }
        /*
         * 匹配byte
         */
        public static List<long> searchIndex(string fileName,string targetByteStr) {
            byte[] bytePattern = convertToByte(targetByteStr);
            List<long> matchList = new List<long>();
            if (bytePattern.Length == 0) {
                return matchList;
            }
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open)) {
                byte[] fileByte = new byte[fileStream.Length];
                fileStream.Read(fileByte,0,fileByte.Length);
                for (long i = 0; i < fileByte.Length; i++) {
                    if (fileByte[i] == bytePattern[0]) {
                        long currPos = i;
                        for (int j = 0; j < bytePattern.Length; j++) {
                            if (fileByte[currPos++] != bytePattern[j]) {
                                break;
                            }
                        }
                        if (currPos - i == bytePattern.Length) {
                            matchList.Add(i);
                        }
                    }
                    
                }
            }
            return matchList;
        }

        /*
         *替换字节块，全部替换
         * 
         * pattern：目标字节块
         *
         */
        public static bool replaceByte(string fileName, string targetByteStr, string newByteStr)
        {
            if (!hasMatch(fileName, targetByteStr))
            {
                return false;
            }
            List<long> replaceIndex = searchIndex(fileName, targetByteStr);
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                byte[] newByte = convertToByte(newByteStr);
                foreach (long pos in replaceIndex)
                {
                    fileStream.Position = pos;
                    fileStream.Write(newByte, 0, newByte.Length);
                }
            }
            return true;
        }
        /*
         *替换字节块,指定位置
         */
        public static bool replaceByte(string fileName, string targetByteStr, string newByteStr,List<long> replaceIndex) {
            if (!hasMatch(fileName, targetByteStr)) {
                return false;
            }
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open)) {
                byte[] newByte = convertToByte(newByteStr);
                foreach (long pos in replaceIndex) {
                    fileStream.Position = pos;
                    fileStream.Write(newByte,0,newByte.Length);
                }
            }
            return true;
        }

        public static bool replaceByte(string fileName, string newByteStr, List<long> replaceIndex) {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                byte[] newByte = convertToByte(newByteStr);
                foreach (long pos in replaceIndex)
                {
                    fileStream.Position = pos;
                    fileStream.Write(newByte, 0, newByte.Length);
                }
            }
            return true;
        }
    }
}
