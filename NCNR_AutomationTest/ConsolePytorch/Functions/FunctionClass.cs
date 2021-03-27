using System;

namespace NCNR_AutomationTest.ConsolePytorch.Functions
{
    class FunctionClass
    {
        #region Public Methods
        public static Double[] GetFFT(string file)
        {
            var test = Prepare(file, out int SampleRate);
            return test;

        }

        /* The method receives the file path and the sample 
        rate is calculated internally and passed through the output parameter*/
        public static Double[] Prepare(String wavePath, out int SampleRate)

        {
            Double[] data;
            byte[] wave;
            byte[] sR = new byte[4];
            System.IO.FileStream WaveFile = System.IO.File.OpenRead(wavePath);
            wave = new byte[WaveFile.Length];
            data = new Double[(wave.Length - 44) / 4];//shifting the headers out of the PCM (Pulse code modulation) data;
            WaveFile.Read(wave, 0, Convert.ToInt32(WaveFile.Length));//read the wave file into the wave variable
            /***********Converting and PCM accounting***************/
            for (int i = 0; i < data.Length - i * 4; i++)
            {
                data[i] = (BitConverter.ToInt32(wave, (1 + i) * 4)) / 65536.0;
                //65536.0.0=2^n,       n=bits per sample;
            }
            /**************assigning sample rate**********************/
            for (int i = 24; i < 28; i++)
            {
                sR[i - 24] = wave[i];
            }
            SampleRate = BitConverter.ToInt32(sR, 0);
            return data;
        }
    }
    #endregion
}
