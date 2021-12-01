using System.Collections.Generic;
using UnityEngine;

namespace EnvDev
{
    public class TweenPlayerManager : MonoBehaviour
    {
        static TweenPlayerManager s_Instance;

        public static TweenPlayerManager Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    var go = new GameObject("[Tween Player Manager]")
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    
                    if (Application.isPlaying)
                        DontDestroyOnLoad(go);
                    s_Instance = go.AddComponent<TweenPlayerManager>();
                }

                return s_Instance;
            }
        }

        readonly List<TweenPlayer> m_TweenPlayers = new List<TweenPlayer>();

        public TweenPlayer Get()
        {
            var tweenPlayer = new TweenPlayer();
            m_TweenPlayers.Add(tweenPlayer);
            return tweenPlayer;
        }

        public void Release(TweenPlayer player)
        {
            m_TweenPlayers.Remove(player);
        }

        void Update()
        {
            var dt = Time.deltaTime;
            var count = m_TweenPlayers.Count;
            for (var i = 0; i < count; i++)
                m_TweenPlayers[i].Update(dt);
        }
    }
}