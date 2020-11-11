using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IBrewery.Client.Models
{
    public class BrewingPhase : BrewingBase
    {
        private readonly IArduinoConnection arduinoConnection;

        private Phase phase;
        public Phase Phase
        {
            get { return phase; }
            set { phase = value; 
                OnPropertyChanged(); }
        }

        private DateTime? begin;

        public DateTime? Begin
        {
            get { return begin; }
            set { begin = value; 
                OnPropertyChanged(); }
        }

        private DateTime? end;

        public DateTime? End
        {
            get { return end; }
            set
            {
                end = value;
                OnPropertyChanged();
            }
        }

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value;
                OnPropertyChanged();
            }
        }

        private bool isCompleted;

        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                isCompleted = value;
                OnPropertyChanged();
            }
        }

        private double progress;
        public double Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }

        public void Compute()
        {
            double output = 0.0;

            if (IsRunning)
            {
                if (Phase.Typ == PhaseTyp.KeepHeat)
                {
                    //Zeit läuft
                    if (Begin != null)
                    {
                        Trace.WriteLine("Compute KeepHeat");
                        double duration = (DateTime.Now - (DateTime)Begin).TotalMinutes;                       
                        output = (duration * 100.0) / Phase.PeriodOfMinutes;
                    }
                    else
                    {
                        output = 0.0;
                    }
                }
                else if (Phase.Typ == PhaseTyp.HeatUp)
                {
                    //Aufheizen
                    Trace.WriteLine("Compute KeepHeat");
                    int currentTemp = arduinoConnection.GetTemperature();

                    output = (currentTemp * 100.0) / Phase.TargetTemperature;

                }
                else if (Phase.Typ == PhaseTyp.HeatDown)
                {
                    //Abkühlen
                    Trace.WriteLine("Compute KeepHeat");
                    int currentTemp = arduinoConnection.GetTemperature();

                    output = (Phase.TargetTemperature * 100.0) / currentTemp;
                }
                else
                {
                    throw new Exception("Phase.Typ ist nicht bekannt.");
                }
            }
            else if (IsCompleted)
            {
                output = 100.0;
            }

            Trace.WriteLine("Compute out: " + output);
            Progress = output;
        }
               
        public BrewingPhase(Phase phase, IArduinoConnection arduinoConnection)
        {
            this.Phase = phase;
            this.arduinoConnection = arduinoConnection;
        }
    }
}
