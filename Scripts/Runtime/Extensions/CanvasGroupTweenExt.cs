using System;
using UnityEngine;

namespace EnvDev
{
    public static class CanvasGroupTweenExt
    {
        #region Alpha
        
        public static Tween TweenAlphaBy(this CanvasGroup canvasGroup, float alphaDelta, float duration,
            Func<double, double> easeFunc)
        {
            var startAlpha = canvasGroup.alpha;
            var targetAlpha = startAlpha + alphaDelta;
            return TweenAlpha(canvasGroup, startAlpha, targetAlpha, duration, easeFunc);
        }
        
        public static Tween TweenAlphaTo(this CanvasGroup canvasGroup, float targetAlpha, float duration,
            Func<double, double> easeFunc)
        {
            var startAlpha = canvasGroup.alpha;
            return TweenAlpha(canvasGroup, startAlpha, targetAlpha, duration, easeFunc);
        }
        
        public static Tween TweenAlphaFrom(this CanvasGroup canvasGroup, float startAlpha, float duration,
            Func<double, double> easeFunc)
        {
            var targetAlpha = canvasGroup.alpha;
            return TweenAlpha(canvasGroup, startAlpha, targetAlpha, duration, easeFunc);
        }
        
        public static Tween TweenAlpha(this CanvasGroup canvasGroup, float startAlpha, float targetAlpha,
            float duration, Func<double, double> easeFunc)
        {
            return new Tween(t =>
            {
                canvasGroup.alpha = Mathf.LerpUnclamped(startAlpha, targetAlpha, t);
            }, duration, easeFunc);
        }
        
        #endregion
    }
}