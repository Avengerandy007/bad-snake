using static SDL2.SDL;
using Vector;

namespace Apple;

class Generics{

	public static Apple appleObject = new Apple();

	public static void CreateApple(){
		appleObject = new Apple();
	}

	public static void Render(){
		SDL_SetRenderDrawColor(Window.renderer, 128, 0, 0, 255);
		SDL_RenderDrawRect(Window.renderer, ref appleObject.rect);
		SDL_RenderFillRect(Window.renderer, ref appleObject.rect);
		SDL_SetRenderDrawColor(Window.renderer, 0, 0, 0, 255);
	}

}

class Apple : Generics{

	public PosVect pos;
	public SDL_Rect rect;

	public Apple(){
		
		pos = SetPos();

		rect = new SDL_Rect{
			x = pos.x,
			y = pos.y,
			w = 50,
			h = 50
		};
	}

	PosVect SetPos(){
		Random randomSpawnCell = new Random();
		int spawnCellX = randomSpawnCell.Next(0, 20);
		int spawnCellY = randomSpawnCell.Next(0, 20);
		foreach(var part in Player.Generics.previousParts){
			if (part.position.x == spawnCellX * 50 && part.position.y == spawnCellY * 50){
				return SetPos();
			}
		}
		return new PosVect(spawnCellX * 50, spawnCellY * 50);
	}
}

