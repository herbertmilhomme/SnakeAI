using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeAI.Shared;

namespace SnakeAI.WinForms
{
	public class Snake : SnakeAI.Shared.Snake, ISnake, IMovable
	{
		/*public override int score = 1;
		/// <summary>
		/// amount of moves the snake can make before it dies
		/// </summary>
		public override int lifeLeft = 200;
		/// <summary>
		/// amount of time the snake has been alive
		/// </summary>
		public override int lifetime = 0;
		/// <summary>
		/// itterator to run through the foodlist (used for replay)
		/// </summary>
		public override int foodItterate = 0;

		public override float fitness = 0;

		public override bool dead = false;
		/// <summary>
		/// if this snake is a replay of best snake
		/// </summary>
		public override bool replay = false;

		/// <summary>
		/// snakes vision
		/// </summary>
		public override float[] vision { get; set; }
		/// <summary>
		/// snakes decision
		/// </summary>
		public override float[] decision { get; set; }

		public override Vector head { get; set; }
		/// <summary>
		/// snakes body
		/// </summary>
		public override List<Vector> body { get; set; }
		/// <summary>
		/// list of food positions (used to replay the best snake)
		/// </summary>
		public override List<Food> foodList { get; set; }
		public override Food food { get; set; }
		public override NeuralNet brain { get; set; }*/
		public eDirection direction { get; set; }

		public Snake() : this(Core.hidden_layers) { direction = eDirection.Down; }

		public Snake(int layers): base ()
		{
			head = new Vector(800, Core.height / 2);
			food = new Food();
			body = new List<Vector>();
			if (!Core.humanPlaying)
			{
				vision = new float[24];
				decision = new float[4];
				foodList = new List<Food>();
				foodList.Add(food.Clone());
				brain = new NeuralNet(24, Core.hidden_nodes, 4, layers);
				body.Add(new Vector(800, (Core.height / 2) + Core.SIZE));
				body.Add(new Vector(800, (Core.height / 2) + (2 * Core.SIZE)));
				score += 2;
			}
		}

		/// <summary>
		/// this constructor passes in a list of food positions so that a replay can replay the best snake
		/// </summary>
		/// <param name="foods"></param>
		public Snake(List<Food> foods)
		{
			replay = true;
			vision = new float[24];
			decision = new float[4];
			body = new List<Vector>();
			foodList = new List<Food>(foods.Count);
			//clone all the food positions in the foodlist
			foreach (Food f in foods)
			{
				foodList.Add(f.Clone());
			}
			food = foodList[foodItterate];
			foodItterate++;
			head = new Vector(800, Core.height / 2);
			body.Add(new Vector(800, (Core.height / 2) + Core.SIZE));
			body.Add(new Vector(800, (Core.height / 2) + (2 * Core.SIZE)));
			score += 2;
		}

		/*// <summary>
		/// check if a position collides with the snakes body
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool bodyCollide(float x, float y)
		{
			for (int i = 0; i < body.Count; i++)
			{
				if (x == body[i].x && y == body[i].y)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// check if a position collides with the food
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool foodCollide(float x, float y)
		{
			if (x == food.pos.x && y == food.pos.y)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// check if a position collides with the wall
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool wallCollide(float x, float y)
		{
			if (x >= Core.width - (Core.SIZE) || x < 400 + Core.SIZE || y >= Core.height - (Core.SIZE) || y < Core.SIZE)
			{
				return true;
			}
			return false;
		}*/

		/// <summary>
		/// move the snake
		/// </summary>
		public override void move()
		{
			if (!dead)
			{
				if (!Core.humanPlaying && !Core.modelLoaded)
				{
					lifetime++;
					lifeLeft--;
				}
				if (foodCollide(head.x, head.y))
				{
					eat();
				}
				//shiftBody();
				if (wallCollide(head.x, head.y))
				{
					dead = true;
				}
				else if (bodyCollide(head.x, head.y))
				{
					dead = true;
				}
				else if (lifeLeft <= 0 && !Core.humanPlaying)
				{
					dead = true;
				}
			}
		}

		/*// <summary>
		/// eat food
		/// </summary>
		public void eat()
		{
			int len = body.Count - 1;
			score++;
			if (!Core.humanPlaying && !Core.modelLoaded)
			{
				if (lifeLeft < 500)
				{
					if (lifeLeft > 400)
					{
						lifeLeft = 500;
					}
					else
					{
						lifeLeft += 100;
					}
				}
			}
			//ToDo: Move to Game Manager/Client
			if (len >= 0)
			{
				body.Add(new Vector(body[len].x, body[len].y));
			}
			else
			{
				body.Add(new Vector(head.x, head.y));
			}
			if (!replay)
			{
				food = new Food();
				while (bodyCollide(food.pos.x, food.pos.y))
				{
					food = new Food();
				}
				if (!Core.humanPlaying)
				{
					foodList.Add(food);
				}
			}
			//if the snake is a replay, then we dont want to create new random foods, 
			//want to see the positions the best snake had to collect
			else
			{
				food = foodList[foodItterate];
				foodItterate++;
			}
		}*/

		/// <summary>
		/// clone a version of the snake that will be used for a replay
		/// </summary>
		/// <returns></returns>
		public override ISnake cloneForReplay()
		{
			Snake clone = new Snake(foodList);
			clone.brain = brain.Clone();
			return clone;
		}

		/// <summary>
		/// clone the snake
		/// </summary>
		/// <returns></returns>
		public override ISnake Clone()
		{
			Snake clone = new Snake(Core.hidden_layers);
			clone.brain = brain.Clone();
			return clone;
		}

		/// <summary>
		/// crossover the snake with another snake
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public override ISnake crossover(ISnake parent)
		{
			Snake child = new Snake(Core.hidden_layers);
			child.brain = brain.crossover(parent.brain);
			return child;
		}

		/*// <summary>
		/// mutate the snakes brain
		/// </summary>
		public void mutate()
		{
			brain.mutate(Core.mutationRate);
		}

		/// <summary>
		/// calculate the fitness of the snake
		/// </summary>
		public void calculateFitness()
		{
			if (score < 10)
			{
				fitness = float.Parse((Math.Floor((double)lifetime * lifetime) * Math.Pow(2, score)).ToString());
			}
			else
			{
				fitness = float.Parse(Math.Floor((double)lifetime * lifetime).ToString());
				fitness *= float.Parse(Math.Pow(2, 10).ToString());
				fitness *= (score - 9);
			}
		}

		/// <summary>
		/// look in all 8 directions and check for food, body and wall
		/// </summary>
		public void look()
		{
			vision = new float[24];
			float[] temp = lookInDirection(new Vector(-Core.SIZE, 0));
			vision[0] = temp[0];
			vision[1] = temp[1];
			vision[2] = temp[2];
			temp = lookInDirection(new Vector(-Core.SIZE, -Core.SIZE));
			vision[3] = temp[0];
			vision[4] = temp[1];
			vision[5] = temp[2];
			temp = lookInDirection(new Vector(0, -Core.SIZE));
			vision[6] = temp[0];
			vision[7] = temp[1];
			vision[8] = temp[2];
			temp = lookInDirection(new Vector(Core.SIZE, -Core.SIZE));
			vision[9] = temp[0];
			vision[10] = temp[1];
			vision[11] = temp[2];
			temp = lookInDirection(new Vector(Core.SIZE, 0));
			vision[12] = temp[0];
			vision[13] = temp[1];
			vision[14] = temp[2];
			temp = lookInDirection(new Vector(Core.SIZE, Core.SIZE));
			vision[15] = temp[0];
			vision[16] = temp[1];
			vision[17] = temp[2];
			temp = lookInDirection(new Vector(0, Core.SIZE));
			vision[18] = temp[0];
			vision[19] = temp[1];
			vision[20] = temp[2];
			temp = lookInDirection(new Vector(-Core.SIZE, Core.SIZE));
			vision[21] = temp[0];
			vision[22] = temp[1];
			vision[23] = temp[2];
		}

		/// <summary>
		/// look in a direction and check for food, body and wall
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public virtual float[] lookInDirection(Vector direction)
		{
			float[] look = new float[3];
			Vector pos = new Vector(head.x, head.y);
			float distance = 0;
			bool foodFound = false;
			bool bodyFound = false;
			pos.Add(direction);
			distance += 1;
			while (!wallCollide(pos.x, pos.y))
			{
				if (!foodFound && foodCollide(pos.x, pos.y))
				{
					foodFound = true;
					look[0] = 1;
				}
				if (!bodyFound && bodyCollide(pos.x, pos.y))
				{
					bodyFound = true;
					look[1] = 1;
				}
				//if (replay && Core.seeVision) {
				//	stroke(0, 255, 0);
				//	point(pos.x, pos.y);
				//	if (foodFound) {
				//		noStroke();
				//		fill(255, 255, 51);
				//		ellipseMode(CENTER);
				//		ellipse(pos.x, pos.y, 5, 5);
				//	}
				//	if (bodyFound) {
				//		noStroke();
				//		fill(102, 0, 102);
				//		ellipseMode(CENTER);
				//		ellipse(pos.x, pos.y, 5, 5);
				//	}
				//}
				pos.Add(direction);
				distance += 1;
			}
			//if (replay && Core.seeVision) {
			//	noStroke();
			//	fill(0, 255, 0);
			//	ellipseMode(CENTER);
			//	ellipse(pos.x, pos.y, 5, 5);
			//}
			look[2] = 1 / distance;
			return look;
		}

		/// <summary>
		/// think about what direction to move
		/// </summary>
		public void think()//eDirection d = eDirection.Down
		{
			decision = brain.output(vision);
			int maxIndex = 0;
			float max = 0;
			for (int i = 0; i < decision.Length; i++)
			{
				if (decision[i] > max)
				{
					max = decision[i];
					maxIndex = i;
				}
			}

			switch (maxIndex)
			{
				case 0:
					//return eDirection.Up;
					moveUp();
					break;
				case 1:
					//return eDirection.Down;
					moveDown();
					break;
				case 2:
					//return eDirection.Left;
					moveLeft();
					break;
				case 3:
					//return eDirection.Right;
					moveRight();
					break;
				//default:
				//	return direction;
			}
		}*/

		public void MoveLeft(int i_Index)
		{
			//Body[i_Index].x--;
			//Body[i_Index].x-=1;
			//Body[i_Index] = new Vector(Body[i_Index].x-1, Body[i_Index].y);
			body[i_Index] = new Vector() { x = body[i_Index].x - 1, y = body[i_Index].y};
		}
		public void MoveRight(int i_Index)
		{
			//Body[i_Index].x++;
			body[i_Index] = new Vector(body[i_Index].x+1, body[i_Index].y);
		}
		public void MoveUp(int i_Index)
		{
			//Body[i_Index].y--;
			body[i_Index] = new Vector(body[i_Index].x, body[i_Index].y-1);
		}
		public void MoveDown(int i_Index)
		{
			//Body[i_Index].y++;
			body[i_Index] = new Vector(body[i_Index].x, body[i_Index].y+1);
		}

		public override void moveUp()
		{
			//if (yVel != Core.SIZE)
			//{
			//	xVel = 0; yVel = -Core.SIZE;
			//}
			if(direction != eDirection.Up && direction != eDirection.Down)
				direction = eDirection.Up;
		}
		public override void moveDown()
		{
			//if (yVel != -Core.SIZE)
			//{
			//	xVel = 0; yVel = Core.SIZE;
			//}
			if(direction != eDirection.Down && direction != eDirection.Up)
				direction = eDirection.Down;
		}
		public override void moveLeft()
		{
			//if (xVel != Core.SIZE)
			//{
			//	xVel = -Core.SIZE; yVel = 0;
			//}
			if(direction != eDirection.Left && direction != eDirection.Right)
				direction = eDirection.Left;
		}
		public override void moveRight()
		{
			//if (xVel != -Core.SIZE)
			//{
			//	xVel = Core.SIZE; yVel = 0;
			//}
			if(direction != eDirection.Right && direction != eDirection.Left)
				direction = eDirection.Right;
		}

		internal void Add(Vector head)
		{
			body.Add(head);
		}
	}
}
