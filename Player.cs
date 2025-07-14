using static SDL2.SDL;
using Vector;

namespace Player{

	class Generics{
		static protected Queue<Part> previousParts = new Queue<Part>();
		static public int lenght = 0;
		static protected PosVect position;
		public SDL_Rect rect;

		public static void Update(){
			Program.playerHead.Move();
		}

		public static void RenderPlayer(){
			SDL_SetRenderDrawColor(Window.renderer, 0, 128, 0, 255);
			Program.playerHead.Render();
			Part.RenderParts();
			SDL_SetRenderDrawColor(Window.renderer, 0, 0, 0, 255);
		}

		public static void IncreaseLenght(){
			lenght++;
			previousParts.Append(new Part());
		}
		
	}

	class Head : Generics{
		int speed = 50;
		DirVect direction;

		public Head(){

			rect = new SDL_Rect{
				x = 450,
				y = 450,
				w = 50,
				h = 50
			};

			position = new PosVect(rect.x, rect.y);
			direction = new DirVect("left");

		}

		public void Move(){
			position.x += speed * direction.x;
			position.y += speed * direction.y;
			rect.x = position.x;
			rect.y = position.y;
			DestroyLastPart();
		}
		
		void DestroyLastPart(){
			if (previousParts.Count() >= lenght && lenght != 0){
				previousParts.Dequeue();
				previousParts.Append(new Part());
			}
		}
		
		public void Render(){
			SDL_RenderDrawRect(Window.renderer, ref rect);
			SDL_RenderFillRect(Window.renderer, ref rect);
		}
	}

	class Part : Head{

		public Part(){
			rect = Program.playerHead.rect;
		}

		public static void RenderParts(){
		 	foreach (var part in previousParts){
				SDL_RenderDrawRect(Window.renderer, ref part.rect);
				SDL_RenderFillRect(Window.renderer, ref part.rect);
			}	
		}
	}

}
