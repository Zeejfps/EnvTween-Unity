using System.Collections;
using EnvDev;
using NUnit.Framework;
using UnityEngine;

public class TweenPlayerTests
{
    public static double EaseFunc(double d)
    {
        return d;
    }
    
    public class PlayMethodTests
    {
        [Test]
        public void PlayOneTween()
        {
            var go = new GameObject();

            var tweenPlayer = new TweenPlayer();
            tweenPlayer.Play(
                go.transform.TweenPosition(Vector3.zero, Vector3.one, 1f, EaseFunc),
                go.transform.TweenRotationTo(Quaternion.identity, 1f, EaseFunc)).Then(() =>
            {
                
            });

            tweenPlayer.Stop();
        }
    }
}
