using System;

namespace EnvDev
{
    public interface ITweenHandle
    {
        ITweenHandle OnCompleted(Action action);

        bool Stop();
    }
}