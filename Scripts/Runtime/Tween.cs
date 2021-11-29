using System;

namespace EnvDev
{
    public readonly struct Tween
    {
        public readonly Action<float> Lerp;
        public readonly float Duration;
        public readonly Func<double, double> Ease;

        public Tween(Action<float> lerpFunc, float duration, Func<double, double> easeFunc)
        {
            Lerp = lerpFunc;
            Duration = duration;
            Ease = easeFunc;
        }
    }
}