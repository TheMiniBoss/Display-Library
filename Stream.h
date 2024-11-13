#pragma once

#include <iostream>
#include <fstream>
#include <string>

using namespace std;
using ib = ios_base;
using Stream = basic_ios<char, char_traits<char>>;

/// <summary>
/// Convertit en chemin d'acc�s
/// </summary>
/// <param name="_filePath">Nom du fichier</param>
/// <param name="_fileExtension">Extendion du fichier (sans le .)</param>
/// <param name="_folderPath">Chemin d'acc�s du dossier contenant le fichier</param>
/// <returns>Chemin d'acc�s</returns>
string ComputePath(const string& _filePath, const string& _fileExtension, const string& _folderPath = "");

/// <summary>
/// Affiche x lignes dans le contenu du fichier � partir de x lignes dans la console
/// </summary>
/// <param name="_filePath">Chemin d'acc�s du fichier</param>
/// <param name="_rowsCount">Nombre de ligne � afficher</param>
/// <param name="_startIndex">Ligne par o� commencer</param>
void DisplayByRows(const string& _filePath, const int _rowsCount, const int _startIndex);

/// <summary>
/// Affiche x caract�res dans le contenu du fichier � partir de x lignes dans la console
/// </summary>
/// <param name="_filePath">Chemin d'acc�s</param>
/// <param name="_charCount">Nombre de caract�re � afficher</param>
/// <param name="_startIndex">Caract�re par o� commencer</param>
void DisplayByChar(const string& _filePath, const int _charCount, const int _startIndex);

/// <summary>
/// Ajoute une donn�e � une ligne donn�e
/// </summary>
/// <param name="_filePath">Chemin d'acc�s</param>
/// <param name="_value">Valeur � ajouter</param>
/// <param name="_rowToAdd">Ligne o� on ajoute la valeur</param>
void AddValue(const string& _filePath, const string& _value, const int _rowToAdd = 0);
