using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace OculusRacingCar
{
    public interface IProjection : ILoadable
    {
        MeshGeometry3D Geometry { get; }
        Vector3D CameraLeftPosition { get; }
        Vector3D CameraRightPosition { get; }
        StereoMode StereoMode { get; set; }
    }
}
