
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
            var pixels = new List<(int, int, Pixel)>();
            int? height = null;
            int? width = null;
            EatSpace();
            // W( integer )
            // H( integer )
            // P( h w r g b )
            while ( !End() )
            {
                var c = _contents[_index];
                switch (  c )
                {
                    case 'P':
                        var  pixel = ParsePixel();
                        pixels.Add( pixel );
                        break;
                    case 'H':
                        var h = ParseHeight();
                        if ( height.HasValue )
                        {
                            throw new Exception( "double definition of Height" );
                        }
                        height = h;
                        break;
                    case 'W':
                        var w = ParseWidth();
                        if ( width.HasValue )
                        {
                            throw new Exception( "double definition of Height" );
                        }
                        width = w;
                        break;
                    default:
                    // TODO track line number?
                        throw new Exception( "Error Parsing" );

                }
            }
            var l = new List<PixelRow>();
            var pixelArray = pixels.ToArray();
            for( var h = 1; h <= height.Value; h++ )
            {
                var (row, errorColumn) = CreatePixelRow( pixelArray.Where( p => p.Item1 == h ).ToArray(), width.Value );
                if ( errorColumn.HasValue )
                {
                    throw new Exception( $"Row {h} has incorrect number of columns {errorColumn.Value}" );
                }
                l.Add( row );
            }
            return new Image { Height = height.Value, Width = width.Value, Rows = l };
        }

        private static (PixelRow, int?) CreatePixelRow( (int, int, Pixel)[] pixelsInRow, int width )
        {
            var l = new List<Pixel>();
            for( var w = 1; w <= width; w++ )
            {
                var pixelsInColumn = Array.FindAll( pixelsInRow, p => p.Item2 == w );    
                if ( pixelsInColumn.Length != 1 )
                {
                    return (null, w);
                }
                l.Add( pixelsInColumn[0].Item3 );
            }
            return (new PixelRow { Row = l }, null);
        }

        private int ParseHeight()
        {
            Require( 'H' );
            EatSpace();
            Require( '(' );
            EatSpace();
            var height = ParseInt();
            EatSpace();
            Require( ')' );
            EatSpace();

            return height;
        }

        private int ParseWidth()
        {
            Require( 'W' );
            EatSpace();
            Require( '(' );
            EatSpace();
            var width = ParseInt();
            EatSpace();
            Require( ')' );
            EatSpace();

            return width;
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
