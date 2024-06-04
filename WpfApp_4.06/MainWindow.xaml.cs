using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp_4._06
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartProcess_Click(object sender, RoutedEventArgs e)
        {
            string processPath = "notepad.exe"; 
            int exitCode = await StartProcessAsync(processPath);
            CompletionCodeTextBlock.Text = $"Process completed with exit code: {exitCode}";
        }

        private Task<int> StartProcessAsync(string processPath)
        {
            return Task.Run(() =>
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = processPath;
                    process.Start();
                    process.WaitForExit();
                    return process.ExitCode;
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() => MessageBox.Show($"Error starting process: {ex.Message}"));
                    return -1; 
                }
            });
        }
    }
}
