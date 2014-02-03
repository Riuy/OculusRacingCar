using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace OculusRacingCar
{
    public interface ITracker : ILoadable
    {
        bool IsActive { get; set; }
        bool IsEnabled { get; set; }
        Quaternion BaseRotation { get; set; }
        Quaternion Rotation { get; set; }
        Quaternion RotationOffset { get; set; }
        Vector3D BasePosition { get; set; }
        Vector3D Position { get; set; }
        Vector3D PositionOffset { get; set; }
        double PositionScaleFactor { get; set; }
        void Calibrate();
    }
}
