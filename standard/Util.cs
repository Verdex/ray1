
using System;

namespace ray1.standard
{
    public static class Util
    {
        public static double Square( double x )
        {
            return x * x;
        }
        
        public static double SquareRoot( double x )
        {
            return Math.Pow( x, 0.5 );
        }

        public static double DistanceSquared( Point start, Point end )
        {
            var x = Square(end.X - start.X); 
            var y = Square(end.Y - start.Y);
            var z = Square(end.Z - start.Z);
            return x + y + z;
        }

        public static double Distance( Point start, Point end )
        {
            return SquareRoot( DistanceSquared(start, end) );
        }
    }
}
