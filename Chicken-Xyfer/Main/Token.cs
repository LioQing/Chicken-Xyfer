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
        public static string GetTokenFromFile()
        {
            string CurrentDir = Directory.GetCurrentDirectory();
            DirectoryInfo DirInfo = new DirectoryInfo(CurrentDir);
            string path = DirInfo.Parent.Parent.FullName + @"\token.gitignore";
            return File.ReadAllText(path);
        }
    }
}
