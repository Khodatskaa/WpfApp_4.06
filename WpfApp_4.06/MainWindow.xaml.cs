using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;

namespace WpfApp_4._06
{
    public partial class MainWindow : Window
    {
        private System.Timers.Timer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Elapsed += TimerElapsed;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntervalTextBox.Text, out int interval) && interval > 0)
            {
                _timer.Interval = interval;
                _timer.Start();
                StartButton.IsEnabled = false;
                StopButton.IsEnabled = true;
                UpdateProcessList();
            }
            else
            {
                MessageBox.Show("Please enter a valid interval in milliseconds.");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(UpdateProcessList);
        }

        private void UpdateProcessList()
        {
            var processes = Process.GetProcesses().Select(p => new
            {
                p.Id,
                p.ProcessName,
                p.WorkingSet64,
                StartTime = SafeGetStartTime(p)
            }).ToList();

            ProcessDataGrid.ItemsSource = processes;
        }

        private string SafeGetStartTime(Process p)
        {
            try
            {
                return p.StartTime.ToString("g");
            }
            catch
            {
                return "N/A";
            }
        }

        private void ProcessDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProcessDataGrid.SelectedItem != null)
            {
                dynamic selectedProcess = ProcessDataGrid.SelectedItem;
                int processId = selectedProcess.Id;

                try
                {
                    Process process = Process.GetProcessById(processId);

                    string processDetails = $"Process ID: {process.Id}\n" +
                                            $"Start Time: {SafeGetStartTime(process)}\n" +
                                            $"Total CPU Time: {process.TotalProcessorTime}\n" +
                                            $"Number of Threads: {process.Threads.Count}\n" +
                                            $"Number of Instances: {Process.GetProcessesByName(process.ProcessName).Length}";

                    ProcessDetailsTextBlock.Text = processDetails;
                }
                catch (Exception ex)
                {
                    ProcessDetailsTextBlock.Text = $"Error retrieving process details: {ex.Message}";
                }
            }
        }
    }
}
