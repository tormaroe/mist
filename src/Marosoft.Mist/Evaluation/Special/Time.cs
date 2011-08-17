using Marosoft.Mist.Parsing;
using Marosoft.Mist.Lexing;
using System;
using System.Diagnostics;

namespace Marosoft.Mist.Evaluation.Special
{
    /// <summary>
    /// This code was ripped out of QuickBencher
    /// http://quickbencher.codeplex.com/
    /// </summary>

    [SpecialForm("time")]
    public class Time : SpecialForm
    {
        public override Expression Call(Expression expr)
        {
            var currentProcess = Process.GetCurrentProcess();

            TimeSpan _userProcessorTimeStart;
            TimeSpan _userProcessorTimeEnd;
            TimeSpan _privilegedProcessorTimeStart;
            TimeSpan _privilegedProcessorTimeEnd;

            /* RECORD START TIME */
            Stopwatch _stopwatch = Stopwatch.StartNew();
            _userProcessorTimeStart = currentProcess.UserProcessorTime;
            _privilegedProcessorTimeStart = currentProcess.PrivilegedProcessorTime;

            /* DO THE ACTUAL OPERATION */
            var result = Evaluate(expr.Elements.Second());

            /* RECORD STOP TIME */
            _userProcessorTimeEnd = currentProcess.UserProcessorTime;
            _privilegedProcessorTimeEnd = currentProcess.PrivilegedProcessorTime;
            _stopwatch.Stop();

            /* CALCULATE ELAPSED TIME */
            var realTime = _stopwatch.ElapsedMilliseconds / 1000d;
            var userTime = GetDuration(_userProcessorTimeStart, _userProcessorTimeEnd);
            var systemTime = GetDuration(_privilegedProcessorTimeStart, _privilegedProcessorTimeEnd);
            
            Output(realTime, userTime, systemTime);
            
            return result;
        }

        private static double GetDuration(TimeSpan start, TimeSpan end)
        {
            var executionDuration = end - start;
            return executionDuration.TotalMilliseconds / 1000d;
        }

        private static void Output(double realTime, double userTime, double systemTime)
        {
            Console.WriteLine("Real time: {0} sec.", realTime);
            Console.WriteLine("User time: {0} sec.", userTime);
            Console.WriteLine("System time: {0} sec.", systemTime);
        }
    }
}
