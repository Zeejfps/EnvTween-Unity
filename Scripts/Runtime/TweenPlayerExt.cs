using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EnvDev
{
    public static class TweenPlayerExt
    {
        public static TaskAwaiter<bool> GetAwaiter(this TweenPlayer tweenPlayer)
        {
            return new TweenPlayerCompletionSource(tweenPlayer).Task.GetAwaiter();
        }

        class TweenPlayerCompletionSource : TaskCompletionSource<bool>
        {
            TweenPlayer m_TweenPlayer;

            public TweenPlayerCompletionSource(TweenPlayer tweenPlayer)
            {
                m_TweenPlayer = tweenPlayer;
                if (!m_TweenPlayer.IsPlaying)
                {
                    TrySetResult(true);
                    return;
                }

                m_TweenPlayer.Stopped += TweenPlayer_OnStopped;
            }

            void TweenPlayer_OnStopped()
            {
                m_TweenPlayer.Stopped -= TweenPlayer_OnStopped;
                m_TweenPlayer = null;
                TrySetResult(true);
            }
        }
    }
}