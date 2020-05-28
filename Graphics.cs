using System;
using SDL2;

namespace VKR {
    class Graphics {
        IntPtr window;
        IntPtr renderer;

        public Graphics(string title, int width, int height) {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);

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

        public void Flip() {
            SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
            SDL.SDL_RenderClear(renderer);
            SDL.SDL_RenderPresent(renderer);
        }

        public class Image {
            IntPtr surface;

            Graphics parent;

            public Image(string file, Graphics parent) {
                if (parent == null) {
                    throw new Exception("Image constructor error: fill parent parametr");
                }
                this.parent = parent;

                IntPtr tmpImage = SDL_image.IMG_Load(file);

                surface = SDL.SDL_ConvertSurfaceFormat(tmpImage, SDL.SDL_GetWindowPixelFormat(parent.window), 0);
            }

            public Image(string file, byte r, byte g, byte b, Graphics parent) {
                if (parent == null) {
                    throw new Exception("Image constructor error: fill parent parametr");
                }
                this.parent = parent;

                IntPtr tmpImage = SDL_image.IMG_Load(file);

                surface = SDL.SDL_ConvertSurfaceFormat(tmpImage, SDL.SDL_GetWindowPixelFormat(parent.window), 0);

                SDL.SDL_SetColorKey(surface, (int)SDL.SDL_bool.SDL_TRUE, SDL.SDL_MapRGB(getFormat(), r, g, b));
            }

            public void Draw(int x, int y) {
                SDL.SDL_Rect area = new SDL.SDL_Rect();
                area.x = x;
                area.y = y;

                SDL.SDL_BlitSurface(surface, IntPtr.Zero, parent.window, ref area);
            }

            public void Draw(int x, int y, int startX, int startY, int endX, int endY) {
                SDL.SDL_Rect area = new SDL.SDL_Rect();
                area.x = x;
                area.y = y;

                SDL.SDL_Rect srcArea = new SDL.SDL_Rect();
                srcArea.x = startX;
                srcArea.y = startY;
                srcArea.w = endX;
                srcArea.h = endY;

                SDL.SDL_BlitSurface(surface, ref srcArea, parent.window, ref area);
            }

            int GetWidth() {
                unsafe {
                    SDL.SDL_Surface* image = (SDL.SDL_Surface*)surface.ToPointer();
                    return (*image).w;
                }
                
            }

            int GetHeight() {
                unsafe {
                    SDL.SDL_Surface* image = (SDL.SDL_Surface*)surface.ToPointer();
                    return (*image).h;
                }
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