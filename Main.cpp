#include <iostream>
#include <fstream>
#include <string>

#include "DisplaySystem.h"

using namespace std;
using namespace Display;

void AddValueToArray(string*& _array, const int _arraySize, string _value)
{
	//construit un tab _size + 1
	string* _newArray = new string[_arraySize + 1];

	//init le nouveau tableau de toutes les anciennes valeurs
	for (int _index = 0; _index < _arraySize; _index++)
	{
		_newArray[_index] = _array[_index];
	}

	//on affecte à la dernière addresse la nouvelle valeur
	_newArray[_arraySize] = _value;

	//désallouer l'ancien tableau
	delete[] _array;

	//retourne le nouveau tableau
	/*return _newArray*/;
	_array = _newArray;
}

void DemoThomas()
{
	ifstream _stream = ifstream("Thomas.txt");
	string _text;
	int _index = 0;
	string* _grid = nullptr;
	while (getline(_stream, _text))
	{
		AddValueToArray(_grid, _index, _text);
		_index++;
	}

	DisplayCenterMultiLine(_grid, _index);

	_stream.close();
}

void DemoGrid()
{
	ifstream _stream = ifstream("O3D.txt");
	string _text;
	int _index = 0;
	string* _grid = nullptr;
	while (getline(_stream, _text))
	{
		AddValueToArray(_grid, _index, _text);
		_index++;
	}

	DisplayRainbowCenterMultiLine(_grid, _index);

	_stream.close();
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
		"1- O3D Arc-en-ciel",
		"2- ASCII Art      ",
		"3- Texte Simple   ",
		"4- Texte Multiple ",
		"5- Quitter        ",
	};
	int _value;
	DisplayCenterMultiLineWithInput(_texts, size(_texts), _value);

	switch (_value)
	{
	case '1':
		DemoGrid();
		break;
	case '2':
		DemoThomas();
		break;
	case '3':
		DemoText();
		break;
	case '4':
		DemoText2();
		break;
	case '5':
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