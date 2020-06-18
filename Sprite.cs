using System;
using System.Collections.Generic;

namespace VKR {
    class Sprite {
        int currentFrame = 0;
        int lastLine = 0;
        int framesListLength = 0;
        Dictionary<string, Animation> animations;
        Animation currentAnimation;
        
        public Graphics.Image Image { get; private set; }
        public Point Position { get; private set; } = new Point(0, 0);
        public int FramesCount { get; set; } = 0;
        public bool Animate = false;
        public int SheetLength { get; set; } = 100;
        public Graphics.FlipTypes FlipType { get; set; }

        public Sprite(string file, Graphics parent) {
            Image = new Graphics.Image(file, parent);

            animations = new Dictionary<string, Animation>();
        }

        public void AddAnimation(Animation anim) {
            if (animations.ContainsKey(anim.Name))
                throw new Exception("Animation with name " + anim.Name + " already exist");

            animations.Add(anim.Name, anim);
        }
        public Animation GetAnimation(string name) {
            return animations[name];
        }
        public void RemoveAnimation(string name) {
            animations.Remove(name);
        }

        public void SetPosition(Point position) {
            Point locPosition = Image.Position - Position; 
            
            Position = position;
            Image.SetPosition(Position + locPosition); 
        }

        public void Draw() {
            Image.FlipType = FlipType;

            Image.Draw();
        }
        public void Draw(string animationName, int howSlowly) {
            Image.FlipType = FlipType;

            if (Animate) 
                framesAnimate(animationName, howSlowly);
            else 
                Image.Draw();
        }
        public void Draw(Point p1, Point p2, int howSlowly) {
            Image.FlipType = FlipType;

            if (Animate)
                sheetAnimate(p1, p2, howSlowly);

            Image.Draw();
        }

        void framesAnimate(string animationName, int howSlowly) {
            int currentFrameTmp = currentFrame / (howSlowly + 1);

            if (currentAnimation == null || currentAnimation.Name != animationName) {
                if(!animations.TryGetValue(animationName, out currentAnimation))
                    throw new Exception("Animation with name " + animationName + " not exist");

                currentFrame = 0;
                currentFrameTmp = 0;
            }
            if (currentFrameTmp >= currentAnimation.Frames.Count) {
                currentFrame = 0;
                currentFrameTmp = 0;
            }  

            currentAnimation.Frames[currentFrameTmp].FlipType = FlipType;
            currentAnimation.Frames[currentFrameTmp].Draw(Image.Position, Image.Width, Image.Height, Image.Angle);

            currentFrame++;
        }
        void sheetAnimate(Point p1, Point p2, int howSlowly) {
            int currentFrameTmp = currentFrame / (howSlowly + 1);

            if (currentFrameTmp >= FramesCount) {
                currentFrame = 0;
                lastLine = 0;
                framesListLength = 0;

                Image.Crop(p1, p2);
            }else {
                int x = p2.X * (currentFrameTmp + 1) - lastLine * p2.X * framesListLength;
                int y = p2.Y * (lastLine + 1);

                if (x > Image.StartWidth || ((currentFrameTmp / (SheetLength * (lastLine + 1))) > 0)) {
                    lastLine++;
                    x -= lastLine * p2.X * currentFrameTmp;
                    y *= (lastLine + 1);

                    framesListLength += currentFrameTmp;
                }

                Point tmpP = new Point(x, y);
                Image.Crop(p1 + tmpP - p2, p2);
            }
            currentFrame++;
        }
    }
}