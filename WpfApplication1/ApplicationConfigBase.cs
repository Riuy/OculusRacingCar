using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusRacingCar
{
    public class ApplicationConfigBase : ViewModelBase, IApplicationConfig
    {
        private string _defaultMediaFile;
        public string DefaultMediaFile
        {
            get { return _defaultMediaFile; }
            set
            {
                _defaultMediaFile = value;
                OnPropertyChanged("DefaultMediaFile");
            }
        }

        private string _samplesFolder;
        public string SamplesFolder
        {
            get { return _samplesFolder; }
            set
            {
                _samplesFolder = value;
                OnPropertyChanged("SamplesFolder");
            }
        }

        private int _cameraFieldOfView;
        public int CameraFieldOfView
        {
            get { return _cameraFieldOfView; }
            set
            {
                _cameraFieldOfView = value;
                OnPropertyChanged("CameraFieldOfView");
            }
        }

        private int _viewportsHorizontalOffset;
        public int ViewportsHorizontalOffset
        {
            get { return _viewportsHorizontalOffset; }
            set
            {
                _viewportsHorizontalOffset = value;
                OnPropertyChanged("ViewportsHorizontalOffset");
            }
        }

        private int _viewportsVerticalOffset;
        public int ViewportsVerticalOffset
        {
            get { return _viewportsVerticalOffset; }
            set
            {
                _viewportsVerticalOffset = value;
                OnPropertyChanged("ViewportsVerticalOffset");
            }
        }

        private double _neckHeight;
        public double NeckHeight
        {
            get { return _neckHeight; }
            set
            {
                _neckHeight = value;
                OnPropertyChanged("NeckHeight");
            }
        }

        private bool _readSideCarPresets;
        public bool ReadSideCarPresets
        {
            get { return _readSideCarPresets; }
            set
            {
                _readSideCarPresets = value;
                OnPropertyChanged("ReadSideCarPresets");
            }
        }

        private string _defaultMedia;
        public string DefaultMedia
        {
            get { return _defaultMedia; }
            set
            {
                _defaultMedia = value;
                OnPropertyChanged("DefaultMedia");
            }
        }

        private string _defaultEffect;
        public string DefaultEffect
        {
            get { return _defaultEffect; }
            set
            {
                _defaultEffect = value;
                OnPropertyChanged("DefaultEffect");
            }
        }

        private string _defaultDistortion;
        public string DefaultDistortion
        {
            get { return _defaultDistortion; }
            set
            {
                _defaultDistortion = value;
                OnPropertyChanged("DefaultDistortion");
            }
        }

        private string _defaultProjection;
        public string DefaultProjection
        {
            get { return _defaultProjection; }
            set
            {
                _defaultProjection = value;
                OnPropertyChanged("DefaultProjection");
            }
        }

        private string _defaultTracker;
        public string DefaultTracker
        {
            get { return _defaultTracker; }
            set
            {
                _defaultTracker = value;
                OnPropertyChanged("DefaultTracker");
            }
        }

        private string _defaultStabilizer;
        public string DefaultStabilizer
        {
            get { return _defaultStabilizer; }
            set
            {
                _defaultStabilizer = value;
                OnPropertyChanged("DefaultStabilizer");
            }
        }

        public ApplicationConfigBase()
        {
            this.CameraFieldOfView = 78;
            this.ViewportsHorizontalOffset = 65;
            this.ViewportsVerticalOffset = 0;
            this.NeckHeight = 0.4;
        }
    }
}
