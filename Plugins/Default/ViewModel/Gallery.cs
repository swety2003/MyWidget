using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using Microsoft.Win32;
using PluginSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Default.ViewModel
{
    partial class Gallery : ObservableRecipient
    {
        Default.View.Gallery view;
        public Default.Model.Gallery.Config cfg;
        public Gallery(Default.View.Gallery view)
        {
            this.view = view;

            cfg = Model.Gallery.Config.Load(view.GetPluginConfigFilePath());
        }

        public List<string> Files = new List<string>();

        [ObservableProperty]
        ImageSource img;



        private string[] exts = new string[]
        {
            ".jpg",
            ".png",
            ".bmp",
            ".jpeg",

        };
        [RelayCommand]
        private void SetTargetFolder()
        {
            var dl = new OpenFileDialog();
            var ret = dl.ShowDialog();
            if (ret == true)
            {
                var folder = Path.GetDirectoryName(dl.FileName);
                cfg.folder = folder;
                InitFolder();

            }

        }

        public void InitFolder()
        {
            Files = new List<string>();
            if (cfg.folder == null)
            {
                return;
            }
            var files = Directory.GetFiles(cfg.folder);
            foreach (var item in files)
            {
                var ext = Path.GetExtension(item).ToLower();
                if (exts.Contains(ext))
                {
                    Files.Add(item);
                }
            }

            LoadImg(Files.FirstOrDefault());
        }

        public void LoadImg(string f)
        {
            if (f == null)
            {
                return;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(f, FileMode.Open)))
            {
                try
                {
                    FileInfo fi = new FileInfo(f);
                    byte[] bytes = reader.ReadBytes((int)fi.Length);
                    reader.Close();

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(bytes);
                    bitmapImage.EndInit();
                    bitmapImage.DecodePixelWidth = 800;
                    bitmapImage.DecodePixelHeight = 800;
                    Img = bitmapImage;

                }
                catch (Exception ex)
                {
                    Growl.Error(ex.Message);
                }
            }

        }

        //public void Receive(OnExitMsg message)
        //{
        //    cfg.Save(view.GetPluginConfigFilePath());
        //}

        private int index = 0;
        internal void Next()
        {
            if (index <= Files.Count - 2)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            try
            {

                LoadImg(Files[index]);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
