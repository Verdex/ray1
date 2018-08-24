using System;

namespace ray1.convert
{
    public static class Program 
    {
        public static void Main(string[] args)
        {
            using( var parser = new Parser( 
@" 

W( 3 )

H( 4 )

P( 1 1 1 D 19 )
P( 1 2 2 E 1A )
P( 1 3 3 F 1B)
P( 2 1 4 10 1C )
P( 2 2 5 11 1D)
P( 2 3 6 12 1E)
P( 3 1 7 13 1F)
P( 3 2 8 14 20 )
P( 3 3 9 15 21)
P( 4 1 A 16 22 )
P( 4 2 B 17 23 )
P( 4 3 C 18 24)

") )
            {
                var x = parser.ParseImageFile();

                Console.WriteLine( $" h = {x.Height}");
                Console.WriteLine( $" w = {x.Width}");
                foreach( var row in x.Rows )
                {
                    foreach( var column in row.Row )
                    {
                        Console.WriteLine( $" r = {column.Red}");
                        Console.WriteLine( $" g = {column.Green}");
                        Console.WriteLine( $" b = {column.Blue}");
                    }
                }
            }

            Png.CreatePngFromImage( null );

        }
    }
}
