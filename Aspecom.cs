using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;


namespace aspecon
{
    class Aspecom
    {
        public SpectrumLine[] firstSpectrum, secondSpectrum, resultSpectrum;
        RangeComparison[] rangeComparison;


        public void ReadFirstSpectrum(string path)
        {
            ReadSpectrum(path, ref firstSpectrum);
        }
        public void ReadSecondSpectrum(string path)
        {
            ReadSpectrum(path, ref secondSpectrum);
        }
        public void ReadSpectrum(string path, ref SpectrumLine[] spectrumLines)
        {
            int lineCount = File.ReadAllLines(path).Count();
            StreamReader sr = new StreamReader(path);
            spectrumLines = new SpectrumLine[lineCount];
            SpectrumLine sl = new SpectrumLine();
            
                        string line;
            string[] lineData = new string[2];
            sr.ReadLine();//skip the first line containing the header
            
            //CultureInfo ci = new CultureInfo("en-us");



            for (int i = 0; i < lineCount - 1; i++)
            {
                line = sr.ReadLine();

                lineData = line.Split('\t');
                //lineData[0] = lineData[0].Substring(0, lineData[0].Length - 4);
                //lineData[ 1] = lineData[1].Substring(0, lineData[1].Length - 4);

                spectrumLines[i] = new SpectrumLine();
                spectrumLines[i].Frequency = double.Parse(NormalizeAudacityFloat(lineData[0]));
                spectrumLines[i].Amplitude = double.Parse(NormalizeAudacityFloat(lineData[1]));
            }



        }
        private string NormalizeAudacityFloat(string number)//ugliest hack ever
        {
            if (number != "")
            {
                string[] parts = number.Split('.');
                return parts[0] + "." + parts[1];
            }
            else
                return "";
        }
        public void CompareSpectra()
        {
            if (firstSpectrum != null && secondSpectrum != null)
            {
                resultSpectrum = new SpectrumLine[firstSpectrum.Length - 1];
                for (int i = 0; i < resultSpectrum.Length; i++)
                {
                    resultSpectrum[i] = new SpectrumLine();
                    resultSpectrum[i].Frequency = firstSpectrum[i].Frequency;
                    if(firstSpectrum[i].Amplitude<0&&secondSpectrum[i].Amplitude<0)
                        resultSpectrum[i].Amplitude = Math.Abs(firstSpectrum[i].Amplitude - secondSpectrum[i].Amplitude);
                    else
                    resultSpectrum[i].Amplitude = firstSpectrum[i].Amplitude - secondSpectrum[i].Amplitude;
                }

            }
            else
                throw new NullReferenceException("can't compare null spectra");
            //CompareRanges(16);

        }
        public void CompareRanges(int range)
        {
            rangeComparison = new RangeComparison[range];
            int rangeLength = resultSpectrum.Length / range;
            for (int i = 0; i < range; i++)
            {
                rangeComparison[i] = new RangeComparison();
                rangeComparison[i].StartFreq = resultSpectrum[i * rangeLength].Frequency;
                if ((i + 1) * rangeLength > resultSpectrum.Length)
                    rangeLength = resultSpectrum.Length - ((i + 1) * rangeLength);
                rangeComparison[i].EndFreq = resultSpectrum[(i + 1) * rangeLength].Frequency;
                for (int j = 0; j < rangeLength; j++)
                {
 
                    rangeComparison[i].Difference += Math.Abs(resultSpectrum[i * rangeLength+j].Amplitude);
                }
            }
        }
    }
}