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

        private void StartProcess_Click(object sender, RoutedEventArgs e)
        {
            string processPath = "cmd.exe";
            string arguments = "/c WpfApp_4.06 7 3 +"; 

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

        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: ParentProcess <number1> <number2> <operation>");
                return;
            }

            if (!int.TryParse(args[0], out int num1) || !int.TryParse(args[1], out int num2))
            {
                Console.WriteLine("Invalid numbers provided.");
                return;
            }

            string operation = args[2];
            int result = 0;

            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        Console.WriteLine("Cannot divide by zero.");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid operation.");
                    return;
            }

            Console.WriteLine($"Result: {result}");
        }
    }
}
