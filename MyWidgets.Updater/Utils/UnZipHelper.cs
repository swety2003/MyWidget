using Downloader;
using Ionic.Zip;
using Spectre.Console;

namespace MyWidgets.Updater.Utils;

public class ZipHelper
{

    private ProgressTask _progressTask;

    private string Source;

    public async Task UnZipAsync(string source,string target=".")
    {
        Source= source;



        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                // Define tasks
                _progressTask = ctx.AddTask("[blue]正在准备...[/]");
                _progressTask.IsIndeterminate = true;
                try
                {

                    using ZipFile zip = ZipFile.Read(source);
                    zip.ExtractProgress += ZipOnExtractProgress;
                    zip.ExtractProgress += ZipOnExtractProgress;
                    zip.ExtractAll(target, ExtractExistingFileAction.OverwriteSilently);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                _progressTask.Value = _progressTask.MaxValue;

    });
    }

    private void ZipOnExtractProgress(object? sender, ExtractProgressEventArgs e)
    {
        if (e.TotalBytesToTransfer>0)
        {

            _progressTask.IsIndeterminate = false;

            //_progressTask.Increment(per - _progressTask.Percentage);

            _progressTask.MaxValue = e.TotalBytesToTransfer;
            _progressTask.Value = e.BytesTransferred;

            _progressTask.Description = 
                $"[yellow][[{Path.GetFileName(this.Source)}]][/] T:{FileSizeHelper.AutoUnit(e.TotalBytesToTransfer)} L:{FileSizeHelper.AutoUnit(e.TotalBytesToTransfer-e.BytesTransferred)}";

        }
        else
        {
            _progressTask.Description = $"解压文件： [[{Path.GetFileName(this.Source)}]]";
        }
    }
}