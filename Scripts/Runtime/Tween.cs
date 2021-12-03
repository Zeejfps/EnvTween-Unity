using System;
using UnityEngine;

namespace EnvDev
{
    public readonly struct Tween
    {
        public readonly Action<float> Lerp;
        public readonly float Duration;
        public readonly Func<double, double> Ease;
        public readonly Func<Tween> Next;

        public Tween(Action<float> lerpFunc, float duration, Func<double, double> easeFunc)
        {
            Lerp = lerpFunc;
            Duration = duration;
            Ease = easeFunc;
            Next = default;
        }
        
        public Tween(Action<float> lerpFunc, float duration, Func<double, double> easeFunc, Func<Tween> onComplete)
        {
            Lerp = lerpFunc;
            Duration = duration;
            Ease = easeFunc;
            Next = onComplete;
        }

        public static Tween Wait(float seconds)
        {
            return new Tween(t => { }, seconds, d => d);
        }
        
        public static Tween Group(params Tween[] tweens)
        {
            var maxDuration = 0f;
            for (var i = 0; i < tweens.Length; i++)
            {
                var tween = tweens[i];
                var duration = tween.Duration;
                if (duration > maxDuration)
                    maxDuration = duration;
            }

            return new Tween(t =>
            {
                var tweenCount = tweens.Length;
                for (var i = 0; i < tweenCount; i++)
                {
                    var tween = tweens[i];
                    var min = 0f;
                    var max = tween.Duration / maxDuration;
                    var relativeT = InverseLerp(min, max, t);
                    tween.Lerp((float)tween.Ease(relativeT));
                }
            }, maxDuration, d => d);
        }

        public static Tween Sequence(Tween tween, Func<Tween> playNext)
        {
            return new Tween(tween.Lerp, tween.Duration, tween.Ease, playNext);
        }

        public static Tween Sequence(params Func<Tween>[] sequence)
        {
            return Sequence(sequence, 0);
        }

        static Tween Sequence(Func<Tween>[] sequence, int firstTweenIndex)
        {
            var startTween = sequence[firstTweenIndex].Invoke();
            var nextStartTweenIndex = firstTweenIndex + 1;

            if (nextStartTweenIndex >= sequence.Length)
                return new Tween(startTween.Lerp, startTween.Duration, startTween.Ease);
            
            return new Tween(startTween.Lerp, startTween.Duration, startTween.Ease, () => Sequence(sequence, nextStartTweenIndex));
        }

        static float InverseLerp(float a, float b, float value) => (value - a) / (b - a);

    }
}