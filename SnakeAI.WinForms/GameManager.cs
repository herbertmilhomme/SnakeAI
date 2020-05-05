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
		public int Score
		{
			get { return snake == null ? 0 : snake.Length - 1; }//.score; }
			//set { this.snake.score = value; }
		}
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

		public override void fileSelectedIn(string path)
		{
			using (System.IO.FileStream selection = System.IO.File.Open(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
				if (selection == null)
				{
					//println("Window was closed or the user hit cancel.");
					Console.WriteLine("Window was closed or the user hit cancel.");
				}
				else
				{
					//string path = selection.getAbsolutePath();
					System.Data.DataTable modelTable = new System.Data.DataTable("header");
					modelTable.ReadXml(selection); //loadTable(path, "header");
					Matrix[] weights = new Matrix[modelTable.Columns.Count - 1];
					float[][] in_ = new float[hidden_nodes][];
					for (int i = 0; i < hidden_nodes; i++)
					{
						in_[i] = new float[25];
						for (int j = 0; j < 25; j++)
						{
							//in_[i][j] = modelTable.getFloat(j + i * 25, "L0");
							in_[i][j] = (float)modelTable.Rows[j + i * 25]["L0"];
						}
					}
					weights[0] = new Matrix(in_);

					for (int h = 1; h < weights.Length - 1; h++)
					{
						float[][] hid = new float[hidden_nodes][];
						for (int i = 0; i < hidden_nodes; i++)
						{
							hid[i] = new float[hidden_nodes + 1];
							for (int j = 0; j < hidden_nodes + 1; j++)
							{
								//hid[i][j] = modelTable.getFloat(j + i * (hidden_nodes + 1), "L" + h);
								hid[i][j] = (float)modelTable.Rows[j + i * (hidden_nodes + 1)]["L" + h];
							}
						}
						weights[h] = new Matrix(hid);
					}

					float[][] out_ = new float[4][];
					for (int i = 0; i < 4; i++)
					{
						out_[i] = new float[hidden_nodes + 1];
						for (int j = 0; j < hidden_nodes + 1; j++)
						{
							//out_[i][j] = modelTable.getFloat(j + i * (hidden_nodes + 1), "L" + (weights.Length - 1));
							out_[i][j] = (float)modelTable.Rows[j + i * (hidden_nodes + 1)]["L" + (weights.Length - 1)];
						}
					}
					weights[weights.Length - 1] = new Matrix(out_);

					evolution = new List<int>();
					int g = 0;
					//int genscore = modelTable.getInt(g, "Graph");
					int genscore = (int)modelTable.Rows[g]["Graph"];
					while (genscore != 0)
					{
						evolution.Add(genscore);
						g++;
						//genscore = modelTable.getInt(g, "Graph");
						genscore = (int)modelTable.Rows[g]["Graph"];
					}
					modelLoaded = true;
					humanPlaying = false;
					snake = new Snake(weights.Length - 1);
					snake.brain.load(weights);
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
