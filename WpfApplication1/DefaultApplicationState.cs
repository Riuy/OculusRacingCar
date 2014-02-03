using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OculusRacingCar
{
    public class DefaultApplicationState : ViewModelBase, IApplicationState
    {
        #region Properties

        public static readonly DependencyProperty MediaPluginProperty =
            DependencyProperty.Register("MediaPlugin", typeof(IPlugin<IMedia>), typeof(DefaultApplicationState),
            new FrameworkPropertyMetadata(OnMediaPluginChanged));
        public IPlugin<IMedia> MediaPlugin
        {
            get { return (IPlugin<IMedia>)GetValue(MediaPluginProperty); }
            set { SetValue(MediaPluginProperty, value); }
        }

        private static void OnMediaPluginChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (IPlugin<IMedia>)args.OldValue;
            if (oldValue != null)
                oldValue.Unload();

            var newValue = (IPlugin<IMedia>)args.NewValue;
            if (newValue != null)
                newValue.Load();

            ((DefaultApplicationState)obj).OnPropertyChanged("MediaPlugin");
        }

        public static readonly DependencyProperty ProjectionPluginProperty =
            DependencyProperty.Register("ProjectionPlugin", typeof(IPlugin<IProjection>), typeof(DefaultApplicationState),
            new FrameworkPropertyMetadata(OnProjectionPluginChanged));
        public IPlugin<IProjection> ProjectionPlugin
        {
            get { return (IPlugin<IProjection>)GetValue(ProjectionPluginProperty); }
            set { SetValue(ProjectionPluginProperty, value); }
        }

        private static void OnProjectionPluginChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var state = (DefaultApplicationState)obj;

            var oldValue = (IPlugin<IProjection>)args.OldValue;
            if (oldValue != null)
            {
                oldValue.Unload();
            }

            var newValue = (IPlugin<IProjection>)args.NewValue;
            if (newValue != null)
            {
                newValue.Load();
                newValue.Content.StereoMode = state.StereoInput;
            }

            ((DefaultApplicationState)obj).OnPropertyChanged("ProjectionPlugin");
        }

        public static readonly DependencyProperty DistortionPluginProperty =
            DependencyProperty.Register("DistortionPlugin", typeof(IPlugin<DistortionBase>), typeof(DefaultApplicationState),
            new FrameworkPropertyMetadata(OnDistortionPluginChanged));
        public IPlugin<DistortionBase> DistortionPlugin
        {
            get { return (IPlugin<DistortionBase>)GetValue(DistortionPluginProperty); }
            set { SetValue(DistortionPluginProperty, value); }
        }

        private static void OnDistortionPluginChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldValue = (IPlugin<DistortionBase>)args.OldValue;
            if (oldValue != null)
                oldValue.Unload();

            var newValue = (IPlugin<DistortionBase>)args.NewValue;
            if (newValue != null)
                newValue.Load();

            ((DefaultApplicationState)obj).OnPropertyChanged("DistortionPlugin");
        }

        public static readonly DependencyProperty StereoInputProperty =
            DependencyProperty.Register("StereoInput", typeof(StereoMode), typeof(DefaultApplicationState),
            new FrameworkPropertyMetadata(OnStereoInputChanged));
        public StereoMode StereoInput
        {
            get { return (StereoMode)GetValue(StereoInputProperty); }
            set { SetValue(StereoInputProperty, value); }
        }

        private static void OnStereoInputChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var projectionPlugin = ((DefaultApplicationState)obj).ProjectionPlugin;
            if (projectionPlugin != null && projectionPlugin.Content != null)
            {
                projectionPlugin.Content.StereoMode = (StereoMode)args.NewValue;
            }
        }

        private LayoutMode _stereoOutput;
        public LayoutMode StereoOutput
        {
            get
            {
                return _stereoOutput;
            }
            set
            {
                _stereoOutput = value;
                OnPropertyChanged("StereoOutput");
            }
        }

        public ProjectionMode ModeProj { get; set; }

        #endregion

        public DefaultApplicationState()
        {
            ModeProj = ProjectionMode.PLANE;

            MediaPlugin = new CapturePlugin();
            DistortionPlugin = new BarrelPlugin();
            ProjectionPlugin = new PlanePlugin();

            this.StereoOutput = LayoutMode.SideBySide;
            this.StereoInput = StereoMode.Mono;
        }

        public void Unload()
        {

            this.MediaPlugin.Unload();
            this.DistortionPlugin.Unload();
            this.ProjectionPlugin.Unload();
        }
    }
}
