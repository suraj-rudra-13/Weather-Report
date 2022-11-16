using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticulatesXMLLinq
{
    public class Location
    {
        public String Name { get; }
        public List<Reading> Readings { get; }

        public Location(String name)
        {
            this.Name = name;
            this.Readings = new List<Reading>();
        }

        public override String ToString()
        {
            return String.Format("Location: {0}", this.Name);
        }
    }
}
