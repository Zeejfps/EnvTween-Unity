using System;
using UnityEngine;

namespace EnvDev
{
    public class TweenPlayer : MonoBehaviour
    {
        const int k_MaxTweenCount = 1024;
        
        static TweenPlayer s_Instance;
        public static TweenPlayer Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    var go = new GameObject("[Tween Player]")
                    {
                        hideFlags = HideFlags.DontSave
                    };
                    s_Instance = go.AddComponent<TweenPlayer>();
                    if (Application.isPlaying)
                        DontDestroyOnLoad(go);
                }
                return s_Instance;
            }
        }

        int m_TweenCount;
        readonly Tween[] m_Tweens = new Tween[k_MaxTweenCount];
        readonly TweenHandle[] m_Handles = new TweenHandle[k_MaxTweenCount];
        
        public ITweenHandle Play(Tween tween)
        {
            var tweenIndex = m_TweenCount;
            var handle = new TweenHandle(tweenIndex);
            m_Tweens[tweenIndex] = tween;
            m_Handles[tweenIndex] = handle;
            m_TweenCount++;
            return handle;
        }

        internal bool Stop(TweenHandle handle)
        {
            if (!handle.IsPlaying)
                return false;

            var tweenIndex = handle.TweenIndex;
            handle.TweenIndex = -1;
            StopTween(tweenIndex);
            
            return true;
        }

        void StopTween(int tweenIndex)
        {
            if (tweenIndex >= m_TweenCount)
                return;

            var lastTweenIndex = m_TweenCount - 1;
            if (m_TweenCount == 1 || lastTweenIndex == tweenIndex)
            {
                m_TweenCount--;
                return;
            }

            var lastTweenHandle = m_Handles[lastTweenIndex];
            lastTweenHandle.TweenIndex = tweenIndex;
            
            m_Tweens[tweenIndex] = m_Tweens[lastTweenIndex];
            m_Handles[tweenIndex] = lastTweenHandle;
            m_TweenCount--;
        }

        void Update()
        {
            var dt = Time.deltaTime;
            for (var i = 0; i < m_TweenCount;)
            {
                var tween = m_Tweens[i];
                tween = tween.Update(dt);
                if (tween.Runtime >= tween.Duration)
                {
                    var handle = m_Handles[i];
                    handle.TweenIndex = -1;

                    StopTween(i);

                    handle.OnCompletedAction?.Invoke();
                }
                else
                {
                    m_Tweens[i] = tween;
                    i++;
                }
            }
        }

        void OnDestroy()
        {
            s_Instance = null;
        }
    }

    class TweenHandle : ITweenHandle
    {
        public bool IsPlaying => TweenIndex >= 0;
        
        internal int TweenIndex;

        public Action OnCompletedAction;


        public TweenHandle(int tweenIndex)
        {
            TweenIndex = tweenIndex;
            OnCompletedAction = null;
        }

        public bool Stop()
        {
            return TweenPlayer.Instance.Stop(this);
        }

        public ITweenHandle OnCompleted(Action action)
        {
            OnCompletedAction = action;
            return this;
        }
    }
}