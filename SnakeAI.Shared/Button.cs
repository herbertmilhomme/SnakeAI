namespace SnakeAI.Shared
{
	public abstract class Button
	{
		float X, Y, W, H;
		string text;
		public Button(float x, float y, float w, float h, string t)
		{
			X = x;
			Y = y;
			W = w;
			H = h;
			text = t;
		}

		public bool collide(float x, float y)
		{
			if (x >= X - W / 2 && x <= X + W / 2 && y >= Y - H / 2 && y <= Y + H / 2)
			{
				return true;
			}
			return false;
		}

		public abstract void show();
		//{
		//	fill(255);
		//	stroke(0);
		//	rectMode(CENTER);
		//	rect(X, Y, W, H);
		//	textSize(22);
		//	textAlign(CENTER, CENTER);
		//	fill(0);
		//	noStroke();
		//	text(text, X, Y - 3);
		//}
	}
}