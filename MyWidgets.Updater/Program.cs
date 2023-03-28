// See https://aka.ms/new-console-template for more information

using MyWidgets.Updater.Utils;
using Spectre.Console;
using System.Diagnostics;

if (args.Length!=2)
{
#if !DEBUG
    Environment.Exit(-1);
#endif
}

try
{
    await DoUpdate();
}
catch (Exception ex)
{
    AnsiConsole.WriteLine("升级失败！");

    AnsiConsole.WriteException(ex);

}


async Task DoUpdate()
{

#if !DEBUG
    var downloadUrl = args[0];
    var programPid = args[1];

    Process processes = Process.GetProcessById(int.Parse(programPid));

    var modulePath = processes.MainModule.FileName;

    var installPath = Path.GetDirectoryName(processes.MainModule.FileName);

    if (processes != null)
    {
        processes.Kill();
    }


    var zip_f = await new FileDownloader().StartAsync(downloadUrl);

#else

var modulePath = "";
var zip_f = @"D:\Source\Repos\MyWidget\build\Debug\net6.0-windows\618292312.zip";
var installPath = @"D:\Source\Repos\MyWidget\build\Debug\net6.0-windows\";

#endif

    var selfDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
    var unzipDestPath = Path.Combine(selfDir, "net6.0-windows");


    await new ZipHelper().UnZipAsync(zip_f, selfDir);

    File.Delete(zip_f);


    DirectoryHelper.DirCopy(unzipDestPath, installPath);

    AnsiConsole.MarkupLine("程序执行结束。");

    Process.Start(modulePath);
}




