#include "Stream.h"

string ComputePath(const string& _filePath, const string& _fileExtension, const string& _folderPath)
{
	return _folderPath + _filePath + "." + _fileExtension;
}

void DisplayByRows(const string& _filePath, const int _rowsCount, const int _startIndex)
{
	ifstream _stream = ifstream(_filePath);
	if (!_stream)
	{
		cerr << "Error -> the file : " << _filePath << " doesn't exists !" << endl;
		return;
	}

	string _line;
	for (int _index = _startIndex; _index < _rowsCount + _startIndex; _index++)
	{
		getline(_stream, _line);
		if (_index >= _startIndex) cout << _line << endl;
	}
}

void DisplayByChar(const string& _filePath, const int _charCount, const int _startIndex)
{
	ifstream _stream = ifstream(_filePath);
	char _char;
	int _index = 0;

	_stream.seekg(streampos(_startIndex - 1));
	for (int _index = 0; _index < _charCount; _index++)
	{
		_stream.get(_char);
		cout << _char;
	}
}

int ComputeLengthOfFile(const string& _filePath)
{
	ifstream _stream = ifstream(_filePath);
	_stream.seekg(0, _stream.end);
	return _stream.tellg();
}

int SetCursorPositionByRow(const string& _filePath, const int _rowToMove)
{
	ifstream _stream = ifstream(_filePath);
	const int _fileLength = ComputeLengthOfFile(_filePath);
	char _char;
	int _currentRow = 0;

	for (int _index = 0; _index < _fileLength; _index++)
	{
		_stream.get(_char);
		if (_char == '\n' && ++_currentRow >= _rowToMove) return _index;
	}
}

string ComputeBufferAtIndex(const string& _filePath, const int _startIndex)
{
	ifstream _stream = ifstream(_filePath);

	// On définit la taille du fichier restant
	_stream.seekg(_startIndex, _stream.end);
	const int _length = _stream.tellg();
	_stream.seekg(0, _stream.beg);

	//On créer un tableau
	char* _buffer = new char[_length];
	_stream.read(_buffer, _length);

	string _text = _buffer;

	delete[] _buffer;
	return _text;
}

void AddValue(const string& _filePath, const string& _value, const int _rowToAdd)
{
	ofstream _stream = ofstream(_filePath, ib::ate);
	const int _startIndex = SetCursorPositionByRow(_filePath, _rowToAdd);

	_stream.seekp(streampos(_startIndex));
	_stream.seekp(0, _stream.beg);
	cout << _value << endl;
}