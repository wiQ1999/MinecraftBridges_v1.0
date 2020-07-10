using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftBridges_v1._0
{
	class Map : Curve
	{
		public int[,] MainMap { get; set; }

		public Map(int a_iStartx, int a_iStartz, int a_iEndx, int a_iEndz) : base(a_iStartx, a_iStartz, a_iEndx, a_iEndz)
		{
		}

		public void ShowMap()
		{
			CreateMap();

			for (int x = 0; x < this.MainMap.GetLength(0); x++)
			{
				for (int z = 0; z < this.MainMap.GetLength(1); z++)
				{
					if(this.MainMap[x, z] != 0)
					{
						Console.SetCursorPosition(x, z);
						Console.Write(this.MainMap[x, z]);
					}
				}
			}
		}

		private void CreateMap()
		{
			//szukanie skrajnych punktów
			int _iBiggestX = MainPoints[0].x;
			int _iBiggestZ = MainPoints[0].z;
			int _iSmallestX = MainPoints[0].x;
			int _iSmallestZ = MainPoints[0].z;

			foreach (Point p in MainPoints)
			{
				if (p.x > _iBiggestX)
					_iBiggestX = p.x;
				if (p.z > _iBiggestZ)
					_iBiggestZ = p.z;
				if (p.x < _iSmallestX)
					_iSmallestX = p.x;
				if (p.z < _iSmallestZ)
					_iSmallestZ = p.z;
			}

			//obliczenie różnucy skrajnych punktów
			//tworzenie mapy 2D
			this.MainMap = new int[Math.Abs(_iBiggestX - _iSmallestX) + 1, Math.Abs(_iBiggestZ - _iSmallestZ) + 1];

			//wypełnienie tablicy punktami krzywej
			foreach (Point p in CurvePoints)
			{
				this.MainMap[p.x - _iSmallestX, p.z - _iSmallestZ] = 1;
			}
		}
	}
}
