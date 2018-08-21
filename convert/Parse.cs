
namespace ray1.convert
{
    public static class Parse
    {
        public static Image ParseImageFile( string fileContents )
        {
            // W( integer )
            // H( integer )
            // P( h w hex-rgb )
            var i = 0;
            while ( i < fileContents.Length )
            {
                var c = fileContents[i];
                if ( c == ' '
                    || c == '\n'
                    || c == '\r' )
                {
                    i++;
                    continue;
                }

                switch ( Char.ToLower( c ) )
                {
                    case 'p':
                        var (pi, pixel) = ParsePixel( fileContents, i );
                        i = pi;
                        break;
                    case 'h':
                    case 'w':
                    default:
                    // TODO track line number?
                        throw new Exception( "Error Parsing" );

                }
            }
        }

        private static (int, Pixel) ParsePixel( string contents, int index )
        {

        }
    }
}
