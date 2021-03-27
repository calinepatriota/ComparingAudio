using NCNR_AutomationTest.ConsolePytorch.Functions;
using NCNR_AutomationTest.Utils;
using NUnit.Framework;

namespace NCNR_AutomationTest.ConsolePytorch.Tests
{
    [TestFixture]
    class AudioTests
    {


        [TestCase("noisy1_stereo")]
        [TestCase("noisy1_stereo_2")]
        [Test]
        public void TestFilteringAudio(string audio)
        {
            var testName = TestContext.CurrentContext.Test.MethodName;

            Util.ReadConsoleOutput(Constants.path, "ConsoleApp.exe" + " " +"./"+audio +".wav"+ " " +"./"+audio +"Denoised.wav", audio);
            var one = FunctionClass.GetFFT(Constants.path + audio+".wav");
            var two = FunctionClass.GetFFT(Constants.path + audio+"Denoised.wav");

            Assert.That(one, Is.Not.Empty);
            Assert.That(two, Is.Not.Empty);
            Assert.AreNotEqual(one, two);

        }
    }
}
