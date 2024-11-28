using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Utils.Profiling
{
    public class TimeProfiler
    {
        private DateTime StartTime;
        private DateTime PrevStartTime;
        private DateTime EndTime;
        private bool running;

        public TimeProfiler()
        {
            StartTime = DateTime.Now;
            PrevStartTime = DateTime.Now;
            EndTime = DateTime.Now;
            running = false;
        }

        public void Start()
        {
            StartTime = DateTime.Now;
            running = true;
        }

        public void Stop()
        {
            EndTime = DateTime.Now;
            PrevStartTime = StartTime;
            running = false;
        }

        public void Lap()
        {
            StartTime = EndTime;
            PrevStartTime = StartTime;
            EndTime = DateTime.Now;
            running = true;
        }

        public double ElapsedTime
        {
            get
            {
                return running
                    ? (EndTime - PrevStartTime).TotalMilliseconds
                    : (EndTime - StartTime).TotalMilliseconds;
            }
        }
    }
}
