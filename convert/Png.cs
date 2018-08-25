
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using static ray1.util.Crc;
using static ray1.util.Zip;

namespace ray1.convert
{
    public class Png
    {
        private static readonly byte[] _signature = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };
        private static readonly List<byte> _end = CreatePngChunk( "IEND", new List<byte>() );

        public static IEnumerable<byte> CreatePngFromImage( Image image ) =>
            _signature.Concat( CreateHeader( width: image.Width, height: image.Height ) )
                      .Concat( CreateData( image.Rows ) )
                      .Concat( _end )
                      .ToList();

        private static List<byte> CreateHeader( int width, int height ) =>
            CreatePngChunk( "IHDR", 
                Int( width )
                .Concat( Int( height ) )
                .Concat( new byte[] { 8 // Bit Depth
                                    , 2 // Color Type (each pixle is RGB triple) 
                                    , 0 // Compression
                                    , 0 // Filter
                                    , 0 // Interlace
                                    } ).ToList() );

        private static List<byte> CreateData( IEnumerable<PixelRow> rows ) => 
           CreatePngChunk( "IDAT", 
                Compress( rows.SelectMany( r => r.Row )
                              .SelectMany( p => new [] { p.Red, p.Green, p.Blue } )
                              .ToArray() ).ToList() );

        private static List<byte> CreatePngChunk( string type, List<byte> data )
        {
            var body = Type( type ).Concat( data ).ToList();
            var crc = CalculateCrc( body );
            return Int( data.Count ).Concat( body ).Concat( crc ).ToList();
        }

        private static byte[] Int( int i ) => BitConverter.GetBytes( i ).Reverse().ToArray();
        private static byte[] Type( string str ) => Encoding.ASCII.GetBytes( str );
    }
}
