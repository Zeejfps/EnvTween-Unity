using System;

namespace EnvDev
{
    public interface ITweenHandle
    {
        /// <summary>
        /// Returns true if the tween is still playing
        /// </summary>
        bool IsPlaying { get; }
        
        /// <summary>
        /// Interrupts the playing of this tween
        /// </summary>
        /// <returns></returns>
        bool Stop();
        
        /// <summary>
        /// Executed when the tween comes to completion
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ITweenHandle OnCompleted(Action action);
        
        /// <summary>
        /// Executed when the tween is interrupted
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ITweenHandle OnInterrupted(Action action);

        /// <summary>
        /// Executed when a tween is stopped regardless of if it was completed or interrupted
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ITweenHandle OnStopped(Action action);
    }
}