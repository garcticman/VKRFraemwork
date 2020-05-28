using System;
using SDL2;
using VKR;

namespace Test
{
    class Program
    {
        static int Main(string[] args)
        {
            // Graphics.Image img = new Graphics.Image("", null);

            // if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) != 0) {
            //     return 1;
            // }

            // IntPtr window = SDL.SDL_CreateWindow("Title", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED,
            //     1020, 800, SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            // if (window == null) {
            //     return 1;
            // }

            // IntPtr screen_surface = SDL.SDL_GetWindowSurface(window);

            // unsafe {
            //     var surface = (SDL.SDL_Surface *)screen_surface.ToPointer();

            //     SDL.SDL_FillRect(screen_surface, IntPtr.Zero, 
            //         SDL.SDL_MapRGB((*surface).format, 0, 100, 0));
            //     SDL.SDL_UpdateWindowSurface(window);
            // }

            // SDL.SDL_Delay(2000);

            // SDL.SDL_DestroyWindow(window);
            // SDL.SDL_Quit();

            Game game = new Game("Test", 640, 480);
            return game.Execute();
        }


    }
}
