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
	const u_int& _size = 3;
	const string _texts[] =
	{
		"Fin de partie !",
		"",
		"[ESC] pour quitter",
	};
	DisplayCenterMultiLine(_texts, _size);
}


int main()
{
	DemoGrid();
	//DemoText();
	//DemoText2();
	return EXIT_SUCCESS;
}