
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace ray1.convert
{
    public static class Ppm
    {
        public static IEnumerable<byte> CreatePpmFromImage( Image image ) =>
            Str( "P6" )
                .Concat( Str( "\n" ) )
                .Concat( Str( image.Width.ToString() ) )
                .Concat( Str( "\n" ) )
                .Concat( Str( image.Height.ToString() ) )
                .Concat( Str( "\n" ) )
                .Concat( Str( "255" ) ) // Max color value
                .Concat( Str( "\n" ) )
                .Concat( Pixels( image.Rows ) ); 

        private static IEnumerable<byte> Pixels( IEnumerable<PixelRow> rows ) => 
             rows.SelectMany( r => r.Row )
                 .SelectMany( p => new [] { p.Red, p.Green, p.Blue } );

        private static byte[] Str( string str ) => Encoding.ASCII.GetBytes( str );
    }
}
