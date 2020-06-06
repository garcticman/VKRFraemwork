using System;
using SDL2;

namespace VKR {
    class Input {
        public delegate void InputHandler(InputEvent iEvent);
        public event InputHandler keyHandler;
        public event InputHandler quitHandler;
		public event InputHandler mouseHandler;
        InputEvent inputEvent;
        public Input() {
            inputEvent = new InputEvent();
        }
        public void Update() {
            while(inputEvent.PollEvent()) {
                if(inputEvent.IsExit()) {
                    quitHandler(inputEvent);
                }
                if(inputEvent.AnyKeyPressed()) {
                    keyHandler(inputEvent);
                }
				if(inputEvent.IsMouseActing()) {
					mouseHandler(inputEvent);
				}
            }
        }
    }

    class InputEvent {
        public enum Keys {
            UNKNOWN = SDL.SDL_Scancode.SDL_SCANCODE_UNKNOWN,

			A = SDL.SDL_Scancode.SDL_SCANCODE_A,
			B = SDL.SDL_Scancode.SDL_SCANCODE_B,
			C = SDL.SDL_Scancode.SDL_SCANCODE_C,
			D = SDL.SDL_Scancode.SDL_SCANCODE_D,
			E = SDL.SDL_Scancode.SDL_SCANCODE_E,
			F = SDL.SDL_Scancode.SDL_SCANCODE_F,
			G = SDL.SDL_Scancode.SDL_SCANCODE_G,
			H = SDL.SDL_Scancode.SDL_SCANCODE_H,
			I = SDL.SDL_Scancode.SDL_SCANCODE_I,
			J = SDL.SDL_Scancode.SDL_SCANCODE_J,
			K = SDL.SDL_Scancode.SDL_SCANCODE_K,
			L = SDL.SDL_Scancode.SDL_SCANCODE_L,
			M = SDL.SDL_Scancode.SDL_SCANCODE_M,
			N = SDL.SDL_Scancode.SDL_SCANCODE_N,
			O = SDL.SDL_Scancode.SDL_SCANCODE_O,
			P = SDL.SDL_Scancode.SDL_SCANCODE_P,
			Q = SDL.SDL_Scancode.SDL_SCANCODE_Q,
			R = SDL.SDL_Scancode.SDL_SCANCODE_R,
			S = SDL.SDL_Scancode.SDL_SCANCODE_S,
			T = SDL.SDL_Scancode.SDL_SCANCODE_T,
			U = SDL.SDL_Scancode.SDL_SCANCODE_U,
			V = SDL.SDL_Scancode.SDL_SCANCODE_V,
			W = SDL.SDL_Scancode.SDL_SCANCODE_W,
			X = SDL.SDL_Scancode.SDL_SCANCODE_X,
			Y = SDL.SDL_Scancode.SDL_SCANCODE_Y,
            Z = SDL.SDL_Scancode.SDL_SCANCODE_Z,

            _1 = SDL.SDL_Scancode.SDL_SCANCODE_1,
			_2 = SDL.SDL_Scancode.SDL_SCANCODE_2,
			_3 = SDL.SDL_Scancode.SDL_SCANCODE_3,
			_4 = SDL.SDL_Scancode.SDL_SCANCODE_4,
			_5 = SDL.SDL_Scancode.SDL_SCANCODE_5,
			_6 = SDL.SDL_Scancode.SDL_SCANCODE_6,
			_7 = SDL.SDL_Scancode.SDL_SCANCODE_7,
			_8 = SDL.SDL_Scancode.SDL_SCANCODE_8,
			_9 = SDL.SDL_Scancode.SDL_SCANCODE_9,
			_0 = SDL.SDL_Scancode.SDL_SCANCODE_0,

			_return = SDL.SDL_Scancode.SDL_SCANCODE_RETURN,
			escape = SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE,
			bakcspace = SDL.SDL_Scancode.SDL_SCANCODE_BACKSPACE,
			tab = SDL.SDL_Scancode.SDL_SCANCODE_TAB,
			space = SDL.SDL_Scancode.SDL_SCANCODE_SPACE,

			minus = SDL.SDL_Scancode.SDL_SCANCODE_MINUS,
			equals = SDL.SDL_Scancode.SDL_SCANCODE_EQUALS,
			leftbucket = SDL.SDL_Scancode.SDL_SCANCODE_LEFTBRACKET,
			rightbacket = SDL.SDL_Scancode.SDL_SCANCODE_RIGHTBRACKET,
			backslash = SDL.SDL_Scancode.SDL_SCANCODE_BACKSLASH,
			nonushah = SDL.SDL_Scancode.SDL_SCANCODE_NONUSHASH,
			semicolon = SDL.SDL_Scancode.SDL_SCANCODE_SEMICOLON,
			apostrophe = SDL.SDL_Scancode.SDL_SCANCODE_APOSTROPHE,
			grave = SDL.SDL_Scancode.SDL_SCANCODE_GRAVE,
			comma = SDL.SDL_Scancode.SDL_SCANCODE_COMMA,
			period = SDL.SDL_Scancode.SDL_SCANCODE_PERIOD,
			slash = SDL.SDL_Scancode.SDL_SCANCODE_SLASH,

			capslock = SDL.SDL_Scancode.SDL_SCANCODE_CAPSLOCK,

			f1 = SDL.SDL_Scancode.SDL_SCANCODE_F1,
			f2 = SDL.SDL_Scancode.SDL_SCANCODE_F2,
			f3 = SDL.SDL_Scancode.SDL_SCANCODE_F3,
			f4 = SDL.SDL_Scancode.SDL_SCANCODE_F4,
			f5 = SDL.SDL_Scancode.SDL_SCANCODE_F5,
			f6 = SDL.SDL_Scancode.SDL_SCANCODE_F6,
			f7 = SDL.SDL_Scancode.SDL_SCANCODE_F7,
			f8 = SDL.SDL_Scancode.SDL_SCANCODE_F8,
			f9 = SDL.SDL_Scancode.SDL_SCANCODE_F9,
			f10 = SDL.SDL_Scancode.SDL_SCANCODE_F10,
			f11 = SDL.SDL_Scancode.SDL_SCANCODE_F11,
			f12 = SDL.SDL_Scancode.SDL_SCANCODE_F12,

			printscreen = SDL.SDL_Scancode.SDL_SCANCODE_PRINTSCREEN,
			scrolllock = SDL.SDL_Scancode.SDL_SCANCODE_SCROLLLOCK,
			pause = SDL.SDL_Scancode.SDL_SCANCODE_PAUSE,
			insert = SDL.SDL_Scancode.SDL_SCANCODE_INSERT,
			home = SDL.SDL_Scancode.SDL_SCANCODE_HOME,
			pageup = SDL.SDL_Scancode.SDL_SCANCODE_PAGEUP,
			delete = SDL.SDL_Scancode.SDL_SCANCODE_DELETE,
			end = SDL.SDL_Scancode.SDL_SCANCODE_END,
			pagedown = SDL.SDL_Scancode.SDL_SCANCODE_PAGEDOWN,
			right = SDL.SDL_Scancode.SDL_SCANCODE_RIGHT,
			left = SDL.SDL_Scancode.SDL_SCANCODE_LEFT,
			down = SDL.SDL_Scancode.SDL_SCANCODE_DOWN,
			up = SDL.SDL_Scancode.SDL_SCANCODE_UP,

			monusbackslash = SDL.SDL_Scancode.SDL_SCANCODE_NONUSBACKSLASH,
			application = SDL.SDL_Scancode.SDL_SCANCODE_APPLICATION,
			power = SDL.SDL_Scancode.SDL_SCANCODE_POWER,
		
			/* not sure whether there's a reason to enable these */
			/*	SDL_SCANCODE_LOCKINGCAPSLOCK = 130, */
			/*	SDL_SCANCODE_LOCKINGNUMLOCK = 131, */
			/*	SDL_SCANCODE_LOCKINGSCROLLLOCK = 132, */
			SDL_SCANCODE_KP_COMMA = 133,
			SDL_SCANCODE_KP_EQUALSAS400 = 134,

			SDL_SCANCODE_INTERNATIONAL1 = 135,
			SDL_SCANCODE_INTERNATIONAL2 = 136,
			SDL_SCANCODE_INTERNATIONAL3 = 137,
			SDL_SCANCODE_INTERNATIONAL4 = 138,
			SDL_SCANCODE_INTERNATIONAL5 = 139,
			SDL_SCANCODE_INTERNATIONAL6 = 140,
			SDL_SCANCODE_INTERNATIONAL7 = 141,
			SDL_SCANCODE_INTERNATIONAL8 = 142,
			SDL_SCANCODE_INTERNATIONAL9 = 143,
			SDL_SCANCODE_LANG1 = 144,
			SDL_SCANCODE_LANG2 = 145,
			SDL_SCANCODE_LANG3 = 146,
			SDL_SCANCODE_LANG4 = 147,
			SDL_SCANCODE_LANG5 = 148,
			SDL_SCANCODE_LANG6 = 149,
			SDL_SCANCODE_LANG7 = 150,
			SDL_SCANCODE_LANG8 = 151,
			SDL_SCANCODE_LANG9 = 152,

			SDL_SCANCODE_ALTERASE = 153,
			SDL_SCANCODE_SYSREQ = 154,
			SDL_SCANCODE_CANCEL = 155,
			SDL_SCANCODE_CLEAR = 156,
			SDL_SCANCODE_PRIOR = 157,
			SDL_SCANCODE_RETURN2 = 158,
			SDL_SCANCODE_SEPARATOR = 159,
			SDL_SCANCODE_OUT = 160,
			SDL_SCANCODE_OPER = 161,
			SDL_SCANCODE_CLEARAGAIN = 162,
			SDL_SCANCODE_CRSEL = 163,
			SDL_SCANCODE_EXSEL = 164,

			SDL_SCANCODE_KP_00 = 176,
			SDL_SCANCODE_KP_000 = 177,
			SDL_SCANCODE_THOUSANDSSEPARATOR = 178,
			SDL_SCANCODE_DECIMALSEPARATOR = 179,
			SDL_SCANCODE_CURRENCYUNIT = 180,
			SDL_SCANCODE_CURRENCYSUBUNIT = 181,
			SDL_SCANCODE_KP_LEFTPAREN = 182,
			SDL_SCANCODE_KP_RIGHTPAREN = 183,
			SDL_SCANCODE_KP_LEFTBRACE = 184,
			SDL_SCANCODE_KP_RIGHTBRACE = 185,
			SDL_SCANCODE_KP_TAB = 186,
			SDL_SCANCODE_KP_BACKSPACE = 187,
			SDL_SCANCODE_KP_A = 188,
			SDL_SCANCODE_KP_B = 189,
			SDL_SCANCODE_KP_C = 190,
			SDL_SCANCODE_KP_D = 191,
			SDL_SCANCODE_KP_E = 192,
			SDL_SCANCODE_KP_F = 193,
			SDL_SCANCODE_KP_XOR = 194,
			SDL_SCANCODE_KP_POWER = 195,
			SDL_SCANCODE_KP_PERCENT = 196,
			SDL_SCANCODE_KP_LESS = 197,
			SDL_SCANCODE_KP_GREATER = 198,
			SDL_SCANCODE_KP_AMPERSAND = 199,
			SDL_SCANCODE_KP_DBLAMPERSAND = 200,
			SDL_SCANCODE_KP_VERTICALBAR = 201,
			SDL_SCANCODE_KP_DBLVERTICALBAR = 202,
			SDL_SCANCODE_KP_COLON = 203,
			SDL_SCANCODE_KP_HASH = 204,
			SDL_SCANCODE_KP_SPACE = 205,
			SDL_SCANCODE_KP_AT = 206,
			SDL_SCANCODE_KP_EXCLAM = 207,
			SDL_SCANCODE_KP_MEMSTORE = 208,
			SDL_SCANCODE_KP_MEMRECALL = 209,
			SDL_SCANCODE_KP_MEMCLEAR = 210,
			SDL_SCANCODE_KP_MEMADD = 211,
			SDL_SCANCODE_KP_MEMSUBTRACT = 212,
			SDL_SCANCODE_KP_MEMMULTIPLY = 213,
			SDL_SCANCODE_KP_MEMDIVIDE = 214,
			SDL_SCANCODE_KP_PLUSMINUS = 215,
			SDL_SCANCODE_KP_CLEAR = 216,
			SDL_SCANCODE_KP_CLEARENTRY = 217,
			SDL_SCANCODE_KP_BINARY = 218,
			SDL_SCANCODE_KP_OCTAL = 219,
			SDL_SCANCODE_KP_DECIMAL = 220,
			SDL_SCANCODE_KP_HEXADECIMAL = 221,

			SDL_SCANCODE_LCTRL = 224,
			SDL_SCANCODE_LSHIFT = 225,
			SDL_SCANCODE_LALT = 226,
			SDL_SCANCODE_LGUI = 227,
			SDL_SCANCODE_RCTRL = 228,
			SDL_SCANCODE_RSHIFT = 229,
			SDL_SCANCODE_RALT = 230,
			SDL_SCANCODE_RGUI = 231,

			SDL_SCANCODE_MODE = 257,

			/* These come from the USB consumer page (0x0C) */
			SDL_SCANCODE_AUDIONEXT = 258,
			SDL_SCANCODE_AUDIOPREV = 259,
			SDL_SCANCODE_AUDIOSTOP = 260,
			SDL_SCANCODE_AUDIOPLAY = 261,
			SDL_SCANCODE_AUDIOMUTE = 262,
			SDL_SCANCODE_MEDIASELECT = 263,
			SDL_SCANCODE_WWW = 264,
			SDL_SCANCODE_MAIL = 265,
			SDL_SCANCODE_CALCULATOR = 266,
			SDL_SCANCODE_COMPUTER = 267,
			SDL_SCANCODE_AC_SEARCH = 268,
			SDL_SCANCODE_AC_HOME = 269,
			SDL_SCANCODE_AC_BACK = 270,
			SDL_SCANCODE_AC_FORWARD = 271,
			SDL_SCANCODE_AC_STOP = 272,
			SDL_SCANCODE_AC_REFRESH = 273,
			SDL_SCANCODE_AC_BOOKMARKS = 274,

			/* These come from other sources, and are mostly mac related */
			SDL_SCANCODE_BRIGHTNESSDOWN = 275,
			SDL_SCANCODE_BRIGHTNESSUP = 276,
			SDL_SCANCODE_DISPLAYSWITCH = 277,
			SDL_SCANCODE_KBDILLUMTOGGLE = 278,
			SDL_SCANCODE_KBDILLUMDOWN = 279,
			SDL_SCANCODE_KBDILLUMUP = 280,
			SDL_SCANCODE_EJECT = 281,
			SDL_SCANCODE_SLEEP = 282,

			SDL_SCANCODE_APP1 = 283,
			SDL_SCANCODE_APP2 = 284,

			/* These come from the USB consumer page (0x0C) */
			SDL_SCANCODE_AUDIOREWIND = 285,
			SDL_SCANCODE_AUDIOFASTFORWARD = 286,

			/* This is not a key, simply marks the number of scancodes
			 * so that you know how big to make your arrays. */
			SDL_NUM_SCANCODES = 512
            
        }
        public enum MouseButtons : uint {
			BUTTON_LEFT = SDL.SDL_BUTTON_LEFT,
			BUTTON_MIDDLE = SDL.SDL_BUTTON_MIDDLE,
			BUTTON_RIGHT = SDL.SDL_BUTTON_RIGHT,
			BUTTON_X1 = SDL.SDL_BUTTON_X1,
			BUTTON_X2 = SDL.SDL_BUTTON_X2
		}
		SDL.SDL_Event evnt;
        static int state = 0;
        public bool PollEvent() {
			SDL.SDL_PumpEvents();

            if (SDL.SDL_PollEvent(out evnt) != 0) {
                return true;
            }
            return false;
        }

		public bool IsMouseActing() {
			return (evnt.type == SDL.SDL_EventType.SDL_MOUSEBUTTONUP || evnt.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN
				|| evnt.type == SDL.SDL_EventType.SDL_MOUSEMOTION);
		}
		public bool IsMouseButtonDown(MouseButtons button) {
			return (evnt.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && evnt.button.button == (byte)button);
		}
		public bool IsMouseButtonUp(MouseButtons button) {
			return (evnt.type == SDL.SDL_EventType.SDL_MOUSEBUTTONUP && evnt.button.button == (byte)button);
		}

		public static bool IsMouseButtonPressed(MouseButtons button) {
			return (SDL.SDL_BUTTON((uint)button) & SDL.SDL_GetMouseState(IntPtr.Zero, IntPtr.Zero)) > 0;
		}
		public static Point GetMouseCoords() {
			int x, y;
			SDL.SDL_GetMouseState(out x, out y);
			return new Point(x, y);
		}

        public bool AnyKeyPressed() {
            if (evnt.key.keysym.sym != 0) 
                return true;
            return false;
        }
        public bool IsKeyDown(Keys key) {
            return (evnt.type == SDL.SDL_EventType.SDL_KEYDOWN && evnt.key.repeat == 0 
				&& evnt.key.keysym.scancode == (SDL.SDL_Scancode)key);
        }
        public bool IsKeyUp(Keys key) {
            return (evnt.type == SDL.SDL_EventType.SDL_KEYUP && evnt.key.repeat == 0 
				&& evnt.key.keysym.scancode == (SDL.SDL_Scancode)key);
        }

        public static bool IsKeyPressed(Keys key) {
            var keyState = getKeyboardState();

            if (keyState != IntPtr.Zero) {
                unsafe {
                    var p = (byte*)keyState.ToPointer();
                    if (p[(int)key] != 0) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsExit() {
            return (evnt.type == SDL.SDL_EventType.SDL_QUIT);
        }
        static IntPtr getKeyboardState() {
            return SDL.SDL_GetKeyboardState( out state );
        }
    }
}
