

namespace ray1.convert
{
    public static class Program 
    {
        public static void Main(string[] args)
        {
            using( var parser = new Parser( 
@" 
P( 1234 5678 CD 34 01 )
P( 1234 5678 CD 34 01 )

P( 1234 5678 CD 34 01 )

") )
            {
                var x = parser.ParseImageFile();
            }

        }
    }
}
