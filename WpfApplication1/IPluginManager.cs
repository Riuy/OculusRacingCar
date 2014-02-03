using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusRacingCar
{
    public interface IPluginManager : IDisposable
    {
        IEnumerable<IPlugin<IMedia>> Medias { get; }
        IEnumerable<IPlugin<IProjection>> Projections { get; }
        IEnumerable<IPlugin<DistortionBase>> Distortions { get; }
    }
}
