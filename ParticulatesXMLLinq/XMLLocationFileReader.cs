using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Text;

namespace ParticulatesXMLLinq
{
    public class XMLLocationFileReader : ILocationFileReader
    {
        public Location ReadLocationFromFile(ConfigRecord configRecord)
        {
            // Open the file to read from on the local file system, if this file is missing then return
            // immediately from this method
            if (!File.Exists(configRecord.Filename))
            {
                // Cannot open the file as it does not exist for whatever reason, so return immediately.
                return null;
            }

            // Open file and load into memory as XML
            XDocument xmlDoc = XDocument.Load(configRecord.Filename);

            // Create location (should only be one in file but retrieve first to be sure)
            var loc = (from l in xmlDoc.Descendants("Location")
                       select l).First();

            Location location = new Location(loc.Attribute("name").Value);

            // Obtain readings from this location
            var read = from c in loc.Descendants("Reading")
                       select c;

            foreach (var c in read)
            {
                var date = c.Attribute("date").Value;
                var value = Int32.Parse(c.Element("value").Value);
                var temperature = Double.Parse(c.Element("temperature").Value);
                var humidity = Double.Parse(c.Element("humidity").Value);

                Reading reading = new Reading(date, value, temperature, humidity);
                location.Readings.Add(reading);
            }

            return location;
        }
    }
}
