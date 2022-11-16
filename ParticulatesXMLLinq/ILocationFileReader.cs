using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticulatesXMLLinq
{
    public interface ILocationFileReader
    {
        Location ReadLocationFromFile(ConfigRecord configRecord);
    }
}
