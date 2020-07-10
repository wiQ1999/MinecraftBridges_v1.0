using System;

namespace MinecraftBridges_v1._0
{
	class MainProgram
	{
		static void Main(string[] args)
		{
			Map map = new Map(12, 2, 1, 14);

			//map.AddPoint();

			map.ShowMap();

			Console.SetCursorPosition(0, 30);
		}
	}
}
