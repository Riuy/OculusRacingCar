using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusRacingCar
{
    public class BarrelPlugin : PluginBase<DistortionBase>
    {
        public BarrelPlugin()
        {
            try
            {
                Name = "Barrel";
                var effect = new BarrelEffect();
                Content = effect;
/*                Panel = new BarrelPanel(effect);
                InjectConfig(PluginConfig.FromSettings(ConfigHelper.LoadConfig().AppSettings.Settings));*/
            }
            catch (Exception exc)
            {
                Logger.Instance.Error(string.Format("Error while loading '{0}'", GetType().FullName), exc);
            }
        }
    }
}
