
using System;

namespace ray1.standard
{
    public static class Util
    {
        public static double Square( double x ) => x * x;
        
        public static double SquareRoot( double x ) => Math.Pow( x, 0.5 );

        public static double DistanceSquared( Point start, Point end )
        {
            var x = Square(end.X - start.X); 
            var y = Square(end.Y - start.Y);
            var z = Square(end.Z - start.Z);
            return x + y + z;
        }

        public static double Distance( Point start, Point end ) => 
            SquareRoot( DistanceSquared(start, end) );

        public static double Magnitude( Vector v ) => 
            SquareRoot( Square( v.X ) + Square( v.Y ) + Square( v.Z ) );

        public static double DotProduct( Vector a, Vector b ) => 
            a.X * b.X + a.Y * b.Y + a.Z * b.Z;

        public static double Angle( Vector a, Vector b ) =>
            Math.Acos( DotProduct( a, b ) / ( Magnitude( a ) * Magnitude( b ) ) );
    }
}
