namespace SnakeAI.Shared
{
	public abstract class EvolutionGraph : System.IDisposable //PApplet
	{
		private bool disposedValue = false; // To detect redundant calls
		
		EvolutionGraph() //: base()
		{
			//super();
			//PApplet.runSketch(new string[] { this.getClass().getSimpleName() }, this);
		}

		public abstract void settings();
		//{
		//	size(900, 600);
		//}

		public abstract void setup();
		//{
		//	background(150);
		//	frameRate(30);
		//}

		public abstract void draw();
		//{
		//	background(150);
		//	fill(0);
		//	strokeWeight(1);
		//	textSize(15);
		//	textAlign(CENTER, CENTER);
		//	text("Generation", Core.width / 2, Core.height - 10);
		//	translate(10, Core.height / 2);
		//	rotate(System.Math.PI / 2);
		//	text("Score", 0, 0);
		//	rotate(-System.Math.PI / 2);
		//	translate(-10, -Core.height / 2);
		//	textSize(10);
		//	float x = 50;
		//	float y = Core.height - 35;
		//	float xbuff = (Core.width - 50) / 51.0;
		//	float ybuff = (Core.height - 50) / 200.0;
		//	for (int i = 0; i <= 50; i++)
		//	{
		//		text(i, x, y);
		//		x += xbuff;
		//	}
		//	x = 35;
		//	y = Core.height - 50;
		//	float ydif = ybuff * 10.0;
		//	for (int i = 0; i < 200; i += 10)
		//	{
		//		text(i, x, y);
		//		line(50, y, Core.width, y);
		//		y -= ydif;
		//	}
		//	strokeWeight(2);
		//	stroke(255, 0, 0);
		//	int score = 0;
		//	for (int i = 0; i < Core.evolution.Count; i++)
		//	{
		//		int newscore = Core.evolution[i];
		//		line(50 + (i * xbuff), Core.height - 50 - (score * ybuff), 50 + ((i + 1) * xbuff), Core.height - 50 - (newscore * ybuff));
		//		score = newscore;
		//	}
		//	stroke(0);
		//	strokeWeight(5);
		//	line(50, 0, 50, Core.height - 50);
		//	line(50, Core.height - 50, Core.width, Core.height - 50);
		//}

		public abstract void exit();
		//{
		//	dispose();
		//	graph = null;
		//}

		#region IDisposable Support
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~EvolutionGraph()
		// {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		void System.IDisposable.Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}