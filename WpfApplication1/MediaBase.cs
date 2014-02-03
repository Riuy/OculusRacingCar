using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OculusRacingCar
{
    public abstract class MediaBase : ViewModelBase, IMedia
    {
        public abstract FrameworkElement Media { get; }

        public abstract void Load();
        public abstract void Unload();
    }
}