namespace Crux.Models
{
    public class AdaptiveSize
    {
        public int MinWindowWidth { get; }
        public int Width { get; }
        public int Height { get; }

        public AdaptiveSize(int minWindowWidth, int width, int height = -1)
        {
            MinWindowWidth = minWindowWidth;
            Width = width;
            Height = height == -1 ? Width : height;
        }
    }
}