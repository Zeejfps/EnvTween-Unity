using System;

namespace EnvDev
{
    public interface ITweenHandle
    {
        bool IsPlaying { get; }
        
        ITweenHandle OnCompleted(Action action);

        bool Stop();
    }
}