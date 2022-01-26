using System;
using UnityEngine;

namespace EnvDev
{
    public class TweenPlayer : MonoBehaviour
    {
        [SerializeField] private int m_MaxTweenCount = 1024;
        
        static TweenPlayer s_Instance;
        public static TweenPlayer Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType<TweenPlayer>();
                    if (s_Instance == null)
                    {
                        var go = new GameObject("[Tween Player]")
                        {
                            hideFlags = HideFlags.DontSave
                        };
                        s_Instance = go.AddComponent<TweenPlayer>();
                    }
                }
                return s_Instance;
            }
        }

        int m_TweenCount;
        Tween[] m_Tweens;
        TweenHandle[] m_Handles;
        
        public ITweenHandle Play(Tween tween)
        {
            var tweenIndex = m_TweenCount;
            if (tweenIndex >= m_MaxTweenCount)
            {
                Debug.LogWarning("Max Tween Count Reached! Please adjust the value if you require more tweens.");
                return new TweenHandle(-1);
            }
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

        private void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Debug.LogWarning("Only one TweenPlayer is supported");
                Destroy(this);
                return;
            }
            
            s_Instance = this;
            m_Tweens = new Tween[m_MaxTweenCount];
            m_Handles = new TweenHandle[m_MaxTweenCount];
            
            if (Application.isPlaying)
                DontDestroyOnLoad(gameObject);
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
            if (s_Instance == this)
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