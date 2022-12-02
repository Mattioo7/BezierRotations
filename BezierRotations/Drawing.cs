using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierRotations
{
	internal static class Drawing
	{

		public static void drawBezierCurve(ProjectData projectData)
		{
			drawVertices(projectData);
			projectData.graphics.DrawBeziers(projectData.Pen, Vertex.ToPointFArray(projectData.points));

			projectData.PictureBox.Refresh();
		}

		public static void generatePointsForBezierCurve(ProjectData projectData)
		{
			List<Vertex> points = new List<Vertex>();

			if (projectData.numberOfPoints % 4 != 0)
			{
				// wyskakuje okienko albo jakiś error czy cokolwiek
				// wgl mogę zrobić walidację na input
				// 3k + 1
			}

			Vertex startPoint = new Vertex(50, projectData.PictureBox.Height / 2 + 100);
			Vertex endPoint = new Vertex(projectData.PictureBox.Width - 50, projectData.PictureBox.Height / 2 - 100);

			points.Add(startPoint);

			Random random = new Random();
			for (int i = 0; i < projectData.numberOfPoints - 2; ++i)
			{
				int x = random.Next((int)(0.25 * projectData.PictureBox.Width), (int)(0.75 * projectData.PictureBox.Width));
				int y = random.Next((int)(0.25 * projectData.PictureBox.Height), (int)(0.75 * projectData.PictureBox.Height));
				Vertex point = new Vertex(x, y);
				points.Add(point);
			}
			points.Add(endPoint);
			points.OrderBy(p => p.X);

			projectData.points = points;
		}

		private static void drawVertices(ProjectData projectData)
		{
			int RADIUS = projectData.RADIUS;

			foreach (Vertex point in projectData.points)
			{
				projectData.graphics.FillEllipse(Brushes.Black, (int)point.X - RADIUS + 2, (int)point.Y - RADIUS + 2, (RADIUS - 2) * 2, (RADIUS - 2) * 2);
			}
		}

		public static void reDraw()
		{

		}
	}
}
