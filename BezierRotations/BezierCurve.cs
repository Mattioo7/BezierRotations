using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierRotations
{
	internal class BezierCurve
	{
		public static Vector2 Point2(float t, List<Vector2> controlPoints)
		{
			int N = controlPoints.Count - 1;
			if (N > 16)
			{
				Debug.WriteLine("You have used more than 16 control points. The maximum control points allowed is 16.");
				controlPoints.RemoveRange(16, controlPoints.Count - 16);
			}

			if (t <= 0) return controlPoints[0];
			if (t >= 1) return controlPoints[controlPoints.Count - 1];

			Vector2 p = new Vector2();

			for (int i = 0; i < controlPoints.Count; ++i)
			{
				Vector2 bn = Bernstein(N, i, t) * controlPoints[i];
				p += bn;
			}

			return p;
		}

		public static List<Vector2> PointList2(List<Vector2> controlPoints, float interval = 0.0001f)
		{
			int N = controlPoints.Count - 1;
			if (N > 16)
			{
				Debug.WriteLine("You have used more than 16 control points. The maximum control points allowed is 16.");
				controlPoints.RemoveRange(16, controlPoints.Count - 16);			// Mogę w sumie też obcinać do 16 liczbę punktów (chyba raczej do 12)
			}

			List<Vector2> points = new List<Vector2>();
			for (float t = 0.0f; t <= 1.0f + interval - 0.0001f; t += interval)
			{
				Vector2 p = new Vector2();
				for (int i = 0; i < controlPoints.Count; ++i)
				{
					Vector2 bn = Bernstein(N, i, t) * controlPoints[i];
					p += bn;
				}
				points.Add(p);
			}

			return points;
		}

		public static List<Vector2> calculateDerivativeForBezierCurve(List<Vector2> controlPoints, List<Vector2> points, float interval = 0.0001f)
		{
			int N = controlPoints.Count - 1;
			if (N > 16)
			{
				Debug.WriteLine("You have used more than 16 control points. The maximum control points allowed is 16.");
				controlPoints.RemoveRange(16, N - 16);            // Mogę w sumie też obcinać do 16 liczbę punktów (chyba raczej do 12)
			}

			List<Vector2> slopes = new List<Vector2>();
			for (float t = 0.0f; t <= 1.0f + interval - 0.0001f; t += interval)
			{
				Vector2 p = new Vector2();
				for (int i = 0; i < N - 1; ++i)
				{
					Vector2 bn = N * Bernstein(N - 1, i, t) * (controlPoints[i + 1] - controlPoints[i]);
					p += bn;
				}
				points.Add(p);
			}

			return slopes;
		}

		public static void drawBezierCurve(ProjectData projectData)
		{
			foreach (Vector2 point in projectData.bezierLine)
			{
				projectData.graphics.FillEllipse(Brushes.Black, (int)point.X, (int)point.Y, 2, 2);
			}
		}

		private static float Bernstein(int n, int i, float t)
		{
			float t_i = (float)Math.Pow(t, i);
			float t_n_minus_i = (float)Math.Pow((1 - t), (n - i));

			float basis = Binomial(n, i) * t_i * t_n_minus_i;
			return basis;
		}

		private static float Binomial(int n, int i)
		{
			float ni;
			float a1 = Factorial[n];
			float a2 = Factorial[i];
			float a3 = Factorial[n - i];
			ni = a1 / (a2 * a3);
			return ni;
		}

		private static float[] Factorial = new float[]
		{
		1.0f,
		1.0f,
		2.0f,
		6.0f,
		24.0f,
		120.0f,
		720.0f,
		5040.0f,
		40320.0f,
		362880.0f,
		3628800.0f,
		39916800.0f,
		479001600.0f,
		6227020800.0f,
		87178291200.0f,
		1307674368000.0f,
		20922789888000.0f,
		};

	}
}
