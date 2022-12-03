using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace BezierRotations
{
	internal static class Drawing
	{

		public static void drawBezierCurve(ProjectData projectData)
		{
			drawVertices(projectData);
			drawLines(projectData);
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
			points = points.OrderBy(p => p.X).ToList();

			projectData.points = points;
		}

		public static void drawVertices(ProjectData projectData)
		{
			int RADIUS = projectData.RADIUS;

			foreach (Vertex point in projectData.points)
			{
				projectData.graphics.FillEllipse(Brushes.Black, (int)point.X - RADIUS + 2, (int)point.Y - RADIUS + 2, (RADIUS - 2) * 2, (RADIUS - 2) * 2);
			}
		}
		public static void drawLines(ProjectData projectData)
		{
			for (int i = 0; i < projectData.points.Count - 1; ++i)
			{
				projectData.graphics.DrawLine(new Pen(Brushes.LightBlue), projectData.points[i].point, projectData.points[i + 1].point);
			}
		}

		public static void reDraw(ProjectData projectData)
		{
			projectData.graphics.Clear(Color.White);
			drawVertices(projectData);
			drawLines(projectData);
			List<Vector2> vector2s = Vertex.ToVector2List(projectData.points);
			projectData.bezierLine = BezierCurve.PointList2(vector2s);
			BezierCurve.drawBezierCurve(projectData);
			projectData.PictureBox.Refresh();
		}
	}
}
