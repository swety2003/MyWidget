using Downloader;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWidgets.Updater.Utils
{
    public class FileDownloader
    {
        private IDownload download;

        private string targetName = "";

        private string realPath, tmpPath;


        private void Download_DownloadFileCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            File.Move(tmpPath, realPath);
        }

        private void Download_DownloadProgressChanged(object? sender, Downloader.DownloadProgressChangedEventArgs e)
        {

            _progressTask.IsIndeterminate = false;
            _progressTask.Increment(e.ProgressPercentage - _progressTask.Percentage);
            _progressTask.Description = $"[yellow][[{this.targetName}]][/] T:{FileSizeHelper.AutoUnit(e.TotalBytesToReceive)} S:{FileSizeHelper.AutoUnit(e.BytesPerSecondSpeed)}/s";
        }


        private ProgressTask _progressTask;

        public async Task<string> StartAsync(string url, string targetDic = ".", string name = "")
        {

            var oldName = Path.GetFileName(url);

            this.targetName = string.IsNullOrEmpty(name) ? oldName : name;

            realPath = Path.Combine(targetDic, this.targetName);

            if (File.Exists(realPath))
            {
                Console.WriteLine($"跳过下载: {targetName}");

                return realPath;
            }

            tmpPath = realPath + ".download";

            if (!Directory.Exists(targetDic))
            {
                Directory.CreateDirectory(targetDic);
            }
            download = DownloadBuilder.New()
                .WithUrl(url)
                .WithFileLocation(tmpPath)
                .WithConfiguration(new DownloadConfiguration())
                .Build();

            download.DownloadProgressChanged += Download_DownloadProgressChanged;
            download.DownloadFileCompleted += Download_DownloadFileCompleted;



            await AnsiConsole.Progress()
                .StartAsync(async ctx =>
                {
                    // Define tasks
                    _progressTask = ctx.AddTask("[blue]Preparing...[/]");
                    _progressTask.IsIndeterminate = true;
                    try
                    {
                        await download.StartAsync();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    while (download.Status != DownloadStatus.Completed)
                    {

                    }

                });

            return realPath;
        }
    }
}
