#include "Display.h"

void DemoGrid()
{
	string* _grid = new string[25];
	for (u_int _row = 0; _row < 25; _row++)
	{
		_grid[_row] = "......................................................";
	}

	DisplayRainbowCenterMultiLine(_grid, 25);

	delete[] _grid;

}

void DemoText()
{
	const string& _text = "Fin de partie !    [ESC] pour quitter";
	DisplayRainbowCenterLine(_text);
}

void DemoText2()
{
	const u_int& _size = 4;
	const string _texts[] =
	{
		"Fin de partie !",
		"",
		"",
		"[ESC] pour quitter",
	};
	DisplayCenterMultiLine(_texts, _size);
}

void Demo()
{
	const string _texts[] =
	{
		"Choisissez une démo :",
		"",
		"1- Grille Arc-en-ciel",
		"2- Texte Simple      ",
		"3- Texte Multiple    ",
		"4- Quitter           ",
	};
	int _value;
	DisplayCenterMultiLineWithInput(_texts, size(_texts), _value);

	switch (_value)
	{
	case '1':
		DemoGrid();
		break;
	case '2':
		DemoText();
		break;
	case '3':
		DemoText2();
		break;
	case '4':
		system("cls");
		return;
		break;
	default:
		return Demo();
	}
	return Demo();
}

int main()
{
	locale::global(std::locale(""));

	Demo();
	return EXIT_SUCCESS;
}