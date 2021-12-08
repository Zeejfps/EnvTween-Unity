namespace EnvDev
{
    public static class TweenExt
    {
        public static ITweenHandle Play(this Tween tween)
        {
            var player = TweenPlayer.Instance;
            return player.Play(tween);
        }
    }
}