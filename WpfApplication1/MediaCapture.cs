using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Drawing.Imaging;
using Image = System.Windows.Controls.Image;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Threading;

namespace OculusRacingCar
{
    class MediaCapture : MediaBase
    {
        private readonly Image _media;
        public override FrameworkElement Media
        {
            get
            {
                return _media;
            }
        }

        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideoSource;

        private void InitDirectShow()
        {
            FinalVideoSource = new VideoCaptureDevice(VideoCaptureDevices[0].MonikerString);
            FinalVideoSource.NewFrame += new NewFrameEventHandler(FinalVideoSource_NewFrame);
            FinalVideoSource.Start();
        }

        private void CloseDirectShow()
        {
            if (FinalVideoSource.IsRunning)
            {
                FinalVideoSource.Stop();
            }
        }

        void FinalVideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            System.Drawing.Image imgforms = (Bitmap)eventArgs.Frame.Clone();

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();
            imgforms.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);

            bi.StreamSource = ms;
            bi.EndInit();

            bi.Freeze();
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                this._media.Source = bi;
                OnPropertyChanged("Media");
             }));
        }

        public MediaCapture()
        {
            _media = new Image();
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            this.InitDirectShow();
        }

        public override void Load()
        {
        }

        public override void Unload()
        {
            this.CloseDirectShow();
        }
    }
}
