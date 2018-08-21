
using System.Collections.Generic;

namespace ray1.convert
{
    public class Image
    {
        public int Width;
        public int Height;
        public Pixel Pixels;
        public IEnumerable<PixelRow> Rows;
    }

    public class PixelRow
    {
        public IEnumerable<Pixel> Row;
    }

    public class Pixel
    {
        public byte Red;
        public byte Green;
        public byte Blue;
    }
}
