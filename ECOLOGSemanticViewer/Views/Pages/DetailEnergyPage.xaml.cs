﻿using ECOLOGSemanticViewer.ViewModels.PageViewModels;
using ECOLOGSemanticViewer.Views.Windows;
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

namespace ECOLOGSemanticViewer.Views.Pages
{
    /// <summary>
    /// DetailEnergyPage.xaml の相互作用ロジック
    /// </summary>
    public partial class DetailEnergyPage : Page
    {
        public DetailEnergyPage()
        {
            InitializeComponent();
        }

        private void clearButtonCalar()
        {
            this.ButtonMin.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonMedian.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonMax.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonDistUnderMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonDistMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonDistUpperMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonComMinMax.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonComMinMode.Foreground = SystemColors.ControlDarkDarkBrush;
            this.ButtonComModeMax.Foreground = SystemColors.ControlDarkDarkBrush;
        }

        private void Button_Click_Number(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.createNumberModel();
        }

        private void Button_Click_Percent(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.createPercentModel();
        }

        private void Button_Click_Min(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonMin.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel ;
            if (context == null) { return; }

            context.SetLevelAnnotation(context.EnergyHistogramDatum.MinLevel);
        }

        private void Button_Click_Mode(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonMode.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetLevelAnnotation(context.EnergyHistogramDatum.ModeLevel);
        }

        private void Button_Click_Median(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonMedian.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetLevelAnnotation(context.EnergyHistogramDatum.MedianLevel);
        }

        private void Button_Click_Max(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonMax.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetLevelAnnotation(context.EnergyHistogramDatum.MaxLevel);
        }

        private void Button_Click_UnderMode(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonDistUnderMode.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetDistAnnotation(context.EnergyHistogramDatum.UnderModeData, context.DistUnderMode);
        }

        private void Button_Click_DistMode(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonDistMode.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetDistAnnotation(context.EnergyHistogramDatum.ModeData, context.DistMode);
        }

        private void Button_Click_UpperMode(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonDistUpperMode.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetDistAnnotation(context.EnergyHistogramDatum.UpperModeData, context.DistUpperMode);
        }

        private void Button_Click_ComMinMax(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonComMinMax.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetCompAnnotation(context.EnergyHistogramDatum.MinLevel, context.EnergyHistogramDatum.MaxLevel);
        }

        private void Button_Click_ComMinMode(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonComMinMode.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetCompAnnotation(context.EnergyHistogramDatum.MinLevel, context.EnergyHistogramDatum.ModeLevel);
        }

        private void Button_Click_ComModeMax(object sender, RoutedEventArgs e)
        {
            clearButtonCalar();
            this.ButtonComModeMax.Foreground = Brushes.Orange;

            var context = this.DataContext as DetailEnergyPageViewModel;
            if (context == null) { return; }

            context.SetCompAnnotation(context.EnergyHistogramDatum.ModeLevel, context.EnergyHistogramDatum.MaxLevel);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MapWindow window = new MapWindow();
            window.Show();
        }
    }
}
