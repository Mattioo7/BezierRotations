using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BezierRotations
{
	internal static class Drawing
	{

		public static void drawBezierCurve(ProjectData projectData)
		{
			drawVertices(projectData);
			drawLines(projectData);
			projectData.graphics.DrawBeziers(projectData.pen, Vertex.ToPointFArray(projectData.controlPoints));

			projectData.pictureBox.Refresh();
		}

		public static List<Vertex> generatePointsForBezierCurve(ProjectData projectData)
		{
			List<Vertex> points = new List<Vertex>();

			if (projectData.numberOfPoints > 12)
			{
				projectData.numberOfPoints = 12;
			}

			Vertex startPoint = new Vertex(50, projectData.pictureBox.Height / 2 + 100);
			Vertex endPoint = new Vertex(projectData.pictureBox.Width - 50, projectData.pictureBox.Height / 2 - 100);

			points.Add(startPoint);

			Random random = new Random();
			for (int i = 0; i < projectData.numberOfPoints - 2; ++i)
			{
				int x = random.Next((int)(0.1 * projectData.pictureBox.Width), (int)(0.9 * projectData.pictureBox.Width));
				int y = random.Next((int)(0.1 * projectData.pictureBox.Height), (int)(0.9 * projectData.pictureBox.Height));
				Vertex point = new Vertex(x, y);
				points.Add(point);
			}
			points.Add(endPoint);
			points = points.OrderBy(p => p.X).ToList();

			return points;
		}

		public static void drawVertices(ProjectData projectData, bool tmp = false)
		{
			int RADIUS = projectData.RADIUS;

			if (tmp == true)
			{
				foreach (Vertex point in projectData.controlPoints)
				{
					projectData.graphicsTmp.FillEllipse(Brushes.Black, (int)point.X - RADIUS + 2, (int)point.Y - RADIUS + 2, (RADIUS - 2) * 2, (RADIUS - 2) * 2);
				}
				return;
			}

			foreach (Vertex point in projectData.controlPoints)
			{
				projectData.graphics.FillEllipse(Brushes.Black, (int)point.X - RADIUS + 2, (int)point.Y - RADIUS + 2, (RADIUS - 2) * 2, (RADIUS - 2) * 2);
			}
		}
		public static void drawLines(ProjectData projectData, bool tmp = false)
		{
			if (tmp == true)
			{
				for (int i = 0; i < projectData.controlPoints.Count - 1; ++i)
				{
					projectData.graphicsTmp.DrawLine(new Pen(Brushes.LightBlue), projectData.controlPoints[i].point, projectData.controlPoints[i + 1].point);
				}
				return;
			}
			
			for (int i = 0; i < projectData.controlPoints.Count - 1; ++i)
			{
				projectData.graphics.DrawLine(new Pen(Brushes.LightBlue), projectData.controlPoints[i].point, projectData.controlPoints[i + 1].point);
			}
		}

		public static void drawTexture(ProjectData projectData, (int x, int y) center)
		{
			// obliczenie lewgo górnego wierzchołka
			int startX = center.x - projectData.texture.Width / 2;
			int startY = center.y - projectData.texture.Height / 2;

			//projectData.graphics.DrawImage(projectData.texture, startX, startY);

			for (int i = 0; i < projectData.texture.Width; ++i)
			{
				for (int j = 0; j < projectData.texture.Height; ++j)
				{
					Color color = projectData.texture.GetPixel(i, j);
					if (color.A != 0 && startX + i >= 0 && startX + i < projectData.bitmapSnoopTmp.Width - 1 && startY + j >= 0 && startY + j < projectData.bitmapSnoopTmp.Height - 1)
					{
						projectData.bitmapSnoop.SetPixel(startX + i, startY + j, color);
					}
				}
			}
		}

		public static void drawTextureTmp(ProjectData projectData, (int x, int y) center)
		{
			// obliczenie lewgo górnego wierzchołka
			int startX = center.x - projectData.texture.Width / 2;
			int startY = center.y - projectData.texture.Height / 2;

			for (int i = 0; i < projectData.texture.Width; ++i)
			{
				for (int j = 0; j < projectData.texture.Height; ++j)
				{
					Color color = projectData.texture.GetPixel(i, j);
					if (color.A != 0 && startX + i >= 0 && startX + i < projectData.bitmapSnoopTmp.Width - 1 && startY + j >= 0 && startY + j < projectData.bitmapSnoopTmp.Height - 1)
					{
						projectData.bitmapSnoopTmp.SetPixel(startX + i, startY + j, color);
					}
				}
			}
		}

		public static void drawTextureTmp2(ProjectData projectData, (int x, int y) center)
		{
			// obliczenie lewgo górnego wierzchołka
			int startX = center.x - projectData.texture.Width / 2;
			int startY = center.y - projectData.texture.Height / 2;

			for (int i = 0; i < projectData.texture.Width; ++i)
			{
				for (int j = 0; j < projectData.texture.Height; ++j)
				{
					Color color = projectData.textureSnoopTmp2.GetPixel(i, j);
					if (color.A != 0 && startX + i >= 0 && startX + i < projectData.bitmapSnoopTmp.Width - 1 && startY + j >= 0 && startY + j < projectData.bitmapSnoopTmp.Height - 1)
					{
						projectData.bitmapSnoopTmp.SetPixel(startX + i, startY + j, color);
					}
				}
			}
		}

		public static void drawTexture(ProjectData projectData, Vector2 center, bool tmp = false, int mode = 0)
		{
			if (mode == 2)
			{
				drawTextureTmp2(projectData, ((int x, int y))(center.X, center.Y));
				return;
			}
					
			if (tmp == true)
			{
				drawTextureTmp(projectData, ((int x, int y))(center.X, center.Y));
				return;
			}
				
			drawTexture(projectData, ((int x, int y))(center.X, center.Y));
		}

		public static void convertToGrayScale(ProjectData projectData)
		{
			ColorMatrix colorMatrix = new ColorMatrix(
			   new float[][]
			   {
				   new float[] {.3f, .3f, .3f, 0, 0},
				   new float[] {.59f, .59f, .59f, 0, 0},
				   new float[] {.11f, .11f, .11f, 0, 0},
				   new float[] {0, 0, 0, 1, 0},
				   new float[] {0, 0, 0, 0, 1}
			   });
			ImageAttributes img = new ImageAttributes();
			img.SetColorMatrix(colorMatrix);
			

			Bitmap newBmp = new Bitmap(projectData.texture.Width, projectData.texture.Height);
			Graphics newBmpGraphics = Graphics.FromImage(newBmp);
			newBmpGraphics.DrawImage(projectData.texture, new Rectangle(0, 0, projectData.texture.Width, projectData.texture.Height), 0, 0, projectData.texture.Width, projectData.texture.Height, GraphicsUnit.Pixel, img);
			projectData.texture = newBmp;
			projectData.textureGraphics = newBmpGraphics;
			using (var textureSnoop = new BmpPixelSnoop(projectData.texture))
			{
				projectData.textureSnoop = textureSnoop;
			}

			Bitmap newBmpTmp = new Bitmap(projectData.textureTmp.Width, projectData.textureTmp.Height);
			Graphics newBmpGraphicsTmp = Graphics.FromImage(newBmpTmp);
			newBmpGraphicsTmp.DrawImage(projectData.textureTmp, new Rectangle(0, 0, projectData.textureTmp.Width, projectData.textureTmp.Height), 0, 0, projectData.textureTmp.Width, projectData.textureTmp.Height, GraphicsUnit.Pixel, img);
			projectData.textureTmp = newBmpTmp;
			projectData.textureGraphicsTmp = newBmpGraphicsTmp;
			using (var textureSnoopTmp = new BmpPixelSnoop(projectData.textureTmp))
			{
				projectData.textureSnoopTmp = textureSnoopTmp;
			}

		}

		public static void reDraw(ProjectData projectData)
		{
			projectData.graphics.Clear(Color.White);
			drawLines(projectData);
			drawVertices(projectData);
			List<Vector2> controlPointsAsVectors = Vertex.ToVector2List(projectData.controlPoints);
			projectData.bezierLine = BezierCurve.PointList2(controlPointsAsVectors);
			projectData.bezierLineDerivatives = BezierCurve.calculateDerivativeForBezierCurve(controlPointsAsVectors);
			BezierCurve.drawBezierCurve(projectData);

			//projectData.graphics.DrawImage(projectData.texture, 500, 100);
			Drawing.drawTexture(projectData, projectData.bezierLine[projectData.currentPosition]);

			projectData.pictureBox.Refresh();
		}
	}
}
