using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierRotations
{
	internal class Drawing
	{
		const int RADIUS = 4;
		Pen Pen { get; set; }
		PictureBox PictureBox { get; set; }
		Graphics graphics { get; set; }

		public Drawing(Pen pen, PictureBox pictureBox, Graphics graphics)
		{
			Pen = pen;
			PictureBox = pictureBox;
			this.graphics = graphics;
		}

		public void drawBezierCurve(int numberOfPoints)
		{
			List<PointF> points = generatePointsForBezierCurve(numberOfPoints);

			drawVertices(points);
			graphics.DrawBeziers(Pen, points.ToArray());

			PictureBox.Refresh();
		}

		private List<PointF> generatePointsForBezierCurve(int numberOfPoints)
		{
			List<PointF> points = new List<PointF>();

			if (numberOfPoints % 4 != 0)
			{
				// wyskakuje okienko albo jakiś error czy cokolwiek
				// wgl mogę zrobić walidację na input
				// 3k + 1
			}

			PointF startPoint = new PointF(50, PictureBox.Height / 2 + 100);
			PointF endPoint = new PointF(PictureBox.Width - 50, PictureBox.Height / 2 - 100);

			points.Add(startPoint);

			Random random = new Random();
			for (int i = 0; i < numberOfPoints - 2; ++i)
			{
				int x = random.Next((int)(0.25 * PictureBox.Width), (int)(0.75 * PictureBox.Width));
				int y = random.Next((int)(0.25 * PictureBox.Height), (int)(0.75 * PictureBox.Height));
				PointF point = new PointF(x, y);
				points.Add(point);
			}
			points.Add(endPoint);
			points.OrderBy(p => p.X);

			return points;
		}

		private void drawVertices(List<PointF> points)
		{
			foreach(PointF point in points)
			{
				graphics.FillEllipse(Brushes.Black, (int)point.X - RADIUS + 2, (int)point.Y - RADIUS + 2, (RADIUS - 2) * 2, (RADIUS - 2) * 2);
			}
		}
	}
}
