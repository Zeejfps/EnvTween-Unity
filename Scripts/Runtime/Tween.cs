using System;
using System.Runtime.CompilerServices;

namespace EnvDev
{
    public readonly struct Tween
    {
        public readonly Func<Tween, float, Tween> UpdateFunc;
        public readonly Action<float> Lerp;
        public readonly float Runtime;
        public readonly float Duration;
        public readonly Func<double, double> Ease;
        public readonly Func<Tween> Next;

        public Tween(Action<float> lerpFunc, float duration, Func<double, double> easeFunc)
        {
            UpdateFunc = UpdateTween;
            Lerp = lerpFunc;
            Runtime = 0f;
            Duration = duration;
            Ease = easeFunc;
            Next = null;
        }

        public Tween(Func<Tween, float, Tween> updateFunc, Action<float> lerpFunc, float runtime, float duration,
            Func<double, double> easeFunc, Func<Tween> next)
        {
            UpdateFunc = updateFunc;
            Lerp = lerpFunc;
            Runtime = runtime;
            Duration = duration;
            Ease = easeFunc;
            Next = next;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tween Update(float dt)
        {
            return UpdateFunc.Invoke(this, dt);
        }

        public static Tween Custom(Action<float> lerpFunc, float duration, Func<double, double> easeFunc)
        {
            return new Tween(UpdateTween, lerpFunc, 0f, duration, easeFunc, null);
        }

        public static Tween Wait(float duration)
        {
            return new Tween(t => { }, duration, d => d);
        }

        public static Tween Sequence(params Func<Tween>[] sequence)
        {
            return Sequence(sequence, 0);
        }

        public static Tween Group(params Tween[] group)
        {
            var firstTween = group[0];
            Tween UpdateGroup(Tween self, float dt)
            {
                var tweenCount = group.Length;
                var runtime = self.Runtime + dt;
                var duration = self.Duration;
                for (var tweenIndex = 0; tweenIndex < tweenCount;)
                {
                    var tween = group[tweenIndex];
                    tween = tween.Update(dt);
                    if (tween.Runtime >= tween.Duration)
                    {
                        var lastTweenIndex = tweenCount - 1;
                        group[tweenIndex] = group[lastTweenIndex];
                        tweenCount--;
                    }
                    else
                    {
                        runtime = tween.Runtime;
                        duration = tween.Duration;
                        group[tweenIndex] = tween;
                        tweenIndex++;
                    }
                }
            
                if (tweenCount == 0 && self.Next != null) 
                    return self.Next();
            
                return new Tween(self.UpdateFunc, self.Lerp, runtime, duration, self.Ease, self.Next);
            }

            return new Tween(UpdateGroup, t => { }, 0f, firstTween.Duration, d => d, firstTween.Next);
        }

        static Tween Sequence(Func<Tween>[] sequence, int activeIndex)
        {
            var activeTween = sequence[activeIndex].Invoke();
            var nextActiveIndex = activeIndex + 1;
            if (nextActiveIndex >= sequence.Length)
                return activeTween;

            return new Tween(activeTween.UpdateFunc, activeTween.Lerp, activeTween.Runtime, activeTween.Duration, activeTween.Ease,
                () => Sequence(sequence, nextActiveIndex));
        }

        static Tween UpdateTween(Tween tween, float dt)
        {
            var runtime = tween.Runtime + dt;
            var t = runtime / tween.Duration;
            if (t >= 1f)
            {
                tween.Lerp((float)tween.Ease(1f));
                if (tween.Next != null)
                    return tween.Next();
            }
            else
            {
                tween.Lerp((float)tween.Ease(t));
            }
            
            return new Tween(tween.UpdateFunc, tween.Lerp, runtime, tween.Duration, tween.Ease, tween.Next);
        }
    }
}