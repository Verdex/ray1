using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

using static ray1.standard.Util;

namespace ray1.standard
{
    public static class Program
    {
        public static void Main()
        {

            var v1 = new Vector { X = 1, Y = 0, Z = 0 };
            var v2 = new Vector { X = 0, Y = 1, Z = 0 };
            
            var a = Angle( v1, v2 );
            Console.WriteLine( a );


            return;
            

            var s = new Sphere { Radius = 25, Center = new Point { X = 200, Y = 0, Z = 0 } };             

            var l = new List<(int, int, bool)>();
            for( var h = 1; h <= 100; h++ )
            {
                for( var w = 1; w <= 100; w++ )
                {
                    var p = s.Collides( new Line { Start = new Point { X = 0, Y = h - 50, Z = w - 50 }, 
                                                   End = new Point { X = 1, Y = h - 50, Z = w - 50 } } );
                    
                    l.Add( (h, w, p != null ) );
                }
            }

            var o = new StringBuilder();
            o.Append( "H( 100 )\n" );
            o.Append( "W( 100 )\n" );
            foreach( var (h, w, p) in l )
            {
                if ( p )
                {
                    o.Append( $"P( {h} {w} FF FF FF )\n" );
                }
                else
                {
                    o.Append( $"P( {h} {w} 0 0 0 )\n" );
                }
            }
            File.WriteAllText( "Jabber.image", o.ToString() );
        }
    }
}
