using SDL2;

namespace VKR {
    class Game {
        bool run;
        Graphics graphics;
        Input input;

        public Game(string title, int width, int height) {
            graphics = new Graphics(title, width, height);
            input = new Input();

            run = true;
        }

        public int Execute() {
            while(run) {
                input.Update();
            }

            SDL.SDL_Quit();
            return 0;
        }

        Graphics GetGraphics() {
            return graphics;
        }

        Input GetInput() {
            return input;
        }

        void Exit() {
            run = false;
        }
    }
}