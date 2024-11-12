#pragma once
#include <iostream>
#include <string>
#include <conio.h>
#include <random>

#define new new(_NORMAL_BLOCK, __FILE__, __LINE__)
#define CLEAR  cin.clear();\
               cin.ignore(numeric_limits<streamsize>::max(), '\n');

typedef unsigned int u_int;

using namespace std;

void Config();
