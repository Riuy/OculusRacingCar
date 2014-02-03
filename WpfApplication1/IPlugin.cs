using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OculusRacingCar
{
    public interface IPlugin<T> : ILoadable, INotifyPropertyChanged
    {
        string Name { get; set; }
        T Content { get; set; }
        FrameworkElement Panel { get; set; }
        PluginConfig ExtractConfig();
        void InjectConfig(PluginConfig plugin);
    }
}
