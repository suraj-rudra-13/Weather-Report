using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ParticulatesXMLLinq
{
	public class Producer
	{
		private static int runningThreads = 0; // Class wide count of producer threads that are running
		private static object locker = new object(); // Used for MUTEX control of static properties

		private string id; // Identifier for this producer
		public Thread T { get; } // Thread upon which this instance of a producer runs
		private bool finished; // Flag to control if this producer has finished or if it should continue to produce
		private PCQueue pcQueue; // Shared PCQueue that this producer is producing work items for

		private int duration = 100000000; // Increase or decrease this value to slow down or speed up the producer

		private ConfigData configFile; // Configuration data (details of datasets to be processed)
		private ILocationFileReader IOhandler; // File handler for reading data

		public static int RunningThreads // Property getter/setter methods
		{
			get
			{
				lock (locker)
				{
					return runningThreads;
				}
			}

			private set
			{
				lock (locker)
				{
					// MUTEX control for potential multiple thread access to this property
					runningThreads = value;
				}
			}
		}

		public bool Finished // Property getter/setter methods
		{
			get
			{
				lock (locker)
				{
					return finished;
				}
			}

			set
			{
				lock (locker)
				{
					// MUTEX control for potential multiple thread access to this property
					finished = value;
				}
			}
		}

		public Producer(string id, PCQueue pcQueue, ConfigData configFile, ILocationFileReader IOhandler)
		{
			this.id = id;
			finished = false; // Initially not finished
			this.pcQueue = pcQueue;
			this.configFile = configFile;
			this.IOhandler = IOhandler;

			T = new Thread(run); // Create a new thread for this producer
			T.Start(); // Get it started

			RunningThreads++; // Increment the number of running producer threads;
		}

		// Thread code for the producer
		public void run()
		{
			ConfigRecord configRecord = null;

			// While not finished, generate a new work item and enqueue it on the PCQueue
			while (!Finished)
			{
				// Lock configuration file and obtain next filename to process
				// If there are no filenames left then set filename to null so that nothing is produced
				lock (configFile)
				{
					if (configFile.NextRecord < configFile.configRecords.Count)
					{
						configRecord = configFile.configRecords[configFile.NextRecord++];
					}
					else
					{
						configRecord = null;
					}
				}

				// only queue item if there is a config record to read
				if (configRecord != null)
				{
					// Enqueue a new work item
					pcQueue.enqueueItem(new Work(configRecord, IOhandler));

					// Output a message to state that this producer has produced a work item
					Console.WriteLine("Producer:{0} has created and enqueued Work Item:{1}", id, configRecord.ToString());
				}

				// Simulate long running producer activity
				for (int i = 0; i < duration; i++)
				{
					Math.Sqrt(i);
				}
			}

			// Decrement the number of running producer threads
			RunningThreads--;

			// Output that this producer has finished
			Console.WriteLine("Producer:{0} has finished", id);
		}
	}
}
