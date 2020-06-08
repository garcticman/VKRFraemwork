using System;
using VKR;
using SDL2;

namespace Test
{
    class Program 
    {
        static int Main(string[] args)
        {
            Game game = new Game("Test", 640, 480, new MyScene());
            return game.Execute();
        }
    }

    class MyScene : Scene {
        Input input;
        Graphics graphics;
        Sprite testImage;
        Graphics.Image chara;

        public override void Start() {
            input = game.GetInput();
            graphics = game.GetGraphics();

            Console.WriteLine("Hello");

            testImage = new Sprite("hello.bmp", graphics);

            chara = new Graphics.Image("char.png", graphics);

            testImage.Image.SetPosition(new Point(-testImage.Image.GetStartingWidth() / 2, -testImage.Image.GetStartingHeight() / 2));
        }

        public override void Update() {
            graphics.ClearAll(255, 0, 0);

            if (InputEvent.IsKeyPressed(InputEvent.Keys.S)) {
                testImage.SetPosition(new Point(testImage.Position.X, testImage.Position.Y + 1));
            }
            if (InputEvent.IsKeyPressed(InputEvent.Keys.D)) {
                testImage.SetPosition(new Point(testImage.Position.X + 1, testImage.Position.Y));
            }
            if (InputEvent.IsKeyPressed(InputEvent.Keys.W)) {
                testImage.SetPosition(new Point(testImage.Position.X, testImage.Position.Y - 1));
            }
            if (InputEvent.IsKeyPressed(InputEvent.Keys.A)) {
                testImage.SetPosition(new Point(testImage.Position.X - 1, testImage.Position.Y));
            }

            if (InputEvent.IsMouseButtonPressed(InputEvent.MouseButtons.BUTTON_LEFT)) {
                testImage.SetPosition(new Point(InputEvent.GetMouseCoords().X, InputEvent.GetMouseCoords().Y));
            }
            if (InputEvent.IsMouseButtonPressed(InputEvent.MouseButtons.BUTTON_RIGHT)) {
                chara.SetPosition(new Point(InputEvent.GetMouseCoords().X, InputEvent.GetMouseCoords().Y));
            }

            testImage.Draw();

            chara.Draw();

            graphics.Render();
        }
        public override void OnKeyAction(InputEvent inputEvent) {
            if (inputEvent.IsKeyDown(InputEvent.Keys.V)) {
                Console.WriteLine("v - down");
            }
            if (inputEvent.IsKeyUp(InputEvent.Keys.V)) {
                Console.WriteLine("v - up");
            }
            if (inputEvent.IsKeyDown(InputEvent.Keys.B)) {
                Console.WriteLine("b - down");
            }
            if (inputEvent.IsKeyUp(InputEvent.Keys.B)) {
                Console.WriteLine("b - up");
            }

            if (inputEvent.IsKeyDown(InputEvent.Keys.space)) {
                chara.Width /= 5;
                chara.Height /= 5;
            }
        }

        Point p = new Point(0, 0);
        public override void OnMouseActing(InputEvent inputEvent) {
            if (inputEvent.IsMouseButtonDown(InputEvent.MouseButtons.BUTTON_RIGHT)) {
                p = testImage.Position;
                testImage.SetPosition(new Point(InputEvent.GetMouseCoords().X, InputEvent.GetMouseCoords().Y));
            } else if (inputEvent.IsMouseButtonUp(InputEvent.MouseButtons.BUTTON_RIGHT)) {
                testImage.SetPosition(p);
            }
        }
        public override void OnExit(InputEvent inputEvent) {
            game.Exit();
        }
        public override void Destroy() {
            
        }
    }
}
