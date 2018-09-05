
using static ray1.standard.Util;

namespace ray1.standard
{
    public class Sphere : ICollidable
    {
        public Point Center;
        public double Radius;

        public Point Collides( Line line )
        {
            var a = Square( line.End.X - line.Start.X ) 
                  + Square( line.End.Y - line.Start.Y )
                  + Square( line.End.Z - line.Start.Z );

            var b = 2 * ( ( line.End.X - line.Start.X ) * ( line.Start.X - Center.X )
                        + ( line.End.Y - line.Start.Y ) * ( line.Start.Y - Center.Y )
                        + ( line.End.Z - line.Start.Z ) * ( line.Start.Z - Center.Z ) );

            var c = Square( Center.X ) + Square( Center.Y ) + Square( Center.Z ) 
                  + Square( line.Start.X ) + Square( line.Start.Y ) + Square( line.Start.Z )
                  - 2 * ( Center.X * line.Start.X + Center.Y * line.Start.Y + Center.Z * line.Start.Z )
                  - Square( Radius );


            var determineIntersection = Square( b ) - 4 * a * c;

            if ( determineIntersection < 0 )
            {
                return null;
            }
            else if ( determineIntersection == 0 )
            {
                return line.At( -b / (2 * a) ); 
            }
            else
            {
                var pos = (-b + SquareRoot( determineIntersection )) / (2 * a);
                var neg = (-b - SquareRoot( determineIntersection )) / (2 * a);

                var posPoint = line.AtPositiveT( pos );
                var negPoint = line.AtPositiveT( neg );

                if ( posPoint == null )
                {
                    return negPoint;
                }

                if ( negPoint == null )
                {
                    return posPoint;
                }
                
                return DistanceSquared( line.Start, posPoint ) < DistanceSquared( line.Start, negPoint ) 
                        ? posPoint : negPoint;
            }
        }
    }
}
