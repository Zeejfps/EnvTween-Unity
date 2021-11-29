using System;

namespace EnvDev
{
    public class TweenPlayer
    {
        public bool IsPlaying => m_TweenCount > 0;

        public event Action Stopped;

        int m_TweenCount;
        Action m_ThenAction;
        readonly Tween[] m_Tweens;
        readonly float[] m_Runtimes;

        public TweenPlayer(int tweenCacheSize = 16)
        {
            m_Tweens = new Tween[tweenCacheSize];
            m_Runtimes = new float[tweenCacheSize];
        }

        public TweenPlayer Play(params Tween[] tweens)
        {
            var tweenCount = tweens.Length;
            var offset = m_TweenCount;
            for (var i = 0; i < tweenCount; i++, m_TweenCount++)
            {
                var index = offset + i;
                m_Tweens[index] = tweens[i];
                m_Runtimes[index] = 0f;
            }

            return this;
        }

        public void Then(Action action)
        {
            m_ThenAction = action;
        }

        public void Stop()
        {
            m_TweenCount = 0;
            Stopped?.Invoke();
        }

        public void Update(float dt)
        {
            if (m_TweenCount == 0)
                return;

            // i is incremented manually when needed
            for (var i = 0; i < m_TweenCount;)
            {
                var tween = m_Tweens[i];
                var runtime = m_Runtimes[i];
                var duration = tween.Duration;

                // We reached the end of the tween
                if (runtime >= duration)
                {
                    tween.Lerp(1f);

                    // Swap the last tween with this one
                    var lastTweenIndex = m_TweenCount - 1;

                    m_Tweens[i] = m_Tweens[lastTweenIndex];
                    m_Runtimes[i] = m_Runtimes[lastTweenIndex];

                    // Lower the tween count, but DO NOT increment i
                    m_TweenCount--;
                    continue;
                }

                var t = tween.Ease(runtime / duration);
                tween.Lerp((float)t);

                m_Runtimes[i] += dt;

                // NOTE: Manually incrementing i here because we don't want to increment it when we swap
                i++;
            }

            if (m_TweenCount == 0)
            {
                if (m_ThenAction != null)
                {
                    var thenAction = m_ThenAction;
                    m_ThenAction = null;
                    thenAction.Invoke();
                }

                // Have to check here again in-case 'm_ThenAction' added more tweens
                if (m_TweenCount == 0)
                    Stopped?.Invoke();
            }
        }
    }
}