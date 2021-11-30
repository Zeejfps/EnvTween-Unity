using System;
using UnityEngine;

namespace EnvDev
{
    public static class RectTransformExt
    {
        #region Size Delta
        
        public static Tween TweenSizeDeltaTo(this RectTransform rectTransform, Vector2 to, float duration,
            Func<double, double> easeFunc)
        {
            var from = rectTransform.sizeDelta;
            return TweenSizeDelta(rectTransform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenSizeDelta(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration,
            Func<double, double> easeFunc)
        {
            return new Tween(t =>
            {
                rectTransform.sizeDelta = Vector2.LerpUnclamped(from, to, t);
            }, duration, easeFunc);
        }
        
        #endregion
    }
}