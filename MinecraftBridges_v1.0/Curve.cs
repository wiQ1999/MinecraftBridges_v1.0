using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftBridges_v1._0
{
	class Curve
	{
		/// <summary>
		/// List of points represents curve main points
		/// </summary>
		public List<Point> MainPoints { get; set; }
		/// <summary>
		/// List of points represents Bezier curve
		/// </summary>
		public List<Point> CurvePoints { get; set; }

		/// <summary>
		/// CTOR
		/// </summary>
		/// <param name="a_iStartx">Start point vector x</param>
		/// <param name="a_iStartz">Start point vector z</param>
		/// <param name="a_iEndx">End point vector x</param>
		/// <param name="a_iEndz">End point vector z</param>
		public Curve(int a_iStartx, int a_iStartz, int a_iEndx, int a_iEndz)
		{
			this.MainPoints = new List<Point>();
			this.CurvePoints = new List<Point>();

			Point p = new Point { x = a_iStartx, z = a_iStartz };
			MainPoints.Add(p);
			p = new Point { x = a_iEndx, z = a_iEndz };
			MainPoints.Add(p);

			CountPoints();
		}

		/// <summary>
		/// Add a new point to change the curve
		/// </summary>
		public void AddPoint()
		{
			Console.WriteLine("Point " + this.MainPoints.Count);
			Point p = new Point();

			Console.Write("X: ");
			p.x = int.Parse(Console.ReadLine());
			Console.Write("Z: ");
			p.z = int.Parse(Console.ReadLine());

			this.MainPoints.Add(p);
		}

		/// <summary>
		/// Select right curve operation
		/// </summary>
		private void CountPoints()
		{
			switch (this.MainPoints.Count)
			{
				case 2:
					CreateCurve(0);
					break;
				case 3:
					CreateCurve(1);
					break;
				case 4:
					CreateCurve(2);
					break;
				default:
					Console.WriteLine("Zła liczba punktów");
					break;
			}
		}

		/// <summary>
		/// Creates curve from selected type od operation
		/// </summary>
		/// <param name="a_iType">Opeartion type</param>
		private void CreateCurve(int a_iType)
		{
			float t = 0f;
			while(t <= 1f)
			{
				Point p = new Point();

				switch (a_iType)
				{
					case 0:
						p.x = (int)((1 - t) * this.MainPoints[0].x + t * this.MainPoints[1].x);
						p.z = (int)((1 - t) * this.MainPoints[0].z + t * this.MainPoints[1].z);

						this.CurvePoints.Add(p);
						break;
					case 1:

						break;
					case 2:

						break;
				}

				t += 0.01f;
				t = (float)Math.Round(t, 2);
			}
		}
	}
}
