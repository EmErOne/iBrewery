using IBrewery.Client.Commands;
using IBrewery.Client.DataBase;
using IBrewery.Client.Models;
using System;
using System.Windows.Input;

namespace IBrewery.Client.ViewModels
{
    public class AddPumpInteralViewModel : ViewModelBase
    {
        public Action CloseAction;

        private PumpInterval pumpInterval = new PumpInterval();

        public PumpInterval PumpInterval
        {
            get { return pumpInterval; }
            set
            {
                pumpInterval = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand AbortCommand { get; }

        public AddPumpInteralViewModel()
        {
            SaveCommand = new RelayCommand(SaveExecute, SaveCanExecute);
            AbortCommand = new RelayCommand(AbortExecute);
        }

        private bool SaveCanExecute(object obj)
        {
            bool output = true;                      

            if(PumpInterval.RestTimeInSeconds < 0)
            {
                output = false;
            }
            if (PumpInterval.RestTimeInSeconds < 0)
            {
                output = false;
            }

            return output;
        }


        private void AbortExecute(object obj)
        {
            CloseAction();
        }

        private void SaveExecute(object obj)
        {
            using (BreweryContext db = new BreweryContext())
            {
                db.PumpIntervals.Add(PumpInterval);
                db.SaveChanges();
            }
            CloseAction();
        }
    }
}
