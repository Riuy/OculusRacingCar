using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;

namespace OculusRacingCar
{
    unsafe public class OculusRiftTracker : TrackerBase, ITracker
    {
        [System.Runtime.InteropServices.DllImportAttribute(@"RiftWrapper.dll")]
        static extern int OVR_Init();

        [DllImport(@"RiftWrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int OVR_Peek(float* w, float* x, float* y, float* z);


        public OculusRiftTracker()
        {
        }

        public override void Load()
        {
            try
            {
                if (!IsEnabled)
                {
                    IsEnabled = true;
                    var result = OVR_Init();
                    ThrowErrorOnResult(result, "Error while initializing the Oculus Rift");
                }
            }
            catch (Exception exc)
            {
                Logger.Instance.Error(exc.Message, exc);
                IsEnabled = false;
            }
        }

        public override void Unload()
        {
        }

        public void Update()
        {
            try
            {
                float w, x, y, z;
                var result = OVR_Peek(&w, &x, &y, &z);
                ThrowErrorOnResult(result, "Error while getting data from the Razer Hydra");
                RawRotation = new Quaternion(x, -y, z, -w);

                UpdatePositionAndRotation();
            }
            catch(Exception exc)
            {
                Logger.Instance.Error(exc.Message, exc);
            }
        }

        private static void ThrowErrorOnResult(int result, string message)
        {
            if (result == -1)
            {
                throw new Exception(message);
            }
        }
    }
}
