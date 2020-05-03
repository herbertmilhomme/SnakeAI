using System.Collections.Generic;

namespace SnakeAI.Shared
{
	public interface ISnake
	{
		bool bodyCollide(float x, float y);
		void calculateFitness();
		ISnake Clone();
		ISnake cloneForReplay();
		ISnake crossover(ISnake parent);
		void eat();
		bool foodCollide(float x, float y);
		void look();
		float[] lookInDirection(Vector direction);
		void move();
		void moveDown();
		void moveLeft();
		void moveRight();
		void moveUp();
		void mutate();
		//void shiftBody();
		//void show();
		void think();
		bool wallCollide(float x, float y);


		/// <summary>
		/// snakes vision
		/// </summary>
		float[] vision { get; set; }
		/// <summary>
		/// snakes decision
		/// </summary>
		float[] decision { get; set; }

		Vector head { get; set; }
		/// <summary>
		/// snakes body
		/// </summary>
		List<Vector> body { get; set; }
		/// <summary>
		/// list of food positions (used to replay the best snake)
		/// </summary>
		List<Food> foodList { get; set; }
		Food food { get; set; }
		NeuralNet brain { get; set; }
		int Length { get; }
	}
}