using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ParticulatesXMLLinq
{
	class Consumer
	{
		private static int runningThreads = 0; // Class wide count of consumer threads that are running
		private static object locker = new object(); // Used for MUTEX control of static properties

		private string id; // Identifier for this consumer
		public Thread T { get; } // Thread upon which this instance of a consumer runs
		private bool finished; // Flag to control if this consumer has finished or if it should continue to consume
		private PCQueue pcQueue; // Shared PCQueue that this consumer is consuming work items from

		private int duration = 100000000; // Increase or decrease this value to slow down or speed up the consumer

		private LocationList locationList; // List of candidates which will be added to

		private ProgressManager progManager;

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
					// MUTEX control for potential multiple thread access to the finished flag
					finished = value;
				}
			}
		}

		public Consumer(string id, PCQueue pcQueue, LocationList locationList, ProgressManager progManager)
		{
			this.id = id;
			finished = false; // Initially not finished
			this.pcQueue = pcQueue;
			this.locationList = locationList;
			this.progManager = progManager;

			T = new Thread(run); // Create a new thread for this consumer
			T.Start(); // Get it started

			RunningThreads++; // Increment the number of running consumer threads;
		}

		// Thread code for the consumer
		public void run()
		{
			// While not finished, dequeue a work item from the PCQueue and consume this work item by invoking
			// its ReadData() method
			while (!Finished)
			{
				// Dequeue a work item
				var item = pcQueue.dequeueItem();

				if (!ReferenceEquals(null, item))
				{
					// Invoke the work item's ReadData() method, which returns a constituency
					Location location = item.ReadData();

					// Ensure null returns are ignored (will happen if data not in correct format or cannot open file)
					if (!ReferenceEquals(null, location))
					{
						// Add this constituency to the constituency list (lock it while modifying) 
						lock (locationList)
						{
							locationList.Locations.Add(location);
						}

						// Output to the console
						Console.WriteLine("Consumer:{0} has consumed Work Item:{1}", id, item.configRecord.ToString());
					}
					else
					{
						// Output to the console
						Console.WriteLine("Consumer:{0} has rejected Work Item:{1}", id, item.configRecord.ToString());
					}

					// Simulate long running consumer activity
					for (int i = 0; i < duration; i++)
					{
						Math.Sqrt(i);
					}

					// Update Progress Manager to indicate that one more piece of data has been consumed
					progManager.ItemsRemaining--;
				}
			}

			// Decrement the number of running consumer threads
			RunningThreads--;

			// Output that this consumer has finished
			Console.WriteLine("Consumer:{0} has finished", id);
		}
	}
}
