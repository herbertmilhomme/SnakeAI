using System;

namespace SnakeAI.Shared
{
	public class Matrix
	{

		public int rows, cols;
		public float[][] matrix;

		public Matrix(int r, int c)
		{
			rows = r;
			cols = c;
			matrix = new float[rows][];
			for (int i = 0; i < rows; i++)
			{
				matrix[i] = new float[cols];
				//for (int j = 0; j < cols; j++)
				//{
				//	matrix[i][j] = matrix[i][j];
				//}
			}
		}

		public Matrix(float[][] m)
		{
			matrix = m;
			rows = matrix.Length;
			cols = matrix[0].Length;
		}

		public void output()
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					//print(matrix[i][j] + " ");
					Console.Write(matrix[i][j] + " ");
				}
				//println();
				Console.WriteLine();
			}
			//println();
			Console.WriteLine();
		}

		public Matrix dot(Matrix n)
		{
			Matrix result = new Matrix(rows, n.cols);

			if (cols == n.rows)
			{
				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < n.cols; j++)
					{
						float sum = 0;
						for (int k = 0; k < cols; k++)
						{
							sum += matrix[i][k] * n.matrix[k][j];
						}
						result.matrix[i][j] = sum;
					}
				}
			}
			return result;
		}

		public void randomize()
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					matrix[i][j] = new System.Random().Next(-1, 1);
				}
			}
		}

		public Matrix singleColumnMatrixFromArray(float[] arr)
		{
			Matrix n = new Matrix(arr.Length, 1);
			for (int i = 0; i < arr.Length; i++)
			{
				n.matrix[i][0] = arr[i];
			}
			return n;
		}

		public float[] ToArray()
		{
			float[] arr = new float[rows * cols];
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					arr[j + i * cols] = matrix[i][j];
				}
			}
			return arr;
		}

		public Matrix addBias()
		{
			Matrix n = new Matrix(rows + 1, 1);
			for (int i = 0; i < rows; i++)
			{
				n.matrix[i][0] = matrix[i][0];
			}
			n.matrix[rows][0] = 1;
			return n;
		}

		public Matrix activate()
		{
			Matrix n = new Matrix(rows, cols);
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					n.matrix[i][j] = relu(matrix[i][j]);
				}
			}
			return n;
		}

		public float relu(float x)
		{
			return Math.Max(0, x);
		}

		public void mutate(float mutationRate)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					float rand = new System.Random().Next(1);
					if (rand < mutationRate)
					{
						matrix[i][j] += randomGaussian() / 5;

						if (matrix[i][j] > 1)
						{
							matrix[i][j] = 1;
						}
						if (matrix[i][j] < -1)
						{
							matrix[i][j] = -1;
						}
					}
				}
			}
		}

		public Matrix crossover(Matrix partner)
		{
			Matrix child = new Matrix(rows, cols);

			int randC = (int)Math.Floor((double)new System.Random().Next(cols));
			int randR = (int)Math.Floor((double)new System.Random().Next(rows));

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					if ((i < randR) || (i == randR && j <= randC))
					{
						child.matrix[i][j] = matrix[i][j];
					}
					else
					{
						child.matrix[i][j] = partner.matrix[i][j];
					}
				}
			}
			return child;
		}

		public Matrix Clone()
		{
			Matrix clone = new Matrix(rows, cols);
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					clone.matrix[i][j] = matrix[i][j];
				}
			}
			return clone;
		}
	}
}