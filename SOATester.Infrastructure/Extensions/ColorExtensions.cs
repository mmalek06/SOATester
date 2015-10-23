using System.Windows.Media;

namespace SOATester.Infrastructure.Extensions {
    public static class ColorExtensions {
        private const int MIN_LIGHTNESS = 1;
        private const int MAX_LIGHTNESS = 10;
        private const float MIN_LIGHTNESS_COEF = 1f;
        private const float MAX_LIGHTNESS_COEF = 0.4f;

        public static Color ChangeLightness(this Color color, int lightness) {
            if (lightness < MIN_LIGHTNESS) {
                lightness = MIN_LIGHTNESS;
            } else if (lightness > MAX_LIGHTNESS) {
                lightness = MAX_LIGHTNESS;
            }

            float coef = MIN_LIGHTNESS_COEF +
              (
                (lightness - MIN_LIGHTNESS) *
                  ((MAX_LIGHTNESS_COEF - MIN_LIGHTNESS_COEF) / (MAX_LIGHTNESS - MIN_LIGHTNESS))
              );

            return Color.FromArgb(99, (byte)(int)(color.R * coef), (byte)(int)(color.G * coef),
                (byte)(int)(color.B * coef));
        }
    }
}
