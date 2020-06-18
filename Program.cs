using System;
using VKR;
using SDL2;

namespace Test
{
    class Program 
    {
        static int Main(string[] args)
        {
            // Game game = new Game("Test", 640, 480, new MyScene());
            // return game.Execute();

            float[][] array = new float[5][];
            for(int i = 0; i < 5; i++) {
                array[i] = new float[5] { 1 + i, 2 - i, 5 + i *2, 4 + i, 5};
            }

            SortMatrix(array);

            return 0;
        }

        static float[][] SortMatrix(float[][] matrix) {
            Element[] rows = new Element[matrix.Length];
            float[][] result = matrix;

            for (int i = 0; i < matrix.Length; i++) {
                rows[i] = new Element();
                rows[i].row = i;

                for (int j = 0; j < matrix[i].Length; j++) {
                    rows[i].sum += matrix[i][j];
                }
            }

            for (int i = 0; i < rows.Length; i++) {
                for (int j = 0; j < rows.Length - 1; j++) {
                    if (rows[j].sum < rows[j+1].sum) {
                        var tmp = rows[j];
                        rows[j] = rows[j+1];
                        rows[j+1] = tmp;
                    }
                }
            }

            for (int i = 0; i < rows.Length; i++) {
                result[rows[i].row] = matrix[i];
                Console.WriteLine(rows[i].sum);
            }

            return result;
        }
    }

    class Element {
        public int row;
        public float sum;
    }

    class MyScene : Scene {
        Input input;
        Graphics graphics;
        Sprite testImage;
        Sprite square;
        Sprite testFrames;
        bool isRun;

        Graphics.Text disney;

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

            testFrames = new Sprite("res/char_idle/adventurer-idle-00.png", graphics);
            testFrames.Image.Height *= 2;
            testFrames.Image.Width *= 2;
            testFrames.Animate = true;

            Animation anim_idle = new Animation("idle", testFrames);
            anim_idle.AddFrame("res/char_idle/adventurer-idle-00.png");
            anim_idle.AddFrame("res/char_idle/adventurer-idle-01.png");
            anim_idle.AddFrame("res/char_idle/adventurer-idle-02.png");

            Animation anim_run = new Animation("run", testFrames);
            anim_run.AddFrame("res/char_run/adventurer-run-00.png");
            anim_run.AddFrame("res/char_run/adventurer-run-01.png");
            anim_run.AddFrame("res/char_run/adventurer-run-02.png");
            anim_run.AddFrame("res/char_run/adventurer-run-03.png");
            anim_run.AddFrame("res/char_run/adventurer-run-04.png");
            anim_run.AddFrame("res/char_run/adventurer-run-05.png");

            disney = new Graphics.Text("res/disney.ttf", "Hello world", 28, graphics);
            disney.Color = new Color(0, 0, 0, 200);
        }

        public override void Update() {
            graphics.ClearAll(255, 255, 255);

            isRun = false;

            if (InputEvent.IsKeyPressed(InputEvent.Keys.S)) {
                testImage.SetPosition(new Point(testImage.Position.X, testImage.Position.Y + 1));

                testFrames.SetPosition(new Point(testFrames.Position.X, testFrames.Position.Y + 2));

                isRun = true;
            }
            if (InputEvent.IsKeyPressed(InputEvent.Keys.D)) {
                testImage.SetPosition(new Point(testImage.Position.X + 1, testImage.Position.Y));

                testFrames.SetPosition(new Point(testFrames.Position.X + 2, testFrames.Position.Y));

                isRun = true;
            }
            if (InputEvent.IsKeyPressed(InputEvent.Keys.W)) {
                testImage.SetPosition(new Point(testImage.Position.X, testImage.Position.Y - 1));

                testFrames.SetPosition(new Point(testFrames.Position.X, testFrames.Position.Y - 2));

                isRun = true;
            }
            if (InputEvent.IsKeyPressed(InputEvent.Keys.A)) {
                testImage.SetPosition(new Point(testImage.Position.X - 1, testImage.Position.Y));

                testFrames.SetPosition(new Point(testFrames.Position.X - 2, testFrames.Position.Y));

                isRun = true;
            }

            if (InputEvent.IsMouseButtonPressed(InputEvent.MouseButtons.BUTTON_LEFT)) {
                testImage.SetPosition(new Point(InputEvent.GetMouseCoords().X, InputEvent.GetMouseCoords().Y));
                disney.SetPosition(new Point(InputEvent.GetMouseCoords().X, InputEvent.GetMouseCoords().Y));
            }

            testImage.Draw(new Point(0, 45), new Point(25, 45), 8);
            if(isRun) 
                testFrames.Draw("run", 6);
            else 
                testFrames.Draw("idle", 20);

            square.Draw(Point.Zero, new Point(32, 32), 64);
            disney.Draw();

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

                disney.SetText(disney.Value + " v- up");
            }
            if (inputEvent.IsKeyDown(InputEvent.Keys.B)) {
                Console.WriteLine("b - down");
            }
            if (inputEvent.IsKeyUp(InputEvent.Keys.B)) {
                Console.WriteLine("b - up");
            }
            if (inputEvent.IsKeyDown(InputEvent.Keys.A)) {
                testImage.FlipType = Graphics.FlipTypes.Horizontal;

                testFrames.FlipType = Graphics.FlipTypes.Horizontal;
            } else if (inputEvent.IsKeyDown(InputEvent.Keys.D)) {
                testImage.FlipType = Graphics.FlipTypes.None;

                testFrames.FlipType = Graphics.FlipTypes.None;
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
