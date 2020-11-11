using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace IBrewery.Client.Models
{
    public class BrewingData : BrewingBase
    {
        private int temperature;
        public int Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChanged();
            }
        }

        private bool pumpState;

        public bool PumpStateOn
        {
            get { return pumpState; }
            set
            {
                pumpState = value;
                OnPropertyChanged();
            }
        }

        private bool heaterState;
        public bool HeaterStateOn
        {
            get { return heaterState; }
            set
            {
                heaterState = value;
                OnPropertyChanged();
            }
        }


        private DateTime? start;

        public DateTime? Start
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }



        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged();
            }
        }

    }
}
