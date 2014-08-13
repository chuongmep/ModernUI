﻿using ModernUI.Windows.Controls;
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

namespace ModernUI.App.Content
{
    /// <summary>
    /// Interaction logic for ControlsModernWindow.xaml
    /// </summary>
    public partial class ControlsModernWindow : UserControl
    {
        public ControlsModernWindow()
        {
            InitializeComponent();
        }

        private void BlankWindow_Click(object sender, RoutedEventArgs e)
        {
            // create a blank modern window with lorem content
            // the BlankWindow ModernWindow styles is found in the mui assembly at Assets/ModernWindowEx.xaml

            var wnd = new ModernWindow
            {
                Style = (Style)App.Current.Resources["BlankWindow"],
                Title = "ModernWindow",
                IsTitleVisible = true == this.title.IsChecked,
                Content = new LoremIpsum(),
                Width = 480,
                Height = 480
            };

            if (true == this.logo.IsChecked)
            {
                wnd.LogoData = PathGeometry.Parse("M 43.8489,37.2997C 43.8489,37.2997 44.2106,34.8791 45.6137,34.8791C 47.0177,34.8791 48.9456,38.1112 48.9456,38.1112C 48.9456,38.1112 44.5479,37.2997 43.8489,37.2997 Z M 51.2373,24.1416C 50.5825,23.013 47.1567,21.7126 45.3788,21.7126C 43.6029,21.7126 40.7975,21.7126 40.7975,21.7126C 40.7975,21.7126 39.3265,19.0499 35.8683,19.0499C 32.4072,19.0499 32.6401,20.5925 32.6401,21.9097L 32.6401,27.2347L 31.0714,28.8905L 23.8278,28.8905C 23.8278,28.8905 21.7871,30.2401 21.7871,33.1576C 21.7871,36.0751 22.6925,46.2416 28.7717,47.1819C 35.9643,48.2962 37.1957,44.9534 37.1957,44.5517C 37.1957,42.8588 37.2382,40.2938 37.2382,40.2938C 37.2382,40.2938 39.3446,44.318 42.5253,44.318C 45.706,44.318 47.5555,46.1452 47.5555,48.0263C 47.5555,49.9095 47.5555,51.5081 47.5555,51.5081C 47.5555,51.5081 47.4372,53.6873 45.5678,53.6873C 43.6956,53.6873 41.5761,53.6873 41.5761,53.6873C 41.5761,53.6873 40.266,52.667 40.266,51.2558C 40.266,49.8451 40.9065,49.4619 41.6544,49.4619C 42.4019,49.4619 43.0163,49.5487 43.0163,49.5487L 43.0163,46.5806C 43.0163,46.5806 36.9923,46.5414 36.9923,51.1539C 36.9923,55.7651 40.1426,56.9501 42.6685,56.9501C 45.1918,56.9501 46.7832,56.9501 46.7832,56.9501C 46.7832,56.9501 54.2129,55.9946 54.2129,41.3163C 54.2129,26.6363 51.8925,25.271 51.2373,24.1416 Z M 29.475,27.2174L 23.3318,27.2107L 31.0285,19.4668L 31.0302,25.7124L 29.475,27.2174 Z ");
            }
            if (true == this.noresize.IsChecked)
            {
                wnd.ResizeMode = ResizeMode.NoResize;
            }
            else if (true == this.canminimize.IsChecked)
            {
                wnd.ResizeMode = ResizeMode.CanMinimize;
            }
            else if (true == this.canresize.IsChecked)
            {
                wnd.ResizeMode = ResizeMode.CanResize;
            }
            else if (true == this.canresizewithgrip.IsChecked)
            {
                wnd.ResizeMode = ResizeMode.CanResizeWithGrip;
            }

            wnd.Show();
        }
    }
}
