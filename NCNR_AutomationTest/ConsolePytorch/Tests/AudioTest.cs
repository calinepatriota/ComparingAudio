using NCNR_AutomationTest.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCNR_AutomationTest.ConsolePytorch.Tests
{
    [TestFixture]
    class AudioTest
    {

        [Test]
        public void TestFilteringAudio()
        {
            var file = Util.FilePath("\\TestData\\126.txt");

            var one = Util.GetFFT(@"C:\Users\cbp\Downloads\TestingAudio.mp3");
            var two = Util.GetFFT(@"C:\Users\cbp\Downloads\TestAudio2.mp3");
            Assert.That(one, Is.Not.Empty);
            Assert.That(two, Is.Not.Empty);
            Assert.AreNotEqual(one, two);
        }
    }
}
