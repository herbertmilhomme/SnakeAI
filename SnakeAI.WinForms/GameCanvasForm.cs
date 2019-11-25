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
		private Snake m_Snake;
		private Piece m_Fruit;
		private GameManager m_GameManager;

		public GameCanvasForm()
		{
			InitCustomLabelFont();
			InitializeComponent();
			GenLabel.Parent = Background;
			RedLabel.Parent = Background;
			BlueLabel.Parent = Background;
			ScoreLabel.Parent = Background;
			HighscoreLabel.Parent = Background;
			MutationLabel.Parent = Background;
			m_Snake = new Snake();
			m_Fruit = new Piece();
			m_GameManager = new GameManager(w: Canvas.Size.Width, h: Canvas.Size.Height);

			GameTimer.Interval = 1000 / m_GameManager.Speed;
			GameTimer.Tick += UpdateScreen;
			GameTimer.Start();

			StartGame();
		}

		private void StartGame()
		{
			ResetSettings();
			GenerateFruit();
		}

		//ToDo: Add `SnakeAI.Core.Draw()` here...
		private void UpdateScreen(object sender, EventArgs e)
		{
			if (m_GameManager.GameOver)
			{
				//if (UserInputController.KeyPressed(Keys.Enter))
				//{
					StartGame();
				//}
			}
			else
			{
				if (UserInputController.KeyPressed(Keys.Right) && m_GameManager.Direction != eDirection.Left)
					m_GameManager.Direction = eDirection.Right;
				else if (UserInputController.KeyPressed(Keys.Left) && m_GameManager.Direction != eDirection.Right)
					m_GameManager.Direction = eDirection.Left;
				else if (UserInputController.KeyPressed(Keys.Up) && m_GameManager.Direction != eDirection.Down)
					m_GameManager.Direction = eDirection.Up;
				else if (UserInputController.KeyPressed(Keys.Down) && m_GameManager.Direction != eDirection.Up)
					m_GameManager.Direction = eDirection.Down;

				DoMove();
			}

			Canvas.Invalidate();
		}

		private void ResetSettings()
		{
			m_GameManager = new GameManager(w: Canvas.Size.Width, h: Canvas.Size.Height);
			m_Snake.Body.Clear();
			//GameOverPic.Hide();
			//GameOverLabel.Hide();
			Piece head = new Piece { X = 10, Y = 5 };//(int)(Canvas.Size.Height/2)
			m_Snake.Add(head);
			ScoreLabel.Text = m_GameManager.Score.ToString();
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			Graphics canvas = e.Graphics;
			//canvas.DrawRectangle(Pens.White, Canvas.Bounds);
			//ControlPaint.DrawBorder(canvas, Canvas.Bounds, Color.White, ButtonBorderStyle.Solid);
			ControlPaint.DrawBorder(canvas, new Rectangle(0, 0, Canvas.Size.Width, Canvas.Size.Height), Color.White, ButtonBorderStyle.Solid);
			#region Snake Game
			if (!m_GameManager.GameOver)
			{
				for (int i = 0; i < m_Snake.Length; i++)
				{
					Brush snakeColour;
					if (i == 0)
						snakeColour = Brushes.White;     //Draw head
					else
						snakeColour = Brushes.Yellow;    //Rest of body

					canvas.FillRectangle(snakeColour,
						new Rectangle(m_Snake.Body[i].X * m_GameManager.Width,
									  m_Snake.Body[i].Y * m_GameManager.Height,
									  m_GameManager.Width, m_GameManager.Height));


					canvas.FillRectangle(Brushes.Red,
						new Rectangle(m_Fruit.X * m_GameManager.Width,
							 m_Fruit.Y * m_GameManager.Height, m_GameManager.Width, m_GameManager.Height));
				}
				for (int x = 1; x < Canvas.Size.Width / m_GameManager.Width; x++)
				{
					canvas.DrawLine(Pens.Black, x1: x * m_GameManager.Width, y1: 1, x2: x * m_GameManager.Width, y2: Canvas.Size.Height-2);
				}
				for (int y = 1; y < Canvas.Size.Height/m_GameManager.Height; y++)
				{
					canvas.DrawLine(Pens.Black, x1: 1, y1: y * m_GameManager.Height, x2: Canvas.Size.Width-2, y2: y * m_GameManager.Height);
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

		private void GenerateFruit()
		{
			int maxXPosition = Canvas.Size.Width / m_GameManager.Width;
			int maxYPositon = Canvas.Size.Height / m_GameManager.Height;

			Random random = new Random();

			m_Fruit = new Piece { X = random.Next(0, maxXPosition), Y = random.Next(0, maxYPositon) };
		}

		private void DoMove()
		{
			for (int i = m_Snake.Length - 1; i >= 0; i--)
			{
				//Move head
				if (i == 0)
				{
					switch (m_GameManager.Direction)
					{
						case eDirection.Right:
							m_Snake.MoveRight(i);
							break;
						case eDirection.Left:
							m_Snake.MoveLeft(i);
							break;
						case eDirection.Up:
							m_Snake.MoveUp(i);
							break;
						case eDirection.Down:
							m_Snake.MoveDown(i);
							break;
					}

					int maxXPos = Canvas.Size.Width / m_GameManager.Width;
					int maxYPos = Canvas.Size.Height / m_GameManager.Height;

					if (m_Snake.Body[i].X < 0 || m_Snake.Body[i].Y < 0
						|| m_Snake.Body[i].X >= maxXPos || m_Snake.Body[i].Y >= maxYPos)
					{
						Die();
					}

					for (int j = 1; j < m_Snake.Length; j++)
					{
						if (m_Snake.Body[i].X == m_Snake.Body[j].X &&
						   m_Snake.Body[i].Y == m_Snake.Body[j].Y)
						{
							Die();
						}
					}

					if (m_Snake.Body[0].X == m_Fruit.X && m_Snake.Body[0].Y == m_Fruit.Y)
					{
						Eat();
					}

				}
				else
				{
					//Move body
					m_Snake.Body[i].X = m_Snake.Body[i - 1].X;
					m_Snake.Body[i].Y = m_Snake.Body[i - 1].Y;
				}
			}
		}

		private void Eat()
		{
			Piece piece = new Piece
			{
				X = m_Snake.Body[m_Snake.Length - 1].X,
				Y = m_Snake.Body[m_Snake.Length - 1].Y
			};
			m_Snake.Add(piece);

			m_GameManager.Score += m_GameManager.Points;
			ScoreLabel.Text = m_GameManager.Score.ToString();

			GenerateFruit();
		}

		private void Die()
		{
			m_GameManager.GameOver = true;
		}

		private void GameCanvasForm_KeyDown(object sender, KeyEventArgs e)
		{
			UserInputController.ChangeState(e.KeyCode, true);
		}

		private void GameCanvasForm_KeyUp(object sender, KeyEventArgs e)
		{
			UserInputController.ChangeState(e.KeyCode, false);
		}
	}
}
