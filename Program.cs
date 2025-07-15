using System;

class Program{
	
	public static Player.Head playerHead = new Player.Head();

	public static void Main(){
		Console.WriteLine("Hello from bad snake clone");
		Window.Setup();
		Apple.Generics.CreateApple();
		Window.MainLoop();
	}

	public static void Update(){
		Player.Generics.Update();
	}
}
