namespace Assets.Resources.Scripts.Extensions
{
    public static class FloatExtensions
    {
        public static float Clamp(this float source, float min, float max)
        {
            if (source < min) return min;
            if (source > max) return max;
            return source;
        }
    }
}