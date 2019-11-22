using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAI.WinForms
{
	public class Piece
	{
		private int m_x;
		private int m_y;

		public Piece ()
		{
			m_x = 0;
			m_y = 0;
		}

		public int X { get; set; }

		public int Y { get; set; }

	}
}
