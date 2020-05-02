namespace SnakeAI.Shared
{
	public class Population
	{

		Snake[] snakes;
		public Snake bestSnake;

		public int bestSnakeScore = 0;
		public int gen = 0;
		int samebest = 0;

		public float bestFitness = 0;
		float fitnessSum = 0;

		public Population(int size)
		{
			snakes = new Snake[size];
			for (int i = 0; i < snakes.Length; i++)
			{
				snakes[i] = new Snake();
			}
			bestSnake = (Snake)snakes[0].Clone();
			bestSnake.replay = true;
		}

		/// <summary>
		/// check if all the snakes in the population are dead
		/// </summary>
		/// <returns></returns>
		public bool done()
		{  
			for (int i = 0; i < snakes.Length; i++)
			{
				if (!snakes[i].dead)
					return false;
			}
			if (!bestSnake.dead)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// update all the snakes in the generation
		/// </summary>
		public void update()
		{  
			if (!bestSnake.dead)
			{  
				//if the best snake is not dead update it, this snake is a replay of the best from the past generation
				bestSnake.look();
				bestSnake.think();
				bestSnake.move();
			}
			for (int i = 0; i < snakes.Length; i++)
			{
				if (!snakes[i].dead)
				{
					snakes[i].look();
					snakes[i].think();
					snakes[i].move();
				}
			}
		}

		/// <summary>
		/// show either the best snake or all the snakes
		/// </summary>
		public void show()
		{  
			if (Core.replayBest)
			{
				bestSnake.show();
				//show the brain of the best snake
				bestSnake.brain.show(0, 0, 360, 790, bestSnake.vision, bestSnake.decision);  
			}
			else
			{
				for (int i = 0; i < snakes.Length; i++)
				{
					snakes[i].show();
				}
			}
		}

		/// <summary>
		/// set the best snake of the generation
		/// </summary>
		public void setBestSnake()
		{  
			float max = 0;
			int maxIndex = 0;
			for (int i = 0; i < snakes.Length; i++)
			{
				if (snakes[i].fitness > max)
				{
					max = snakes[i].fitness;
					maxIndex = i;
				}
			}
			if (max > bestFitness)
			{
				bestFitness = max;
				bestSnake = (Snake)snakes[maxIndex].cloneForReplay();
				bestSnakeScore = snakes[maxIndex].score;
				//samebest = 0;
				//mutationRate = defaultMutation;
			}
			else
			{
				bestSnake = (Snake)bestSnake.cloneForReplay();
				/*samebest++;
				//if the best snake has remained the same for more than 3 generations, raise the mutation rate
				if(samebest > 2) {  
				   mutationRate *= 2;
				   samebest = 0;
				}*/
			}
		}

		/// <summary>
		/// selects a random number in range of the fitnesssum and if a snake falls in that range then select it
		/// </summary>
		/// <returns></returns>
		public Snake selectParent()
		{  
			//float rand = Core.Rand.Next(fitnessSum);
			float rand = (float)(Core.Rand.NextDouble() * fitnessSum);
			float summation = 0;
			for (int i = 0; i < snakes.Length; i++)
			{
				summation += snakes[i].fitness;
				if (summation > rand)
				{
					return snakes[i];
				}
			}
			return snakes[0];
		}

		public void naturalSelection()
		{
			Snake[] newSnakes = new Snake[snakes.Length];

			setBestSnake();
			calculateFitnessSum();

			//add the best snake of the prior generation into the new generation
			newSnakes[0] = (Snake)bestSnake.Clone();  
			for (int i = 1; i < snakes.Length; i++)
			{
				Snake child = (Snake)selectParent().crossover(selectParent());
				child.mutate();
				newSnakes[i] = child;
			}
			//snakes = newSnakes.Clone();
			snakes = new System.Collections.Generic.List<Snake>(newSnakes).ToArray();
			Core.evolution.Add(bestSnakeScore);
			gen += 1;
		}

		public void mutate()
		{
			//start from 1 as to not override the best snake placed in index 0
			for (int i = 1; i < snakes.Length; i++)
			{  
				snakes[i].mutate();
			}
		}

		/// <summary>
		/// calculate the fitnesses for each snake
		/// </summary>
		public void calculateFitness()
		{  
			for (int i = 0; i < snakes.Length; i++)
			{
				snakes[i].calculateFitness();
			}
		}

		/// <summary>
		/// calculate the sum of all the snakes fitnesses
		/// </summary>
		public void calculateFitnessSum()
		{  
			fitnessSum = 0;
			for (int i = 0; i < snakes.Length; i++)
			{
				fitnessSum += snakes[i].fitness;
			}
		}
	}
}