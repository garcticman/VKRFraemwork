using System;
using SDL2;

namespace VKR {
    class Graphics {
        IntPtr window;
        IntPtr renderer;

        int screenWidth, screenHeight;
        public Graphics(string title, int width, int height) {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "nearest");

            window = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 
                width, height, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            if (window == IntPtr.Zero) {
                throw new Exception("window creation error");
            } 

            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | 
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC); 
            if (window == IntPtr.Zero) {
                throw new Exception("renderer creation error");
            }

            screenWidth = width;
            screenHeight = height;
        }

        public void Render() {
            SDL.SDL_RenderPresent(renderer);
        }

        public void ClearAll() {
            SDL.SDL_RenderClear(renderer);
        }
        public void ClearAll(byte r, byte g, byte b) {
            SDL.SDL_RenderClear(renderer);
            SDL.SDL_SetRenderDrawColor(renderer, r, g, b, 255);
        }

        public class Image {
            public enum FlipType {
                Horizontal = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL,
                Vertical = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL,
                None = SDL.SDL_RendererFlip.SDL_FLIP_NONE,
            }
            IntPtr surface;
            Graphics parent;
            SDL.SDL_Rect area;
            SDL.SDL_Rect renderArea;
            SDL.SDL_RendererFlip flipType;
            public int StartWidth { get; private set; }
            public int StartHeight { get; private set; }
            public Point Position { get; private set; } = new Point(0, 0);
            int height, width;
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
 
            public Image(string file, Graphics parent) {
                if (parent == null) {
                    throw new Exception("Image constructor error: fill parent parametr");
                }
                this.parent = parent;

                surface = SDL_image.IMG_LoadTexture(parent.renderer, file);
                if (surface == IntPtr.Zero) {
                    throw new Exception(SDL.SDL_GetError());
                }

                unsafe {
                    SDL.SDL_Surface* image = (SDL.SDL_Surface*)surface.ToPointer();
                    StartWidth = (*image).w;
                    StartHeight = (*image).h;
                }

                Width = StartWidth;
                Height = StartHeight;

                area.x = 0;
                area.y = 0;
                area.w = width;
                area.h = height;

                renderArea.x = 0;
                renderArea.y = 0;
                renderArea.w = width;
                renderArea.h = height;

                flipType = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
            }

            public void Draw() {
                SDL.SDL_RenderCopyEx(parent.renderer, surface, ref renderArea, ref area, Angle, IntPtr.Zero, flipType);
            }

            public void SetPosition(Point point) {
                Position = point;
                
                area.x = point.X;
                area.y = point.Y;
                // area.w = renderArea.w;
                // area.h = renderArea.h;
            }
            public void Crop(Point p1, Point p2) {
                renderArea.x = p1.X;
                renderArea.y = p1.Y;
                renderArea.w = p2.X;
                renderArea.h = p2.Y;

                // Width = renderArea.w;
                // Height = renderArea.h;
            }
            public void Flip(FlipType flipType) {
                this.flipType = (SDL.SDL_RendererFlip)flipType;
            }

            IntPtr getFormat() {
                unsafe {
                    SDL.SDL_Surface *tmpSurface;

                    tmpSurface = (SDL.SDL_Surface *)surface.ToPointer();

                    return (*tmpSurface).format;
                }
            }
        }
    }
}