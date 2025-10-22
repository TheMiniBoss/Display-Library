using System.Drawing;

namespace TMB.Display
{

    public enum EMode { Text, Background, BlinkText, BlinkBackground }

    public class Color
    {
        public enum EPreset { Red, LightRed, Purple, LightPurple, Orange, Yellow, White, Gray, Dark };

        public int R { get; set; } = 0;
        public int G { get; set; } = 0;
        public int B { get; set; } = 0;

        public EMode Mode { get; set; } = EMode.Text;
        public EPreset Preset { get; set; } = EPreset.White;

        public Color() { R = G = B = 0; }

        public Color(EPreset _preset)
        {
            Preset = _preset;
            SetColorFromPreset();
        }

        private void SetColorFromPreset()
        {
            switch (Preset)
            {
                case EPreset.Red:
                    R = 255; G = 0; B = 0;
                    break;
                case EPreset.LightRed:
                    R = 255; G = 128; B = 128;
                    break;
                case EPreset.Purple:
                    R = 128; G = 0; B = 128;
                    break;
                case EPreset.LightPurple:
                    R = 255; G = 128; B = 255;
                    break;
                case EPreset.Orange:
                    R = 255; G = 165; B = 0;
                    break;
                case EPreset.Yellow:
                    R = 255; G = 255; B = 0;
                    break;
                case EPreset.White:
                    R = 255; G = 255; B = 255;
                    break;
                case EPreset.Gray:
                    R = 128; G = 128; B = 128;
                    break;
                case EPreset.Dark:
                    R = 32; G = 32; B = 32;
                    break;
            }
        }

        public Color(int _r, int _g, int _b, EMode _mode = EMode.Text)
        {
            R = _r;
            G = _g;
            B = _b;
            Mode = _mode;
        }

        public override string ToString()
        {
            if (Mode == EMode.Text || Mode == EMode.BlinkText)
                return $"\x1b[38;2;{R};{G};{B}m"; // foreground
            else if (Mode == EMode.Background || Mode == EMode.BlinkBackground)
                return $"\x1b[48;2;{R};{G};{B}m"; // background
            return $"R: {R}, G: {G}, B: {B}";
        }
    }

    static class Gradient
    {
        const string RESET = "\x1b[0m"; // Reset ANSI color
        const string BLINK = "\x1b[5m"; // Blink ANSI code

        private static Color ClampGradient2D(Color _start, Color _end, int _index, int _maxDisplayChar)
        {
            int Interpolate(int _a, int _b)
            {
                float _range = _b - _a;
                float _normalize = _index * (_range / _maxDisplayChar);
                return Math.Clamp((int)(_a + _normalize), 0, 255);
            }

            return new Color(
                Interpolate(_start.R, _end.R),
                Interpolate(_start.G, _end.G),
                Interpolate(_start.B, _end.B)
            );
        }

        [Obsolete]
        private static Color ClampRainbow2D(int _index, int _maxDisplayChar)
        {
            if (_maxDisplayChar == 0) return new Color(0, 255, 0);

            int _totalSegments = 6;
            int _segmentLength = _maxDisplayChar / _totalSegments;
            int _segment = _index / _segmentLength;
            int _localIndex = _index % _segmentLength;

            Color start, end;

            switch (_segment)
            {
                case 0: start = new Color(255, 0, 0); end = new Color(255, 127, 0); break;  // Red -> Orange
                case 1: start = new Color(255, 127, 0); end = new Color(255, 255, 0); break;// Orange -> Yellow
                case 2: start = new Color(255, 255, 0); end = new Color(0, 255, 0); break;  // Yellow -> Green
                case 3: start = new Color(0, 255, 0); end = new Color(0, 64, 255); break;   // Green -> Blue
                case 4: start = new Color(0, 64, 255); end = new Color(255, 0, 255); break; // Blue -> Purple
                case 5: start = new Color(255, 0, 255); end = new Color(255, 0, 0); break;  // Purple -> Red
                default: return new Color(255, 0, 0);                                       // Red
            }
            return Gradient.ClampGradient2D(start, end, _localIndex, _segmentLength);
        }

        private static Color ClampGradient(Color[] _colors, int _index, int _maxDisplayChar)
        {
            if (_colors.Length < 2) return _colors[0];
            if (_maxDisplayChar == 0) return _colors[0];

            int _totalSegments = _colors.Length - 1;
            int _segmentLength = _maxDisplayChar / _totalSegments;
            int _segment = _index / _segmentLength;
            int _localIndex = _index % _segmentLength;

            Color start = _colors[Math.Clamp(_segment, 0, _colors.Length)];
            Color end = _colors[Math.Clamp(_segment + 1, 1, _colors.Length - 1)];

            return Gradient.ClampGradient2D(start, end, _localIndex, _segmentLength);
        }

        /// <summary>
        /// Return a string with gradient colors applied to each character
        /// </summary>
        /// <param name="_text">Raw text</param>
        /// <param name="_mode">Text color mode</param>
        /// <param name="_colors">Gradient colors</param>
        /// <returns></returns>
        public static string PrintGradient(string _text, EMode _mode = EMode.Text, params Color[] _colors)
        {
            int _size = _text.Length;
            string _newText = "";

            for (int i = 0; i < _size; i++)
            {
                Color _color = ClampGradient(_colors, i, _size);
                _color.Mode = _mode;
                _newText += _color.ToString() + _text[i];
            }
            if (_mode == EMode.BlinkText || _mode == EMode.BlinkBackground)
            {
                return BLINK + _newText + RESET;
            }
            return _newText + RESET;
        }
    }

    public static class Display
    {
        /// <summary>
        /// Display anchor positions for SetCursorPositionWithAnchors method
        /// </summary>
        public enum EAnchors { TopLeft, TopCenter, TopRight, CenterLeft, Center, CenterRight, BottomLeft, BottomCenter, BottomRight }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">String value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(string? _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value ?? string.Empty, _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Object value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(object? _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value?.ToString() ?? string.Empty, _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Ulong value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(ulong _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Long value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(long _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Uint value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(uint _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Int value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(int _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Float value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(float _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Decimal value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(decimal _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Double value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(double _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">List of characters value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(char[]? _buffer, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(new string(_buffer), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Char value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(char _buffer, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_buffer.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors
        /// </summary>
        /// <param name="_value">Boolean value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void Write(bool _value, EMode _mode, params Color[] _colors)
        {
            string _output = Gradient.PrintGradient(_value.ToString(), _mode, _colors);
            Console.Write(_output);
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">String value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(string? _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Object value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(object? _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Ulong value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(ulong _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Long value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(long _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Uint value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(uint _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Int value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(int _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Float value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(float _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Decimal value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(decimal _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Double value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(double _value, EMode _mode, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">List of characters value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(char[]? _buffer, EMode _mode, params Color[] _colors)
        {
            Write(_buffer, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Char value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(char _buffer, EMode _mode = EMode.Text, params Color[] _colors)
        {
            Write(_buffer, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Writes text to the console with gradient colors and adds a new line at the end
        /// </summary>
        /// <param name="_value">Boolean value to display</param>
        /// <param name="_mode">The text color mode</param>
        /// <param name="_colors">The gradient colors</param>
        public static void WriteLine(bool _value, EMode _mode = EMode.Text, params Color[] _colors)
        {
            Write(_value, _mode, _colors);
            Console.WriteLine();
        }

        /// <summary>
        /// Coordinates structure with X (column) and Y (row) for the cursor position
        /// </summary>
        public struct Coordinates
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Coordinates()
            {
                X = 0;
                Y = 0;
            }
            public Coordinates(int _x, int _y)
            {
                X = _x;
                Y = _y;
            }
        }

        static Coordinates coords = new Coordinates();

        //public Display()
        //{
        //    coords = new Coordinates(0, 0);
        //}

        /// <summary>
        /// Return the size of the console window
        /// </summary>
        /// <returns>Return the coordinates X (column) and Y (row)</returns>
        public static Coordinates GetWindowSize()
        {
            return new Coordinates(Console.WindowWidth, Console.WindowHeight);
        }

        /// <summary>
        /// Return the position of the center of the console
        /// </summary>
        /// <returns>Return the coordinates X (column) and Y (row)</returns>
        public static Coordinates GetCenterPosition()
        {
            int _x = Console.WindowWidth / 2;
            int _y = Console.WindowHeight / 2;
            return new Coordinates(_x, _y);
        }

        /// <summary>
        /// Get position X (column) and Y (row) of the center of the console
        /// </summary>
        /// <returns>Return the coordinates X (column) and Y (row)</returns>
        public static Coordinates GetCursorPosition()
        {
            return new Coordinates(Console.CursorLeft, Console.CursorTop);
        }

        /// <summary>
        /// Return the position X (column) and Y (row) of the cursor in the console
        /// </summary>
        /// <param name="_x">X (column)</param>
        /// <param name="_y">Y (row)</param>
        public static void SetCursorPosition(int _x, int _y)
        {
            if (_x >= Console.WindowWidth) _x = Console.WindowWidth - 1;
            if (_y >= Console.WindowHeight) _y = Console.WindowHeight - 1;
            Console.SetCursorPosition(_x, _y);
            Coordinates coords = new Coordinates(_x, _y);
        }


        /// <summary>
        /// Set the position of the cursor in the console using anchors with the possibility to add padding
        /// </summary>
        /// <param name="_text">The text to display</param>
        /// <param name="_anchors">The anchor position</param>
        /// <param name="_paddingX">The padding on the X (column)</param>
        /// <param name="_paddingY">The padding on the Y (row)</param>
        public static void SetCursorPositionWithAnchors(string _text, EAnchors _anchors, int _paddingX = 0, int _paddingY = 0)
        {
            Coordinates windowSize = GetWindowSize();
            Coordinates coordinates = new Coordinates(0, 0);
            switch (_anchors)
            {
                case EAnchors.TopLeft:
                    coordinates.X = _paddingX;
                    coordinates.Y = _paddingY;
                    break;
                case EAnchors.TopCenter:
                    coordinates.X = ((windowSize.X - _text.Length) / 2) + _paddingX;
                    coordinates.Y = _paddingY;
                    break;
                case EAnchors.TopRight:
                    coordinates.X = (windowSize.X - _text.Length) - _paddingX;
                    coordinates.Y = _paddingY;
                    break;
                case EAnchors.CenterLeft:
                    coordinates.X = _paddingX;
                    coordinates.Y = (windowSize.Y / 2) + _paddingY;
                    break;
                case EAnchors.Center:
                    coordinates.X = ((windowSize.X - _text.Length) / 2) + _paddingX;
                    coordinates.Y = (windowSize.Y / 2) + _paddingY;
                    break;
                case EAnchors.CenterRight:
                    coordinates.X = (windowSize.X - _text.Length) - _paddingX;
                    coordinates.Y = (windowSize.Y / 2) + _paddingY;
                    break;
                case EAnchors.BottomLeft:
                    coordinates.X = _paddingX;
                    coordinates.Y = (windowSize.Y - 1) - _paddingY;
                    break;
                case EAnchors.BottomCenter:
                    coordinates.X = ((windowSize.X - _text.Length) / 2) + _paddingX;
                    coordinates.Y = (windowSize.Y - 1) - _paddingY;
                    break;
                case EAnchors.BottomRight:
                    coordinates.X = (windowSize.X - _text.Length) - _paddingX;
                    coordinates.Y = (windowSize.Y - 1) - _paddingY;
                    break;
            }
            SetCursorPosition(coordinates.X, coordinates.Y);
        }

        /// <summary>
        /// Return the position X (column) of the cursor in the console
        /// </summary>
        /// <param name="_x">X (column)</param>
        public static void SetCursorPositionX(int _x)
        {
            Console.SetCursorPosition(_x, Console.GetCursorPosition().Top);
            coords.X = _x;
        }

        /// <summary>
        /// Return the position Y (row) of the cursor in the console
        /// </summary>
        /// <param name="_y">Y (row)</param>
        public static void SetCursorPositionY(int _y)
        {
            Console.SetCursorPosition(Console.GetCursorPosition().Left, _y);
            coords.Y = _y;
        }

        /// <summary>
        /// Set visibility of the cursor
        /// </summary>
        /// <param name="_show">True to show the cursor, false to hide it</param>
        public static void SetShowCursor(bool _show)
        {
            Console.CursorVisible = _show;
        }

        /// <summary>
        /// Make a beep sound in the console speaker
        /// </summary>
        public static void Beep()
        {
            Console.Beep();
        }

        /// <summary>
        /// Clear the console window
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Wait until the specified key is pressed
        /// </summary>
        /// <param name="_exitKey">The key to continue (-1 = any key)</param>
        public static void WaitKey(int _exitKey = -1)
        {
            int _key = 0;
            do
            {
                if (Console.KeyAvailable)
                {
                    if (_exitKey == -1)
                    {
                        break;
                    }
                    _key = Console.ReadKey(true).KeyChar;
                }
            } while (_key != _exitKey);
        }

        private static string RainbowChar(string _text)
        {
            int _size = _text.Length;
            string _newText = "";
            for (int i = 0; i < _size; i++)
            {
                Random _rand = new Random();
                Color _color = new Color(_rand.Next(32, 255), _rand.Next(32, 255), _rand.Next(32, 255));
                _color.Mode = EMode.Text;
                _newText += _color.ToString() + _text[i];
            }
            return _newText + "\x1b[0m"; // Reset ANSI color
        }

        private static string RainbowText(string _text)
        {
            Random _rand = new Random();
            Color _color = new Color(_rand.Next(32, 255), _rand.Next(32, 255), _rand.Next(32, 255));
            _color.Mode = EMode.Text;
            return _color.ToString() + _text + "\x1b[0m"; // Reset ANSI color
        }

        /// <summary>
        /// Print in a sparkling color until the user decide to quit
        /// </summary>
        /// <param name="_text">Text to show</param>
        /// <param name="_exitKey">Key to press to quit (Any key by default)</param>
        /// <param name="_sync">All characters change to the same color</param>
        public static void Sparkle(string _text, int _exitKey = -1, bool _sync = false)
        {
            Coordinates _position = GetCursorPosition();
            int _key = 0;
            do
            {
                SetCursorPosition(_position.X, _position.Y);
                Console.Write(_sync ? RainbowText(_text) : RainbowChar(_text));
                if (Console.KeyAvailable)
                {
                    if (_exitKey == -1)
                    {
                        break;
                    }
                    _key = Console.ReadKey(true).KeyChar;
                }
            } while (_key != _exitKey);
        }
    }
}