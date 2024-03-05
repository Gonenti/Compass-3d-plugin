namespace TeapotPlugin.StressTesting
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Microsoft.VisualBasic.Devices;
    using TeapotPlugin.Model;
    using TeapotPlugin.Wrapper;

    /// <summary>
    /// A class for load testing.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point.
        /// </summary>
        /// <param name="args">Аргументы.</param>
        static void Main(string[] args)
        {
            TeapotParameters parameters = new TeapotParameters();
            TeapotBuilder builder = new TeapotBuilder();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var streamWriter = new StreamWriter($"log.txt", true);
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var count = 0;

            while (true)
            {
                const double gigabyteInByte = 0.000000000931322574615478515625;
                builder.BuildTeapot(parameters);
                var computerInfo = new ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory
                                  - computerInfo.AvailablePhysicalMemory)
                                 * gigabyteInByte;
                streamWriter.WriteLine($"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
                streamWriter.Flush();

            }

            stopWatch.Stop();
            streamWriter.Close();
            streamWriter.Dispose();
            Console.Write($"End {new ComputerInfo().TotalPhysicalMemory}");
        }
    }
}
