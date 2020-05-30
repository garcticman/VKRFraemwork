using System;
using VKR;

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
        Graphics.Image testImage;
        Graphics.Image chara;

        public override void Start() {
            input = game.GetInput();
            graphics = game.GetGraphics();

            Console.WriteLine("Hello");

            testImage = new Graphics.Image("hello.bmp", graphics);

            chara = new Graphics.Image("char.png", graphics);
        }

        public override void Update() {
            if(input.IsExit()) 
                game.Exit();

            graphics.ClearAll(255, 0, 0);
            testImage.Draw(0, 0);

            chara.Draw(0, 0, 500, 500);
            graphics.Render();
        }

        public override void Destroy() {
            
        }
    }
}
