
using System;
using System.IO;
using System.Text;


namespace ray1.view
{
    public static class Program
    {
        private const byte SlashN = 0x0A;
        private const byte SlashR = 0x0D;

        public static void Main(string[] args)
        {
            foreach( var a in args )
            {
                var bytes = File.ReadAllBytes( a );
                var output = ToString( a, bytes );
                Console.Write( output ); 
            }
        }

        private static string ToString( string fileName, byte[] fileBytes )
        {
            var o = new StringBuilder();

            o.Append( "======================\n" );
            o.Append( $"File name: {fileName}\n\n" );

            var i = 0;
            // TODO filter out \r\n or whatever it is and replace with \n
            foreach( var b in SquashEndline( fileBytes ) )
            {
                if ( b == SlashN || b == SlashR )
                {
                    o.Append( "\\n\n" );
                }
                else
                {
                    o.Append( b.ToString( "X2" ) );
                }

                if ( i > 10 ) 
                {
                    o.Append( "\n" );
                    i = 0;
                }
                else
                {
                    i++;
                }
            }

            return o.ToString();
        }

        private static IEnumerable<byte> SquashEndline( byte[] bs )
        {
            for( var i = 0; i < bs.Length; i ++ )
            {
                if ( i < bs.Length - 1 && bs[i] == SlashR && bs[i+1] == SlashN )
                {
                    yield return SlashN;
                    i++;
                }
                else
                {
                    yield return bs[i]
                }
            }
        }
    }
}
