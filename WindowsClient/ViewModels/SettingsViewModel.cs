using IBrewery.Client.Commands;
using IBrewery.Client.DataBase;
using IBrewery.Client.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace IBrewery.Client.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region All Lists Defs
        private ObservableCollection<FoundationCriteria> foundationCriterias;
        public ObservableCollection<FoundationCriteria> FoundationCriterias
        {
            get { return foundationCriterias; }
            set
            {
                if (value != foundationCriterias)
                {
                    foundationCriterias = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<FoundationIngrediet> foundationIngrediets;
        public ObservableCollection<FoundationIngrediet> FoundationIngrediets
        {
            get { return foundationIngrediets; }
            set
            {
                if (value != foundationIngrediets)
                {
                    foundationIngrediets = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<PumpInterval> pumpIntervals;
        public ObservableCollection<PumpInterval> PumpIntervals
        {
            get { return pumpIntervals; }
            set
            {
                if (value != pumpIntervals)
                {
                    pumpIntervals = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<EMailAddress> emailAdresses;
        public ObservableCollection<EMailAddress> EmailAddresses
        {
            get { return emailAdresses; }
            set
            {
                if (value != emailAdresses)
                {
                    emailAdresses = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Version
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            }
        }
        #endregion

        #region Commands Defs
        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }
        #endregion

        public SettingsViewModel()
        {
            SaveCommand = new RelayCommand(SaveExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);

            using var db = new BreweryContext();
            FoundationIngrediets = new ObservableCollection<FoundationIngrediet>(db.FoundationIngrediets.ToList());
            FoundationCriterias = new ObservableCollection<FoundationCriteria>(db.FoundationCriterias.ToList());
            PumpIntervals = new ObservableCollection<PumpInterval>(db.PumpIntervals.ToList());
            EmailAddresses = new ObservableCollection<EMailAddress>(db.EMailAddresses.ToList());
        }

        private void DeleteExecute(object obj)
        {
            if(obj is PumpInterval pumpInterval)
            {
                using var db = new BreweryContext();
                var item = db.PumpIntervals.FirstOrDefault(p => p.ID == pumpInterval.ID);
                if (item != null)
                {
                    db.PumpIntervals.Remove(item);
                    db.SaveChanges();
                    PumpIntervals.Remove(pumpInterval);
                }
            }
            else if (obj is EMailAddress eMailAddress)
            {
                using var db = new BreweryContext();
                var item = db.EMailAddresses.FirstOrDefault(p => p.ID == eMailAddress.ID);
                if (item != null)
                {
                    db.EMailAddresses.Remove(item);
                    db.SaveChanges();
                    EmailAddresses.Remove(eMailAddress);
                }
            }
            else if (obj is FoundationCriteria foundationCriteria)
            {
                using var db = new BreweryContext();
                var item = db.FoundationCriterias.FirstOrDefault(p => p.ID == foundationCriteria.ID);
                if (item != null)
                {
                    db.FoundationCriterias.Remove(item);
                    db.SaveChanges();
                    FoundationCriterias.Remove(foundationCriteria);
                }
            }
            else if (obj is FoundationIngrediet foundationIngrediets)
            {
                using var db = new BreweryContext();
                var item = db.FoundationIngrediets.FirstOrDefault(p => p.ID == foundationIngrediets.ID);
                if (item != null)
                {
                    db.FoundationIngrediets.Remove(item);
                    db.SaveChanges();
                    FoundationIngrediets.Remove(foundationIngrediets);
                }
            }
        }

        private void SaveExecute(object obj)
        {
            using var db = new BreweryContext();
            // Save FoundationIngrediets
            foreach (var foundationIngrediet in FoundationIngrediets)
            {
                if (foundationIngrediet.ID == 0)
                {
                    db.FoundationIngrediets.Add(foundationIngrediet);
                    db.SaveChanges();
                }
                else
                {
                    var dbPfoundation = db.FoundationIngrediets.FirstOrDefault(e => e.ID == foundationIngrediet.ID);
                    if (dbPfoundation != null)
                    {
                        dbPfoundation.Amount = foundationIngrediet.Amount;
                        dbPfoundation.Name = foundationIngrediet.Name;
                        dbPfoundation.Typ = foundationIngrediet.Typ;
                        db.SaveChanges();
                    }
                }
            }

            // Save FoundationCriterias
            foreach (var foundationCriteria in FoundationCriterias)
            {
                if (foundationCriteria.ID == 0)
                {
                    db.FoundationCriterias.Add(foundationCriteria);
                    db.SaveChanges();
                }
                else
                {
                    var dbPfoundation = db.FoundationCriterias.FirstOrDefault(e => e.ID == foundationCriteria.ID);
                    if (dbPfoundation?.Description != foundationCriteria.Description)
                    {
                        dbPfoundation.Description = foundationCriteria.Description;
                        db.SaveChanges();
                    }
                }
            }

            // Save PumpIntervals
            foreach (var interval in PumpIntervals)
            {
                if (interval.ID == 0)
                {
                    db.PumpIntervals.Add(interval);
                    db.SaveChanges();
                }
                else
                {
                    var dbPumpInterval = db.PumpIntervals.FirstOrDefault(e => e.ID == interval.ID);
                    if (dbPumpInterval != null)
                    {
                        dbPumpInterval.Name = interval.Name;
                        dbPumpInterval.RestTimeInSeconds = interval.RestTimeInSeconds;
                        dbPumpInterval.RunningTimeInSeconds = interval.RunningTimeInSeconds;
                        db.SaveChanges();
                    }
                }
            }

            // Save EmailAddresses
            foreach (var address in EmailAddresses)
            {
                if (address.ID == 0)
                {
                    db.EMailAddresses.Add(address);
                    db.SaveChanges();
                }
                else
                {
                    var dbEmail = db.EMailAddresses.FirstOrDefault(e => e.ID == address.ID);

                    if (dbEmail?.Address != address.Address)
                    {
                        dbEmail.Address = address.Address;
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
