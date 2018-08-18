
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ray1.view
{
    public static class Program
    {
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

            var i = 8;
            while ( i < fileBytes.Length )
            {
                var (chunk, newOffset) = PngChunk.Parse( fileBytes, i );
                i += newOffset;
                o.Append( chunk.Display() + "\n\n" );
            }

            return o.ToString();
        }

        private class PngChunk
        {
            public Int32 Length;
            public ChunkType Type;
            public string ChunkTypeString;
            public byte[] Data;
            public byte[] Crc;

            public string Display() 
                => $"Length = {Length} : Type = {ChunkTypeString} : Data = {BitConverter.ToString(Data)} : Crc = {BitConverter.ToString(Crc)}";

            public static (PngChunk, Int32) Parse( byte[] bytes, int offset )
            {
                var lengthArray = new ArraySegment<byte>( bytes, offset, 4 ).Reverse().ToArray();
                var length = BitConverter.ToInt32( lengthArray, 0 );
                Console.WriteLine( "blarg" );
                var typeArray = new ArraySegment<byte>( bytes, 3 + offset, 4 ).ToArray();
                Console.WriteLine( $"blarg2 {length}" );
                var dataArray = new ArraySegment<byte>( bytes, 7 + offset, length ).ToArray();
                Console.WriteLine( "blarg3" );
                var crcArray = new ArraySegment<byte>( bytes, length + 7 + offset, 4 ).ToArray();
                Console.WriteLine( "blarg4" );

                return (new PngChunk 
                { 
                    Length = length, 
                    Type = ChunkType.Unknown, 
                    ChunkTypeString = Encoding.ASCII.GetString( typeArray ),
                    Data = dataArray,
                    Crc = crcArray,
                }, length + 12);
            }
        }

        private enum ChunkType 
        {
            FirstChunk,
            Unknown,
        }
    }
}
