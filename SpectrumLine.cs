using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aspecon
{
    class SpectrumLine
        /*
          this class holds a representation of a line from an Audacity plot spectrum file,
         containing a frequency and its amplitude
         */
    {
        public double Frequency
        {
            get;
            set;
        }
        public double Amplitude
        {
            get;
            set;
        }
    }
}
