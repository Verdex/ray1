using System;
using System.IO;
using System.Linq;

namespace ray1.convert
{
    public static class Program 
    {
        public static void Main(string[] args)
        {
            /*if ( args.Length != 4 )
            {
                Console.WriteLine( "Wrong Number Of Parameters" );
                Console.WriteLine( "Usage:  Convert.exe -s input.image -d output.png" );
                return;
            }

            string inputFileName = null;
            string outputFileName = null;
            if ( args[0] == "-s" )
            {
                inputFileName = args[1];
            }
            else
            {
                Console.WriteLine( $"Unknown argument:  {args[0]}" ) ;
            }

            if ( args[2] == "-d" )
            {
                outputFileName = args[3];
            }
            else
            {
                Console.WriteLine( $"Unknown argument:  {args[2]}" ) ;
            }*/


            string inputFileName = "test.image";
            string outputFileName = "output.png";

            var inputFile = File.ReadAllText( inputFileName );

            using( var parser = new Parser( inputFile ) )
            {
                var image = parser.ParseImageFile();

                Png.SavePngFromImageUsingBitmap( image, outputFileName );
                //var bytes = Png.CreatePngFromImage( image );
                //var bytes = Ppm.CreatePpmFromImage( image );
                //File.WriteAllBytes( outputFileName, bytes.ToArray() );
            }
        }
    }
}
