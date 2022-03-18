using System;
using UnityEngine;

namespace EnvDev
{
    public static class TransformLocalScaleExt
    {
        #region Y

        public static Tween TweenLocalScaleYTo(this Transform transform, float to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.localScale.y;
            return TweenLocalScaleY(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenLocalScaleY(this Transform transform, float from, float to, float duration,
            Func<double, double> easeFunc)
        {
            return new Tween(t =>
            {
                var localScale = transform.localScale;
                localScale.y = Mathf.LerpUnclamped(from, to, t);
                transform.localScale = localScale;
            }, duration, easeFunc);
        }

        #endregion
        
        public static Tween TweenLocalScaleUniformlyTo(this Transform transform, float targetValue, float duration,
            Func<double, double> easeFunc)
        {
            var startValue = transform.localScale;
            var targetScale = new Vector3(targetValue, targetValue, targetValue);
            return TweenLocalScale(transform, startValue, targetScale, duration, easeFunc);
        }
        
        public static Tween TweenLocalScaleUniformlyFrom(this Transform transform, float from, float duration,
            Func<double, double> easeFunc)
        {
            var startValue = new Vector3(from, from, from);
            var targetScale = transform.localScale;
            return TweenLocalScale(transform, startValue, targetScale, duration, easeFunc);
        }
        
        public static Tween TweenLocalScaleUniformlyBy(this Transform transform, float delta, float duration,
            Func<double, double> easeFunc)
        {
            var startValue = transform.localScale;
            var targetScale = startValue + new Vector3(delta, delta, delta);
            return TweenLocalScale(transform, startValue, targetScale, duration, easeFunc);
        }
        
        public static Tween TweenLocalScaleUniformly(this Transform transform, float from, float to, float duration,
            Func<double, double> easeFunc)
        {
            var startValue = new Vector3(from, from, from);
            var targetValue = new Vector3(to, to, to);
            return TweenLocalScale(transform, startValue, targetValue, duration, easeFunc);
        }

        public static Tween TweenLocalScaleTo(this Transform transform, Vector3 targetScale, float duration,
            Func<double, double> easeFunc)
        {
            var startValue = transform.localScale;
            return TweenLocalScale(transform, startValue, targetScale, duration, easeFunc);
        }
        
        public static Tween TweenLocalScale(this Transform transform, Vector3 from, Vector3 to,
            float duration, Func<double, double> easeFunc)
        {
            return new Tween(t =>
                {
                    transform.localScale = Vector3.LerpUnclamped(from, to, t);
                },
                duration, easeFunc);
        }
    }
}