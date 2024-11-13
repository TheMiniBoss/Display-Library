#pragma once

#include <iostream>
#include <fstream>
#include <string>

using namespace std;
using ib = ios_base;
using Stream = basic_ios<char, char_traits<char>>;

/// <summary>
/// Convertit en chemin d'accès
/// </summary>
/// <param name="_filePath">Nom du fichier</param>
/// <param name="_fileExtension">Extendion du fichier (sans le .)</param>
/// <param name="_folderPath">Chemin d'accès du dossier contenant le fichier</param>
/// <returns>Chemin d'accès</returns>
string ComputePath(const string& _filePath, const string& _fileExtension, const string& _folderPath = "");

/// <summary>
/// Affiche x lignes dans le contenu du fichier à partir de x lignes dans la console
/// </summary>
/// <param name="_filePath">Chemin d'accès du fichier</param>
/// <param name="_rowsCount">Nombre de ligne à afficher</param>
/// <param name="_startIndex">Ligne par où commencer</param>
void DisplayByRows(const string& _filePath, const int _rowsCount, const int _startIndex);

/// <summary>
/// Affiche x caractères dans le contenu du fichier à partir de x lignes dans la console
/// </summary>
/// <param name="_filePath">Chemin d'accès</param>
/// <param name="_charCount">Nombre de caractère à afficher</param>
/// <param name="_startIndex">Caractère par où commencer</param>
void DisplayByChar(const string& _filePath, const int _charCount, const int _startIndex);

/// <summary>
/// Ajoute une donnée à une ligne donnée
/// </summary>
/// <param name="_filePath">Chemin d'accès</param>
/// <param name="_value">Valeur à ajouter</param>
/// <param name="_rowToAdd">Ligne où on ajoute la valeur</param>
void AddValue(const string& _filePath, const string& _value, const int _rowToAdd = 0);
