using System;
using System.Collections.Generic;

namespace SnakeAI.Shared
{
	public class Snake {

		public int score = 1;
		/// <summary>
		/// amount of moves the snake can make before it dies
		/// </summary>
		public int lifeLeft = 200;  
		/// <summary>
		/// amount of time the snake has been alive
		/// </summary>
		public int lifetime = 0;  
		public int xVel, yVel;
		/// <summary>
		/// itterator to run through the foodlist (used for replay)
		/// </summary>
		public int foodItterate = 0;  

		public float fitness = 0;

		public bool dead = false;
		/// <summary>
		/// if this snake is a replay of best snake
		/// </summary>
		public bool replay = false;  

		/// <summary>
		/// snakes vision
		/// </summary>
		public float[] vision;  
		/// <summary>
		/// snakes decision
		/// </summary>
		public float[] decision;  

		public Vector head;

		/// <summary>
		/// snakes body
		/// </summary>
		public List<Vector> body;  
		/// <summary>
		/// list of food positions (used to replay the best snake)
		/// </summary>
		public List<Food> foodList;  

		public Food food;
		public NeuralNet brain;

		public Snake() : this(Core.hidden_layers) { }

		public Snake(int layers) {
			head = new Vector(800, Core.height / 2);
			food = new Food();
			body = new List<Vector>();
			if (!Core.humanPlaying) {
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
		public Snake(List<Food> foods) {  
			replay = true;
			vision = new float[24];
			decision = new float[4];
			body = new List<Vector>();
			foodList = new List<Food>(foods.Count);
			//clone all the food positions in the foodlist
			foreach (Food f in foods) {  
				foodList.Add(f.Clone());
			}
			food = foodList[foodItterate];
			foodItterate++;
			head = new Vector(800, Core.height / 2);
			body.Add(new Vector(800, (Core.height / 2) + Core.SIZE));
			body.Add(new Vector(800, (Core.height / 2) + (2 * Core.SIZE)));
			score += 2;
		}

		/// <summary>
		/// check if a position collides with the snakes body
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool bodyCollide(float x, float y) {  
			for (int i = 0; i < body.Count; i++) {
				if (x == body[i].x && y == body[i].y) {
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
		public bool foodCollide(float x, float y) {  
			if (x == food.pos.x && y == food.pos.y) {
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
		public bool wallCollide(float x, float y) {  
			if (x >= Core.width - (Core.SIZE) || x < 400 + Core.SIZE || y >= Core.height - (Core.SIZE) || y < Core.SIZE) {
				return true;
			}
			return false;
		}

		/// <summary>
		/// show the snake
		/// </summary>
		public void show() {  
			food.show();
			fill(255);
			stroke(0);
			for (int i = 0; i < body.Count; i++) {
				rect(body[i].x, body[i].y, Core.SIZE, Core.SIZE);
			}
			if (dead) {
				fill(150);
			} else {
				fill(255);
			}
			rect(head.x, head.y, Core.SIZE, Core.SIZE);
		}

		/// <summary>
		/// move the snake
		/// </summary>
		public void move() {  
			if (!dead) {
				if (!Core.humanPlaying && !Core.modelLoaded) {
					lifetime++;
					lifeLeft--;
				}
				if (foodCollide(head.x, head.y)) {
					eat();
				}
				shiftBody();
				if (wallCollide(head.x, head.y)) {
					dead = true;
				} else if (bodyCollide(head.x, head.y)) {
					dead = true;
				} else if (lifeLeft <= 0 && !Core.humanPlaying) {
					dead = true;
				}
			}
		}

		/// <summary>
		/// eat food
		/// </summary>
		public void eat() {  
			int len = body.Count - 1;
			score++;
			if (!Core.humanPlaying && !Core.modelLoaded) {
				if (lifeLeft < 500) {
					if (lifeLeft > 400) {
						lifeLeft = 500;
					} else {
						lifeLeft += 100;
					}
				}
			}
			if (len >= 0) {
				body.Add(new Vector(body[len].x, body[len].y));
			} else {
				body.Add(new Vector(head.x, head.y));
			}
			if (!replay) {
				food = new Food();
				while (bodyCollide(food.pos.x, food.pos.y)) {
					food = new Food();
				}
				if (!Core.humanPlaying) {
					foodList.Add(food);
				}
			}
			//if the snake is a replay, then we dont want to create new random foods, 
			//want to see the positions the best snake had to collect
			else {  
				food = foodList[foodItterate];
				foodItterate++;
			}
		}

		/// <summary>
		/// shift the body to follow the head
		/// </summary>
		public void shiftBody() {  
			float tempx = head.x;
			float tempy = head.y;
			head.x += xVel;
			head.y += yVel;
			float temp2x;
			float temp2y;
			for (int i = 0; i < body.Count; i++) {
				temp2x = body[i].x;
				temp2y = body[i].y;
				body[i].x = tempx;
				body[i].y = tempy;
				tempx = temp2x;
				tempy = temp2y;
			}
		}

		/// <summary>
		/// clone a version of the snake that will be used for a replay
		/// </summary>
		/// <returns></returns>
		public Snake cloneForReplay() {  
			Snake clone = new Snake(foodList);
			clone.brain = brain.Clone();
			return clone;
		}

		/// <summary>
		/// clone the snake
		/// </summary>
		/// <returns></returns>
		public Snake Clone() {  
			Snake clone = new Snake(Core.hidden_layers);
			clone.brain = brain.Clone();
			return clone;
		}

		/// <summary>
		/// crossover the snake with another snake
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public Snake crossover(Snake parent) {  
			Snake child = new Snake(Core.hidden_layers);
			child.brain = brain.crossover(parent.brain);
			return child;
		}

		/// <summary>
		/// mutate the snakes brain
		/// </summary>
		public void mutate() {  
			brain.mutate(Core.mutationRate);
		}

		/// <summary>
		/// calculate the fitness of the snake
		/// </summary>
		public void calculateFitness() {  
			if (score < 10) {
				fitness = float.Parse((Math.Floor((double)lifetime * lifetime) * Math.Pow(2, score)).ToString());
			} else {
				fitness = float.Parse(Math.Floor((double)lifetime * lifetime).ToString());
				fitness *= float.Parse(Math.Pow(2, 10).ToString());
				fitness *= (score - 9);
			}
		}

		/// <summary>
		/// look in all 8 directions and check for food, body and wall
		/// </summary>
		public void look() {  
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
		public float[] lookInDirection(Vector direction) {  
			float[] look = new float[3];
			Vector pos = new Vector(head.x, head.y);
			float distance = 0;
			bool foodFound = false;
			bool bodyFound = false;
			pos.Add(direction);
			distance += 1;
			while (!wallCollide(pos.x, pos.y)) {
				if (!foodFound && foodCollide(pos.x, pos.y)) {
					foodFound = true;
					look[0] = 1;
				}
				if (!bodyFound && bodyCollide(pos.x, pos.y)) {
					bodyFound = true;
					look[1] = 1;
				}
				if (replay && Core.seeVision) {
					stroke(0, 255, 0);
					point(pos.x, pos.y);
					if (foodFound) {
						noStroke();
						fill(255, 255, 51);
						ellipseMode(CENTER);
						ellipse(pos.x, pos.y, 5, 5);
					}
					if (bodyFound) {
						noStroke();
						fill(102, 0, 102);
						ellipseMode(CENTER);
						ellipse(pos.x, pos.y, 5, 5);
					}
				}
				pos.Add(direction);
				distance += 1;
			}
			if (replay && Core.seeVision) {
				noStroke();
				fill(0, 255, 0);
				ellipseMode(CENTER);
				ellipse(pos.x, pos.y, 5, 5);
			}
			look[2] = 1 / distance;
			return look;
		}

		/// <summary>
		/// think about what direction to move
		/// </summary>
		public void think() {  
			decision = brain.output(vision);
			int maxIndex = 0;
			float max = 0;
			for (int i = 0; i < decision.Length; i++) {
				if (decision[i] > max) {
					max = decision[i];
					maxIndex = i;
				}
			}

			switch (maxIndex) {
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

		public void moveUp() {
			if (yVel != Core.SIZE) {
				xVel = 0; yVel = -Core.SIZE;
			}
		}
		public void moveDown() {
			if (yVel != -Core.SIZE) {
				xVel = 0; yVel = Core.SIZE;
			}
		}
		public void moveLeft() {
			if (xVel != Core.SIZE) {
				xVel = -Core.SIZE; yVel = 0;
			}
		}
		public void moveRight() {
			if (xVel != -Core.SIZE) {
				xVel = Core.SIZE; yVel = 0;
			}
		}
	}
}