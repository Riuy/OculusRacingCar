using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OculusRacingCar
{
    public interface IMedia : ILoadable
    {
        FrameworkElement Media { get; }


    }
}
