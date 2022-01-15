#include <emscripten.h>

struct ReversePInvokeFrame
{
    void*   m_savedPInvokeTransitionFrame;
    void* m_savedThread;
};

static int alloced = 0;

void* malloc()
{
// this is our shadow stack, no other calls to malloc are allowed
  if(alloced)
  {
    return (void*)0x100000;
  }
  alloced = 1;
  return (void*)0xFC00;
}

void RhpReversePInvoke2(struct ReversePInvokeFrame *frame) { }

void RhpPInvoke2(void *p) {}
void RhpPInvokeReturn2(void *p) {}
void RhpReversePInvokeReturn2(struct ReversePInvokeFrame *frame) {}
