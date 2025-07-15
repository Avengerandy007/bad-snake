using static SDL2.SDL;

static class Window{
	
	public static IntPtr renderer;
	static IntPtr window;

	static bool running;

	public static void Setup(){
		
		running = true;

		if (InitSDL() == -1){
			InitSDL();
		}

		if (InitWindow() == -1){
			InitWindow();
		}

		if (InitRenderer() == -1){
			InitRenderer();
		}
		
	}

	static int InitSDL(){
		if (SDL_Init(SDL_INIT_VIDEO) < 0){
			Console.WriteLine($"There was a problem initialising SDL: {SDL_GetError()}, retrying.");
			return -1;
		}
		return 0;
	}

	static int InitWindow(){
		window = SDL_CreateWindow("Not well made clone of snake", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 1000, 1000, SDL_WindowFlags.SDL_WINDOW_SHOWN);

		if (window == IntPtr.Zero){
			Console.WriteLine($"There was a problem creating the window: {SDL_GetError()}, retrying.");
			return -1;
		}

		return 0;
	}

	static int InitRenderer(){
		renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

		if (renderer == IntPtr.Zero){
			Console.WriteLine($"There was a problem creating the renderer: {SDL_GetError()}, retrying.");
			return -1;
		}

		return 0;
	}

	public static void MainLoop(){
		inputStopwatch.Start();
		while (running){
			Render();
			Program.Update();
			PollEvents();
			SDL_Delay(50);
		}
		inputStopwatch.Stop();
		DestroySDLElements();
	}

	static void Render(){
		SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
		SDL_RenderClear(renderer);
		Player.Generics.RenderPlayer();
		SDL_RenderPresent(renderer);
	}

	static System.Diagnostics.Stopwatch inputStopwatch = new System.Diagnostics.Stopwatch();

	static void PollEvents(){
		while(SDL_PollEvent(out SDL_Event e) == 1){
			switch (e.type){
				case SDL_EventType.SDL_QUIT:
					running = false;
					break;
				case SDL_EventType.SDL_KEYDOWN:
					if (inputStopwatch.Elapsed.TotalMilliseconds <= 500) break;
					inputStopwatch.Restart();
					switch(e.key.keysym.sym){
						case SDL_Keycode.SDLK_LEFT:
							Program.playerHead.direction.Change("left");
							break;

						case SDL_Keycode.SDLK_RIGHT:
							Program.playerHead.direction.Change("right");
							break;

						case SDL_Keycode.SDLK_UP:
							Program.playerHead.direction.Change("up");
							break;

						case SDL_Keycode.SDLK_DOWN:
							Program.playerHead.direction.Change("down");
							break;
						case SDL_Keycode.SDLK_g:
							Player.Generics.IncreaseLenght();
							break;
					}
					break;
			}
		}
	}

	static void DestroySDLElements(){
		SDL_DestroyWindow(window);
		SDL_DestroyRenderer(renderer);
		SDL_Quit();
	}
}
