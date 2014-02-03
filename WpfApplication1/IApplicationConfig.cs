using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OculusRacingCar
{
    public interface IApplicationConfig : INotifyPropertyChanged
    {
        string DefaultMediaFile { get; set; }
        int CameraFieldOfView { get; set; }
        int ViewportsHorizontalOffset { get; set; }
        int ViewportsVerticalOffset { get; set; }
        string SamplesFolder { get; set; }
        double NeckHeight { get; set; }
        bool ReadSideCarPresets { get; set; }

        string DefaultMedia { get; set; }
        string DefaultEffect { get; set; }
        string DefaultDistortion { get; set; }
        string DefaultProjection { get; set; }
        string DefaultTracker { get; set; }
        string DefaultStabilizer { get; set; }

    }
}
