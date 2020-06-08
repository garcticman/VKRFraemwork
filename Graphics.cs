using System;
using SDL2;

namespace VKR {
    class Graphics {
        IntPtr window;
        IntPtr renderer;
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
            IntPtr surface;
            Graphics parent;
            SDL.SDL_Rect area;
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

            public Image(string file, Graphics parent) {
                if (parent == null) {
                    throw new Exception("Image constructor error: fill parent parametr");
                }
                this.parent = parent;

                surface = SDL_image.IMG_LoadTexture(parent.renderer, file);
                if (surface == IntPtr.Zero) {
                    throw new Exception(SDL.SDL_GetError());
                }

                Width = GetStartingWidth();
                Height = GetStartingHeight();

                area.x = 0;
                area.y = 0;
                area.w = Width;
                area.h = Height;
            }

            public void Draw() {
                SDL.SDL_RenderCopy(parent.renderer, surface, IntPtr.Zero, ref area);
            }

            public int GetStartingWidth() {
                unsafe {
                    SDL.SDL_Surface* image = (SDL.SDL_Surface*)surface.ToPointer();
                    return (*image).w;
                }
                
            }
            public int GetStartingHeight() {
                unsafe {
                    SDL.SDL_Surface* image = (SDL.SDL_Surface*)surface.ToPointer();
                    return (*image).h;
                }
            }

            public void SetPosition(Point point) {
                Position = point;
                area.x = point.X;
                area.y = point.Y;
                area.h = Height;
                area.w = Width;
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