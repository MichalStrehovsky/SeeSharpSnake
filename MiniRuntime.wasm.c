#include <emscripten.h>

struct ReversePInvokeFrame
{
    void*   m_savedPInvokeTransitionFrame;
    void* m_savedThread;
};

void RhpReversePInvoke2(struct ReversePInvokeFrame *frame) { }

extern int __managed__Main(int, void*);
int main(int argc, char **argv)
{
    return __managed__Main(argc, (char *)argv);
}

void RhpPInvoke2(void *p) {}
void RhpPInvokeReturn2(void *p) {}
