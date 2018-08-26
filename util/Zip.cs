
using System.IO;
using System.IO.Compression;

namespace ray1.util
{
    public static class Zip
    {
        public static byte[] DCompress( byte[] bytes )
        {
            using ( var ms = new MemoryStream() )
            using ( var gs = new DeflateStream( ms, CompressionMode.Compress ) )
            {
                gs.Write( bytes, 0, bytes.Length );
                gs.Close();
                return ms.GetBuffer();
            }
        }

        public static byte[] Compress( byte[] bytes )
        {
            using ( var ms = new MemoryStream() )
            using ( var gs = new GZipStream( ms, CompressionMode.Compress ) )
            {
                gs.Write( bytes, 0, bytes.Length );
                gs.Close();
                return ms.GetBuffer();
            }
        }

        public static byte[] Decompress( byte[] bytes )
        {
            using ( var o = new MemoryStream() )
            using ( var ms = new MemoryStream( bytes ) )
            using ( var gs = new GZipStream( ms, CompressionMode.Decompress ) )
            {
                gs.CopyTo( o );
                gs.Close();
                return o.GetBuffer();
            }
        }
    }
}
