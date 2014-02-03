using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusRacingCar
{
    public interface IApplicationState : INotifyPropertyChanged
    {
        IPlugin<IMedia> MediaPlugin { get; set; }
        IPlugin<IProjection> ProjectionPlugin { get; set; }
        IPlugin<DistortionBase> DistortionPlugin { get; set; }
        ProjectionMode ModeProj { get; set; }
        StereoMode StereoInput { get; set; }
        LayoutMode StereoOutput { get; set; }
        void Unload();
    }
}
