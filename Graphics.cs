using System;
using SDL2;

namespace VKR {
    partial class Graphics {
        public enum FlipTypes {
            Horizontal = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL,
            Vertical = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL,
            None = SDL.SDL_RendererFlip.SDL_FLIP_NONE,
        }
        IntPtr window;
        IntPtr renderer;

        int screenWidth, screenHeight;
        public Graphics(string title, int width, int height) {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            if (SDL_ttf.TTF_Init() == -1) {
                throw new Exception("SDL_TTF.dll error");
            }
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

        public class Image : Object2D {
            public int StartWidth { get; private set; }
            public int StartHeight { get; private set; }
            public Image(string file, Graphics parent) : base(parent) {
                setSurface(file);
                setStartingRectangles();
                FlipType = FlipTypes.None;
            }
            public Image(string file, Graphics parent, Point position) : this(file, parent) {
                Position = position;
            }
            public Image(string file, Graphics parent, Point position, int width, int height) : this(file, parent, position) {
                Width = width;
                Height = height;
            }

            IntPtr getFormat() {
                unsafe {
                    SDL.SDL_Surface *tmpSurface;

                    tmpSurface = (SDL.SDL_Surface *)surface.ToPointer();

                    return (*tmpSurface).format;
                }
            }

            void setSurface(string file) {
                surface = SDL_image.IMG_LoadTexture(Parent.renderer, file);
                if (surface == IntPtr.Zero) {
                    throw new Exception(SDL.SDL_GetError());
                }
            }
            void setStartingRectangles() {
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
            }
        }

        public class Text : Object2D {
            IntPtr font;
            SDL.SDL_Color color = new SDL.SDL_Color();
            int size;
            public int Size { get { return size; } 
                set { 
                    size = value;
                    getTextureFromText();
                } 
            } 
            public Color Color { get { return new Color(color.r, color.g, color.b, color.a ); } 
                set {
                    color.r = value.R;
                    color.g = value.G;
                    color.b = value.B;
                    color.a = value.A;

                    getTextureFromText();
                } 
            }
            public string Value { get; private set; }
            public Text(string fontFile, string text, int size, Graphics parent) : base(parent) {
                this.size = size;
                Value = text;

                font = SDL_ttf.TTF_OpenFont(fontFile, Size);
                if (font == IntPtr.Zero) 
                    throw new Exception("Failed to load font.");

                IntPtr textSurface = SDL_ttf.TTF_RenderText_Solid(font, Value, color);
                if (textSurface == IntPtr.Zero) 
                    throw new Exception("Unable to render text surface");

                surface = SDL.SDL_CreateTextureFromSurface(Parent.renderer, textSurface);
                if (surface == IntPtr.Zero) 
                    throw new Exception("Unable to create texture from rendered text");

                int startWidth, startHeight;
                unsafe {
                    SDL.SDL_Surface* image = (SDL.SDL_Surface*)surface.ToPointer();
                    startWidth = (*image).w;
                    startHeight = (*image).h;
                }

                Width = startWidth;
                Height = startHeight;

                area.x = Position.X;
                area.y = Position.Y;
                area.w = width;
                area.h = height;

                renderArea.x = 0;
                renderArea.y = 0;
                renderArea.w = width;
                renderArea.h = height;
            }

            public void SetFont(string file) {
                font = SDL_ttf.TTF_OpenFont(file, Size);
                if (font == IntPtr.Zero) 
                    throw new Exception("Failed to load font.");
                
                getTextureFromText();
            }
            public void SetText(string text) {
                Value = text;
                getTextureFromText();
            }

            void getTextureFromText() {                
                IntPtr textSurface = SDL_ttf.TTF_RenderText_Solid(font, Value, color);
                if (textSurface == IntPtr.Zero) 
                    throw new Exception("Unable to render text surface");

                surface = SDL.SDL_CreateTextureFromSurface(Parent.renderer, textSurface);
                if (surface == IntPtr.Zero) 
                    throw new Exception("Unable to create texture from rendered text");

                int startWidth, startHeight;
                unsafe {
                    SDL.SDL_Surface* image = (SDL.SDL_Surface*)surface.ToPointer();
                    startWidth = (*image).w;
                    startHeight = (*image).h;
                }

                Width = startWidth;
                Height = startHeight;

                renderArea.w = width;
                renderArea.h = height;
            }
        }
    }
}