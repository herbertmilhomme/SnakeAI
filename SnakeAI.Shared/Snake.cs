using System;
using System.Collections.Generic;

namespace SnakeAI.Shared
{
	public abstract class Snake : ISnake
	{
		public virtual int score { get { return Length - 1; } }
		/// <summary>
		/// amount of moves the snake can make before it dies
		/// </summary>
		public virtual int lifeLeft { get; set; }
		/// <summary>
		/// amount of time the snake has been alive
		/// </summary>
		public virtual int lifetime { get; set; }
		//private int xVel, yVel;
		/// <summary>
		/// itterator to run through the foodlist (used for replay)
		/// </summary>
		public virtual int foodItterate { get; set; }

		public virtual float fitness { get; set; }

		public virtual bool dead { get; set; }
		/// <summary>
		/// if this snake is a replay of best snake
		/// </summary>
		public virtual bool replay { get; set; }

		/// <summary>
		/// snakes vision
		/// </summary>
		public virtual float[] vision { get; set; }
		/// <summary>
		/// snakes decision
		/// </summary>
		public virtual float[] decision { get; set; }

		public virtual Vector head { get { return body[0]; } } //; set; }

		/// <summary>
		/// snakes body
		/// </summary>
		public virtual List<Vector> body { get; set; }
		/// <summary>
		/// list of food positions (used to replay the best snake)
		/// </summary>
		public virtual List<Vector> foodList { get; set; }
		public virtual Vector food { get; private set; }
		public virtual NeuralNet brain { get; set; }
		public int Length
		{
			get { return body.Count; }
		}

		public Snake() //: this(Core.hidden_layers)
		{
			this.lifeLeft = 200;
			this.lifetime = 0;
			//this.xVel = xVel;
			//this.yVel = yVel;
			this.foodItterate = 0;
			this.fitness = 0;
			this.dead = false;
			this.replay = false;
			//this.vision = vision;
			//this.decision = decision;
			//this.head = head;
			//this.body = body;
			foodList = new List<Vector>();
			//this.food = food;
			//this.brain = brain;
			body = new List<Vector>();
			//head = new Vector(800, Core.height / 2);
			Vector head = new Shared.Vector { x = (int)((Core.width / Core.SIZE) * .5), y = 5 };
			body.Add(head);
			GenerateFruit(); //food = new Food();
		}

		public Snake(int layers) : this()
		{
			if (!Core.humanPlaying)
			{
				vision = new float[24];
				decision = new float[4];
				foodList = new List<Vector>();
				foodList.Add(food);//.Clone()
				brain = new NeuralNet(24, Core.hidden_nodes, 4, layers);
				body.Add(new Vector((int)((Core.width / Core.SIZE) * .5), (int)(Core.height * .3) + Core.SIZE));
				body.Add(new Vector((int)((Core.width / Core.SIZE) * .5), (int)(Core.height * .3) + (2 * Core.SIZE)));
				//score += 2;
			}
		}

		/// <summary>
		/// this constructor passes in a list of food positions so that a replay can replay the best snake
		/// </summary>
		/// <param name="foods"></param>
		public Snake(List<Vector> foods)
		{
			replay = true;
			vision = new float[24];
			decision = new float[4];
			body = new List<Vector>();
			foodList = foods; //new List<Vector>(foods.Count);
			//clone all the food positions in the foodlist
			//foreach (Vector f in foods)
			//{
			//	foodList.Add(f);//.Clone()
			//}
			food = foodList[foodItterate];
			foodItterate++;
			Vector head = new Vector((int)((Core.width / Core.SIZE) * .5), (int)(Core.height * .3));
			body.Add(new Vector((int)((Core.width / Core.SIZE) * .5), (int)(Core.height * .3) + Core.SIZE));
			body.Add(new Vector((int)((Core.width / Core.SIZE) * .5), (int)(Core.height * .3) + (2 * Core.SIZE)));
			//score += 2;
		}

		/// <summary>
		/// check if a position collides with the snakes body
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool bodyCollide(float x, float y)
		{
			for (int i = 1; i < body.Count; i++)
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
			if (x == food.x && y == food.y)
				return true;
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
			int maxXPos = Core.width / Core.SIZE;
			int maxYPos = Core.height / Core.SIZE;

			//if (x >= Core.width - (Core.SIZE) || x < 400 + Core.SIZE || y >= Core.height - (Core.SIZE) || y < Core.SIZE)
			if (x < 0 || y < 0 || x >= maxXPos || y >= maxYPos)
				return true;
			return false;
		}

		/*// <summary>
		/// show the snake
		/// </summary>
		public abstract void show();*/
		//{
		//	//food.show();
		//	//fill(255);
		//	//stroke(0);
		//	//for (int i = 0; i < body.Count; i++) {
		//	//	rect(body[i].x, body[i].y, Core.SIZE, Core.SIZE);
		//	//}
		//	//if (dead) {
		//	//	fill(150);
		//	//} else {
		//	//	fill(255);
		//	//}
		//	//rect(head.x, head.y, Core.SIZE, Core.SIZE);
		//}

		/// <summary>
		/// move the snake
		/// </summary>
		public virtual void move()
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
				shiftBody();
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

		/// <summary>
		/// eat food
		/// </summary>
		public void eat()
		{
			//int len = Length - 1;
			//score++;
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
			//if (len >= 0)
			//	body.Add(new Vector(body[len].x, body[len].y));
			//else
			//	body.Add(new Vector(head.x, head.y));
			Shared.Vector piece = new Shared.Vector
			{
				x = body[score].x,
				y = body[score].y
			};
			body.Add(piece);
			//if (!replay)
			//{
			//	food = new Vector();
			//	while (bodyCollide(food.x, food.y))
			//	{
			//		food = new Vector();
			//	}
			//	if (!Core.humanPlaying)
			//	{
			//		foodList.Add(food);
			//	}
			//}
			////if the snake is a replay, then we dont want to create new random foods, 
			////want to see the positions the best snake had to collect
			//else
			//{
			//	food = foodList[foodItterate];
			//	foodItterate++;
			//}
			GenerateFruit();
		}

		/// <summary>
		/// shift the body to follow the head
		/// </summary>
		public abstract void shiftBody();
		//{
		//	//float tempx = head.x;
		//	//float tempy = head.y;
		//	Vector tempv = new Vector(head.x, head.y);
		//	//head.x += xVel;
		//	//head.y += yVel;
		//	head = new Vector(head.x + xVel, head.y + yVel);
		//	//float temp2x;
		//	//float temp2y;
		//	Vector temp2v;
		//	for (int i = 0; i < body.Count; i++)
		//	{
		//		//temp2x = body[i].x;
		//		//temp2y = body[i].y;
		//		temp2v = new Vector(body[i].x, body[i].y);
		//		//body[i].x = tempx;
		//		//body[i].y = tempy;
		//		body[i] = temp2v;
		//		//tempx = temp2x;
		//		//tempy = temp2y;
		//		tempv = temp2v;
		//	}
		//}

		/// <summary>
		/// clone a version of the snake that will be used for a replay
		/// </summary>
		/// <returns></returns>
		public abstract ISnake cloneForReplay();
		//{
		//	Snake clone = new Snake(foodList);
		//	clone.brain = brain.Clone();
		//	return clone;
		//}

		/// <summary>
		/// clone the snake
		/// </summary>
		/// <returns></returns>
		public abstract ISnake Clone();
		//{
		//	Snake clone = new Snake(Core.hidden_layers);
		//	clone.brain = brain.Clone();
		//	return clone;
		//}

		/// <summary>
		/// crossover the snake with another snake
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public abstract ISnake crossover(ISnake parent);
		//{
		//	Snake child = new Snake(Core.hidden_layers);
		//	child.brain = brain.crossover(parent.brain);
		//	return child;
		//}

		/// <summary>
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
			//Convert from Absolute to Relative Units
			//float[] temp = lookInDirection(new Vector(-Core.SIZE, 0));
			float[] temp = lookInDirection(new Vector(-1, 0));
			vision[0] = temp[0];
			vision[1] = temp[1];
			vision[2] = temp[2];
			//temp = lookInDirection(new Vector(-Core.SIZE, -Core.SIZE));
			temp = lookInDirection(new Vector(-1, -1));
			vision[3] = temp[0];
			vision[4] = temp[1];
			vision[5] = temp[2];
			//temp = lookInDirection(new Vector(0, -Core.SIZE));
			temp = lookInDirection(new Vector(0, -1));
			vision[6] = temp[0];
			vision[7] = temp[1];
			vision[8] = temp[2];
			//temp = lookInDirection(new Vector(Core.SIZE, -Core.SIZE));
			temp = lookInDirection(new Vector(1, -1));
			vision[9] = temp[0];
			vision[10] = temp[1];
			vision[11] = temp[2];
			//temp = lookInDirection(new Vector(Core.SIZE, 0));
			temp = lookInDirection(new Vector(1, 0));
			vision[12] = temp[0];
			vision[13] = temp[1];
			vision[14] = temp[2];
			//temp = lookInDirection(new Vector(Core.SIZE, Core.SIZE));
			temp = lookInDirection(new Vector(1, 1));
			vision[15] = temp[0];
			vision[16] = temp[1];
			vision[17] = temp[2];
			//temp = lookInDirection(new Vector(0, Core.SIZE));
			temp = lookInDirection(new Vector(0, 1));
			vision[18] = temp[0];
			vision[19] = temp[1];
			vision[20] = temp[2];
			//temp = lookInDirection(new Vector(-Core.SIZE, Core.SIZE));
			temp = lookInDirection(new Vector(-1, 1));
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
		public void think()
		{
			decision = brain.output(vision);
			int maxIndex = 0;
			float max = 0;
			for (int i = 0; i < decision.Length; i++)
				if (decision[i] > max)
				{
					max = decision[i];
					maxIndex = i;
				}

			switch (maxIndex)
			{
				case 0:
					moveUp();
					break;
				case 1:
					moveDown();
					break;
				case 2:
					moveLeft();
					break;
				case 3:
					moveRight();
					break;
			}
		}

		private void GenerateFruit()
		{
			int maxXPosition = Core.width / Core.SIZE; //38 tiles?
			int maxYPosition = Core.height / Core.SIZE; //38 tiles?

			if (!replay)
			{
				do
					food = new Shared.Vector { x = Core.Rand.Next(maxXPosition), y = Core.Rand.Next(maxYPosition) };
					//food = new Shared.Vector() { 
					//	x = Core.SIZE * Core.Rand.Next(maxXPosition + 1) + 440, //Canvas Left
					//	y = Core.SIZE * Core.Rand.Next(maxYPosition + 1) + 40 };
				while (bodyCollide(food.x, food.y));

				if (!Core.humanPlaying)
					foodList.Add(food);
			}
			//if the snake is a replay, then we dont want to create new random foods, 
			//want to see the positions the best snake had to collect
			else
			{
				food = foodList[foodItterate];
				foodItterate++;
			}
		}

		public abstract void moveUp();
		//{
		//	if (yVel != Core.SIZE)
		//	{
		//		xVel = 0; yVel = -Core.SIZE;
		//	}
		//}
		public abstract void moveDown();
		//{
		//	if (yVel != -Core.SIZE)
		//	{
		//		xVel = 0; yVel = Core.SIZE;
		//	}
		//}
		public abstract void moveLeft();
		//{
		//	if (xVel != Core.SIZE)
		//	{
		//		xVel = -Core.SIZE; yVel = 0;
		//	}
		//}
		public abstract void moveRight();
		//{
		//	if (xVel != -Core.SIZE)
		//	{
		//		xVel = Core.SIZE; yVel = 0;
		//	}
		//}
	}
}