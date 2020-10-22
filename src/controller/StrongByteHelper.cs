using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FreeAdobe.src.controller
{
    public class StrongByteHelper
    {

        public static bool hasMatch(string fileName, string pattern)
        {
            return searchIndex(fileName, pattern).Count > 0;
        }
        public static List<long> searchIndex(string fileName, string targetByteStr)
        {
            int[] intPattern = convertToIntArr(targetByteStr);
            List<long> matchList = new List<long>();
            if (intPattern.Length == 0)
            {
                return matchList;
            }
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                byte[] fileByte = new byte[fileStream.Length];
                fileStream.Read(fileByte, 0, fileByte.Length);
                for (long i = 0; i < fileByte.Length; i++)
                {
                    if (fileByte[i] == intPattern[0])
                    {
                        long currPos = i;
                        int matchCount = 0;
                        foreach (int c in intPattern)
                        {
                            if (currPos<fileByte.Length&&fileByte[currPos++] == c || c == -1)
                            {
                                matchCount++;
                            }

                        }

                        if (matchCount == intPattern.Length)
                        {
                            matchList.Add(i);
                        }
                    }

                }
            }
            return matchList;
        }
        /*
         * 为了实现通配符匹配，并且在字节搜索比较时不需要强转，而且更加高效，所以不转为byte，而是转换为int
         * 通配符的格式例子： 01 00 ?? dd ?? ee
         */
        public static int[] convertToIntArr(string text) {
            try
            {
                string[] byteStr = text.Split(' ');
                int[] b = new int[byteStr.Length];
                for (int i = 0; i < b.Length; i++)
                {
                    if (byteStr[i].Equals("??"))
                    {
                        b[i] = -1;
                    }
                    else {
                        b[i] = Convert.ToByte(byteStr[i], 16);
                    }
                    
                }
                return b;
            }
            catch (Exception e)
            {
                string s = e.ToString();
                return new int[0];
            }
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
            DateTime createTime=File.GetCreationTime(fileName);
            DateTime lastWriteTime = File.GetLastWriteTime(fileName);
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                int[] newByte = convertToIntArr(newByteStr);
                foreach (long pos in replaceIndex)
                {
                    fileStream.Position = pos;
                    foreach(int i in newByte) {
                        if (i != -1) {
                            fileStream.WriteByte((byte)i);
                        }
                        else
                        {
                            fileStream.Position++;
                        }
                    }
                    
                }
                
            }
            File.SetCreationTime(fileName,createTime);
            File.SetLastWriteTime(fileName, lastWriteTime);
            return true;
        }

        public static bool replaceByte(string fileName, string targetByteStr, string newByteStr, List<long> replaceIndex)
        {
            if (!hasMatch(fileName, targetByteStr))
            {
                return false;
            }
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                int[] newByte = convertToIntArr(newByteStr);
                foreach (long pos in replaceIndex)
                {
                    fileStream.Position = pos;
                    foreach (int i in newByte)
                    {
                        if (i != -1)
                        {
                            fileStream.WriteByte((byte)i);
                        }
                        else
                        {
                            fileStream.Position++;
                        }
                    }
                }
            }
            return true;
        }

        public static bool replaceByte(string fileName, string newByteStr, List<long> replaceIndex)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                int[] newByte = convertToIntArr(newByteStr);
                foreach (long pos in replaceIndex)
                {
                    fileStream.Position = pos;
                    foreach (int i in newByte)
                    {
                        if (i != -1)
                        {
                            //WriteByte会自动向前提升一个字节
                            fileStream.WriteByte((byte)i);
                        }
                        else {
                            fileStream.Position++;
                        }
                    }
                }
            }
            return true;
        }
    }
}
