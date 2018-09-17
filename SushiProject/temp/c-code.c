#include <stdio.h>

extern char ch;
void find ();
void expression();
void factor();
void term();

int main()
{

find();

do {

expression();

putchar('\n');

} while (ch != '.');

return 0;
}
