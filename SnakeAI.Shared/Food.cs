using System;

namespace SnakeAI.Shared
{
	public class Food
	{
		PVector pos;

		public Food()
		{
			int x = 400 + Core.SIZE + Math.Floor(new System.Random().Next(38)) * Core.SIZE;
			int y = Core.SIZE + Math.Floor(new System.Random().Next(38)) * Core.SIZE;
			pos = new PVector(x, y);
		}

		public void show()
		{
			stroke(0);
			fill(255, 0, 0);
			rect(pos.x, pos.y, Core.SIZE, Core.SIZE);
		}

		public Food Clone()
		{
			Food clone = new Food();
			clone.pos.x = pos.x;
			clone.pos.y = pos.y;

			return clone;
		}
	}
}