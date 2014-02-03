using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusRacingCar
{
    public class DomePlugin : PluginBase<IProjection>
    {
        public DomePlugin()
        {
            try
            {
                Name = "Dome";
                var projection = new DomeProjection();
                Content = projection;
//                Panel = new DomePanel(projection);
//                InjectConfig(PluginConfig.FromSettings(ConfigHelper.LoadConfig().AppSettings.Settings));
            }
            catch (Exception exc)
            {
                Logger.Instance.Error(string.Format("Error while loading '{0}'", GetType().FullName), exc);
            }
        }
    }
}
