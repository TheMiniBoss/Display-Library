#include "Display.h"
#include <Windows.h>
#include "Color.h"

void SetCursorPosition(const u_int& _x, const u_int& _y, const bool _cursor)
{
	static const HANDLE _hOut = GetStdHandle(STD_OUTPUT_HANDLE);
	CONSOLE_CURSOR_INFO _info;
	std::cout.flush();
	_info.dwSize = 100;
	_info.bVisible = _cursor;
	COORD coord = { (SHORT)_x, (SHORT)_y };
	SetConsoleCursorInfo(_hOut, &_info);
	SetConsoleCursorPosition(_hOut, coord);
}

Coord GetCenterConsole()
{
	CONSOLE_SCREEN_BUFFER_INFO _csbi;
	int _columns, _rows;

	GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &_csbi);
	_columns = _csbi.srWindow.Right - _csbi.srWindow.Left;
	_rows = _csbi.srWindow.Bottom - _csbi.srWindow.Top;
	return { _columns, _rows };
}

bool CheckConsoleSize(Coord& _center, Coord& _previousCenter, const string& _text)
{
	const string& _errorText = "Agrandissez la taille de la console !";
	_center = GetCenterConsole();
	if (_previousCenter.x != _center.x || _previousCenter.y != _center.y) system("cls");

	if (_center.x < _text.size() || _center.y < 1)
	{
		if (_errorText.size() > _center.x)
		{
			system("cls");
			return true;
		}
		SetCursorPosition((_center.x - u_int(_errorText.size())) / 2, 0);
		cout << BLINK_COLOR(RED) << _errorText << RESET;
		_previousCenter = _center;
		return true;
	}
	return false;
}


void DisplayCenterLine(const string& _text, const Coord& _padding, const int _exitKey)
{
	int _key = 0;
	Coord _center = GetCenterConsole();
	Coord _previousCenter = _center;
	system("cls");
	do
	{
		if (CheckConsoleSize(_center, _previousCenter, _text)) continue;
		SetCursorPosition(((_center.x + _padding.x) - u_int(_text.size())) / 2, (_center.y + _padding.y) / 2);
		cout << _text;
		if (_kbhit())
		{
			_key = _getch();
		}
		_previousCenter = _center;
	} while (_key != _exitKey);
}

void DisplayCenterLineWithInput(const string& _text, const Coord& _padding, int& _input)
{
	Coord _center = GetCenterConsole();
	Coord _previousCenter = _center;
	system("cls");
	do
	{
		if (CheckConsoleSize(_center, _previousCenter, _text)) continue;
		SetCursorPosition(((_center.x + _padding.x) - u_int(_text.size())) / 2, (_center.y + _padding.y) / 2);
		cout << _text;
		if (_kbhit())
		{
			_input = _getch();
			return;
		}
		_previousCenter = _center;
	} while (true);
}

void DisplayCenterMultiLine(const string* _textArray, const u_int& _size, const Coord& _padding, const int _exitKey)
{
	int _key = 0;
	Coord _center = GetCenterConsole();
	Coord _previousCenter = _center;
	system("cls");
	do
	{
		for (u_int _index = 0; _index < _size; _index++)
		{
			if (CheckConsoleSize(_center, _previousCenter, _textArray[_index])) continue;
			SetCursorPosition(((_center.x + _padding.x) - u_int(_textArray[_index].size())) / 2, (_center.y + (2 * ((_padding.y - _size) / 2 + _index))) / 2);
			cout << _textArray[_index];
			if (_kbhit())
			{
				_key = _getch();
			}
			_previousCenter = _center;
		}
	} while (_key != _exitKey);
}

void DisplayCenterMultiLineWithInput(const string* _textArray, const u_int& _size, int& _input, const Coord& _padding)
{
	Coord _center = GetCenterConsole();
	Coord _previousCenter = _center;
	system("cls");
	do
	{
		for (u_int _index = 0; _index < _size; _index++)
		{
			if (CheckConsoleSize(_center, _previousCenter, _textArray[_index])) continue;
			SetCursorPosition(((_center.x + _padding.x) - u_int(_textArray[_index].size())) / 2, (_center.y + (2 * ((_padding.y - _size) / 2 + _index))) / 2);
			cout << _textArray[_index];
			if (_kbhit())
			{
				_input = _getch();
				return;
			}
			_previousCenter = _center;
		}
	} while (true);
}

void DisplayRainbowCenterLine(const string& _text, const Coord& _padding, const bool _sync, const int _exitKey)
{
	int _key = 0;
	Coord _center = GetCenterConsole();
	Coord _previousCenter = _center;
	system("cls");
	do
	{
		if (CheckConsoleSize(_center, _previousCenter, _text)) continue;
		SetCursorPosition(((_center.x + _padding.x) - u_int(_text.size())) / 2, (_center.y + _padding.y) / 2);
		cout << (_sync ? RainbowText(_text) : RainbowChar(_text));
		if (_kbhit())
		{
			_key = _getch();
		}
		_previousCenter = _center;
	} while (_key != _exitKey);
}

void DisplayRainbowCenterMultiLine(const string* _textArray, const u_int& _size, const Coord& _padding, const bool _sync, const int _exitKey)
{
	int _key = 0;
	Coord _center = GetCenterConsole();
	Coord _previousCenter = _center;
	system("cls");
	do
	{
		for (u_int _index = 0; _index < _size; _index++)
		{
			if (CheckConsoleSize(_center, _previousCenter, _textArray[_index])) continue;
			SetCursorPosition(((_center.x + _padding.x) - u_int(_textArray[_index].size())) / 2, (_center.y + (2 * ((_padding.y - _size) / 2 + _index))) / 2);
			cout << (_sync ? RainbowText(_textArray[_index]) : RainbowChar(_textArray[_index]));

			if (_kbhit())
			{
				_key = _getch();
			}
			_previousCenter = _center;
		}
	} while (_key != _exitKey);
}

void DisplayRainbowCenterMultiLineWithInput(const string* _textArray, const u_int& _size, int& _input, const Coord& _padding, const bool _sync)
{
	Coord _center = GetCenterConsole();
	Coord _previousCenter = _center;
	system("cls");
	do
	{
		for (u_int _index = 0; _index < _size; _index++)
		{
			if (CheckConsoleSize(_center, _previousCenter, _textArray[_index])) continue;
			SetCursorPosition(((_center.x + _padding.x) - u_int(_textArray[_index].size())) / 2, (_center.y + (2 * ((_padding.y - _size) / 2 + _index))) / 2);
			cout << (_sync ? RainbowText(_textArray[_index]) : RainbowChar(_textArray[_index]));

			if (_kbhit())
			{
				_input = _getch();
				return;
			}
			_previousCenter = _center;
		}
	} while (true);
}