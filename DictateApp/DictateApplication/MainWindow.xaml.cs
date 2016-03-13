using Microsoft.ProjectOxford.SpeechRecognition;
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

namespace DictateApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Recognition _recognition = new Recognition();
            this.DataContext = _recognition;
            InitializeComponent();
        }
        
        private void Power_Checked(object sender, RoutedEventArgs e)
        {
            var r = this.DataContext as Recognition;
            progress.Visibility = Power.IsChecked.Value ? Visibility.Visible : Visibility.Hidden;

            if (Power.IsChecked.Value)
                r.Start();
            else
                r.Stop();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var r = this.DataContext as Recognition;
            r.DesiredQuality = (int)slider.Value; // translate [0..4] into [-2..1]
        }
    }
}
