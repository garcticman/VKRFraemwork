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
        Sprite square;

        public override void Start() {
            input = game.GetInput();
            graphics = game.GetGraphics();

            Console.WriteLine("Hello");

            testImage = new Sprite("charzera.png", graphics);
            testImage.FramesCount = 8;
            testImage.Image.SetPosition(new Point(-testImage.Image.Width / 2, -testImage.Image.Height / 2));

            square = new Sprite("frames.png", graphics);
            square.FramesCount = 4;
            square.Animate = true;
        }

        public override void Update() {
            graphics.ClearAll(255, 255, 255);

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

            testImage.Draw(new Point(0, 45), new Point(25, 45), 8);
            square.Draw(Point.Zero, new Point(32, 32), 64);

            graphics.Render();
        }
        public override void OnKeyAction(InputEvent inputEvent) {
            if (inputEvent.IsKeyDown(InputEvent.Keys.V)) {
                Console.WriteLine("v - down");
                testImage.Image.Crop(Point.Zero, new Point(25, 45));
                testImage.Image.Width = 25 * 2;
                testImage.Image.Height = 45 * 2;
                
                testImage.Image.SetPosition(testImage.Position - new Point(testImage.Image.Width, testImage.Image.Height) / 2);
                testImage.Animate = true;
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
            if (inputEvent.IsKeyDown(InputEvent.Keys.A)) {
                testImage.Image.Flip(Graphics.Image.FlipType.Horizontal);
            } else if (inputEvent.IsKeyDown(InputEvent.Keys.D)) {
                testImage.Image.Flip(Graphics.Image.FlipType.None);
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
