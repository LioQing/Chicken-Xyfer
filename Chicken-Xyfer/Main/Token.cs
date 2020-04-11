using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicken_Xyfer.Main
{
    class Token
    {
        public static string GetGrandParentDir()
        {
            string CurrentDir = Directory.GetCurrentDirectory();
            DirectoryInfo DirInfo = new DirectoryInfo(CurrentDir);
            return DirInfo.Parent.Parent.FullName;
        }

        public static string GetTokenFromFile()
        {
            string path = GetGrandParentDir() + @"\token.gitignore";
            return File.ReadAllText(path);
        }
    }
}
