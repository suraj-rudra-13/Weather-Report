using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ParticulatesXMLLinq
{
    public partial class Form : System.Windows.Forms.Form
    {
        public ILocationFileReader IOhandler { get; }
        private ConfigData configData;
        private LocationList locationList;

        public Form(ILocationFileReader IOhandler)
        {
            InitializeComponent();
            this.IOhandler = IOhandler;
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            SetupConfigData();
            RunProducerConsumer();
            FillLocation();

            LoadBtn.Enabled = false;
            locationBtn.Enabled = true;
            DateBtn.Enabled = true;
            LargestParticulatesBtn.Enabled = true;
        }

        public void SetupConfigData()
        {
            configData = new ConfigData();
            //hardcoding the names of files
            configData.configRecords.Add(new ConfigRecord("Location-01.xml"));
            configData.configRecords.Add(new ConfigRecord("Location-02.xml"));
            configData.configRecords.Add(new ConfigRecord("Location-03.xml"));
            configData.configRecords.Add(new ConfigRecord("Location-04.xml"));
            configData.configRecords.Add(new ConfigRecord("Location-05.xml"));
            configData.configRecords.Add(new ConfigRecord("Location-06.xml"));
        }

        public void RunProducerConsumer()
        {
            //Create location list to hold individual Location objects read from XML files
            locationList = new LocationList();

            // Create progress manager with number of files to process
            var progManager = new ProgressManager(configData.configRecords.Count);

            // Create a PCQueue instance, give it a capacity of 3
            var pcQueue = new PCQueue(3);

            // Create 2 Producer instances and 2 Consumer instances, these will begin executing on
            // their respective threads as soon as they are instantiated
            Producer[] producers = { new Producer("P1", pcQueue, configData, IOhandler),
                                     new Producer("P2", pcQueue, configData, IOhandler) };

            Consumer[] consumers = { new Consumer("C1", pcQueue, locationList, progManager),
                                     new Consumer("C2", pcQueue, locationList, progManager) };

            // Keep producing and consuming until all work items are completed
            while (progManager.ItemsRemaining > 0) ;

            // Deactivate the PCQueue so it does not prevent waiting producer and/or consumer threads
            // from completing
            pcQueue.Active = false;

            // Iterate through consumers and signal them to finish
            foreach (var c in consumers)
            {
                c.Finished = true;
            }

            // Wait for all consumers to finish
            while (Consumer.RunningThreads > 0)
            {
                lock (pcQueue)
                {
                    // Pulse the PCQueue to signal any waiting threads
                    Monitor.Pulse(pcQueue);
                }
            }

            // Iterate through producers and signal them to finish
            foreach (var p in producers)
            {
                p.Finished = true;
            }

            // Wait for all producers to finish
            while (Producer.RunningThreads > 0)
            {
                lock (pcQueue)
                {
                    // Pulse the PCQueue to signal any waiting threads
                    Monitor.Pulse(pcQueue);
                }
            }

        }

        public void FillLocation()
        {
            //filling the combo boc by the locations
            foreach (var x in locationList.FillComboBox())
            {
                LocationcomboBox.Items.Add(x);
            }
        }

        private void locationBtn_Click(object sender, EventArgs e)
        {
            //clearing the listbox
            listBox.Items.Clear();

            //adding items of the method location particulates in the listbox
            foreach (var loc in locationList.CalculateLocationParticulates())
            {
                listBox.Items.Add(loc);
            }
        }


        private void DateBtn_Click(object sender, EventArgs e)
        {
            //clearing the listbox
            listBox.Items.Clear();

            //adding items of the method date particulates in the listbox
            var N = locationList.CalculateDateParticulates();
            foreach (var x in N)
            {
                listBox.Items.Add(x);
            }
        }

        private void LargestParticulatesBtn_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            List<String> S = locationList.CalculateLargestParticulates();//method for the largest particulates

            foreach (String s in S)
            {
                listBox.Items.Add(s);
            }
        }

        private void LocationcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String loc = LocationcomboBox.SelectedItem.ToString();
            listBox.Items.Clear();

            //the query for displaying the readings of a particular location
            listBox.Items.Add(loc + ":");
            foreach (var x in locationList.GetLocation(loc))
            {
                listBox.Items.Add(x);
            }
        }
    }
}
