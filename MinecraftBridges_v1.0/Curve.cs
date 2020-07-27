using System;
using System.Collections.Generic;

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
		/// <param name="a_iStartX">Start point vector x</param>
		/// <param name="a_iStartZ">Start point vector z</param>
		/// <param name="a_iEndX">End point vector x</param>
		/// <param name="a_iEndZ">End point vector z</param>
		public Curve(int a_iStartX, int a_iStartZ, int a_iEndX, int a_iEndZ)
		{
			this.MainPoints = new List<Point>();
			this.CurvePoints = new List<Point>();

			Point p = new Point { x = a_iStartX, z = a_iStartZ };
			MainPoints.Add(p);
			p = new Point { x = a_iEndX, z = a_iEndZ };
			MainPoints.Add(p);

			CreateCurve();
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

			CreateCurve();
		}

		/// <summary>
		/// Select only few points to main curve points list
		/// </summary>
		/// <param name="a_oTemporaryCurvePoints">Temporary list of curve points</param>
		private void SelectPoints(List<Point> a_oTemporaryCurvePoints)
		{
			//zapisanie pierwszego punktu krzywej
			this.CurvePoints.Add(a_oTemporaryCurvePoints[0]);

			//zmienna określająca ile przeszukano punktów (indeksów) krzywej tymczasowej
			int _iTempCurveDistance = 0;
			int _iCurvePointIndex = 0;

			//pętla po zatwierdzonych punktach krzywej - początkowo tylko pierwszy punkt
			while (a_oTemporaryCurvePoints[_iTempCurveDistance].x != this.MainPoints[1].x || a_oTemporaryCurvePoints[_iTempCurveDistance].z != this.MainPoints[1].z)
			{
				//pomocnicze punkty głównej krzywej
				int _iActualPointX = this.CurvePoints[_iCurvePointIndex].x;
				int _iActualPointZ = this.CurvePoints[_iCurvePointIndex].z;
				_iCurvePointIndex++;

				//sprawdzanie w pętlu punktów na ukos - dopóki nie są + lub - 2
				bool _fIsPointSaved = false;
				int _iTempRange = _iTempCurveDistance;
				while (a_oTemporaryCurvePoints[_iTempRange].x != _iActualPointX + 2 && a_oTemporaryCurvePoints[_iTempRange].x != _iActualPointX - 2
					&& a_oTemporaryCurvePoints[_iTempRange].z != _iActualPointZ + 2 && a_oTemporaryCurvePoints[_iTempRange].z != _iActualPointZ - 2)
				{
					//pomocnicze punkty tymczasowej krzywej
					int _iTempPointX = a_oTemporaryCurvePoints[_iTempRange].x;
					int _iTempPointZ = a_oTemporaryCurvePoints[_iTempRange].z;

					//sprawdzenie istenia punktu w róznych położeniach
					if (_iActualPointX + 1 == _iTempPointX && _iActualPointZ + 1 == _iTempPointZ)//prawo, góra
					{
						this.CurvePoints.Add(new Point { x = _iTempPointX, z = _iTempPointZ });
						_fIsPointSaved = true;
						break;
					}
					else if (_iActualPointX - 1 == _iTempPointX && _iActualPointZ + 1 == _iTempPointZ)//lewo, góra
					{
						this.CurvePoints.Add(new Point { x = _iTempPointX, z = _iTempPointZ });
						_fIsPointSaved = true;
						break;
					}
					else if (_iActualPointX + 1 == _iTempPointX && _iActualPointZ - 1 == _iTempPointZ)//prawo, dół
					{
						this.CurvePoints.Add(new Point { x = _iTempPointX, z = _iTempPointZ });
						_fIsPointSaved = true;
						break;
					}
					else if (_iActualPointX - 1 == _iTempPointX && _iActualPointZ - 1 == _iTempPointZ)//lewo, dół
					{
						this.CurvePoints.Add(new Point { x = _iTempPointX, z = _iTempPointZ });
						_fIsPointSaved = true;
						break;
					}

					//następny punkt, jeżeli nie jest to koniec listy
					if (_iTempRange == a_oTemporaryCurvePoints.Count - 1)
						break;
					else
						_iTempRange++;
				}

				//jeżeli nie zapisano punktu na ukos
				if (!_fIsPointSaved)
				{
					//sprawdzanie w pętlu punktów sąsiadujących - dopóki nie są + lub - 2
					while (a_oTemporaryCurvePoints[_iTempCurveDistance].x != _iActualPointX + 2 && a_oTemporaryCurvePoints[_iTempCurveDistance].x != _iActualPointX - 2
						&& a_oTemporaryCurvePoints[_iTempCurveDistance].z != _iActualPointZ + 2 && a_oTemporaryCurvePoints[_iTempCurveDistance].z != _iActualPointZ - 2)
					{
						//pomocnicze punkty tymczasowej krzywej
						int _iTempPointX = a_oTemporaryCurvePoints[_iTempCurveDistance].x;
						int _iTempPointZ = a_oTemporaryCurvePoints[_iTempCurveDistance].z;

						if (_iActualPointX == _iTempPointX && _iActualPointZ + 1 == _iTempPointZ)//góra
						{
							this.CurvePoints.Add(new Point { x = _iTempPointX, z = _iTempPointZ });
							break;
						}
						else if (_iActualPointX == _iTempPointX && _iActualPointZ - 1 == _iTempPointZ)//dół
						{
							this.CurvePoints.Add(new Point { x = _iTempPointX, z = _iTempPointZ });
							break;
						}
						else if (_iActualPointX - 1 == _iTempPointX && _iActualPointZ == _iTempPointZ)//lewo
						{
							this.CurvePoints.Add(new Point { x = _iTempPointX, z = _iTempPointZ });
							break;
						}
						else if (_iActualPointX + 1 == _iTempPointX && _iActualPointZ == _iTempPointZ)
						{
							this.CurvePoints.Add(new Point { x = _iTempPointX, z = _iTempPointZ });
							break;
						}

						//następny punkt
						_iTempCurveDistance++;
					}
				}
				else//jeżeli zapisano punktu na ukos
				{
					//ominięcie zbadanych już punktów
					_iTempCurveDistance = _iTempRange;
				}
			}
		}

		/// <summary>
		/// Counts distance between main curve points in X and Z axes
		/// </summary>
		/// <returns>Distance in X and Z axes</returns>
		private Point CountPointsDistance()
		{
			//deklaracja zmiennych
			int _iDistanceX = 1, _iDistanceZ = 1;
			Point _LastPoint = new Point { x = this.MainPoints[0].x, z = this.MainPoints[0].z };

			//pętlap po głównych punktach krzywej - od drugiego punktu
			for (int index = 1; index < this.MainPoints.Count; index++)
			{
				_iDistanceX += Math.Abs(_LastPoint.x - this.MainPoints[index].x);
				_iDistanceZ += Math.Abs(_LastPoint.z - this.MainPoints[index].z);
				_LastPoint = new Point { x = this.MainPoints[index].x, z = this.MainPoints[index].z };
			}

			//zwracanie wartości
			return new Point { x = _iDistanceX, z = _iDistanceZ};
		}

		/// <summary>
		/// Get number length
		/// </summary>
		/// <param name="a_iValue">Number to calculate</param>
		/// <returns>Integer value represents how many numbers is needed to write this value</returns>
		public virtual int NumberLength(int a_iValue)
		{
			//inicjalizacja zmiennych
			int _iResult = 1, i = 10;

			//pętla licząca wielkość liczby
			while (i < a_iValue)
			{
				i *= 10;
				_iResult++;
			}

			//zwracanie wartości
			return _iResult;
		}

		/// <summary>
		/// Creates curve from selected type operation
		/// </summary>
		/// <param name="a_iType">Opeartion type</param>
		private void CreateCurve()
		{
			//wyczyszczenie listy punktów krzywej
			this.CurvePoints.Clear();

			//lista tymczasowa wszstkich punktów krzywej
			List<Point> _oTemporaryCurvePoints = new List<Point>();

			//zmierzenie interwału czasowego krzywej
			float _fInterval;
			int _iNumberSize;
			Point _Distance = CountPointsDistance();
			if (_Distance.x > _Distance.z)
			{
				_iNumberSize = NumberLength(_Distance.x) + 1;
				_fInterval = (float)Math.Round((float)1 / (_Distance.x * 10), _iNumberSize, MidpointRounding.ToZero);
			}
			else
			{
				_iNumberSize = NumberLength(_Distance.z) + 1;
				_fInterval = (float)Math.Round((float)1 / (_Distance.z * 10), _iNumberSize, MidpointRounding.ToZero);
			}

			//obliczanie punktów krzywej
			double t = 0f;
			while(t <= 1f)
			{
				//nowy punkt krzywej
				Point p = new Point();

				//wybranie wzoru
				 switch (this.MainPoints.Count)
				 {
					case 2:				
						p.x = (int)Math.Round((1 - t) * this.MainPoints[0].x + t * this.MainPoints[1].x, MidpointRounding.AwayFromZero);
						p.z = (int)Math.Round((1 - t) * this.MainPoints[0].z + t * this.MainPoints[1].z, MidpointRounding.AwayFromZero);

						_oTemporaryCurvePoints.Add(p);
						break;
					case 3:
						p.x = (int)Math.Round(Math.Pow((1 - t), 2) * this.MainPoints[0].x + 2 * (1 - t) * t * this.MainPoints[2].x + Math.Pow(t, 2) * this.MainPoints[1].x, MidpointRounding.AwayFromZero);
						p.z = (int)Math.Round(Math.Pow((1 - t), 2) * this.MainPoints[0].z + 2 * (1 - t) * t * this.MainPoints[2].z + Math.Pow(t, 2) * this.MainPoints[1].z, MidpointRounding.AwayFromZero);

						_oTemporaryCurvePoints.Add(p);
						break;
					case 4:
						p.x = (int)Math.Round(Math.Pow((1 - t), 3) * this.MainPoints[0].x + 3 * Math.Pow((1 - t), 2) * t * this.MainPoints[2].x + 3 * (1 - t) * Math.Pow(t, 2) * this.MainPoints[3].x + Math.Pow(t, 3) * this.MainPoints[1].x, MidpointRounding.AwayFromZero);
						p.z = (int)Math.Round(Math.Pow((1 - t), 3) * this.MainPoints[0].z + 3 * Math.Pow((1 - t), 2) * t * this.MainPoints[2].z + 3 * (1 - t) * Math.Pow(t, 2) * this.MainPoints[3].z + Math.Pow(t, 3) * this.MainPoints[1].z, MidpointRounding.AwayFromZero);

						_oTemporaryCurvePoints.Add(p);
						break;
					default:
						Console.WriteLine("Nieprawidłowa ilość głównych punktów");
						break;
				 }

				//przesunięcie w czasie na krzywej
				//zaokrąglenie jednostki czasu
				t = Math.Round(t +_fInterval, _iNumberSize);
			}

			//Selekcja punktów krzywej
			SelectPoints(_oTemporaryCurvePoints);
		}
	}
}