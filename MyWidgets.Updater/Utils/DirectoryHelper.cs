namespace MyWidgets.Updater.Utils;

public static class DirectoryHelper
{
    public static void EnsureDirExists(string p,bool isDir = true)
    {
        string dir = p;
        if (!isDir)
        {
            dir = Path.GetDirectoryName(p);
        }
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }
    public static void DirCopy(string sourceFolderName, string destFolderName)
    {
        Console.WriteLine($"复制文件: 从{sourceFolderName}到{destFolderName}");

        if (!Directory.Exists(sourceFolderName))
        {
            Console.WriteLine("该文件不存在");
            return;
        }


        //复制文件夹
        string[] sourceFilesPath = Directory.GetDirectories(sourceFolderName);//获取指定路径中的文件夹

        if (Directory.Exists(sourceFolderName))
        {
            for (int i = 0; i < sourceFilesPath.Length; i++)
            {
                string newDir = sourceFilesPath[i].Replace(sourceFolderName, destFolderName);//将原路径替换为移动后的路径

                Directory.CreateDirectory(newDir);

                DirCopy(sourceFilesPath[i], newDir);//递归
            }
        }

        //复制文件
        string[] sourceFile = Directory.GetFiles(sourceFolderName);//获取指定路径中的文件

        for (int j = 0; j < sourceFile.Length; j++)
        {
            string destFile = sourceFile[j].Replace(sourceFolderName, destFolderName);
            File.Copy(sourceFile[j], destFile,true);
        }

        //删除原文件
        Directory.Delete(sourceFolderName, true);
    }

}