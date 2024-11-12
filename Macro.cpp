#include "Macro.h"
#include "Random.h"


void Config()
{
	locale::global(locale("fr_FR"));
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
}
