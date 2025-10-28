#include "SysTypes.h"

__declspec(dllexport) u08* Add(u08* a, u08* b)
{
	int res = *a + *b;

	return &res;
}