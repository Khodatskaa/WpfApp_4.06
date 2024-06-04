using System;
using System.Diagnostics;
using System.Windows;

namespace WpfApp_4._06
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LaunchNotepad_Click(object sender, RoutedEventArgs e)
        {
            LaunchApplication("notepad.exe");
        }

        private void LaunchCalculator_Click(object sender, RoutedEventArgs e)
        {
            LaunchApplication("calc.exe");
        }

        private void LaunchPaint_Click(object sender, RoutedEventArgs e)
        {
            LaunchApplication("mspaint.exe");
        }

        private void LaunchCustomApp_Click(object sender, RoutedEventArgs e)
        {
            LaunchApplication("your_custom_app.exe");
        }

        private void LaunchApplication(string appPath)
        {
            try
            {
                Process.Start(appPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error launching application: {ex.Message}");
            }
        }
    }
}
