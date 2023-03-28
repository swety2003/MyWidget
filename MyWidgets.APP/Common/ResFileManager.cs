using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWidgets.APP.Common
{
    internal static class ResFileManager
    {
        /// <summary>
        /// 释放内嵌资源至指定位置
        /// </summary>
        /// <param name="resource">嵌入的资源，此参数写作：命名空间.文件夹名.文件名.扩展名</param>
        /// <param name="path">释放到位置</param>
        public static void ExtractFile(string file, string path)
        {
            var resource = $"MyWidgets.APP.{file}";
            Assembly assembly = Assembly.GetExecutingAssembly();
            BufferedStream input = new BufferedStream(assembly.GetManifestResourceStream(resource));
            FileStream output = new FileStream(path, FileMode.Create);
            byte[] data = new byte[1024];
            int lengthEachRead;
            while ((lengthEachRead = input.Read(data, 0, data.Length)) > 0)
            {
                output.Write(data, 0, lengthEachRead);
            }
            output.Flush();
            output.Close();
        }
    }
}
