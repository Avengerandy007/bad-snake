using static SDL2.SDL;
using Vector;

namespace Player{

	class Generics{
		static protected List<Part> previousParts = new List<Part>();
		static public int lenght = 0;
		static protected PosVect position;
		public SDL_Rect rect;

		public static void Update(){
			Program.playerHead.Move();
			Console.WriteLine($"previousParts.Count(): {previousParts.Count()}, lenght = {lenght}");
		}

		public static void RenderPlayer(){
			SDL_SetRenderDrawColor(Window.renderer, 0, 128, 0, 255);
			Program.playerHead.Render();
			if (previousParts.Count() >= 1) Part.RenderParts();
			SDL_SetRenderDrawColor(Window.renderer, 0, 0, 0, 255);
		}

		public static void IncreaseLenght(){
			lenght++;
			previousParts.Add(new Part());
		}
		
	}

	class Head : Generics{
		int speed = 50;
		public DirVect direction;
		System.Diagnostics.Stopwatch moveStopwatch = new System.Diagnostics.Stopwatch();

		public Head(){

			rect = new SDL_Rect{
				x = 450,
				y = 450,
				w = 50,
				h = 50
			};

			position = new PosVect(rect.x, rect.y);
			direction.Change("up");

			moveStopwatch.Start();

		}

		public void Move(){
			if (moveStopwatch.Elapsed.TotalMilliseconds <= 500) return;
			position.x += speed * direction.x;
			position.y += speed * direction.y;
			rect.x = position.x;
			rect.y = position.y;
			DestroyLastPart();
			moveStopwatch.Restart();
		}
		
		void DestroyLastPart(){
			if (lenght != 0){
				previousParts.Remove(previousParts.First());
				previousParts.Add(new Part());
			}
		}
		
		public void Render(){
			SDL_RenderDrawRect(Window.renderer, ref rect);
			SDL_RenderFillRect(Window.renderer, ref rect);
		}
	}

	class Part : Generics{

		SDL_Rect partRect;
		public Part(){
			partRect = Program.playerHead.rect;
		}

		public static void RenderParts(){
		 	foreach (var part in previousParts){
				SDL_RenderDrawRect(Window.renderer, ref part.partRect);
				SDL_RenderFillRect(Window.renderer, ref part.partRect);
			}	
		}
	}

}
