using System;
using System.IO;
using System.Linq;

namespace ray1.convert
{
    public static class Program 
    {
        public static void Main(string[] args)
        {
            var name = args[0];


            string inputFileName = $"{name}.image";
            string outputFileName = $"{name}.png";

            var inputFile = File.ReadAllText( inputFileName );

            using( var parser = new Parser( inputFile ) )
            {
                var image = parser.ParseImageFile();
                Png.SavePngFromImageUsingBitmap( image, outputFileName );
            }
        }
    }
}
