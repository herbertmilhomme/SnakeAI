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
		private Shared.Vector m_Fruit;
		private GameManager m_GameManager;
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
			m_GameManager = new GameManager();//w: Canvas.Size.Width, h: Canvas.Size.Height
			m_GameManager.setup();
			m_Snake = new Snake();
			m_Fruit = new Shared.Vector();

			GameTimer.Interval = 1000 / m_GameManager.Speed;
			GameTimer.Tick += UpdateScreen;
			GameTimer.Start();

			this.KeyPreview = true;
			StartGame();
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
			ResetSettings();
			GenerateFruit();
		}

		//ToDo: Add `SnakeAI.Core.Draw()` here...
		private void UpdateScreen(object sender, EventArgs e)
		{
			if (m_GameManager.GameOver)
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
					m_Snake.moveRight();
				else if (UserInputController.KeyPressed(Keys.Left))// && m_GameManager.Direction != eDirection.Right)
					//m_GameManager.Direction = eDirection.Left;
					m_Snake.moveLeft();
				else if (UserInputController.KeyPressed(Keys.Up))// && m_GameManager.Direction != eDirection.Down)
					//m_GameManager.Direction = eDirection.Up;
					m_Snake.moveUp();
				else if (UserInputController.KeyPressed(Keys.Down))// && m_GameManager.Direction != eDirection.Up)
					//m_GameManager.Direction = eDirection.Down;
					m_Snake.moveDown();

				DoMove();
			}

			Canvas.Invalidate();
		}

		private void ResetSettings()
		{
			m_GameManager = new GameManager();//w: Canvas.Size.Width, h: Canvas.Size.Height
			m_Snake.body.Clear();
			//GameOverPic.Hide();
			//GameOverLabel.Hide();
			Shared.Vector head = new Shared.Vector { x = (int)((Canvas.Size.Width / m_GameManager.Width )/ 2), y = 5 };//
			m_Snake.Add(head);
			//ScoreLabel.Text = m_GameManager.Score.ToString();
			ScoreLabel.Text = string.Format("SCORE: {0}", (m_Snake.Length - 1).ToString());
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
						new Rectangle((int)m_Snake.body[i].x * m_GameManager.Width,
									  (int)m_Snake.body[i].y * m_GameManager.Height,
									  m_GameManager.Width, m_GameManager.Height));


					canvas.FillRectangle(Brushes.Red,
						new Rectangle((int)m_Fruit.x * m_GameManager.Width,
							 (int)m_Fruit.y * m_GameManager.Height, m_GameManager.Width, m_GameManager.Height));
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

			//Random random = new Random();

			m_Fruit = new Shared.Vector { x = Shared.Core.Rand.Next(0, maxXPosition), y = Shared.Core.Rand.Next(0, maxYPositon) };
		}

		private void DoMove()
		{
			for (int i = m_Snake.Length - 1; i >= 0; i--)
			{
				//Move head
				if (i == 0)
				{
					switch (m_Snake.direction)//m_GameManager.Direction
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

					if (m_Snake.body[i].x < 0 || m_Snake.body[i].y < 0
						|| m_Snake.body[i].x >= maxXPos || m_Snake.body[i].y >= maxYPos)
					{
						Die();
					}

					for (int j = 1; j < m_Snake.Length; j++)
					{
						if (m_Snake.body[i].x == m_Snake.body[j].x &&
						   m_Snake.body[i].y == m_Snake.body[j].y)
						{
							Die();
						}
					}

					if (m_Snake.body[0].x == m_Fruit.x && m_Snake.body[0].y == m_Fruit.y)
					{
						Eat();
					}

				}
				else
				{
					//Move body
					//m_Snake.body[i].x = m_Snake.body[i - 1].x;
					//m_Snake.body[i].y = m_Snake.body[i - 1].y;
					m_Snake.body[i] = new Shared.Vector( m_Snake.body[i - 1].x, m_Snake.body[i - 1].y);
				}
			}
		}

		private void Eat()
		{
			Shared.Vector piece = new Shared.Vector
			{
				x = m_Snake.body[m_Snake.Length - 1].x,
				y = m_Snake.body[m_Snake.Length - 1].y
			};
			m_Snake.Add(piece);

			//m_GameManager.Score += m_GameManager.Points;
			//ScoreLabel.Text = string.Format("SCORE: {0}", m_GameManager.Score.ToString());
			ScoreLabel.Text = string.Format("SCORE: {0}", (m_Snake.Length - 1).ToString());

			GenerateFruit();
		}

		private void Die()
		{
			m_GameManager.GameOver = true;
		}

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
	}
}
