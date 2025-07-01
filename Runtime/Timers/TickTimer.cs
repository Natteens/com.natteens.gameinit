using System;

namespace GameInit.Timers
{
    public class TickTimer : Timer
    {
        private readonly float tickInterval;
        private float currentTickTime;
        private readonly int maxTicks;
        private readonly bool hasMaxTicks;
        
        public Action OnTick = delegate { };
        public Action OnAllTicksCompleted = delegate { };
        public int TickCount { get; private set; }
        public int MaxTicks => maxTicks;
        public bool IsCompleted => hasMaxTicks && TickCount >= maxTicks;
        public int RemainingTicks => hasMaxTicks ? Math.Max(0, maxTicks - TickCount) : -1;
        
        public new float Progress => hasMaxTicks ? (float)TickCount / maxTicks : 0f;
        
        // Construtor para timer com limite de ticks
        public TickTimer(float interval, int maxTickCount) : base(interval)
        {
            tickInterval = interval;
            maxTicks = maxTickCount;
            currentTickTime = 0f;
            TickCount = 0;
            hasMaxTicks = true;
        }
        
        // Construtor para timer infinito (apenas intervalo)
        public TickTimer(float interval) : base(interval)
        {
            tickInterval = interval;
            maxTicks = 0;
            currentTickTime = 0f;
            TickCount = 0;
            hasMaxTicks = false;
        }

        public override void Tick(float deltaTime)
        {
            if (IsRunning && !IsCompleted) {
                currentTickTime += deltaTime;
                
                if (currentTickTime >= tickInterval) {
                    TickCount++;
                    Time = TickCount * tickInterval;
                    OnTick.Invoke();
                    currentTickTime -= tickInterval;
                    
                    if (IsCompleted) {
                        Stop();
                        OnAllTicksCompleted.Invoke();
                    }
                }
            }
        }

        public void Reset() 
        {
            Time = 0f;
            currentTickTime = 0f;
            TickCount = 0;
        }

        public float GetTickInterval() => tickInterval;
        public float GetTimeUntilNextTick() => tickInterval - currentTickTime;
    }
}