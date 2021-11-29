using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EnvDev
{
    public static class ImageTweenExt
    {
        #region Alpha

        public static Tween TweenAlphaTo(this Image image, float targetAlpha, float duration,
            Func<double, double> easeFunc)
        {
            var startAlpha = image.color.a;
            return TweenAlpha(image, startAlpha, targetAlpha, duration, easeFunc);
        }
        
        public static Tween TweenAlphaFrom(this Image image, float startAlpha, float duration,
            Func<double, double> easeFunc)
        {
            var targetAlpha = image.color.a;
            return TweenAlpha(image, startAlpha, targetAlpha, duration, easeFunc);
        }
        
        public static Tween TweenAlpha(this Image image, float startAlpha, float targetAlpha, float duration,
            Func<double, double> easeFunc)
        {
            var startColor = image.color;
            startColor.a = startAlpha;
            
            var targetColor = image.color;
            targetColor.a = targetAlpha;

            return TweenColor(image, startColor, targetColor, duration, easeFunc);
        }

        #endregion
        
        #region Color

        public static Tween TweenColorTo(this Image image, Color targetColor, float duration,
            Func<double, double> easeFunc)
        {
            var startColor = image.color;
            return TweenColor(image, startColor, targetColor, duration, easeFunc);
        }
        
        public static Tween TweenColorFrom(this Image image, Color startColor, float duration,
            Func<double, double> easeFunc)
        {
            var targetColor = image.color;
            return TweenColor(image, startColor, targetColor, duration, easeFunc);
        }
        
        public static Tween TweenColor(this Image image, Color startColor, Color targetColor, float duration,
            Func<double, double> easeFunc)
        {
            return new Tween(t =>
            {
                image.color = Color.LerpUnclamped(startColor, targetColor, t);
            }, duration, easeFunc);
        }

        #endregion
    }
}