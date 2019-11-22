using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAI.WinForms
{
	public class Snake : IMovable
	{
		private List<Piece> m_Body;

		public Snake()
		{
			m_Body = new List<Piece>();
		}

		public List<Piece> Body
		{
			get { return m_Body; }
			set { m_Body = value; }
		}

		public int Length
		{
			get { return m_Body.Count; }
		}
		public void MoveLeft(int i_Index)
		{
			m_Body[i_Index].X--;
		}

		public void MoveRight(int i_Index)
		{
			m_Body[i_Index].X++;
		}

		public void MoveUp(int i_Index)
		{
			m_Body[i_Index].Y--;
		}

		public void MoveDown(int i_Index)
		{
			m_Body[i_Index].Y++;
		}

		internal void Add(Piece head)
		{
			m_Body.Add(head);
		}
	}
}
