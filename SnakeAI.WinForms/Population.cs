using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAI.WinForms
{
	public class Population : SnakeAI.Shared.Population
	{
		//public override Shared.ISnake bestSnake { get { return (Snake)snakes[0]; } }
		public Population(int size) : base (size)
		{
			//snakes = new Snake[size];
			for (int i = 0; i < snakes.Length; i++)
			{
				snakes[i] = new Snake();
			}
			//bestSnake = (Snake)snakes[0].Clone();
			//bestSnake.replay = true;
			GameManager.snake = bestSnake;
		}
	}
}