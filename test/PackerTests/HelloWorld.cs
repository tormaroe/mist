using NUnit.Framework;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace test.PackerTests
{
    [TestFixture]
    public class HelloWorld
    {
        /// <summary>
        /// Compilation and execution of mist program is done
        /// with this folder as current folder.
        /// It's created in Setup and completely deleted in Teardown.
        /// </summary>
        const string testfolder = "testfolder";

        [Test]
        public void Test()
        {
            CompileAndValidateMistProgram();
            RunAndValidateMistProgram();
        }

        private static void CompileAndValidateMistProgram()
        {
            Process p = Process.Start(new ProcessStartInfo("mistpacker.exe",
            "-s:..\\PackerTests\\hello-world.mist -o:out.exe")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
            });

            p.WaitForExit(2000);
            p.ExitCode.ShouldEqual(0);
            File.Exists("out.exe").ShouldBeTrue();
        }

        private static void RunAndValidateMistProgram()
        {
            Process p2 = Process.Start(new ProcessStartInfo("out.exe")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
            });

            p2.WaitForExit(2000);
            p2.ExitCode.ShouldEqual(0);
            p2.StandardOutput.ReadToEnd().ShouldEqual("hello, world");
        }

        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(testfolder);
            File.Copy("Marosoft.Mist.dll", Path.Combine(testfolder, "Marosoft.Mist.dll"));
            File.Copy("mistpacker.exe", Path.Combine(testfolder, "mistpacker.exe"));

            Directory.SetCurrentDirectory(testfolder);
        }

        [TearDown]
        public void Teardown()
        {
            Directory.SetCurrentDirectory("..");

            File.Delete(Path.Combine(testfolder, "Marosoft.Mist.dll"));
            File.Delete(Path.Combine(testfolder, "mistpacker.exe"));
            File.Delete(Path.Combine(testfolder, "out.exe"));
            Thread.Sleep(500);
            Directory.Delete(testfolder);
        }

    }
}
