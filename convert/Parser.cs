
using System;
using System.Linq;
using System.Collections.Generic;

namespace ray1.convert
{
    public class Parser : IDisposable
    {
        private readonly string _contents;
        private int _index;
        public Parser( string fileContents )
        {
            _contents = fileContents;
            _index = 0;
        }

        public void Dispose()
        {
            _index = 0;
        }

        public Image ParseImageFile()
        {
            EatSpace();
            // W( integer )
            // H( integer )
            // P( h w hex-rgb )
            while ( !End() )
            {
                var c = _contents[_index];
                switch (  c )
                {
                    case 'P':
                        var  (height, width, pixel) = ParsePixel();
                        Console.WriteLine( $"{height} {width} {pixel.Red} {pixel.Green} {pixel.Blue}" );
                        break;
                    case 'H':
                    case 'W':
                    default:
                    // TODO track line number?
                        throw new Exception( "Error Parsing" );

                }
            }
            return null;
        }

        private (int, int, Pixel) ParsePixel()
        {
            Require( 'P' );
            EatSpace();
            Require( '(' );
            EatSpace();
            var height = ParseInt();
            EatSpace();
            var width = ParseInt();
            EatSpace();
            var red = ParseHexByte();
            EatSpace();
            var green = ParseHexByte();
            EatSpace();
            var blue = ParseHexByte();
            EatSpace();
            Require( ')' );
            EatSpace();

            return (height, width, new Pixel { Red = red, Green = green, Blue = blue } );
        }

        private int ParseInt()
        {
            var digits = new List<char>();
            char? Digit(char t)
            {
                switch( t )
                {
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '0':
                        return t;
                    default:
                        return null;
                }
            }
            var c = _contents[_index];
            var d = Digit( c );
            while( d.HasValue && !End() )
            {
                digits.Add( d.Value );
                _index++;
                if ( !End() )
                {
                    c = _contents[_index];
                    d = Digit( c );
                }
            }
            return int.Parse( new string( digits.ToArray() ) );
        }

        private byte ParseHexByte()
        {
            var digits = new List<char>();
            char? Digit(char t)
            {
                switch( t )
                {
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '0':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                        return t;
                    default:
                        return null;
                }
            }
            var c = _contents[_index];
            var d = Digit( c );
            while( d.HasValue && !End() )
            {
                digits.Add( d.Value );
                _index++;
                if ( !End() )
                {
                    c = _contents[_index];
                    d = Digit( c );
                }
            }
            return byte.Parse( new string( digits.ToArray() ), 
                System.Globalization.NumberStyles.HexNumber );
        }

        private bool End()
        {
            return _index >= _contents.Length;
        }

        private void EatSpace()
        {
            var c = _contents[_index];
            while ( (c == ' '
                || c == '\n'
                || c == '\r')
                && !End() )
            {
                _index++;
                if ( !End() )
                {
                    c = _contents[_index];
                }
            }
        }

        private void Require( char c )
        {
            if ( !End() && _contents[_index] == c )
            {
                _index++;
            }
            else
            {
                throw new Exception( $"error parsing : Expected {c}, but found {_contents[_index]}" );
            }
        }
    }
}
