using System;
using UnityEngine;

namespace EnvDev
{
    public static class TransformTweenExt
    {
        #region Local Angles

        #region X

        public static Tween TweenLocalAnglesXTo(this Transform transform, float to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.localEulerAngles.x;
            return TweenLocalAnglesX(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenLocalAnglesX(this Transform transform, float from,
            float to, float duration, Func<double, double> easeFunc)
        {
            return new Tween(
                t =>
                {
                    var x = Mathf.LerpAngle(from, to, t);
                    var angles = transform.localEulerAngles;
                    angles.x = x;
                    transform.localEulerAngles = angles;
                },
                duration,
                easeFunc);
        }

        #endregion
        
        #region Y

        public static Tween TweenLocalAnglesYTo(this Transform transform, float to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.localEulerAngles.y;
            return TweenLocalAnglesY(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenLocalAnglesY(this Transform transform, float from,
            float to, float duration, Func<double, double> easeFunc)
        {
            return new Tween(
                t =>
                {
                    var y = Mathf.LerpAngle(from, to, t);
                    var angles = transform.localEulerAngles;
                    angles.y = y;
                    transform.localEulerAngles = angles;
                },
                duration,
                easeFunc);
        }

        #endregion
        
        
        #region Z

        public static Tween TweenLocalAnglesZTo(this Transform transform, float to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.localEulerAngles.z;
            return TweenLocalAnglesZ(transform, from, to, duration, easeFunc);
        }
        
        public static Tween TweenLocalAnglesZ(this Transform transform, float from,
            float to, float duration, Func<double, double> easeFunc)
        {
            return new Tween(
                t =>
                {
                    var z = Mathf.LerpAngle(from, to, t);
                    var angles = transform.localEulerAngles;
                    angles.z = z;
                    transform.localEulerAngles = angles;
                },
                duration,
                easeFunc);
        }

        #endregion
        
        public static Tween TweenLocalAnglesBy(this Transform rectTransform, Vector3 from,
            float duration, Func<double, double> easeFunc)
        {
            var to = rectTransform.localEulerAngles;
            return TweenLocalAngles(rectTransform, to, to + @from, duration, easeFunc);
        }

        public static Tween TweenLocalAnglesTo(this Transform rectTransform, Vector3 targetRotation,
            float duration, Func<double, double> easeFunc)
        {
            var startRotation = rectTransform.localEulerAngles;
            return TweenLocalAngles(rectTransform, startRotation, targetRotation, duration, easeFunc);
        }

        public static Tween TweenLocalAngles(this Transform rectTransform, Vector3 startRotation,
            Vector3 targetRotation, float duration, Func<double, double> easeFunc)
        {
            return new Tween(
                t => { rectTransform.localEulerAngles = Vector3.LerpUnclamped(startRotation, targetRotation, t); },
                duration,
                easeFunc);
        }
        
        #endregion

        #region Local Rotation
        
        public static Tween TweenLocalRotationTo(this Transform rectTransform, Quaternion to,
            float duration, Func<double, double> easeFunc)
        {
            var from = rectTransform.localRotation;
            return TweenLocalRotation(rectTransform, from, to, duration, easeFunc);
        }

        public static Tween TweenLocalRotation(this Transform rectTransform, Quaternion from,
            Quaternion to, float duration, Func<double, double> easeFunc)
        {
            return new Tween(
                t =>
                {
                    rectTransform.localRotation = Quaternion.SlerpUnclamped(from, to, t);
                },
                duration,
                easeFunc);
        }

        #endregion

        #region Local Position

        #region X

        public static Tween TweenLocalX(this Transform rectTransform, float startX, float targetX,
            float duration, Func<double, double> easeFunc)
        {
            var startPosition = rectTransform.localPosition;
            startPosition.x = startX;

            var targetPosition = startPosition;
            targetPosition.x = targetX;

            return TweenLocalPosition(rectTransform, startPosition, targetPosition, duration, easeFunc);
        }

        public static Tween TweenLocalXTo(this Transform rectTransform, float targetX, float duration,
            Func<double, double> easeFunc)
        {
            var startPosition = rectTransform.localPosition;

            var targetPosition = startPosition;
            targetPosition.x = targetX;

            return TweenLocalPosition(rectTransform, startPosition, targetPosition, duration, easeFunc);
        }

        #endregion

        #region Y

        public static Tween TweenLocalY(this Transform rectTransform, float startY, float targetY,
            float duration, Func<double, double> easeFunc)
        {
            var startPosition = rectTransform.localPosition;
            startPosition.y = startY;

            var targetPosition = startPosition;
            targetPosition.y = targetY;

            return TweenLocalPosition(rectTransform, startPosition, targetPosition, duration, easeFunc);
        }

        public static Tween TweenLocalYTo(this Transform rectTransform, float targetY, float duration,
            Func<double, double> easeFunc)
        {
            var startPosition = rectTransform.localPosition;

            var targetPosition = startPosition;
            targetPosition.y = targetY;

            return TweenLocalPosition(rectTransform, startPosition, targetPosition, duration, easeFunc);
        }

        #endregion

        #region Z

        public static Tween TweenLocalZ(this Transform rectTransform, float startZ, float targetZ,
            float duration, Func<double, double> easeFunc)
        {
            var startPosition = rectTransform.localPosition;
            startPosition.z = startZ;

            var targetPosition = startPosition;
            targetPosition.z = targetZ;

            return TweenLocalPosition(rectTransform, startPosition, targetPosition, duration, easeFunc);
        }

        public static Tween TweenLocalZTo(this Transform rectTransform, float targetZ, float duration,
            Func<double, double> easeFunc)
        {
            var startPosition = rectTransform.localPosition;

            var targetPosition = startPosition;
            targetPosition.z = targetZ;

            return TweenLocalPosition(rectTransform, startPosition, targetPosition, duration, easeFunc);
        }

        #endregion

        public static Tween TweenLocalPositionBy(this Transform rectTransform, Vector3 deltaPosition,
            float duration, Func<double, double> easeFunc)
        {
            var startPosition = rectTransform.localPosition;
            var targetPosition = startPosition + deltaPosition;
            return TweenLocalPosition(rectTransform, startPosition, targetPosition, duration, easeFunc);
        }

        public static Tween TweenLocalPositionTo(this Transform rectTransform, Vector3 targetPosition,
            float duration, Func<double, double> easeFunc)
        {
            var startPosition = rectTransform.localPosition;
            return TweenLocalPosition(rectTransform, startPosition, targetPosition, duration, easeFunc);
        }

        public static Tween TweenLocalPosition(this Transform rectTransform, Vector3 startPosition,
            Vector3 targetPosition, float duration, Func<double, double> easeFunc)
        {
            return new Tween(
                t => { rectTransform.localPosition = Vector3.LerpUnclamped(startPosition, targetPosition, t); },
                duration,
                easeFunc);
        }

        #endregion

        #region Local Scale

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

        #endregion

        #region Rotation
        
        public static Tween TweenRotationTo(this Transform transform, Quaternion to, float duration,
            Func<double, double> easeFunc)
        {
            var from = transform.rotation;
            return new Tween(t => { transform.rotation = Quaternion.LerpUnclamped(from, to, t); }, duration, easeFunc);
        }

        #endregion

        #region Angles
        
        public static Tween TweenAngles(this Transform transform, Vector3 from, Vector3 to, float duration,
            Func<double, double> easeFunc)
        {
            return new Tween(t => { transform.eulerAngles = Vector3.LerpUnclamped(from, to, t); }, duration, easeFunc);
        }

        #endregion

        #region Position

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

        #endregion
    }
}