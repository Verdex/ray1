
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ray1.util;

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
            public byte[] MyCrc;

            public string Display() 
                => $"Length = {Length} : Type = {ChunkTypeString} : Data = {BitConverter.ToString(Data)} : Crc = {BitConverter.ToString(Crc)} : My Crc = {BitConverter.ToString(MyCrc)}";

            public static (PngChunk, Int32) Parse( byte[] bytes, int offset )
            {
                var lengthArray = new ArraySegment<byte>( bytes, offset, 4 ).Reverse().ToArray();
                var length = BitConverter.ToInt32( lengthArray, 0 );
                var typeArray = new ArraySegment<byte>( bytes, 4 + offset, 4 ).ToArray();
                var dataArray = new ArraySegment<byte>( bytes, 8 + offset, length ).ToArray();
                var crcArray = new ArraySegment<byte>( bytes, length + 8 + offset, 4 ).ToArray();

                return (new PngChunk 
                { 
                    Length = length, 
                    Type = ChunkType.Unknown, 
                    ChunkTypeString = Encoding.ASCII.GetString( typeArray ),
                    Data = dataArray,
                    Crc = crcArray,
                    MyCrc = ray1.util.Crc.CalculateCrc( typeArray.Concat( dataArray ).ToArray() ),
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
