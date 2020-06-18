using System;
using SDL2;

namespace VKR {
    partial class Graphics {
        public abstract class Object2D {
            protected IntPtr surface;
            protected SDL.SDL_Rect area;
            protected SDL.SDL_Rect renderArea;
            protected int height, width;
            public Point Position { get; protected set; } = new Point(0, 0);
            public Graphics.FlipTypes FlipType { get; set; }
            
            public int Height { 
                get { return height; } 
                set {
                    height = value;
                    area.h = value;
                } 
            } 
            public int Width { 
                get { return width; } 
                set {
                    width = value;
                    area.w = value;
                } 
            }
            public double Angle = 0; // in degrees
            public Graphics Parent { get; private set; }
            public Object2D(Graphics parent) {
                if (parent == null) {
                        throw new Exception("Object2D constructor error: fill parent parametr");
                }
                Parent = parent;
            }

            public virtual void Draw() {
                SDL.SDL_RenderCopyEx(Parent.renderer, surface, ref renderArea, ref area, Angle, IntPtr.Zero, (SDL.SDL_RendererFlip)FlipType);
            }
            public virtual void Draw(Point position, int width, int height, double angle) {
                SetPosition(position);
                Width = width;
                Height = height;
                Angle = angle;

                SDL.SDL_RenderCopyEx(Parent.renderer, surface, ref renderArea, ref area, Angle, IntPtr.Zero, (SDL.SDL_RendererFlip)FlipType);
            }

            public virtual void SetPosition(Point point) {
                Position = point;
                
                area.x = point.X;
                area.y = point.Y;
            }
            public virtual void Crop(Point p1, Point p2) {
                renderArea.x = p1.X;
                renderArea.y = p1.Y;
                renderArea.w = p2.X;
                renderArea.h = p2.Y;
            }
        }
    }
}
