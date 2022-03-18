using System;
using UnityEngine;

namespace EnvDev
{
    [DefaultExecutionOrder(-100)]
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
                throw new Exception("Max Tween Count Reached! Please adjust the value if you require more tweens.");
            
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

            handle.OnInterruptedAction?.Invoke();
            StopTween(handle);
            return true;
        }

        void StopTween(TweenHandle handle)
        {
            if (!handle.IsPlaying)
                return;
            
            var tweenIndex = handle.TweenIndex;
            handle.TweenIndex = -1;
            
            if (tweenIndex >= m_TweenCount || tweenIndex >= m_Tweens.Length || tweenIndex < 0)
                return;

            var lastTweenIndex = m_TweenCount - 1;
            if (m_TweenCount == 1 || tweenIndex == lastTweenIndex)
            {
                m_TweenCount--;
                handle.OnStoppedAction?.Invoke();
                return;
            }

            var lastTweenHandle = m_Handles[lastTweenIndex];
            lastTweenHandle.TweenIndex = tweenIndex;
            
            m_Tweens[tweenIndex] = m_Tweens[lastTweenIndex];
            m_Handles[tweenIndex] = lastTweenHandle;
            m_TweenCount--;
            
            handle.OnStoppedAction?.Invoke();
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
                if (tween.Runtime > tween.Duration)
                {
                    var handle = m_Handles[i];
                    handle.OnCompletedAction?.Invoke();
                    StopTween(handle);
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
        public Action OnInterruptedAction;
        public Action OnStoppedAction;


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

        public ITweenHandle OnInterrupted(Action action)
        {
            OnInterruptedAction = action;
            return this;
        }

        public ITweenHandle OnStopped(Action action)
        {
            OnStoppedAction = action;
            return this;
        }
    }
}