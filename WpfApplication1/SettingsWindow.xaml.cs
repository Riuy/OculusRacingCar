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
using System.Windows.Shapes;

namespace OculusRacingCar
{
    /// <summary>
    /// Logique d'interaction pour SettingsWindow.xaml
    /// </summary>

    public partial class SettingsWindow : Window
    {
        private MainWindow parent_;

        public SettingsWindow(MainWindow parent)
        {
            InitializeComponent();

            this.parent_ = parent;
            this.Top = parent_.Top + (parent_.Height / 2) - (this.Height / 2);
            this.Left = parent_.Left + (parent_.Width / 2) - (this.Width / 2);
            
            this.sliderNeckHeight.Value = parent_.Config.NeckHeight;
            this.sliderHorizontalOffset.Value = parent_.Config.ViewportsHorizontalOffset;
            this.sliderVerticalOffset.Value = parent_.Config.ViewportsVerticalOffset;
            this.sliderFOV.Value = parent_.Config.CameraFieldOfView;
            if (parent_.State.ModeProj == ProjectionMode.PLANE)
            {
                this.sliderRatioProjections.Value = ((PlaneProjection)parent_.State.ProjectionPlugin.Content).Ratio;
                this.ComboboxProjection.SelectedIndex = 0;
            }
            else if (parent_.State.ModeProj == ProjectionMode.DOME)
            {
                this.sliderHorizontalCoverage.Value = ((DomeProjection)parent_.State.ProjectionPlugin.Content).HorizontalCoverage;
                this.sliderVerticalCoverage.Value = ((DomeProjection)parent_.State.ProjectionPlugin.Content).VerticalCoverage;
                this.sliderSlices.Value = ((DomeProjection)parent_.State.ProjectionPlugin.Content).Slices;
                this.sliderStacks.Value = ((DomeProjection)parent_.State.ProjectionPlugin.Content).Stacks;
                this.ComboboxProjection.SelectedIndex = 1;
            }

            this.sliderBlue.Value = ((BarrelEffect)parent_.State.DistortionPlugin.Content).BlueOffset;
            this.sliderRed.Value = ((BarrelEffect)parent_.State.DistortionPlugin.Content).RedOffset;


            this.sliderXcenter.Value = ((BarrelEffect)parent_.State.DistortionPlugin.Content).XCenter;
            this.sliderYCenter.Value = ((BarrelEffect)parent_.State.DistortionPlugin.Content).YCenter;
            this.sliderFactor.Value = ((BarrelEffect)parent_.State.DistortionPlugin.Content).Factor;


        }

        private void sliderNeckHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            parent_.Config.NeckHeight = sliderNeckHeight.Value;
        }

        private void sliderHorizontalOffset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            parent_.Config.ViewportsHorizontalOffset = (int)sliderHorizontalOffset.Value;
        }

        private void sliderVerticalOffset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            parent_.Config.ViewportsVerticalOffset = (int)sliderVerticalOffset.Value;
        }

        private void sliderFOV_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            parent_.Config.CameraFieldOfView = (int)sliderFOV.Value;
        }

        private void sliderRatioProjections_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (parent_ != null && parent_.State.ModeProj == ProjectionMode.PLANE)
                ((PlaneProjection)parent_.State.ProjectionPlugin.Content).Ratio = this.sliderRatioProjections.Value;
        }

        private void sliderFactor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((BarrelEffect)parent_.State.DistortionPlugin.Content).Factor = this.sliderFactor.Value;
        }

        private void sliderXcenter_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((BarrelEffect)parent_.State.DistortionPlugin.Content).XCenter = this.sliderXcenter.Value;
        }

        private void sliderYCenter_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((BarrelEffect)parent_.State.DistortionPlugin.Content).YCenter = this.sliderYCenter.Value;
        }

        private void sliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((BarrelEffect)parent_.State.DistortionPlugin.Content).BlueOffset = this.sliderBlue.Value;
        }

        private void sliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((BarrelEffect)parent_.State.DistortionPlugin.Content).RedOffset = this.sliderRed.Value;
        }

        private void SettingWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                this.Close();
            }
        }

        private void ComboboxProjection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (parent_ != null && ComboboxProjection.SelectedIndex == 0) // PLANE
            {
                this.GridProjectionsDome.Visibility = System.Windows.Visibility.Hidden;
                this.GridProjectionsPlane.Visibility = System.Windows.Visibility.Visible;
                parent_.State.ProjectionPlugin = new PlanePlugin();
                this.sliderRatioProjections.Value = ((PlaneProjection)parent_.State.ProjectionPlugin.Content).Ratio;
                parent_.State.ModeProj = ProjectionMode.PLANE;
            }
            else if (parent_ != null && ComboboxProjection.SelectedIndex == 1) // DOME
            {
                this.GridProjectionsPlane.Visibility = System.Windows.Visibility.Hidden;
                this.GridProjectionsDome.Visibility = System.Windows.Visibility.Visible;
                parent_.State.ProjectionPlugin = new DomePlugin();
                parent_.State.ModeProj = ProjectionMode.DOME;
                this.sliderHorizontalCoverage.Value = ((DomeProjection)parent_.State.ProjectionPlugin.Content).HorizontalCoverage;
                this.sliderVerticalCoverage.Value = ((DomeProjection)parent_.State.ProjectionPlugin.Content).VerticalCoverage;
                this.sliderSlices.Value = ((DomeProjection)parent_.State.ProjectionPlugin.Content).Slices;
                this.sliderStacks.Value = ((DomeProjection)parent_.State.ProjectionPlugin.Content).Stacks;
            }
        }

        private void sliderHorizontalCoverage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (parent_ != null && parent_.State.ModeProj == ProjectionMode.DOME)
                ((DomeProjection)parent_.State.ProjectionPlugin.Content).HorizontalCoverage = this.sliderHorizontalCoverage.Value;
        }

        private void sliderVerticalCoverage_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (parent_ != null && parent_.State.ModeProj == ProjectionMode.DOME)
                ((DomeProjection)parent_.State.ProjectionPlugin.Content).VerticalCoverage = this.sliderVerticalCoverage.Value;
        }

        private void sliderSlices_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (parent_ != null && parent_.State.ModeProj == ProjectionMode.DOME)
                ((DomeProjection)parent_.State.ProjectionPlugin.Content).Slices = (int)this.sliderSlices.Value;
        }

        private void sliderStacks_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (parent_ != null && parent_.State.ModeProj == ProjectionMode.DOME)
                ((DomeProjection)parent_.State.ProjectionPlugin.Content).Stacks = (int)this.sliderStacks.Value;
        }
    }
}
