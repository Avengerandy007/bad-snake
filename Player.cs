using static SDL2.SDL;
using Vector;

namespace Player{

	class Generics{
		static public List<Part> previousParts = new List<Part>();
		static public int lenght = 0;
		public const int speed = 50;

		public static void Update(){
			Program.playerHead.Move();
			if (Program.playerHead.CheckIfGameOver() || Program.playerHead.CheckIfCollidedWithWall()){
				RestartGame();
			}
			if (Program.playerHead.CheckIfApple()){
				Program.playerHead.OnAppleEaten();
			}
		}

		static void RestartGame(){
			previousParts.Clear();
			lenght = 0;
			Program.playerHead = new Head();
		}

		public static void RenderPlayer(){
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
		PosVect position;
		public DirVect direction;
		System.Diagnostics.Stopwatch moveStopwatch = new System.Diagnostics.Stopwatch();
		public SDL_Rect rect;

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

		public bool CheckIfGameOver(){
			foreach(var part in previousParts){
				if (CheckCollisions(position, part.position)){
					return true;
				}
			}

			return false;
		}

		public bool CheckIfApple(){
			if (CheckCollisions(position, Apple.Generics.appleObject.pos)) return true;
			else return false;
		}

		public void OnAppleEaten(){
			IncreaseLenght();
			Apple.Generics.CreateApple();
		}

		public bool CheckIfCollidedWithWall(){
			if (position.x >= 1000 || position.x < 0 || position.y >= 1000 || position.y < 0) return true;
			else return false;
		}

		bool CheckCollisions(PosVect objectA, PosVect objectB){
			if (objectA.x == objectB.x && objectA.y == objectB.y) return true;
			else return false;
		}
		
		public void Render(){
			SDL_SetRenderDrawColor(Window.renderer, 0, 0, 128, 255);
			SDL_RenderDrawRect(Window.renderer, ref rect);
			SDL_RenderFillRect(Window.renderer, ref rect);
		}
	}

	class Part : Generics{

		SDL_Rect partRect;
		public PosVect position;
		public Part(){
			partRect = Program.playerHead.rect;
			partRect.x += -Program.playerHead.direction.x * speed;
			partRect.y += -Program.playerHead.direction.y * speed;

			position.x = partRect.x;
			position.y = partRect.y;
		}

		public static void RenderParts(){
			SDL_SetRenderDrawColor(Window.renderer, 0, 128, 0, 255);
		 	foreach (var part in previousParts){
				SDL_RenderDrawRect(Window.renderer, ref part.partRect);
				SDL_RenderFillRect(Window.renderer, ref part.partRect);
			}	
		}
	}

}
