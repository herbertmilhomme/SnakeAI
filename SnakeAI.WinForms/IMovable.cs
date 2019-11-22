using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SnakeAI.WinForms.Snake;

namespace SnakeAI.WinForms
{
	interface IMovable
	{
		void MoveLeft(int i_Index);
		void MoveRight(int i_Index);
		void MoveUp(int i_Index);
		void MoveDown(int i_Index);
	}
}
