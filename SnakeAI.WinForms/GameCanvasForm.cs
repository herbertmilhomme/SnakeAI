using SnakeAI.WinForms;
using SnakeAI.WinForms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeAI.WinForms
{
	public partial class GameCanvasForm : Form
	{
		//private int hs;
		//private Snake m_Snake;
		//private Shared.Vector m_Fruit;
		//private GameManager m_GameManager;
		private System.Drawing.Text.PrivateFontCollection pfc;

		public GameCanvasForm()
		{
			//InitCustomLabelFont();
			InitializeComponent();
			GenLabel.Parent = Background;
			RedLabel.Parent = Background;
			BlueLabel.Parent = Background;
			ScoreLabel.Parent = Background;
			HighscoreLabel.Parent = Background;
			MutationLabel.Parent = Background;
			Shared.Core.width = Canvas.Size.Width; Shared.Core.height = Canvas.Size.Height;
			//m_GameManager = new GameManager();//w: Canvas.Size.Width, h: Canvas.Size.Height
			this.KeyPreview = true;
			GameManager.setup();
			//GameManager.snake = new Snake();
			//GameManager.snake.food = new Shared.Vector();

			GameTimer.Interval = 1000 / GameManager.Speed;
			GameTimer.Tick += UpdateScreen;
			GameTimer.Start();

			//StartGame();
		}

		private void InitCustomLabelFont()
		{
			//Create your private font collection object.
			pfc = new System.Drawing.Text.PrivateFontCollection();

			//Select your font from the resources.
			int fontLength = Properties.Resources.agencyfb_bold.Length;

			// create a buffer to read in to
			byte[] fontdata = Properties.Resources.agencyfb_bold;

			// create an unsafe memory block for the font data
			System.IntPtr data = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontLength);

			// copy the bytes to the unsafe memory block
			System.Runtime.InteropServices.Marshal.Copy(fontdata, 0, data, fontLength);

			// pass the font to the font collection
			pfc.AddMemoryFont(data, fontLength);
		}

		private void StartGame()
		{
			if(GameManager.humanPlaying)
				this.KeyPreview = true;

			ResetSettings();
			//GenerateFruit();
		}

		//ToDo: Add `SnakeAI.Core.Draw()` here...
		private void UpdateScreen(object sender, EventArgs e)
		{
			if (GameManager.humanPlaying)
			{
				if (GameManager.GameOver)
				{
					//if (UserInputController.KeyPressed(Keys.Enter) || UserInputController.KeyPressed(Keys.Space))
					//{
						StartGame();
					//}
				}
				else
				{
					if (UserInputController.KeyPressed(Keys.Right))// && m_GameManager.Direction != eDirection.Left)
						//m_GameManager.Direction = eDirection.Right;
						GameManager.snake.moveRight();
					else if (UserInputController.KeyPressed(Keys.Left))// && m_GameManager.Direction != eDirection.Right)
						//m_GameManager.Direction = eDirection.Left;
						GameManager.snake.moveLeft();
					else if (UserInputController.KeyPressed(Keys.Up))// && m_GameManager.Direction != eDirection.Down)
						//m_GameManager.Direction = eDirection.Up;
						GameManager.snake.moveUp();
					else if (UserInputController.KeyPressed(Keys.Down))// && m_GameManager.Direction != eDirection.Up)
						//m_GameManager.Direction = eDirection.Down;
						GameManager.snake.moveDown();

					GameManager.snake.move();//DoMove();
					ScoreLabel.Text = string.Format("SCORE: {0}", ((Snake)GameManager.snake).score.ToString());
					//ScoreLabel.Text = string.Format("SCORE: {0}", (m_Snake.Length - 1).ToString());
					if (((Snake)GameManager.snake).score > GameManager.highscore)
					{
						GameManager.highscore = ((Snake)GameManager.snake).score;
						HighscoreLabel.Text = string.Format("HIGHSCORE: {0}", GameManager.highscore.ToString());
					}
				}
			}
			else
			{
				if (!GameManager.modelLoaded)
				{
					if (GameManager.pop.done())
					{
						GameManager.highscore = GameManager.pop.bestSnake.score;
						GameManager.pop.calculateFitness();
						GameManager.pop.naturalSelection();
					}
					else
					{
						GameManager.pop.update();
						//Draw Neural Net
						//m_GameManager.pop.show();
						if (GameManager.replayBest)
						{
							//show the brain of the best snake
							//bestSnake.brain.show(0, 0, 360, 790, bestSnake.vision, bestSnake.decision);
							GameManager.snake = GameManager.pop.bestSnake; //vision = m_GameManager.pop.bestSnake.vision; decision = m_GameManager.pop.bestSnake.decision;
							//this.Background.Paint += new System.Windows.Forms.PaintEventHandler(this.Graph_Paint);
							//GameManager.pop.bestSnake.show();
							for (int i = 0; i < GameManager.snake.body.Count; i++)
								GameManager.snake.body[i] = new Shared.Vector() { x = GameManager.snake.body[i].x - 1, y = GameManager.snake.body[i].y };
						}
						else
							for (int i = 0; i < GameManager.pop.Snakes.Length; i++)
							{
								//GameManager.pop.Snakes[i].show();
								GameManager.snake = GameManager.pop.Snakes[i];
								for (int j = 0; j < GameManager.snake.body.Count; j++)
									GameManager.snake.body[j] = new Shared.Vector() { x = GameManager.snake.body[j].x - 1, y = GameManager.snake.body[j].y };
							}
					}
					GenLabel.Text = string.Format("GEN: {0}", GameManager.pop.gen.ToString());
					ScoreLabel.Text = string.Format("SCORE: {0}", GameManager.pop.bestSnakeScore.ToString());
					HighscoreLabel.Text = string.Format("HIGHSCORE: {0}", GameManager.highscore.ToString());
				}
				else
				{
					GameManager.snake.look();
					GameManager.snake.think();
					GameManager.snake.move();
					//m_GameManager.model.show();
					//Draw Neural Net
					//m_GameManager.model.brain.show(0, 0, 360, 790, m_GameManager.model.vision, m_GameManager.model.decision);
					//snake = GameManager.model; //vision = m_GameManager.model.vision; decision = m_GameManager.model.decision;
					//this.Background.Paint += new System.Windows.Forms.PaintEventHandler(this.Graph_Paint);
					if (((Snake)GameManager.snake).dead)
					{
						Snake newmodel = new Snake();
						newmodel.brain = GameManager.snake.brain.Clone();
						GameManager.snake = newmodel;
					}
					ScoreLabel.Text = string.Format("SCORE: {0}", ((Snake)GameManager.snake).score.ToString());
				}
			}

			Canvas.Invalidate();
			Background.Invalidate();
		}

		private void ResetSettings()
		{
			//m_GameManager = new GameManager();//w: Canvas.Size.Width, h: Canvas.Size.Height
			//GameManager.GameOver = false;
			//GameManager.snake.body.Clear();
			//((Snake)GameManager.snake).direction = eDirection.Down;
			//GameOverPic.Hide();
			//GameOverLabel.Hide();
			//Shared.Vector head = new Shared.Vector { x = (int)((Canvas.Size.Width / m_GameManager.Width )/ 2), y = 5 };
			//Shared.Vector head = new Shared.Vector { x = (int)((Shared.Core.width / Shared.Core.SIZE) / 2), y = 5 };
			//((Snake)GameManager.snake).Add(head);
			GameManager.snake = new Snake();
			//ScoreLabel.Text = m_GameManager.Score.ToString();
			ScoreLabel.Text = string.Format("SCORE: {0}", (((Snake)GameManager.snake).score).ToString());
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics canvas = e.Graphics;
			//canvas.DrawRectangle(Pens.White, Canvas.Bounds);
			//ControlPaint.DrawBorder(canvas, Canvas.Bounds, Color.White, ButtonBorderStyle.Solid);
			ControlPaint.DrawBorder(canvas, new Rectangle(0, 0, Canvas.Size.Width, Canvas.Size.Height), Color.White, ButtonBorderStyle.Solid);
			#region Snake Game
			if (!GameManager.GameOver)
			{
				for (int i = 0; i < GameManager.snake.Length; i++)
				{
					Brush snakeColour;
					if (i == 0)
						snakeColour = Brushes.White;     //Draw head
					else
						snakeColour = Brushes.Yellow;    //Rest of body

					canvas.FillRectangle(snakeColour,
						new Rectangle((int)GameManager.snake.body[i].x * GameManager.Width,
									  (int)GameManager.snake.body[i].y * GameManager.Height,
									  GameManager.Width, GameManager.Height));

					canvas.FillRectangle(Brushes.Red,
						new Rectangle((int)GameManager.snake.food.x * GameManager.Width,
							 (int)GameManager.snake.food.y * GameManager.Height, GameManager.Width, GameManager.Height));
				}
				for (int x = 1; x < Canvas.Size.Width / GameManager.Width; x++)
				{
					canvas.DrawLine(Pens.Black, x1: x * GameManager.Width, y1: 1, x2: x * GameManager.Width, y2: Canvas.Size.Height-2);
				}
				for (int y = 1; y < Canvas.Size.Height/GameManager.Height; y++)
				{
					canvas.DrawLine(Pens.Black, x1: 1, y1: y * GameManager.Height, x2: Canvas.Size.Width-2, y2: y * GameManager.Height);
				}
			}
			else
			{
				//GameOver(); 
			}
			#endregion
		}

		//private void GameOver()
		//{
		//	string gameOver = "Your final score is: " + m_GameManager.Score + "\nPress enter to try again!";
		//	GameOverLabel.Font = new Font("Arial", 16, FontStyle.Bold);
		//	GameOverLabel.Text = gameOver;
		//	GameOverPic.Show();
		//	GameOverLabel.Show();
		//}

		//private void GenerateFruit()
		//{
		//	int maxXPosition = Canvas.Size.Width / m_GameManager.Width;
		//	int maxYPositon = Canvas.Size.Height / m_GameManager.Height;
		//
		//	if (!GameManager.snake.replay)
		//	{
		//		do
		//			GameManager.Food = new Shared.Vector { x = GameManager.Rand.Next(0, maxXPosition), y = GameManager.Rand.Next(0, maxYPositon) };
		//		while (GameManager.snake.bodyCollide(GameManager.Food.x, GameManager.Food.y));
		//
		//		if (!GameManager.humanPlaying)
		//			((Snake)GameManager.snake).foodList.Add(GameManager.Food);
		//	}
		//	//if the snake is a replay, then we dont want to create new random foods, 
		//	//want to see the positions the best snake had to collect
		//	else
		//	{
		//		GameManager.Food = ((Snake)GameManager.snake).foodList[GameManager.snake.foodItterate];
		//		GameManager.snake.foodItterate++;
		//	}
		//}

		//private void DoMove()
		//{
		//	if (!GameManager.humanPlaying && !GameManager.modelLoaded)
		//	{
		//		GameManager.snake.lifetime++;
		//		GameManager.snake.lifeLeft--;
		//	}

		//	for (int i = GameManager.snake.Length - 1; i >= 0; i--)
		//	{
		//		//Move head
		//		if (i == 0)
		//		{
		//			switch (((Snake)GameManager.snake).direction)//m_GameManager.Direction
		//			{
		//				case eDirection.Right:
		//					((Snake)GameManager.snake).MoveRight(i);
		//					break;
		//				case eDirection.Left:
		//					((Snake)GameManager.snake).MoveLeft(i);
		//					break;
		//				case eDirection.Up:
		//					((Snake)GameManager.snake).MoveUp(i);
		//					break;
		//				case eDirection.Down:
		//					((Snake)GameManager.snake).MoveDown(i);
		//					break;
		//			}

		//			int maxXPos = Canvas.Size.Width / GameManager.Width;
		//			int maxYPos = Canvas.Size.Height / GameManager.Height;

		//			//if wall collide
		//			if (GameManager.snake.body[i].x < 0 || GameManager.snake.body[i].y < 0
		//				|| GameManager.snake.body[i].x >= maxXPos || GameManager.snake.body[i].y >= maxYPos)
		//			{
		//				Die();
		//			}

		//			//if body collide
		//			for (int j = 1; j < GameManager.snake.Length; j++)
		//			{
		//				if (GameManager.snake.body[i].x == GameManager.snake.body[j].x &&
		//				   GameManager.snake.body[i].y == GameManager.snake.body[j].y)
		//				{
		//					Die();
		//				}
		//			}

		//			//if food collide
		//			if (GameManager.snake.body[0].x == GameManager.snake.food.x && GameManager.snake.body[0].y == GameManager.snake.food.y)
		//			{
		//				Eat();
		//			}

		//			if (GameManager.snake.lifeLeft <= 0 && !GameManager.humanPlaying)
		//			{
		//				Die(); //dead = true;
		//			}
		//		}
		//		else
		//		{
		//			//Move body
		//			//m_Snake.body[i].x = m_Snake.body[i - 1].x;
		//			//m_Snake.body[i].y = m_Snake.body[i - 1].y;
		//			GameManager.snake.body[i] = new Shared.Vector( GameManager.snake.body[i - 1].x, GameManager.snake.body[i - 1].y);
		//		}
		//	}
		//}

		private void Eat()
		{
			//if (!GameManager.humanPlaying && !GameManager.modelLoaded)
			//	if (GameManager.snake.lifeLeft < 500)
			//		if (GameManager.snake.lifeLeft > 400)
			//			GameManager.snake.lifeLeft = 500;
			//		else
			//			GameManager.snake.lifeLeft += 100;
			//
			//Shared.Vector piece = new Shared.Vector
			//{
			//	x = GameManager.snake.body[m_GameManager.Score].x,
			//	y = GameManager.snake.body[m_GameManager.Score].y
			//};
			//((Snake)GameManager.snake).Add(piece);

			//m_GameManager.Score += m_GameManager.Points;
			ScoreLabel.Text = string.Format("SCORE: {0}", ((Snake)GameManager.snake).score.ToString());
			//ScoreLabel.Text = string.Format("SCORE: {0}", (m_Snake.Length - 1).ToString());
			if (((Snake)GameManager.snake).score > GameManager.highscore)
			{
				GameManager.highscore = ((Snake)GameManager.snake).score;
				HighscoreLabel.Text = string.Format("HIGHSCORE: {0}", GameManager.highscore.ToString());
			}

			//GenerateFruit();
		}

		//private void Die()
		//{
		//	((Snake)GameManager.snake).dead = true;
		//	//GameManager.GameOver = true;
		//}

		#region Event Handlers
		private void GameCanvasForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Up || 
				e.KeyCode == System.Windows.Forms.Keys.Down || 
				e.KeyCode == System.Windows.Forms.Keys.Left || 
				e.KeyCode == System.Windows.Forms.Keys.Right)
			{
				// Do not put any logic here, instead use the 
				// KeyDown event after setting IsInputKey to true
				e.IsInputKey = true;
			}
		}

		private void GameCanvasForm_KeyDown(object sender, KeyEventArgs e)
		{
			UserInputController.ChangeState(e.KeyCode, true);
		}

		private void GameCanvasForm_KeyUp(object sender, KeyEventArgs e)
		{
			UserInputController.ChangeState(e.KeyCode, false);
		}

		private void GameCanvasForm_GraphMousePress(object sender, MouseEventArgs e)
		{
			//GameManager.graph = new Shared.EvolutionGraph();
			// Change Scene to Graph Form
		}

		private void GameCanvasForm_LoadMousePress(object sender, MouseEventArgs e)
		{
			//selectInput("Load Snake Model", "fileSelectedIn");
			using (OpenFileDialog open = new OpenFileDialog())
			{
				//open.InitialDirectory
				//open.Filter
				open.FilterIndex = 2;
				open.RestoreDirectory = true;
				open.Title = "Load Snake Model";

				if (open.ShowDialog() == DialogResult.OK)
					GameManager.fileSelectedIn(open.FileName);
			}
		}

		private void GameCanvasForm_SaveMousePress(object sender, MouseEventArgs e)
		{
			//selectOutput("Save Snake Model", "fileSelectedOut");
			using (SaveFileDialog save = new SaveFileDialog())
			{
				save.Title = "Save Snake Model";
				save.ShowDialog();

				if (!string.IsNullOrEmpty(save.FileName) || !string.IsNullOrWhiteSpace(save.FileName))
					GameManager.fileSelectedOut(save.FileName);
			}
		}

		private void GameCanvasForm_IncreaseMousePress(object sender, MouseEventArgs e)
		{
			GameManager.mutationRate *= 2;
			GameManager.defaultmutation = GameManager.mutationRate;
			MutationLabel.Text = string.Format("MUTATION RATE: {0}%", (GameManager.mutationRate * 100).ToString());
		}

		private void GameCanvasForm_DecreaseMousePress(object sender, MouseEventArgs e)
		{
			GameManager.mutationRate /= 2;
			GameManager.defaultmutation = GameManager.mutationRate;
			MutationLabel.Text = string.Format("MUTATION RATE: {0}%", (GameManager.mutationRate * 100).ToString());
		}
		#endregion
	}
}