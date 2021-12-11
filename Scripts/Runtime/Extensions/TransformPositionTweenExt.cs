using System;
using UnityEngine;

namespace EnvDev
{
    public static class TransformPositionTweenExt
    {
        #region X

        public static Tween TweenPositionXBy(this Transform transform, float delta, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.position.x;
            var to = from + delta;
            return TweenPositionX(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenPositionXTo(this Transform transform, float to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.position.x;
            return TweenPositionX(transform, from, to, duration, easeFunc);
        }

        public static Tween TweenPositionX(this Transform transform, float from, float to, float duration,
            Func<double, double> easeFunc)
        {
            return new Tween(t =>
            {
                var position = transform.position;
                position.x = Mathf.LerpUnclamped(from, to, t);
                transform.position = position;
            }, duration, easeFunc);
        }
        
        #endregion
        
        #region Y

        public static Tween TweenPositionYBy(this Transform transform, float delta, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.position.y;
            var to = from + delta;
            return TweenPositionY(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenPositionYTo(this Transform transform, float to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.position.y;
            return TweenPositionY(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenPositionY(this Transform transform, float from, float to, float duration,
            Func<double, double> easeFunc)
        {
            return new Tween(t =>
            {
                var position = transform.position;
                position.y = Mathf.LerpUnclamped(from, to, t);
                transform.position = position;
            }, duration, easeFunc);
        }
        
        #endregion
        
        
        #region Z

        public static Tween TweenPositionZBy(this Transform transform, float delta, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.position.z;
            var to = from + delta;
            return TweenPositionZ(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenPositionZTo(this Transform transform, float to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.position.z;
            return TweenPositionZ(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenPositionZ(this Transform transform, float from, float to, float duration,
            Func<double, double> easeFunc)
        {
            return new Tween(t =>
            {
                var position = transform.position;
                position.z = Mathf.LerpUnclamped(from, to, t);
                transform.position = position;
            }, duration, easeFunc);
        }
        
        #endregion

        public static Tween TweenPositionBy(this Transform transform, Vector3 delta, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.position;
            var to = from + delta;
            return TweenPosition(transform, from, to, duration, easeFunc);
        }

        public static Tween TweenPositionTo(this Transform transform, Vector3 to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.position;
            return TweenPosition(transform, from, to, duration, easeFunc);
        }

        public static Tween TweenPosition(this Transform transform, Vector3 from, Vector3 to, float duration,
            Func<double, double> easeFunc)
        {
            return new Tween(t => { transform.position = Vector3.LerpUnclamped(from, to, t); }, duration, easeFunc);
        }
    }
}