using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticulatesXMLLinq
{
    public class Reading
    {
        public String Date { get; }
        public int Particulates { get; }
        public double Temperature { get; }
        public double Humidity { get; }

        //parameterised constructor for the Reading class
        public Reading(String pdate, int particulates, double temperature, double humidity)
        {
            Date = pdate;
            Particulates = particulates;
            Temperature = temperature;
            Humidity = humidity;
        }
        public override String ToString()
        {
            return string.Format("{0}: {1}, {2}, {3}", this.Date, this.Particulates, this.Temperature, this.Humidity);
        }
    }
}
