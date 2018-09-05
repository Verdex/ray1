
namespace ray1.standard
{
    public class Line
    {
        public Point Start;
        public Point End;

        public Point AtPositiveT( double t )
        {
            if ( t < 0 )
            {
                return null;
            }

            return At( t );
        }

        public Point At( double t )
        {
            var x = Start.X + t * (End.X - Start.X);
            var y = Start.Y + t * (End.Y - Start.Y);
            var z = Start.Z + t * (End.Z - Start.Z);
            return new Point { X = x, Y = y, Z = z };
        }
    }
}
