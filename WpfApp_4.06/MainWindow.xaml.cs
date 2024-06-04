using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp_4._06
{
    public partial class MainWindow : Window
    {
        private Process? _process;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartProcess_Click(object sender, RoutedEventArgs e)
        {
            string processPath = "notepad.exe";
            _process = StartProcess(processPath);
            if (_process != null)
            {
                CompletionCodeTextBlock.Text = "Process started. Choose an action.";
                EnableActionButtons(true);
            }
            else
            {
                CompletionCodeTextBlock.Text = "Failed to start process.";
            }
        }

        private void WaitForCompletion_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null)
            {
                Task.Run(() =>
                {
                    try
                    {
                        _process.WaitForExit();
                        int exitCode = _process.ExitCode;
                        Dispatcher.Invoke(() => CompletionCodeTextBlock.Text = $"Process completed with exit code: {exitCode}");
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(() => MessageBox.Show($"Error waiting for process completion: {ex.Message}"));
                    }
                    finally
                    {
                        Dispatcher.Invoke(() => EnableActionButtons(false));
                    }
                });
            }
        }

        private void ForceClose_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null)
            {
                try
                {
                    _process.Kill();
                    _process.WaitForExit();
                    int exitCode = _process.ExitCode;
                    CompletionCodeTextBlock.Text = $"Process was forced to close. Exit code: {exitCode}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error forcing process to close: {ex.Message}");
                }
                finally
                {
                    EnableActionButtons(false);
                }
            }
        }


        private Process? StartProcess(string processPath)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = processPath;
                process.Start();
                return process;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting process: {ex.Message}");
                return null;
            }
        }

        private void EnableActionButtons(bool enable)
        {
            WaitForCompletionButton.IsEnabled = enable;
            ForceCloseButton.IsEnabled = enable;
        }
    }
}
