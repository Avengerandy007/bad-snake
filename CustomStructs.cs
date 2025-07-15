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

		public void Change(string inDir){
			switch(inDir.ToLower()){
				case "left":
					if (x == 1) break;
					x = -1;
					y = 0;
					break;
				case "right":
					if (x == -1) break;
					x = 1;
					y = 0;
					break;
				case "up":
					if (y == 1) break;
					x = 0;
					y = -1;
					break;
				case "down":
					if (y == -1) break;
					x = 0;
					y = 1;
					break;
				default:
					throw new ArgumentException($"'{inDir}' is not a valid direction.");
			}
		}
	}
}
