namespace Vector{
	public struct PosVect{
		public int x;
		public int y;

		public PosVect(int X, int Y){
			x = X;
			y = Y;
		}
	}

	public struct DirVect{
		public int x;
		public int y;

		public DirVect(string inDir){
			switch(inDir){
				case "left":
					x = -1;
					y = 0;
					break;
				case "right":
					x = 1;
					y = 0;
					break;
				case "up":
					x = 0;
					y = -1;
					break;
				case "down":
					x = 0;
					y = 1;
					break;
				default:
					throw new ArgumentException($"'{inDir}' is not a valid direction.");
			}
		}
	}
}
