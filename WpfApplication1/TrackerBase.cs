using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace OculusRacingCar
{
    public abstract class TrackerBase : ViewModelBase
    {
        private const double MoveFactor = 0.01;

        protected Vector3D RawPosition;
        protected Quaternion RawRotation;

        private Vector3D _position;
        public Vector3D Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }

        private Quaternion _rotation;
        public Quaternion Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
                OnPropertyChanged("Rotation");
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public static readonly DependencyProperty PositionScaleFactorProperty =
            DependencyProperty.Register("PositionScaleFactor", typeof(double),
            typeof(TrackerBase), new FrameworkPropertyMetadata(1D));
        public double PositionScaleFactor
        {
            get { return (double)GetValue(PositionScaleFactorProperty); }
            set { SetValue(PositionScaleFactorProperty, value); }
        }

        private Vector3D _basePosition;
        public Vector3D BasePosition
        {
            get
            {
                return _basePosition;
            }
            set
            {
                _basePosition = value;
                OnPropertyChanged("BasePosition");
            }
        }

        private Vector3D _positionOffset;
        public Vector3D PositionOffset
        {
            get
            {
                return _positionOffset;
            }
            set
            {
                _positionOffset = value;
                OnPropertyChanged("PositionOffset");
            }
        }

        private Quaternion _baseRotation;
        public Quaternion BaseRotation
        {
            get
            {
                return _baseRotation;
            }
            set
            {
                _baseRotation = value;
                OnPropertyChanged("BaseRotation");
            }
        }

        public static readonly DependencyProperty RotationOffsetProperty =
            DependencyProperty.Register("RotationOffset", typeof(Quaternion),
            typeof(TrackerBase), new FrameworkPropertyMetadata(new Quaternion()));
        public Quaternion RotationOffset
        {
            get { return (Quaternion)GetValue(RotationOffsetProperty); }
            set { SetValue(RotationOffsetProperty, value); }
        }

        protected void UpdatePositionAndRotation()
        {
            Rotation = BaseRotation * RawRotation * RotationOffset;
            var relativePos = BasePosition + (RawPosition * PositionScaleFactor);
            var m = Matrix3D.Identity;
            m.Rotate(BaseRotation);
            m.Translate(relativePos);
            m.Rotate(BaseRotation);
            Position = new Vector3D(m.OffsetX, m.OffsetY, m.OffsetZ);
        }

        public virtual void Calibrate()
        {
            var conjugate = new Quaternion(RawRotation.X, RawRotation.Y, RawRotation.Z, RawRotation.W) * RotationOffset;
            conjugate.Conjugate();
            BaseRotation = conjugate;
            BasePosition = -(RawPosition * PositionScaleFactor) + _positionOffset;
        }

        private void Move(Vector3D moveVector)
        {
            var m = Matrix3D.Identity;
            m.Rotate(Rotation);
            m.Translate(moveVector);
            m.Rotate(Rotation);
            BasePosition = BasePosition + new Vector3D(m.OffsetX, m.OffsetY, m.OffsetZ);
        }

        private void Reset()
        {
            BasePosition = new Vector3D();
            BaseRotation = new Quaternion();
        }

        public abstract void Load();
        public abstract void Unload();
    }
}
