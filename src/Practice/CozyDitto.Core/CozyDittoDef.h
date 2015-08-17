#ifndef __COZY_DITTO_DEF__
#define __COZY_DITTO_DEF__

#define MESSAGE_MAP_BEGIN LRESULT CALLBACK ProcessWindowMessage(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam) { switch(msg) {
#define MESSAGE_MAP_END default: return ::DefWindowProc(hWnd, msg, wParam, lParam); break; } return 0; }
#define MESSAGE_HANDLER(msg, action) case (msg) : action(hWnd, msg, wParam, lParam); break;

#endif // __COZY_DITTO_DEF__