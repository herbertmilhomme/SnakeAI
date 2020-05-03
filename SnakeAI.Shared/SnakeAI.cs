using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SnakeAI.Shared
{
	public abstract class Core
	{
		public static int height;
		public static int width;
		public const int SIZE = 20;
		public const int hidden_nodes = 16;
		public const int hidden_layers = 2;
		/// <summary>
		/// 15 is ideal for self play, increasing for AI does not directly increase speed, speed is dependant on processing power
		/// </summary>
		public const int fps = 100;

		public static int highscore { get; set; }

		public static float mutationRate = 0.05f;
		public static float defaultmutation = mutationRate;

		/// <summary>
		/// false for AI, true to play yourself
		/// </summary>
		public static bool humanPlaying = true;
		/// <summary>
		/// shows only the best of each generation
		/// </summary>
		public static bool replayBest = true;
		/// <summary>
		/// see the snakes vision
		/// </summary>
		public static bool seeVision = false;
		public static bool modelLoaded = false;

		//PFont font;

		public static List<int> evolution;

		//Button graphButton;
		//Button loadButton;
		//Button saveButton;
		//Button increaseMut;
		//Button decreaseMut;

		public static EvolutionGraph graph { get; set; }

		public static ISnake snake { get; set; }
		public static ISnake model { get; set; }

		public static Population pop { get; protected set; }

		#region Custom Rand Function
		/// <summary>
		/// Constantly revolving random, that won't repeat the same seed number twice, 
		/// until it cycles thru all possible seed values
		/// </summary>
		public static Random Rand { get { return new Random(Seed()); } }
		/// <summary>
		/// Constantly revolving random, that uses the same seed number that was previously used
		/// </summary>
		public static Random RandWithSetSeed { get { return new Random(Seed(true)); } }
		private static System.UInt16? seed;// = 0x0000; //{ get; set; }
		public static UInt16 Seed(bool useFixedSeed = false)
		{
			//lock (Rand)
			//{
			if (!seed.HasValue)
			{
				//seed = (UInt16)new Random().Next(0, UInt16.MaxValue);
				seed = (UInt16)new Random(DateTime.Now.Millisecond).Next(0, UInt16.MaxValue);
				seed ^= (UInt16)System.DateTime.Now.Ticks;
				seed &= UInt16.MaxValue;
			}
			if (!useFixedSeed)
			{
				seed = (UInt16)(seed * 0x41C64E6D + 0x6073);
			}
			return seed.Value;
			//}
		}
		#endregion

		//public virtual void settings()
		//{
		//	//size(1200, 800);
		//}

		public abstract void setup();
		//{
		//	//font = createFont("agencyfb-bold.ttf", 32);
		//	evolution = new List<int>();
		//	//graphButton = new Button(349, 15, 100, 30, "Graph");
		//	//loadButton = new Button(249, 15, 100, 30, "Load");
		//	//saveButton = new Button(149, 15, 100, 30, "Save");
		//	//increaseMut = new Button(340, 85, 20, 20, "+");
		//	//decreaseMut = new Button(365, 85, 20, 20, "-");
		//	//frameRate(fps);
		//	if (humanPlaying)
		//	{
		//		snake = new Snake();
		//	}
		//	else
		//	{
		//		//adjust size of population
		//		pop = new Population(2000); 
		//	}
		//}

		//public virtual void draw()
		//{
		//	background(0);
		//	noFill();
		//	stroke(255);
		//	line(400, 0, 400, height);
		//	rectMode(CORNER);
		//	rect(400 + SIZE, SIZE, width - 400 - 40, height - 40);
		//	textFont(font);
		//	if (humanPlaying)
		//	{
		//		snake.move();
		//		snake.show();
		//		fill(150);
		//		textSize(20);
		//		text("SCORE : " + snake.score, 500, 50);
		//		if (snake.dead)
		//		{
		//			snake = new Snake();
		//		}
		//	}
		//	else
		//	{
		//		if (!modelLoaded)
		//		{
		//			if (pop.done())
		//			{
		//				highscore = pop.bestSnake.score;
		//				pop.calculateFitness();
		//				pop.naturalSelection();
		//			}
		//			else
		//			{
		//				pop.update();
		//				pop.show();
		//			}
		//			fill(150);
		//			textSize(25);
		//			textAlign(LEFT);
		//			text("GEN : " + pop.gen, 120, 60);
		//			//text("BEST FITNESS : "+pop.bestFitness,120,50);
		//			//text("MOVES LEFT : "+pop.bestSnake.lifeLeft,120,70);
		//			text("MUTATION RATE : " + mutationRate * 100 + "%", 120, 90);
		//			text("SCORE : " + pop.bestSnake.score, 120, height - 45);
		//			text("HIGHSCORE : " + highscore, 120, height - 15);
		//			increaseMut.show();
		//			decreaseMut.show();
		//		}
		//		else
		//		{
		//			model.look();
		//			model.think();
		//			model.move();
		//			model.show();
		//			model.brain.show(0, 0, 360, 790, model.vision, model.decision);
		//			if (model.dead)
		//			{
		//				Snake newmodel = new Snake();
		//				newmodel.brain = model.brain.Clone();
		//				model = newmodel;
		//			}
		//			textSize(25);
		//			fill(150);
		//			textAlign(LEFT);
		//			text("SCORE : " + model.score, 120, height - 45);
		//		}
		//		textAlign(LEFT);
		//		textSize(18);
		//		fill(255, 0, 0);
		//		text("RED < 0", 120, height - 75);
		//		fill(0, 0, 255);
		//		text("BLUE > 0", 200, height - 75);
		//		graphButton.show();
		//		loadButton.show();
		//		saveButton.show();
		//	}
		//}

		public abstract void fileSelectedIn(string path);
		/*{
			using (FileStream selection = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
				if (selection == null)
				{
					//println("Window was closed or the user hit cancel.");
					Console.WriteLine("Window was closed or the user hit cancel.");
				}
				else
				{
					//string path = selection.getAbsolutePath();
					DataTable modelTable = new DataTable("header");
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
					model = new ISnake(weights.Length - 1);
					model.brain.load(weights);
				}
		}*/

		public void fileSelectedOut(string path)
		{
			using (FileStream selection = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
				if (selection == null)
				{
					//println("Window was closed or the user hit cancel.");
					Console.WriteLine("Window was closed or the user hit cancel.");
				}
				else
				{
					//string path = selection.getAbsolutePath();
					DataTable modelTable = new DataTable();
					Snake modelToSave = (Snake)pop.bestSnake.Clone();
					Matrix[] modelWeights = modelToSave.brain.pull();
					float[][] weights = new float[modelWeights.Length][];
					for (int i = 0; i < weights.Length; i++)
					{
						weights[i] = modelWeights[i].ToArray();
					}
					for (int i = 0; i < weights.Length; i++)
					{
						modelTable.Columns.Add("L" + i, typeof(float));
					}
					modelTable.Columns.Add("Graph", typeof(int));
					int maxLen = weights[0].Length;
					for (int i = 1; i < weights.Length; i++)
					{
						if (weights[i].Length > maxLen)
						{
							maxLen = weights[i].Length;
						}
					}
					int g = 0;
					for (int i = 0; i < maxLen; i++)
					{
						DataRow newRow = modelTable.NewRow();
						for (int j = 0; j < weights.Length + 1; j++)
						{
							if (j == weights.Length)
							{
								if (g < evolution.Count)
								{
									//newRow.setInt("Graph", evolution[g]);
									newRow["Graph"] = evolution[g];
									g++;
								}
							}
							else if (i < weights[j].Length)
							{
								//newRow.setFloat("L" + j, weights[j][i]);
								newRow["L" + j] = weights[j][i];
							}
						}
					}
					modelTable.AcceptChanges();
					//saveTable(modelTable, path);
					modelTable.WriteXml(selection);
				}
		}

		/*public virtual void mousePressed()
		{
			//if (graphButton.collide(mouseX, mouseY))
			//{
			//	graph = new EvolutionGraph();
			//}
			//if (loadButton.collide(mouseX, mouseY))
			//{
			//	selectInput("Load Snake Model", "fileSelectedIn");
			//}
			//if (saveButton.collide(mouseX, mouseY))
			//{
			//	selectOutput("Save Snake Model", "fileSelectedOut");
			//}
			//if (increaseMut.collide(mouseX, mouseY))
			//{
			//	mutationRate *= 2;
			//	defaultmutation = mutationRate;
			//}
			//if (decreaseMut.collide(mouseX, mouseY))
			//{
			//	mutationRate /= 2;
			//	defaultmutation = mutationRate;
			//}
		}

		public virtual void keyPressed()
		{
			if (humanPlaying)
			{
				if (Console.KeyAvailable) //(key == CODED)
				{
					ConsoleKeyInfo key = Console.ReadKey(true);
					switch (key.Key) //(keyCode)
					{
						//case UP:
						case ConsoleKey.UpArrow:
							snake.moveUp();
							break;
						//case DOWN:
						case ConsoleKey.DownArrow:
							snake.moveDown();
							break;
						//case LEFT:
						case ConsoleKey.LeftArrow:
							snake.moveLeft();
							break;
						//case RIGHT:
						case ConsoleKey.RightArrow:
							snake.moveRight();
							break;
					}
				}
			}
		}*/
	}
}