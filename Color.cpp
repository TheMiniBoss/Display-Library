#include "Color.h"
#include "Random.h"

string RainbowChar(const string& _text)
{
	const string _rainbowTable[] =
	{
		DARK_RED,
		RED ,
		DARK_ORANGE,
		ORANGE,
		DARK_YELLOW,
		YELLOW,
		LIME,
		GREEN,
		BLUE,
		LIGHT_BLUE,
		CYAN,
		PINK,
		MAGENTA,
		PURPLE,
		BROWN
	};

	const u_int& _rainbowSize = size(_rainbowTable);
	string _newWord = "";
	for (u_int _i = 0; _i < _text.length(); _i++)
	{
		_newWord += _rainbowTable[GetRandomNumberInRange(0, _rainbowSize - 1)] + _text[_i];
	}

	return _newWord + RESET;
}

string RainbowText(const string& _text)
{
	const string _rainbowTable[] =
	{
		DARK_RED,
		RED ,
		DARK_ORANGE,
		ORANGE,
		DARK_YELLOW,
		YELLOW,
		LIME,
		GREEN,
		BLUE,
		LIGHT_BLUE,
		CYAN,
		PINK,
		MAGENTA,
		PURPLE,
		BROWN
	};

	const u_int& _rainbowSize = size(_rainbowTable);
	string _newWord = _rainbowTable[GetRandomNumberInRange(0, _rainbowSize - 1)] + _text;
	return _newWord + RESET;
}

string GetRainbowColor()
{
	const string _rainbowTable[] =
	{
		DARK_RED,
		RED ,
		DARK_ORANGE,
		ORANGE,
		DARK_YELLOW,
		YELLOW,
		LIME,
		GREEN,
		BLUE,
		LIGHT_BLUE,
		CYAN,
		PINK,
		MAGENTA,
		PURPLE,
		BROWN
	};

	const u_int& _rainbowSize = size(_rainbowTable);
	return _rainbowTable[GetRandomNumberInRange(0, _rainbowSize - 1)];
}