using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusRacingCar
{
    public class PlanePlugin : PluginBase<IProjection>
    {
        public PlanePlugin()
        {
            try
            {
                Name = "Plane";
                var projection = new PlaneProjection();
                Content = projection;
//                Panel = new PlanePanel(projection);
//                InjectConfig(PluginConfig.FromSettings(ConfigHelper.LoadConfig().AppSettings.Settings));
            }
            catch (Exception exc)
            {
                Logger.Instance.Error(string.Format("Error while loading '{0}'", GetType().FullName), exc);
            }
        }
    }
}
