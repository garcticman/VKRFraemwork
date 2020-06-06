using SDL2;

namespace VKR {
    class Game {
        bool run;
        Graphics graphics;
        Input input;
        Scene scene;

        public Game(string title, int width, int height, Scene firstScene) {
            graphics = new Graphics(title, width, height);
            input = new Input();
            scene = firstScene;

            scene.SetController(this);

            input.keyHandler += scene.OnKeyAction;
            input.quitHandler += scene.OnExit;
            input.mouseHandler += scene.OnMouseActing;

            run = true;
        }

        public int Execute() {
            scene.Start();

            while(run) {
                input.Update();
                scene.Update();
            }

            scene.Destroy();
            SDL.SDL_Quit();
            return 0;
        }

        public Graphics GetGraphics() {
            return graphics;
        }

        public Input GetInput() {
            return input;
        }

        public Scene GetScene() {
            return scene;
        }

        public void SetScene(Scene nextScene) {
            scene.Destroy();
            scene = nextScene;
            scene.SetController(this);
            scene.Start();
        }

        public void Exit() {
            run = false;
        }
    }
}