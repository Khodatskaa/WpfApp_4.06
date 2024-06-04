using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace WpfApp_4._06
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartProcess_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"E:\someFolder\example.txt"; 
            string searchWord = "bicycle"; 

            string processPath = "cmd.exe";
            string arguments = $"/c {AppDomain.CurrentDomain.BaseDirectory}\\ChildProcess.exe {filePath} {searchWord}";

            StartProcess(processPath, arguments);
        }

        private void StartProcess(string processPath, string arguments)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = processPath;
                startInfo.Arguments = arguments;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting process: {ex.Message}");
            }
        }

        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: ParentProcess <file_path> <search_word>");
                return 1;
            }

            string filePath = args[0];
            string searchWord = args[1];

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int count = 0;

                foreach (string line in lines)
                {
                    count += CountOccurrences(line, searchWord);
                }

                Console.WriteLine($"Number of occurrences of '{searchWord}' in file '{filePath}': {count}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        }

        static int CountOccurrences(string line, string searchWord)
        {
            int index = 0;
            int count = 0;

            while ((index = line.IndexOf(searchWord, index)) != -1)
            {
                index += searchWord.Length;
                count++;
            }

            return count;
        }
    }
}
