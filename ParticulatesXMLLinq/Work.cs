using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticulatesXMLLinq
{
	public class Work
	{
		public ConfigRecord configRecord { get; }
		private ILocationFileReader IOhandler;

		public Work(ConfigRecord data, ILocationFileReader IOhandler)
		{
			this.configRecord = data;
			this.IOhandler = IOhandler;
		}

		public Location ReadData()
		{
			return IOhandler.ReadLocationFromFile(configRecord);
		}
	}
}
