using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticulatesXMLLinq
{
    public class ProgressManager
    {
        private int itemsRemaining;
        public int ItemsRemaining // Property getter/setter methods
        {
            get
            {
                lock (this)
                {
                    return itemsRemaining;
                }
            }

            set
            {
                lock (this)
                {
                    // MUTEX control for potential multiple thread access to this property
                    itemsRemaining = value;
                }
            }
        }

        public ProgressManager()
        {
            this.ItemsRemaining = 0;
        }

        public ProgressManager(int itemsRemaining)
        {
            this.ItemsRemaining = itemsRemaining;
        }
    }
}
