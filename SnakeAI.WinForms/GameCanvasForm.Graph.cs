using System.Drawing;
using System.Windows.Forms;

namespace SnakeAI.WinForms
{
	partial class GameCanvasForm
	{
		private void Graph_Paint(object sender, PaintEventArgs e)
		{
			if (!GameManager.humanPlaying)
			{
				Graphics canvas = e.Graphics;
				//canvas.DrawLine(Pens.White, x1: 400, y1: 0, x2: 400, y2: Canvas.Size.Height);
				canvas.DrawLine(Pens.Green, x1: 400, y1: 0, x2: 400, y2: ClientSize.Height);
				//ControlPaint.DrawBorder(canvas, new Rectangle(400, 0, 1, Canvas.Size.Height), Color.White, ButtonBorderStyle.Solid);
			
			
				#region Neural Net
				int x = 5;
				int y = 200;
				int w = 360;
				int h = ClientSize.Height - 410; //790;
				Snake snake = (Snake)GameManager.snake;
				float[] vision = snake.vision;
				float[] decision = snake.decision;
				int space = 5;
				int nSize = (int)((h - (space * (snake.brain.iNodes - 2))) / snake.brain.iNodes);
				int nSpace = (int)((w - (snake.brain.weights.Length * nSize)) / snake.brain.weights.Length);
				float hBuff = (h - (space * (snake.brain.hNodes - 1)) - (nSize * snake.brain.hNodes)) / 2;
				float oBuff = (h - (space * (snake.brain.oNodes - 1)) - (nSize * snake.brain.oNodes)) / 2;
			
				int maxIndex = 0;
				for (int i = 1; i < decision.Length; i++)
					if (decision[i] > decision[maxIndex])
						maxIndex = i;
			
				//Layer Count
				int lc = 0;  
				Brush fill;
			
				//DRAW NODES
				for (int i = 0; i < snake.brain.iNodes; i++)
				{  
					Brush color = Brushes.Black;
					//DRAW INPUTS
					if (vision[i] != 0)
					{
						fill = Brushes.Green; //(0, 255, 0);
						color = Brushes.White; 
					}
					else
						fill = Brushes.White; //(255);
						//color = Brushes.Black; 
					//stroke(0);
					//ellipseMode(CORNER);
					canvas.FillEllipse(fill, new Rectangle((int)x, (int)y + (int)(i * (nSize + space)), (int)nSize, (int)nSize));
					//textSize(nSize / 2);
					//textAlign(CENTER, CENTER);
					//fill(0);
					//Text = string.Format(i.ToString());//, x + (nSize / 2), y + (nSize / 2) + (i * (nSize + space))
					canvas.DrawString(i.ToString(), new Font("Agency FB", nSize, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0))), color, i < 10 ? x + (int)(nSize * .25) : x, y - 2 + (i * (nSize + space)));
				}
			
				lc++;
			
				for (int a = 0; a < snake.brain.hLayers; a++)
				{
					for (int i = 0; i < snake.brain.hNodes; i++)
					{  
						//DRAW HIDDEN
						fill = Brushes.White; //(255);
						//stroke(0);
						//ellipseMode(CORNER);
						canvas.FillEllipse(fill, new Rectangle((int)(x + (lc * nSize) + (lc * nSpace)), (int)(y + hBuff + (i * (nSize + space))), (int)nSize, (int)nSize));
					}
					lc++;
				}
			
				for (int i = 0; i < snake.brain.oNodes; i++)
				{  
					//DRAW OUTPUTS
					if (i == maxIndex)
						fill = Brushes.Green; //(0, 255, 0);
					else
						fill = Brushes.White; //(255);
					//stroke(0);
					//ellipseMode(CORNER);
					canvas.FillEllipse(fill, new Rectangle((int)(x + (lc * nSpace) + (lc * nSize)), (int)(y + oBuff + (i * (nSize + space))), (int)nSize, (int)nSize));
				}
			
				lc = 1;
				Pen stroke;
			
				//DRAW WEIGHTS
				for (int i = 0; i < snake.brain.weights[0].rows; i++)
					//INPUT TO HIDDEN
					for (int j = 0; j < snake.brain.weights[0].cols - 1; j++)
					{
						if (snake.brain.weights[0].matrix[i][j] < 0)
							stroke = Pens.Blue; //(255, 0, 0);
						else if (snake.brain.weights[0].matrix[i][j] > 0)
							stroke = Pens.Red; //(0, 0, 255);
						else
							stroke = Pens.White;
						canvas.DrawLine(stroke, x1: x + nSize, y1: y + (nSize / 2) + (j * (space + nSize)), x2: x + nSize + nSpace, y2: y + hBuff + (nSize / 2) + (i * (space + nSize)));
					}
			
				lc++;
			
				for (int a = 1; a < snake.brain.hLayers; a++)
				{
					for (int i = 0; i < snake.brain.weights[a].rows; i++)
						//HIDDEN TO HIDDEN
						for (int j = 0; j < snake.brain.weights[a].cols - 1; j++)
						{
							if (snake.brain.weights[a].matrix[i][j] < 0)
								stroke = Pens.Blue; //(255, 0, 0);
							else if (snake.brain.weights[a].matrix[i][j] > 0)
								stroke = Pens.Red; //(0, 0, 255);
							else
								stroke = Pens.White;
							canvas.DrawLine(stroke, x1: x + (lc * nSize) + ((lc - 1) * nSpace), y + hBuff + (nSize / 2) + (j * (space + nSize)), x + (lc * nSize) + (lc * nSpace), y + hBuff + (nSize / 2) + (i * (space + nSize)));
						}
					lc++;
				}
			
				for (int i = 0; i < snake.brain.weights[snake.brain.weights.Length - 1].rows; i++)
					//HIDDEN TO OUTPUT
					for (int j = 0; j < snake.brain.weights[snake.brain.weights.Length - 1].cols - 1; j++)
					{
						if (snake.brain.weights[snake.brain.weights.Length - 1].matrix[i][j] < 0)
							stroke = Pens.Blue; //(255, 0, 0);
						else if (snake.brain.weights[snake.brain.weights.Length - 1].matrix[i][j] > 0)
							stroke = Pens.Red; //(0, 0, 255);
						else
							stroke = Pens.White;
						canvas.DrawLine(stroke, x1: x + (lc * nSize) + ((lc - 1) * nSpace), y + hBuff + (nSize / 2) + (j * (space + nSize)), x + (lc * nSize) + (lc * nSpace), y + oBuff + (nSize / 2) + (i * (space + nSize)));
					}

				//fill(0);
				//textSize(15);
				//textAlign(CENTER, CENTER);
				//Text = string.Format("U"); //, x + (lc * nSize) + (lc * nSpace) + nSize / 2, y + oBuff + (nSize / 2));
				//Text = string.Format("D"); //, x + (lc * nSize) + (lc * nSpace) + nSize / 2, y + oBuff + space + nSize + (nSize / 2));
				//Text = string.Format("L"); //, x + (lc * nSize) + (lc * nSpace) + nSize / 2, y + oBuff + (2 * space) + (2 * nSize) + (nSize / 2));
				//Text = string.Format("R"); //, x + (lc * nSize) + (lc * nSpace) + nSize / 2, y + oBuff + (3 * space) + (3 * nSize) + (nSize / 2));
				canvas.DrawString("U", new Font("Agency FB", 15F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0))), Brushes.White, x + (lc * nSize) + (lc * nSpace) + (int)(nSize * .5) + 10, y + (-8) + oBuff + (nSize / 2));
				canvas.DrawString("D", new Font("Agency FB", 15F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0))), Brushes.White, x + (lc * nSize) + (lc * nSpace) + (int)(nSize * .5) + 10, y + (-8) + oBuff + space + nSize + (nSize / 2));
				canvas.DrawString("L", new Font("Agency FB", 15F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0))), Brushes.White, x + (lc * nSize) + (lc * nSpace) + (int)(nSize * .5) + 10, y + (-8) + oBuff + (2 * space) + (2 * nSize) + (nSize / 2));
				canvas.DrawString("R", new Font("Agency FB", 15F, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0))), Brushes.White, x + (lc * nSize) + (lc * nSpace) + (int)(nSize * .5) + 10, y + (-8) + oBuff + (3 * space) + (3 * nSize) + (nSize / 2));
				#endregion
			}
		}
	}
}