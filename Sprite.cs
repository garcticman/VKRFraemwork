using System;

namespace VKR {
    class Sprite {
        int lastFrame = 0;
        int lastLine = 0;
        int framesListLength = 0;
        
        public Graphics.Image Image { get; private set; }
        public Point Position { get; private set; } = new Point(0, 0);
        public int FramesCount { get; set; } = 0;
        public bool Animate = false;
        public int SheetLength { get; set; } = 100;
        public Sprite(string file, Graphics parent) {
            Image = new Graphics.Image(file, parent);
        }

        public void SetPosition(Point position) {
            Point locPosition = Image.Position - Position; 
            
            Position = position;
            Image.SetPosition(Position + locPosition); 
        }

        public void Draw() {
            Image.Draw();
        }
        public void Draw(Point p1, Point p2, int howSlowly) {
            if (Animate)
                sheetAnimate(p1, p2, howSlowly);

            Image.Draw();
        }

        void sheetAnimate(Point p1, Point p2, int howSlowly) {
            int currentFrame = lastFrame / (howSlowly + 1);

            if (currentFrame >= FramesCount) {
                lastFrame = 0;
                lastLine = 0;
                framesListLength = 0;

                Image.Crop(p1, p2);
            }else {
                int x = p2.X * (currentFrame + 1) - lastLine * p2.X * framesListLength;
                int y = p2.Y * (lastLine + 1);

                if (x > Image.StartWidth || ((currentFrame / (SheetLength * (lastLine + 1))) > 0)) {
                    lastLine++;
                    x -= lastLine * p2.X * currentFrame;
                    y *= (lastLine + 1);

                    framesListLength += currentFrame;
                }

                Point tmpP = new Point(x, y);
                Image.Crop(p1 + tmpP - p2, p2);

                
            }
            lastFrame++;
        }
    }
}