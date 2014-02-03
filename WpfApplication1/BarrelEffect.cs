using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace OculusRacingCar
{
    public class BarrelEffect : DistortionBase
    {
        public static readonly DependencyProperty InputProperty =
            RegisterPixelShaderSamplerProperty("Input", typeof(BarrelEffect), 0);
        public Brush Input
        {
            get { return ((Brush)(GetValue(InputProperty))); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty FactorProperty =
            DependencyProperty.Register("Factor", typeof(double), typeof(BarrelEffect), new UIPropertyMetadata(1.45D, PixelShaderConstantCallback(0)));
        public double Factor
        {
            get { return ((double)(GetValue(FactorProperty))); }
            set { SetValue(FactorProperty, value); }
        }

        public static readonly DependencyProperty XCenterProperty =
            DependencyProperty.Register("XCenter", typeof(double), typeof(BarrelEffect), new UIPropertyMetadata(0.5D, PixelShaderConstantCallback(1)));
        public double XCenter
        {
            get { return ((double)(GetValue(XCenterProperty))); }
            set { SetValue(XCenterProperty, value); }
        }

        public static readonly DependencyProperty YCenterProperty =
            DependencyProperty.Register("YCenter", typeof(double), typeof(BarrelEffect), new UIPropertyMetadata(0.5D, PixelShaderConstantCallback(2)));
        public double YCenter
        {
            get { return ((double)(GetValue(YCenterProperty))); }
            set { SetValue(YCenterProperty, value); }
        }

        public static readonly DependencyProperty BlueOffsetProperty =
            DependencyProperty.Register("BlueOffset", typeof(double), typeof(BarrelEffect), new UIPropertyMetadata(0D, PixelShaderConstantCallback(3)));
        public double BlueOffset
        {
            get { return ((double)(GetValue(BlueOffsetProperty))); }
            set { SetValue(BlueOffsetProperty, value); }
        }

        public static readonly DependencyProperty RedOffsetProperty =
            DependencyProperty.Register("RedOffset", typeof(double), typeof(BarrelEffect), new UIPropertyMetadata(0D, PixelShaderConstantCallback(4)));
        public double RedOffset
        {
            get { return ((double)(GetValue(RedOffsetProperty))); }
            set { SetValue(RedOffsetProperty, value); }
        }

        public BarrelEffect()
        {
            var pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri(@"pack://application:,,,/OculusRacingCar;component/BarrelEffect.ps");
            PixelShader = pixelShader;
            
            this.Factor = 1.50235919376538;
            this.XCenter = 0.5;
            this.YCenter = 0.5;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(FactorProperty);
            UpdateShaderValue(XCenterProperty);
            UpdateShaderValue(YCenterProperty);
            UpdateShaderValue(BlueOffsetProperty);
            UpdateShaderValue(RedOffsetProperty);
        }
    }
}
