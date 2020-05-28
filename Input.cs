using SDL2;

namespace VKR {
    class Input {
        SDL.SDL_Event evnt;

        public void Update() {
            while(SDL.SDL_PollEvent(out evnt) != 0);
        }

        public bool IsMouseButtonDown(byte key) {
            return (evnt.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && evnt.button.button == key);
        }

        public bool IsMouseButtonUp(byte key) {
            return (evnt.type == SDL.SDL_EventType.SDL_MOUSEBUTTONUP && evnt.button.button == key);
        }

        public Point GetButtonDownCoords(){
            return new Point(evnt.button.x, evnt.button.y);
        }

        public bool IsKeyPressed(byte key) {
            return (evnt.type == SDL.SDL_EventType.SDL_KEYDOWN && (byte)evnt.key.keysym.sym == key);
        }

        public byte GetPressedKey() {
            return (byte)evnt.key.keysym.sym;
        }

        public bool IsExit() {
            return (evnt.type == SDL.SDL_EventType.SDL_QUIT);
        }
    }
}
