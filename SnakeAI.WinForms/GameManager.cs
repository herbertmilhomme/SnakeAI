using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeAI.Shared;

namespace SnakeAI.WinForms
{
	internal class GameManager : Core
	{
		public int Width
		{
			get { return Core.SIZE; }
			//set { Core.SIZE = value; }
		}
		public int Height
		{
			get { return Core.SIZE; }
			//set { Core.SIZE = value; }
		}
		public int Speed { get; set; }
		//public int Score
		//{
		//	get { return this.snake == null ? 0 : this.snake.body.Count; }//.score; }
		//	//set { this.snake.score = value; }
		//}
		//public int Points { get; set; }
		public bool GameOver { get; set; }
		//public eDirection Direction { get; set; }

		//public override ISnake snake { get; private set; }
		//public override ISnake model { get; private set; }

		public GameManager() : base()
		{
			//Width = 20;
			//Height = 20;
			Speed = 8;
			//m_Score = 0;
			//Points = 100;
			GameOver = false;
			//Direction = eDirection.Down;
		}

		public GameManager(int w, int h) : this()
		{
			Core.width = w;
			Core.height = h;
		}

		public override void setup()
		{
			//font = createFont("agencyfb-bold.ttf", 32);
			evolution = new List<int>();
			//graphButton = new Button(349, 15, 100, 30, "Graph");
			//loadButton = new Button(249, 15, 100, 30, "Load");
			//saveButton = new Button(149, 15, 100, 30, "Save");
			//increaseMut = new Button(340, 85, 20, 20, "+");
			//decreaseMut = new Button(365, 85, 20, 20, "-");
			//frameRate(fps);
			if (humanPlaying)
			{
				snake = new Snake();
			}
			else
			{
				//adjust size of population
				pop = new Population(2000);
			}
		}
	}

	public enum eDirection
	{
		Down,
		Up,
		Left,
		Right
	};
}
