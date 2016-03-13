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
        Recognition _recognition;

        public MainWindow()
        {
            InitializeComponent();
            _recognition = new Recognition();
            this.DataContext = _recognition;
        }

        private void Power_Checked(object sender, RoutedEventArgs e)
        {
            if (Power.IsChecked.Value)
                _recognition.Start();
            else
                _recognition.Stop();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _recognition.DesiredQuality = (int)slider.Value - 3; // translate [0..4] into [-2..1]
        }
    }
}
