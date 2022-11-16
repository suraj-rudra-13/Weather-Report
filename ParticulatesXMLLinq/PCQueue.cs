using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ParticulatesXMLLinq
{
	public class PCQueue
	{
		private Queue<Work> queue = new Queue<Work>(); // Embedded queue collection to hold work items
		public int Capacity { get; } // Maximum number of work items allowed on the queue
		public bool Active { get; set; } // Only allows enqueue and dequeue of work items when active

		public PCQueue()
		{
			// Defaults to an unbounded queue size, so capacity = 0
			Capacity = 0;
			Active = true;
		}

		public PCQueue(int capacity)
		{
			// Sets a bounded queue size = capacity, or an unbounded queue size if capacity is passed in as < 1
			this.Capacity = Math.Max(capacity, 0); // ie. cannot be a negative capacity
			Active = true;
		}

		public void enqueueItem(Work item)
		{
			// Use this instance of the PCQueue as the locking object for the concurrency related critical regions
			// and thread synchronisation
			lock (this)
			{
				// While this PCQueue is active and full, wait (remember a capacity = 0 means never full)
				while (Active && (Capacity != 0) && (queue.Count == Capacity))
				{
					Monitor.Wait(this);
				}

				// If this PCQueue is active it now has space for a work item so enqueue it 
				if (Active)
				{
					queue.Enqueue(item);

					// Use pulse to inform that the queue is now not empty
					Monitor.Pulse(this);
				}
			}
		}

		public Work dequeueItem()
		{
			Work item = null;

			// Use this instance of the PCQueue as the locking object for the concurrency related critical regions
			// and thread synchronisation
			lock (this)
			{
				// While this PCQueue is active and empty, wait
				while (Active && (queue.Count == 0))
				{
					Monitor.Wait(this);
				}

				// If this PCQueue is active it now has a work item so dequeue a work item and return its reference
				// or return null if the PCQueue is not active
				if (Active)
				{
					item = queue.Dequeue();

					// Use pulse to inform that the queue is now not full
					Monitor.Pulse(this);
				}
			}

			return item;
		}
	}
}
