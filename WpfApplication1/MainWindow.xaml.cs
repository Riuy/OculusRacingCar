using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Threading;
using System.Windows.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

namespace OculusRacingCar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool fullscreen = false;
        private DispatcherTimer updateTime;
        
        GamepadState myGamepad;

        static public uint MAX_THROTTLE = 80;
        static public uint MID_THROTTLE = 90;
        static public uint MIN_THROTTLE = 100;

        static public uint MAX_STEERING = 120;
        static public uint MID_STEERING = 90;
        static public uint MIN_STEERING = 60;

        static public uint MAX_CAMERA = 150;
        static public uint MID_CAMERA = 90;
        static public uint MIN_CAMERA = 30;

        private uint pos_steering = MID_STEERING;
        private uint pos_cam = MID_CAMERA;
        private uint pos_throttle = MID_THROTTLE;

        private Socket ClientSocket = null;

        private TrackerBase myTracker;

        private readonly IApplicationState _state;
        public IApplicationState State
        {
            get { return _state; }
        }

        private readonly IApplicationConfig _config;
        public IApplicationConfig Config
        {
            get { return _config; }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            myTracker = new OculusRiftTracker();
            this.myTracker.Load();

            this._state = new DefaultApplicationState();
            this._config = new ApplicationConfigBase();

            this.updateTime = new DispatcherTimer();
            this.updateTime.Interval = TimeSpan.FromMilliseconds(0.5);
            this.updateTime.Tick += new EventHandler(update);
            this.updateTime.Start();

            this.myTracker.Calibrate();

            this.myGamepad = new GamepadState(0);

            IPAddress ip = IPAddress.Parse("192.168.0.100");
            IPEndPoint ipEnd = new IPEndPoint(ip, 4242);
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket.Connect(ipEnd);
        }

        private void enableFullscreen()
        {
            fullscreen = true;
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            this.Cursor = Cursors.None;
        }

        private void disableFullscreen()
        {
            fullscreen = false;
            this.WindowState = WindowState.Normal;
            this.Cursor = Cursors.Arrow;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
        }

        private void window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F9)
            {
                if (fullscreen)
                    this.disableFullscreen();
                else
                    this.enableFullscreen();
            }
            else if (e.Key == Key.F1)
            {
                var settingsWindow = new SettingsWindow(this);
                settingsWindow.Owner = this;
                settingsWindow.ShowDialog();
            }
            else if (e.Key == Key.Escape)
                this.Close();
        }

        void update(object sender, EventArgs e)
        {
            this.handleEvent();
            var msg = new byte[3];
            msg[0] = (byte)this.pos_throttle;
            msg[1] = (byte)this.pos_steering;
            msg[2] = (byte)this.pos_cam;
            try
            {
              ClientSocket.Send(msg, msg.Length, SocketFlags.None);
            }
            catch (Exception)
            {
                MessageBox.Show("Deconnexion de la voiture");
                this.Close();
            }
        }

        void handleEvent()
        {
            ((OculusRiftTracker)this.myTracker).Update();
            this.myGamepad.Update();

            if (myGamepad.LeftStick.Position.X > 0.15)
            {
                this.pos_steering = MID_STEERING - (uint)((MID_STEERING - MIN_STEERING) * myGamepad.LeftStick.Position.X);
            }
            else if (myGamepad.LeftStick.Position.X < -0.15)
            {
                this.pos_steering = MID_STEERING + (uint)((MAX_STEERING - MID_STEERING) * -(myGamepad.LeftStick.Position.X));
            }
            else
            {
                this.pos_steering = MID_STEERING;
            }
            if (myTracker.Rotation.Y > 0.05)
            {
                this.pos_cam = MID_CAMERA - (uint)((MID_CAMERA - MIN_CAMERA) * myTracker.Rotation.Y);
            }
            else if (myTracker.Rotation.Y < 0.05)
            {
                this.pos_cam = MID_CAMERA + (uint)((MAX_CAMERA - MID_CAMERA) * -(myTracker.Rotation.Y));
            }
            else
            {
                this.pos_cam = MID_CAMERA;
            }
            float tmpTrottle = this.myGamepad.RightTrigger - this.myGamepad.LeftTrigger;
            if (tmpTrottle > 0)
            {
                this.pos_throttle = MID_THROTTLE - (uint)((MID_THROTTLE - MAX_THROTTLE) * tmpTrottle);
            }
            else
            {
                this.pos_throttle = MID_THROTTLE + (uint)((MIN_THROTTLE - MID_THROTTLE) * -tmpTrottle);
            }
        }

        private void window_Closed(object sender, EventArgs e)
        {
            this.State.Unload();
        }
    
    }
}
