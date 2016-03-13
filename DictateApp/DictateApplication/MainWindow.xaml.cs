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
            _recognition = new Recognition();
            this.DataContext = _recognition;
            InitializeComponent();
        }
        
        private void powerChangedEvent(object sender, RoutedEventArgs e)
        {
            progress.Visibility = Power.IsChecked.Value == true ? Visibility.Visible : Visibility.Hidden;

            if (Power.IsChecked.Value)
                _recognition.Start();
            else
                _recognition.Stop();
        }

        private void sliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _recognition.DesiredQuality = (int)slider.Value;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _recognition.Dispose();
        }
    }
}
