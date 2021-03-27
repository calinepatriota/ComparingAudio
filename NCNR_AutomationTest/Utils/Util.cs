using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace NCNR_AutomationTest.Utils
{
    class Util
    {

        #region Public Methods

        public static void ReadConsoleOutput(string path, string consoleLine, string testName)
        {
            var outputConsole = new StreamWriter("output_" + testName + "_" + GetData() + ".txt");
            Process process = new Process();
            process.StartInfo.FileName = "CMD.exe";
            process.StartInfo.Arguments = "'/C cd " + path + "&&" + consoleLine;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    outputConsole.WriteLine(e.Data);
                }
            });

            process.Start();
            PerformanceCounterCPU(testName);
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.Close();
            outputConsole.Close();
        }

        #endregion




        #region Private Methods
        private static void PerformanceCounterCPU(string testName)
        {
            PerformanceCounter myAppCpu = new PerformanceCounter("Process", "% Processor Time", "ConsoleApp", true);
            double consumptionCPU = 0;

            while (consumptionCPU <= 0)
            {
                consumptionCPU = myAppCpu.NextValue();
            }

            double consumptionCPUMax = 0;

            while (myAppCpu.InstanceName != null)
            {
                try
                {
                    consumptionCPU = myAppCpu.NextValue();
                }
                catch (Exception e)
                {
                    break;
                }

                if (consumptionCPU > consumptionCPUMax)
                {
                    consumptionCPUMax = consumptionCPU;
                }

                Console.WriteLine("ConsoleApp.exe % = " + consumptionCPUMax);
                Console.WriteLine("ConsoleApp.exe % = " + consumptionCPU);
                Thread.Sleep(250);
            }
            try
            {
                StreamWriter sw = new StreamWriter("outputCPU_" + testName + "_" + GetData() + ".txt");
                //Write a line of text
                sw.WriteLine("Quantity of processors: " + Environment.ProcessorCount);
                sw.WriteLine("ConsoleApp.exe - % CPU Consumption = " + consumptionCPUMax / Environment.ProcessorCount);
                sw.WriteLine("ConsoleApp.exe - CPU Consumption = " + consumptionCPUMax);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        private static string GetData()
        {
            string formatDate = DateTime.Now.ToString("MM.dd.yyyy HH.mm.ss");
            return formatDate;
        }

        #endregion


    }



}


