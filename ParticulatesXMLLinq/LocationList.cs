using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticulatesXMLLinq
{
    public class LocationList
    {
        public List<Location> Locations { get; }

        public LocationList()
        {
            this.Locations = new List<Location>();
        }
        public List<String> CalculateLocationParticulates()
        {

            List<String> Ans = new List<String>();

            //Query for calculating the Particulates for a specific location
            var locPart =
                from location in this.Locations
                group location by location.Name into locations
                orderby locations.Key ascending
                let totalParticulates = (from parts in locations
                                         from reads in parts.Readings
                                         select (int)reads.Particulates).Sum()
                select new
                {
                    Name = locations.Key,
                    Particulates = totalParticulates
                };

            //Formatting the output so to use it directly in the listbox
            foreach (var row in locPart)
            {
                Ans.Add(String.Format("{0}: {1} Particulates", row.Name, row.Particulates));
            }

            return Ans;
        }
        public List<String> CalculateDateParticulates()
        {
            List<String> ans = new List<String>();

            //Query for calculating the Particulates for a specific date
            var datPart =
                from location in this.Locations
                from readings in location.Readings
                orderby readings.Date ascending
                group readings by readings.Date into readingdate
                let totalParticulates = (from read in readingdate
                                         select (int)read.Particulates).Sum()
                select new
                {
                    Date = readingdate.Key,
                    Particulates = totalParticulates
                };

            //Formatting the output
            foreach (var row in datPart)
            {
                ans.Add(String.Format("{0}: {1} Particulates", row.Date, row.Particulates));
            }

            return ans;
        }
        public List<String> CalculateLargestParticulates()
        {

            List<String> ans = new List<String>();

            //Query for the largest Particulates
            var Largest =
                from location in this.Locations
                from reading in location.Readings
                where reading.Particulates.Equals((from loc in this.Locations
                                                   from readings in loc.Readings
                                                   select (int)readings.Particulates).Max())
                select new
                {
                    Name = location,
                    Date = reading.Date,
                    Particulate = reading.Particulates
                };

            //Formatting the Output
            foreach (var row in Largest)
            {
                ans.Add(String.Format("{0}, Date: {1}, Particulates: {2}", row.Name, row.Date, row.Particulate));
            }
            return ans;
        }
        public List<String> GetLocation(String location)
        {
            List<String> ans = new List<String>();

            //Query for getting readings of a location
            var Getloc =
                from loc in this.Locations
                where loc.Name.Equals(location)
                select loc.Readings;


            foreach (var loc in Getloc)
            {
                foreach (var read in loc)
                {
                    ans.Add(read.ToString());
                }
            }
            return ans;
        }

        public List<String> FillComboBox()
        {
            List<String> ans = new List<String>();

            //Query for location names
            var combobox = from loc in this.Locations
                           select loc.Name;

            //Storing the names in a list
            foreach (var item in combobox)
            {
                ans.Add(item);
            }
            return ans;
        }
    }
}
