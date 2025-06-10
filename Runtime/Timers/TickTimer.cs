using System;

namespace GameInit.Timers
{
    public class TickTimer : Timer
    {
        private float tickInterval;
        private float currentTickTime;
        
        public Action OnTick = delegate { };
        public int TickCount { get; private set; }
        
        public TickTimer(float interval) : base(interval) 
        {
            tickInterval = interval;
            currentTickTime = 0f;
            TickCount = 0;
        }

        public override void Tick(float deltaTime)
        {
            if (IsRunning) {
                Time += deltaTime;
                currentTickTime += deltaTime;
                
                if (currentTickTime >= tickInterval) {
                    TickCount++;
                    OnTick.Invoke();
                    currentTickTime = 0f;
                }
            }
        }

        public void Reset() 
        {
            Time = 0f;
            currentTickTime = 0f;
            TickCount = 0;
        }

        public void Reset(float newInterval) 
        {
            tickInterval = newInterval;
            InitialTime = newInterval;
            Reset();
        }

        public float GetTickInterval() => tickInterval;
        public float GetTimeUntilNextTick() => tickInterval - currentTickTime;
    }
}