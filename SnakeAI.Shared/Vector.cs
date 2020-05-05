using System;

namespace SnakeAI.Shared
{
	public struct Vector
	{
		public float x;
		public float y;

		public Vector(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// Sum two vector points into new location
		/// </summary>
		/// <param name="direction"></param>
		public void Add(Vector direction)
		{
			x += direction.x;
			y += direction.y;
		}
	}
}