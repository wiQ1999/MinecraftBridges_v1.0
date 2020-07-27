using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftBridges_v1._0
{
	class Map : Curve
	{
		/// <summary>
		/// 2D array representing map with curve points
		/// </summary>
		public int[,] MainMap { get; set; }
		/// <summary>
		/// The biggest value of X axes
		/// </summary>
		private int BiggestX { get; set; }
		/// <summary>
		/// The biggest value of Z axes
		/// </summary>
		private int BiggestZ { get; set; }

		/// <summary>
		/// CTOR
		/// </summary>
		/// <param name="a_iStartX">Start point vector x</param>
		/// <param name="a_iStartZ">Start point vector z</param>
		/// <param name="a_iEndX">End point vector x</param>
		/// <param name="a_iEndZ">End point vector z</param>
		public Map(int a_iStartX, int a_iStartZ, int a_iEndX, int a_iEndZ) : base(a_iStartX, a_iStartZ, a_iEndX, a_iEndZ)
		{
			//nadpisanie losową zgodną wartością, aby prawidłowo porównywać wartości
			this.BiggestX = MainPoints[0].x;
			this.BiggestZ = MainPoints[0].z;
		}

		/// <summary>
		/// Generate and show map with curve
		/// </summary>
		public void ShowMap()
		{
			Console.Clear();

			//tworzenie mapy
			Point Distance = CreateMap();

			//wypisanie liczb pomocniczych
			//odległości od krawędzi
			int _iMaxZ = 1;
			if (BiggestX > 9)
			{
				_iMaxZ = 2;
				if (BiggestX > 99)
					_iMaxZ = 3;
			}

			int _iMaxX = 1;
			if (BiggestZ > 9)
			{
				_iMaxX = 2;
				if (BiggestZ > 99)
					_iMaxX = 3;
			}

			int _iConsolePosition = 0;
			for (int z = this.MainMap.GetLength(1) + Distance.z - 1; z >= Distance.z; z--)
			{
				Console.SetCursorPosition(0, _iConsolePosition++);
				Console.Write(z);
			}

			_iConsolePosition = _iMaxZ;
			int _iFlipFlop = 0;
			for (int x = Distance.x; x < this.MainMap.GetLength(0) + Distance.x; x++)
			{
				if (_iFlipFlop == 3)
					_iFlipFlop = 0;

				Console.SetCursorPosition(_iConsolePosition++, this.MainMap.GetLength(1) + _iFlipFlop++);
				Console.Write(x);
			}

			//narysowanie krzywej
			for (int x = 0; x < this.MainMap.GetLength(0); x++)
			{
				for (int z = 0; z < this.MainMap.GetLength(1); z++)
				{
					Console.SetCursorPosition(x + _iMaxX, this.MainMap.GetLength(1) - 1 - z);

					if (this.MainMap[x, z] != 0)
						Console.BackgroundColor = ConsoleColor.Red;
					else
						Console.BackgroundColor = ConsoleColor.Blue;

					Console.Write(" ");
					Console.ResetColor();
				}
			}
		}

		/// <summary>
		/// Generates map and fill it with curve points
		/// </summary>
		/// <returns>The smallest distance X and Z axes</returns>
		private Point CreateMap()
		{
			//obliczenie różnucy skrajnych punktów
			int _iSmallestX = MainPoints[0].x;
			int _iSmallestZ = MainPoints[0].z;
			foreach (Point p in this.MainPoints)
			{
				if (p.x > BiggestX)
					BiggestX = p.x;
				if (p.z > BiggestZ)
					BiggestZ = p.z;
				if (p.x < _iSmallestX)
					_iSmallestX = p.x;
				if (p.z < _iSmallestZ)
					_iSmallestZ = p.z;
			}

			//tworzenie mapy 2D
			this.MainMap = new int[Math.Abs(BiggestX - _iSmallestX) + 1, Math.Abs(BiggestZ - _iSmallestZ) + 1];

			//wypełnienie tablicy punktami krzywej
			foreach (Point p in CurvePoints)
			{
				this.MainMap[p.x - _iSmallestX, p.z - _iSmallestZ] = 1;
			}

			//zwrócenie różnicy punktów
			return new Point { x = _iSmallestX, z = _iSmallestZ };
		}
	}
}
