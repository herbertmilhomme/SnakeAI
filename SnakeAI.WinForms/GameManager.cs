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
		private int m_Width;
		private int m_Height;
		private int m_Speed;
		private int m_Score;
		private int m_Points;
		private bool m_GameOver;
		private eDirection m_Direction;

		public int Width
		{
			get { return m_Width; }
			set { m_Width = value; }
		}
		public int Height
		{
			get { return m_Height; }
			set { m_Height = value; }
		}
		public int Speed
		{
			get { return m_Speed; }
			set { m_Speed = value; }
		}
		public int Score
		{
			get { return this.highscore; }
			set { this.highscore = value; }
		}
		public int Points
		{
			get { return m_Points; }
			set { m_Points = value; }
		}
		public bool GameOver
		{
			get { return m_GameOver; }
			set { m_GameOver = value; }
		}
		public eDirection Direction
		{
			get { return m_Direction; }
			set { m_Direction = value; }
		}

		private Snake snake { get; set; }
		private Snake model { get; set; }

		public GameManager() : base()
		{
			m_Width = 20;
			m_Height = 20;
			m_Speed = 8;
			m_Score = 0;
			m_Points = 100;
			m_GameOver = false;
			m_Direction = eDirection.Down;
		}

		public GameManager(int w, int h) : this()
		{
			Core.width = w;
			Core.height = h;
		}

		public virtual void setup()
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
		Up,
		Down,
		Left,
		Right
	};
}
