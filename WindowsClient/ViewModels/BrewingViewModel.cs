using IBrewery.Client.Commands;
using IBrewery.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Linq;
using IBrewery.Client.DataBase;

namespace IBrewery.Client.ViewModels
{
    public class BrewingViewModel : ViewModelBase, IDisposable
    {

        private readonly IArduinoConnection arduinoConnection = new ArduinoConnection();

        private Recipe recipe;
        public Recipe Recipe
        {
            get { return recipe; }
            set
            {
                recipe = value;
                OnPropertyChanged();
            }
        }

        private BrewingData brewingData;

        public BrewingData BrewingData
        {
            get { return brewingData; }
            set
            {
                brewingData = value;
                OnPropertyChanged();
            }
        }

        private List<BrewingPhase> brewingPhases = new List<BrewingPhase>();

        public List<BrewingPhase> BrewingPhases
        {
            get { return brewingPhases; }
            set
            {
                brewingPhases = value;
                OnPropertyChanged();
            }
        }

        private BrewingPhase currentBrewingPhase;
        private int currentBrewingPhaseIndex = 0;

        public ICommand StartBrewingCommand { get; }
        public ICommand PauseBrewingCommand { get; }
        public ICommand AbortBrewingCommand { get; }
        public ICommand StartNextPhaseCommand { get; }

        private readonly Timer MainTimer;
        private readonly int timerDueTime = 5000;


        public BrewingViewModel(Recipe recipe)
        {
            StartBrewingCommand = new RelayCommand(StartExecute, StartCanExecute);
            PauseBrewingCommand = new RelayCommand(PauseExecute, PauseCanExecute);
            AbortBrewingCommand = new RelayCommand(AbortExecute, AbortCanExecute);
            StartNextPhaseCommand = new RelayCommand(StartNextExecute, StartNextCanExecute);

            MainTimer = new Timer(TimerCallback, null, 0, timerDueTime);

            Recipe = recipe;

            BrewingData = new BrewingData
            {
                HeaterStateOn = false,
                PumpStateOn = false,
                Temperature = 20
            };

            foreach (var phase in Recipe.Phases)
            {
                BrewingPhases.Add(new BrewingPhase(phase, arduinoConnection));
            }

        }



        public BrewingViewModel()
        {
            List<PumpInterval> pumpIntervals = new List<PumpInterval>();
            List<Ingredient> ingredients = new List<Ingredient>();
            List<Phase> phases = new List<Phase>();

            StartBrewingCommand = new RelayCommand(StartExecute, StartCanExecute);
            PauseBrewingCommand = new RelayCommand(PauseExecute, PauseCanExecute);
            AbortBrewingCommand = new RelayCommand(AbortExecute, AbortCanExecute);
            StartNextPhaseCommand = new RelayCommand(StartNextExecute, StartNextCanExecute);

            MainTimer = new Timer(TimerCallback, null, 0, timerDueTime);


            pumpIntervals.Add(new PumpInterval { Name = "AUS", RestTimeInSeconds = 0, RunningTimeInSeconds = 0 });
            pumpIntervals.Add(new PumpInterval { Name = "Langsam", RestTimeInSeconds = 60, RunningTimeInSeconds = 10 });
            pumpIntervals.Add(new PumpInterval { Name = "Schnell", RestTimeInSeconds = 120, RunningTimeInSeconds = 20 });

            ingredients.Add(new Ingredient { Name = "Gerste", Amount = 10, Typ = IngredientTyp.Kilogram });
            ingredients.Add(new Ingredient { Name = "Hopfen", Amount = 0.7, Typ = IngredientTyp.Kilogram });
            ingredients.Add(new Ingredient { Name = "Wasser", Amount = 86, Typ = IngredientTyp.Liter });

            phases.Add(new Phase { Position = 0, Name = "Aufwärmen", PeriodOfMinutes = 1, TargetTemperature = 50, Typ = PhaseTyp.HeatUp, PumpInterval = pumpIntervals[0] });
            phases.Add(new Phase { Position = 1, Name = "Halten", PeriodOfMinutes = 1, TargetTemperature = 50, Typ = PhaseTyp.KeepHeat, PumpInterval = pumpIntervals[2] });
            phases.Add(new Phase { Position = 2, Name = "Aufwärmen 2", PeriodOfMinutes = 1, TargetTemperature = 70, Typ = PhaseTyp.KeepHeat, PumpInterval = pumpIntervals[2] });
            phases.Add(new Phase { Position = 3, Name = "Rast 2", PeriodOfMinutes = 1, TargetTemperature = 70, Typ = PhaseTyp.KeepHeat, PumpInterval = pumpIntervals[2] });

            foreach (var phase in phases)
            {
                BrewingPhases.Add(new BrewingPhase(phase, arduinoConnection));
            }

            Recipe = new Recipe()
            {
                ID = 0,
                Description = "Das ist ein seher seher leckeres Bier welches hier gebraut werden soll.",
                Name = "Keller Pilz",
                Ingredients = ingredients,
                Phases = phases,
                IsFavorite = false
            };

            BrewingData = new BrewingData
            {
                HeaterStateOn = true,
                PumpStateOn = true,
                Temperature = 85
            };
        }

        /// <summary>
        /// Immer wenn X Sekunden verget wird Process() gestarted.
        /// </summary>
        /// <param name="o"></param>
        private void TimerCallback(Object o)
        {
            Trace.WriteLine(DateTime.Now.ToLongTimeString() + " Process()");
            Process();
        }

        private void Process()
        {
            if (BrewingData.IsRunning && currentBrewingPhase != null)
            {
                switch (currentBrewingPhase.Phase.Typ)
                {
                    case PhaseTyp.HeatUp:
                        ProcessHeatUp();
                        break;
                    case PhaseTyp.HeatDown:
                        ProcessHeatDown();
                        break;
                    case PhaseTyp.KeepHeat:
                        ProcessKeepHeat();
                        break;
                    default: throw new Exception("Phase Typ ist nicht bekannt!");
                }
            }
        }

        private void ProcessHeatDown()
        {
            Trace.WriteLine(DateTime.Now.ToLongTimeString() + " ProcessHeatDown()");
            int temp = arduinoConnection.GetTemperature();
            if (temp <= currentBrewingPhase.Phase.TargetTemperature)
            {
                StartNextPhase();
            }
        }

        private void ProcessHeatUp()
        {
            Trace.WriteLine(DateTime.Now.ToLongTimeString() + " ProcessHeatUp()");
            int temp = arduinoConnection.GetTemperature();
            if (temp >= currentBrewingPhase.Phase.TargetTemperature)
            {
                StartNextPhase();
            }
        }

        private void ProcessKeepHeat()
        {
            Trace.WriteLine(DateTime.Now.ToLongTimeString() + " ProcessKeepHeat()");
            TimeSpan timeSpan = DateTime.Now - (DateTime)currentBrewingPhase.Begin;
            if (timeSpan.TotalMinutes >= currentBrewingPhase.Phase.PeriodOfMinutes)
            {
                StartNextPhase();
            }
            else
            {
                currentBrewingPhase.Compute();
            }
        }

        private void StartNextPhase()
        {
            Trace.WriteLine(DateTime.Now.ToLongTimeString() + " StartNextPhase()");

            currentBrewingPhase.IsCompleted = true;
            currentBrewingPhase.IsRunning = false;
            currentBrewingPhase.Progress = 100;
            currentBrewingPhase.End = DateTime.Now;

            currentBrewingPhaseIndex += 1;

            if (BrewingPhases.Count > currentBrewingPhaseIndex)
            {
                currentBrewingPhase = BrewingPhases[currentBrewingPhaseIndex];
                currentBrewingPhase.Begin = DateTime.Now;
                currentBrewingPhase.IsRunning = true;
            }
            else
            {
                StopProcess();
            }
        }

        private void StopProcess()
        {
            BrewingData.IsRunning = false;
            arduinoConnection.TurnHeaterOff();
            arduinoConnection.TurnPumpOff();

            bool isCompeted = true;

            foreach(var phase in BrewingPhases)
            {
                if(phase.IsCompleted == false)
                {
                    isCompeted = false;
                    break;
                }
            }

            BrewProcess brewProcess = new BrewProcess
            {
                IsCompleted = isCompeted,
                Recipe = Recipe,
                RecipeID = Recipe.ID,
                Time = DateTime.Now
            };

            using BreweryContext db = new BreweryContext();
            db.BrewProcesses.Add(brewProcess);
            db.SaveChanges();
        }

        private bool StartNextCanExecute(object obj)
        {
            if (BrewingData.IsRunning && BrewingPhases?.Count > currentBrewingPhaseIndex)
            {
                return true;
            }

            return false;
        }

        private void StartNextExecute(object obj)
        {
            StartNextPhase();
        }

        private bool AbortCanExecute(object obj)
        {
            return BrewingData.IsRunning;
        }

        private void AbortExecute(object obj)
        {
            StopProcess();
        }

        private bool PauseCanExecute(object obj)
        {
            return BrewingData.IsRunning;
        }

        private void PauseExecute(object obj)
        {
            BrewingData.IsRunning = false;
        }

        private bool StartCanExecute(object obj)
        {
            return BrewingData.IsRunning == false && BrewingPhases.Count > 0;
        }

        private void StartExecute(object obj)
        {
            if (BrewingData.Start == null)
            {
                BrewingData.Start = DateTime.Now;
                currentBrewingPhase = BrewingPhases[currentBrewingPhaseIndex];
                currentBrewingPhase.Begin = DateTime.Now;
                currentBrewingPhase.IsRunning = true;
            }
            else
            {
                foreach(var phase in BrewingPhases)
                {
                    phase.IsCompleted = false;
                    phase.Progress = 0.0;
                    phase.Begin = null;
                    phase.End = null;
                }
            }

            BrewingData.IsRunning = true;
        }

        public void Dispose()
        {
            MainTimer.Dispose();
        }
    }
}
