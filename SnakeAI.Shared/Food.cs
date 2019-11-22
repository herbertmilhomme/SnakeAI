using System;

namespace SnakeAI.Shared
{
	public class Food
	{
		public Vector pos;

		public Food()
		{
			//int x = 400 + Core.SIZE + (int)Math.Floor((double)Core.Rand.Next(38)) * Core.SIZE;
			//int y = Core.SIZE + (int)Math.Floor((double)Core.Rand.Next(38)) * Core.SIZE;
			int x = 400 + Core.SIZE + (int)Math.Floor((double)(Core.Rand.NextDouble() * 38)) * Core.SIZE;
			int y = Core.SIZE + (int)Math.Floor((double)(Core.Rand.NextDouble() * 38)) * Core.SIZE;
			pos = new Vector(x, y);
		}

		public virtual void show()
		{
			//stroke(0);
			//fill(255, 0, 0);
			//rect(pos.x, pos.y, Core.SIZE, Core.SIZE);
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