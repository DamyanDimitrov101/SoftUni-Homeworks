using Chronometer.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private Stopwatch sw;
        private bool isRunning;
        private IList<string> laps;

        public Chronometer()
        {
            this.sw = new Stopwatch();
        }

        public string GetTime => String.Format("{0:mm\\:ss\\.fff}", this.sw.Elapsed);

        public List<string> Laps => this.laps.ToList();

        public string Lap()
        {
            string lap = this.GetTime;
            this.laps.Add(lap);
            return lap;
        }

        public void Reset()
        {
            this.Stop();
            this.sw.Reset();
            this.laps = new List<string>();
        }

        public void Start()
        {
            this.sw.Start();
            this.laps = new List<string>();
            this.isRunning = true;
        }

        public void Stop()
        {
            this.sw.Stop();
            this.isRunning = false;
        }
    }
}
