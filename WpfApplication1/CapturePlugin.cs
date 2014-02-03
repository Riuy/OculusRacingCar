using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusRacingCar
{
    public class CapturePlugin : PluginBase<IMedia>
    {
        public CapturePlugin()
        {
            Name = "Capture";
            var media = new MediaCapture();
            Content = media;
//            Panel = new GdiPanel(media);
        }
    }
}
