using System;

namespace MinecraftBridges_v1._0
{
	class MainProgram
	{
		static void Main(string[] args)
		{
			Console.Title = "MinecraftBridges v1.0 Alfa";

			Map map = new Map(7, 50, -3, -8);

			map.AddPoint();
			map.AddPoint();

			map.ShowMap();

			Console.SetCursorPosition(0, map.MainMap.GetLength(1) + 3);
		}
	}
}
