namespace SnakeAI.Shared
{
	public class EvolutionGraph : PApplet
	{

		EvolutionGraph() : base()
		{
			//super();
			PApplet.runSketch(new string[] { this.getClass().getSimpleName() }, this);
		}

		void settings()
		{
			size(900, 600);
		}

		void setup()
		{
			background(150);
			frameRate(30);
		}

		void draw()
		{
			background(150);
			fill(0);
			strokeWeight(1);
			textSize(15);
			textAlign(CENTER, CENTER);
			text("Generation", Core.width / 2, Core.height - 10);
			translate(10, Core.height / 2);
			rotate(System.Math.PI / 2);
			text("Score", 0, 0);
			rotate(-System.Math.PI / 2);
			translate(-10, -Core.height / 2);
			textSize(10);
			float x = 50;
			float y = Core.height - 35;
			float xbuff = (Core.width - 50) / 51.0;
			float ybuff = (Core.height - 50) / 200.0;
			for (int i = 0; i <= 50; i++)
			{
				text(i, x, y);
				x += xbuff;
			}
			x = 35;
			y = Core.height - 50;
			float ydif = ybuff * 10.0;
			for (int i = 0; i < 200; i += 10)
			{
				text(i, x, y);
				line(50, y, Core.width, y);
				y -= ydif;
			}
			strokeWeight(2);
			stroke(255, 0, 0);
			int score = 0;
			for (int i = 0; i < Core.evolution.Count; i++)
			{
				int newscore = Core.evolution[i];
				line(50 + (i * xbuff), Core.height - 50 - (score * ybuff), 50 + ((i + 1) * xbuff), Core.height - 50 - (newscore * ybuff));
				score = newscore;
			}
			stroke(0);
			strokeWeight(5);
			line(50, 0, 50, Core.height - 50);
			line(50, Core.height - 50, Core.width, Core.height - 50);
		}

		void exit()
		{
			dispose();
			graph = null;
		}
	}
}