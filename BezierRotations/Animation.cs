using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BezierRotations
{
	internal static class Animation
	{
		public enum AnimationType
		{
			Rotation,
			Moving,
			OnlyMoving
		}

		public enum RotationType
		{
			Naive,
			Shear
		}
		
		/*public static void calculateRotations(ProjectData projectData)
		{
			List<Vector2> bezierLine = projectData.bezierLine;

			for (int i = 0; i < bezierLine.Count() - 2; i++)
			{
				Vector2 a = bezierLine[i];
				Vector2 b = bezierLine[i + 1];
				Vector2 c = bezierLine[i + 2];

				float slope1 = slope(a.X, a.Y, b.X, b.Y);
				float slope2 = slope(b.X, b.Y, c.X, c.Y);

				float diff = slope2 - slope1; // kąt o jaki obrócić


			}
		}

		public static float slope(float x1, float y1, float x2, float y2)
		{
			if (x2 - x1 != 0F)
			{
				return (y2 - y1) / (x2 - x1);
			}
			return int.MaxValue;
		}*/

		public static void doAnimation(Object obj, EventArgs myEventArgs, ProjectData projectData)
		{
			switch (projectData.animationType)
			{
				case AnimationType.Rotation:
					doRotationAnimation(projectData);
					break;
				case AnimationType.Moving:
					doMovingAnimation(projectData);
					break;
				case AnimationType.OnlyMoving:
					doOnlyMovingAnimation(projectData);
					break;
			}
		}

		private static void doRotationAnimation(ProjectData projectData)
		{
			switch (projectData.rotationType)
			{
				case RotationType.Naive:
					doNaiveRotationAnimation(projectData);
					break;
				case RotationType.Shear:
					//doShearRotationAnimation(projectData);
					//shear(projectData);
					doProShear(projectData);
					break;
			}
		}

		private static void doMovingAnimation(ProjectData projectData)
		{
			moveAlongBezierLine(projectData);

			switch (projectData.rotationType)
			{
				case RotationType.Naive:
					doBezierNaiveRotation(projectData);
					break;
				case RotationType.Shear:
					//bezierShearRotation(projectData);
					break;
			}
		}
		
		private static void doOnlyMovingAnimation(ProjectData projectData)
		{
			moveAlongBezierLine(projectData);
			onlyMoveAnimation(projectData);
		}
		
		private static void doBezierNaiveRotation(ProjectData projectData)
		{
			// zmiana kąta
			Vector2 currentPoint = projectData.bezierLineDerivatives[projectData.currentPosition];
			projectData.angle = -(float)Math.Atan2(currentPoint.Y, currentPoint.X);

			// narysowanie nowej kanwy bez tekstury
			projectData.graphicsTmp.Clear(Color.White);
			Drawing.drawLines(projectData, tmp: true);
			Drawing.drawVertices(projectData, tmp: true);
			//BezierCurve.drawBezierCurve(projectData, tmp: true);
					
			// inicjalizacja nowe tekstury
			projectData.texture = new Bitmap(projectData.textureTmp.Width, projectData.textureTmp.Height);
			projectData.textureGraphics = Graphics.FromImage(projectData.texture);
			using (var newTextureSnoop = new BmpPixelSnoop(projectData.texture))
			{
				projectData.textureSnoop = newTextureSnoop;
			}

			for (int j = 0; j < projectData.textureTmp.Height; ++j)
			{
				for (int i = 0; i < projectData.textureTmp.Width; ++i)
				{
					(float newX, float newY) = naiveRotationForPoint(-projectData.textureTmp.Width / 2f + i, -projectData.textureTmp.Height / 2f + j, projectData.angle);
					newX += projectData.textureTmp.Width / 2f;
					newY += projectData.textureTmp.Height / 2f;

					if (newX >= 0 && newX < projectData.textureTmp.Width - 1 && newY >= 0 && newY < projectData.textureTmp.Height - 1)
					{
						Color color = projectData.textureSnoopTmp.GetPixel(i, j);
						projectData.textureSnoop.SetPixel((int)(newX + 0.5f), (int)(newY + 0.5f), color);
					}
				}
			}

			// nałożenie tekstury z szachownicą na pomocniczą kanwę
			var position = projectData.bezierLine[projectData.currentPosition];
			Drawing.drawTexture(projectData, projectData.bezierLine[projectData.currentPosition], tmp: true);

			// DEBUG - narysowanie wektora
			/*Point grot = new Point((int)(projectData.bezierLine[projectData.currentPosition].X + projectData.bezierLineDerivatives[projectData.currentPosition].X),
				(int)(projectData.bezierLine[projectData.currentPosition].Y + projectData.bezierLineDerivatives[projectData.currentPosition].Y));
			Point center = new Point((int)projectData.bezierLine[projectData.currentPosition].X, (int)projectData.bezierLine[projectData.currentPosition].Y);
			projectData.graphicsTmp.DrawLine(new Pen(Brushes.Red, 2), center, grot);*/

			BezierCurve.drawBezierCurve(projectData, tmp: true);

			// zamiana canvasów; bez znaczenia, bo i tak jest czyszczona, ale nie tworzę nowej chociaż
			(projectData.pictureBox.Image, projectData.bitmapTmp) = (projectData.bitmapTmp, (Bitmap)projectData.pictureBox.Image);
			(projectData.graphics, projectData.graphicsTmp) = (projectData.graphicsTmp, projectData.graphics);
			(projectData.bitmapSnoop, projectData.bitmapSnoopTmp) = (projectData.bitmapSnoopTmp, projectData.bitmapSnoop);

			projectData.pictureBox.Refresh();
		}


		private static void doNaiveRotationAnimation(ProjectData projectData)
		{
			float angle = projectData.angle;
			
			// narysowanie nowej kanwy bez tekstury
			projectData.graphicsTmp.Clear(Color.White);
			Drawing.drawLines(projectData, tmp: true);
			Drawing.drawVertices(projectData, tmp: true);
			BezierCurve.drawBezierCurve(projectData, tmp: true);

			// inicjalizacja nowe tekstury
			projectData.texture = new Bitmap(projectData.textureTmp.Width, projectData.textureTmp.Height);
			projectData.textureGraphics = Graphics.FromImage(projectData.texture);
			using (var newTextureSnoop = new BmpPixelSnoop(projectData.texture))
			{
				projectData.textureSnoop = newTextureSnoop;
			}

			for (int j = 0; j < projectData.textureTmp.Height; ++j)
			{
				for (int i = 0; i < projectData.textureTmp.Width; ++i)
				{
					(float newX, float newY) = naiveRotationForPoint(-projectData.textureTmp.Width / 2f + i, -projectData.textureTmp.Height / 2f + j, angle);
					newX += projectData.textureTmp.Width / 2f;
					newY += projectData.textureTmp.Height / 2f;

					if (newX >= 0 && newX < projectData.textureTmp.Width - 1 && newY >= 0 && newY < projectData.textureTmp.Height - 1)
					{
						Color color = projectData.textureSnoopTmp.GetPixel(i, j);
						projectData.textureSnoop.SetPixel((int)(newX + 0.5f), (int)(newY + 0.5f), color);
					}
				}
			}

			// nałożenie tekstury z szachownicą na pomocniczą kanwę
			Drawing.drawTexture(projectData, projectData.bezierLine[projectData.currentPosition], tmp: true);

			// zwiększenie kąta
			projectData.angle += projectData.angleDiff;

			// zamiana canvasów; bez znaczenia, bo i tak jest czyszczona, ale nie tworzę nowej chociaż
			(projectData.pictureBox.Image, projectData.bitmapTmp) = (projectData.bitmapTmp, (Bitmap)projectData.pictureBox.Image);
			(projectData.graphics, projectData.graphicsTmp) = (projectData.graphicsTmp, projectData.graphics);
			(projectData.bitmapSnoop, projectData.bitmapSnoopTmp) = (projectData.bitmapSnoopTmp, projectData.bitmapSnoop);

			projectData.pictureBox.Refresh();
		}

		private static void doShearRotationAnimation(ProjectData projectData)
		{
			Xshear1(projectData, projectData.angle, projectData.texture.Width, projectData.texture.Height);
			Yshear(projectData, projectData.angle, projectData.texture.Width, projectData.texture.Height);
			Xshear2(projectData, projectData.angle, projectData.texture.Width, projectData.texture.Height);

			projectData.angle += projectData.angleDiff;

			// nałożenie tekstury z szachownicą na pomocniczą kanwę
			Drawing.drawTexture(projectData, projectData.bezierLine[projectData.currentPosition], tmp: true);

			// zamiana canvasów; bez znaczenia, bo i tak jest czyszczona, ale nie tworzę nowej chociaż
			(projectData.pictureBox.Image, projectData.bitmapTmp) = (projectData.bitmapTmp, (Bitmap)projectData.pictureBox.Image);
			(projectData.graphics, projectData.graphicsTmp) = (projectData.graphicsTmp, projectData.graphics);
			(projectData.bitmapSnoop, projectData.bitmapSnoopTmp) = (projectData.bitmapSnoopTmp, projectData.bitmapSnoop);

			projectData.pictureBox.Refresh();
		}

		private static void doProShear(ProjectData projectData)
		{
			shear1(projectData, 1);
			//projectData.pictureBox.Refresh();
			shear(projectData, 2);
			//projectData.pictureBox.Refresh();
			shear(projectData, 3);
			projectData.angle -= projectData.angleDiff;
			projectData.pictureBox.Refresh();
			return;
		}

		private static (float x, float y) naiveRotationForPoint(float x, float y, float alfa)
		{
			float cos = (float)Math.Cos(alfa);
			float sin = (float)Math.Sin(alfa);
			float[,] invertedMatrix = new float[2, 2] { { cos, sin }, { -sin, cos } };
			float[] vector = new float[2] { x, y };
			float[] result = new float[2];

			// multiply invertedMatrix and vector
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					result[i] += invertedMatrix[i, j] * vector[j];
				}
			}

			return (result[0], result[1]);
		}

		private static (float x, float y) shearPixel(float x, float y, float alfa)
		{
			float tan = (float)Math.Tan(alfa / 2f);
			float sin = (float)Math.Sin(alfa);

			float new_X = x - y * tan;
			float new_Y = new_X * sin + y;
			new_X = new_X - new_Y * tan;

			return (new_X, new_Y);
		}

		private static (float x, float y) shearPixel1(float x, float y, float alfa)
		{
			float tan = (float)Math.Tan(alfa / 2f);
			float new_X = x - y * tan;
			return (new_X, y);
		}

		private static (float x, float y) shearPixel2(float x, float y, float alfa)
		{
			float sin = (float)Math.Sin(alfa);
			float new_Y = x * sin - y;
			return (x, new_Y);
		}

		private static (float x, float y) shearPixel3(float x, float y, float alfa)
		{
			float tan = (float)Math.Tan(alfa / 2f);
			float new_X = x - y * tan;
			return (new_X, y);
		}

		private static void Xshear1(ProjectData projectData, float shear, int width, int height)
		{
			// inicjalizacja nowe tekstury
			Bitmap texture = new Bitmap(projectData.textureTmp.Width, projectData.textureTmp.Height);
			Graphics textureGraphics = Graphics.FromImage(texture);
			using BmpPixelSnoop bmpPixelSnoop = new BmpPixelSnoop(texture);

			for (int y = 0; y < height; y++)
			{
				int ii = (int)Math.Floor(shear * (y + 0.5f));
				float ff = (shear * (y + 0.5f)) - (int)(shear * (y + 0.5f));
				Color prev_left = Color.Black;
				for (int x = 0; x < width; x++)
				{
					Color pixel = Color.Black;
					if (width -  x >= 0 && width - x < width && y >= 0 && y < height)
					{
						pixel = projectData.textureSnoopTmp.GetPixel(width - x, y);
					}					

					//Color left = ff * pixel;
					int leftR = (int)(pixel.R * ff);
					int leftG = (int)(pixel.G * ff);
					int leftB = (int)(pixel.B * ff);
					Color left = Color.FromArgb(leftR, leftG, leftB);

					//pixel = pixel – left + prev_left;
					int pixelR = pixel.R - left.R + prev_left.R;
					int pixelG = pixel.G - left.G + prev_left.G;
					int pixelB = pixel.B - left.B + prev_left.B;

					pixel = Color.FromArgb(pixelR, pixelG, pixelB);

					prev_left = left;

					if (width - x + ii >= 0 && width - x + ii < width && y >= 0 && y < height)
						bmpPixelSnoop.SetPixel(width - x + ii, y, pixel);
				}
				if (ii >= 0 && ii < width && y >= 0 && y < height)
					bmpPixelSnoop.SetPixel(ii, y, prev_left);
			}

			projectData.texture = texture;
			projectData.textureGraphics = textureGraphics;
			projectData.textureSnoop = bmpPixelSnoop;
		}

		private static void Xshear2(ProjectData projectData, float shear, int width, int height)
		{
			// inicjalizacja nowe tekstury
			Bitmap texture = new Bitmap(projectData.textureTmp.Width, projectData.textureTmp.Height);
			Graphics textureGraphics = Graphics.FromImage(texture);
			using BmpPixelSnoop bmpPixelSnoop = new BmpPixelSnoop(texture);

			for (int y = 0; y < height; y++)
			{
				int ii = (int)Math.Floor(shear * (y + 0.5f));
				float ff = (shear * (y + 0.5f)) - (int)(shear * (y + 0.5f));
				Color prev_left = Color.Black;
				for (int x = 0; x < width; x++)
				{
					Color pixel = Color.Black;
					if (width - x >= 0 && width - x < width && y >= 0 && y < height)
					{
						pixel = projectData.textureSnoop.GetPixel(width - x, y);
					}

					//Color left = ff * pixel;
					int leftR = (int)(pixel.R * ff);
					int leftG = (int)(pixel.G * ff);
					int leftB = (int)(pixel.B * ff);
					Color left = Color.FromArgb(leftR, leftG, leftB);

					//pixel = pixel – left + prev_left;
					int pixelR = pixel.R - left.R + prev_left.R;
					int pixelG = pixel.G - left.G + prev_left.G;
					int pixelB = pixel.B - left.B + prev_left.B;

					pixel = Color.FromArgb(pixelR, pixelG, pixelB);

					prev_left = left;

					if (width - x + ii >= 0 && width - x + ii < width && y >= 0 && y < height)
						bmpPixelSnoop.SetPixel(width - x + ii, y, pixel);
				}
				if (ii >= 0 && ii < width && y >= 0 && y < height)
					bmpPixelSnoop.SetPixel(ii, y, prev_left);
			}

			projectData.texture = texture;
			projectData.textureGraphics = textureGraphics;
			projectData.textureSnoop = bmpPixelSnoop;
		}

		private static void Yshear(ProjectData projectData, float shear, int width, int height)
		{

			// inicjalizacja nowe tekstury
			Bitmap texture = new Bitmap(projectData.textureTmp.Width, projectData.textureTmp.Height);
			Graphics textureGraphics = Graphics.FromImage(texture);
			using BmpPixelSnoop bmpPixelSnoop = new BmpPixelSnoop(texture);

			for (int x = 0; x < width; x++)
			{
				int ii = (int)Math.Floor(shear * (x + 0.5f));
				float ff = (shear * (x + 0.5f)) - (int)(shear * (x + 0.5f));
				Color prev_left = Color.Black;
				
				for (int y = 0; y < height; y++)
				{
					Color pixel = Color.Black;
					if (x >= 0 && x < width && height - y >= 0 && height - y < height)
					{
						pixel = projectData.textureSnoop.GetPixel(x, height - y);
					}

					//Color left = ff * pixel;
					int leftR = (int)(pixel.R * ff);
					int leftG = (int)(pixel.G * ff);
					int leftB = (int)(pixel.B * ff);
					Color left = Color.FromArgb(leftR, leftG, leftB);

					//pixel = pixel – left + prev_left;
					int pixelR = pixel.R - left.R + prev_left.R;
					int pixelG = pixel.G - left.G + prev_left.G;
					int pixelB = pixel.B - left.B + prev_left.B;

					pixel = Color.FromArgb(pixelR, pixelG, pixelB);

					prev_left = left;

					if (x >= 0 && x < width && height - y + ii >= 0 && height - y + ii < height)
						bmpPixelSnoop.SetPixel(x, height - y + ii, pixel);
				}
				if (x >= 0 && x < width && ii >= 0 && ii < height)
					bmpPixelSnoop.SetPixel(x, ii, prev_left);
			}

			projectData.texture = texture;
			projectData.textureGraphics = textureGraphics;
			projectData.textureSnoop = bmpPixelSnoop;
		}

		private static void shear(ProjectData projectData, int mode = 0)
		{
			//projectData.angle -= projectData.angleDiff;
			float angle = projectData.angle;

			// narysowanie nowej kanwy bez tekstury
			projectData.graphicsTmp.Clear(Color.White);
			Drawing.drawLines(projectData, tmp: true);
			Drawing.drawVertices(projectData, tmp: true);
			BezierCurve.drawBezierCurve(projectData, tmp: true);

			// inicjalizacja nowe tekstury
			projectData.textureTmp2 = new Bitmap(projectData.texture);

			//projectData.graphicsTmp.DrawImage(projectData.textureTmp2, 500, 500);
			//projectData.pictureBox.Refresh();

			projectData.textureGraphicsTmp2 = Graphics.FromImage(projectData.textureTmp2);
			using (var newTextureSnoopTmp2 = new BmpPixelSnoop(projectData.textureTmp2))
			{
				projectData.textureSnoopTmp2 = newTextureSnoopTmp2;
			}

			// inicjalizacja nowe tekstury
			projectData.texture = new Bitmap(projectData.textureTmp.Width, projectData.textureTmp.Height);
			projectData.textureGraphics = Graphics.FromImage(projectData.texture);
			using (var newTextureSnoop = new BmpPixelSnoop(projectData.texture))
			{
				projectData.textureSnoop = newTextureSnoop;
			}

			for (int j = 0; j < projectData.textureTmp.Height; ++j)
			{
				for (int i = 0; i < projectData.textureTmp.Width; ++i)
				{
					float newX = 0;
					float newY = 0;
					if (mode == 0)
					{
						(newX, newY) = shearPixel(-projectData.textureTmp.Width / 2f + i, -projectData.textureTmp.Height / 2f + j, angle);
					}
					else if (mode == 1)
					{
						(newX, newY) = shearPixel1(-projectData.textureTmp.Width / 2f + i, -projectData.textureTmp.Height / 2f + j, angle);
					}
					else if (mode == 2)
					{
						(newX, newY) = shearPixel2(-projectData.textureTmp.Width / 2f + i, -projectData.textureTmp.Height / 2f + j, angle);
					}
					else if (mode == 3)
					{
						(newX, newY) = shearPixel3(-projectData.textureTmp.Width / 2f + i, -projectData.textureTmp.Height / 2f + j, angle);
					}

					newX += projectData.textureTmp.Width / 2f;
					newY += projectData.textureTmp.Height / 2f;

					if (newX >= 0 && newX < projectData.textureTmp.Width - 1 && newY >= 0 && newY < projectData.textureTmp.Height - 1)
					{
						Color color = projectData.textureTmp2.GetPixel(i, j);
						projectData.textureSnoop.SetPixel((int)(newX + 0.5f), (int)(newY + 0.5f), color);
					}
				}
			}

			// nałożenie tekstury z szachownicą na pomocniczą kanwę
			Drawing.drawTexture(projectData, projectData.bezierLine[projectData.currentPosition], tmp: true, mode: 0);

			// zwiększenie kąta
			//projectData.angle -= projectData.angleDiff;

			// zamiana canvasów; bez znaczenia, bo i tak jest czyszczona, ale nie tworzę nowej chociaż
			(projectData.pictureBox.Image, projectData.bitmapTmp) = (projectData.bitmapTmp, (Bitmap)projectData.pictureBox.Image);
			(projectData.graphics, projectData.graphicsTmp) = (projectData.graphicsTmp, projectData.graphics);
			(projectData.bitmapSnoop, projectData.bitmapSnoopTmp) = (projectData.bitmapSnoopTmp, projectData.bitmapSnoop);

			//projectData.pictureBox.Refresh();
		}

		private static void shear1(ProjectData projectData, int mode = 0)
		{
			//projectData.angle -= projectData.angleDiff;
			float angle = projectData.angle;

			// narysowanie nowej kanwy bez tekstury
			projectData.graphicsTmp.Clear(Color.White);
			Drawing.drawLines(projectData, tmp: true);
			Drawing.drawVertices(projectData, tmp: true);
			BezierCurve.drawBezierCurve(projectData, tmp: true);

			//projectData.graphicsTmp.DrawImage(projectData.texture, 500, 500);

			// inicjalizacja nowe tekstury
			projectData.texture = new Bitmap(projectData.textureTmp.Width, projectData.textureTmp.Height);
			projectData.textureGraphics = Graphics.FromImage(projectData.texture);
			using (var newTextureSnoop = new BmpPixelSnoop(projectData.texture))
			{
				projectData.textureSnoop = newTextureSnoop;
			}

			
			for (int j = 0; j < projectData.textureTmp.Height; ++j)
			{
				for (int i = 0; i < projectData.textureTmp.Width; ++i)
				{
					float newX = 0;
					float newY = 0;
					if (mode == 0)
					{
						throw new Exception();
					}
					else if (mode == 1)
					{
						(newX, newY) = shearPixel1(-projectData.textureTmp.Width / 2f + i, -projectData.textureTmp.Height / 2f + j, angle);
					}
					else if (mode == 2)
					{
						throw new Exception();
					}
					else if (mode == 3)
					{
						throw new Exception();
					}

					newX += projectData.textureTmp.Width / 2f;
					newY += projectData.textureTmp.Height / 2f;

					if (newX >= 0 && newX < projectData.textureTmp.Width - 1 && newY >= 0 && newY < projectData.textureTmp.Height - 1)
					{
						Color color = projectData.textureSnoopTmp.GetPixel(i, j);
						projectData.textureSnoop.SetPixel((int)(newX + 0.5f), (int)(newY + 0.5f), color);
					}
				}
			}

			// nałożenie tekstury z szachownicą na pomocniczą kanwę
			Drawing.drawTexture(projectData, projectData.bezierLine[projectData.currentPosition], tmp: true);

			// zwiększenie kąta
			//projectData.angle -= projectData.angleDiff;

			// zamiana canvasów; bez znaczenia, bo i tak jest czyszczona, ale nie tworzę nowej chociaż
			(projectData.pictureBox.Image, projectData.bitmapTmp) = (projectData.bitmapTmp, (Bitmap)projectData.pictureBox.Image);
			(projectData.graphics, projectData.graphicsTmp) = (projectData.graphicsTmp, projectData.graphics);
			(projectData.bitmapSnoop, projectData.bitmapSnoopTmp) = (projectData.bitmapSnoopTmp, projectData.bitmapSnoop);

			//projectData.pictureBox.Refresh();
		}

		private static void moveAlongBezierLine(ProjectData projectData)
		{
			if (projectData.currentPosition < projectData.bezierLine.Count - 10)
			{
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
				++projectData.currentPosition;
			}
			else
			{
				projectData.currentPosition = 0;
			}
		}

		private static void onlyMoveAnimation(ProjectData projectData)
		{
			// narysowanie nowej kanwy bez tekstury
			projectData.graphicsTmp.Clear(Color.White);
			Drawing.drawLines(projectData, tmp: true);
			Drawing.drawVertices(projectData, tmp: true);
			BezierCurve.drawBezierCurve(projectData, tmp: true);

			// inicjalizacja nowe tekstury
			projectData.texture = new Bitmap(projectData.texture);
			projectData.textureGraphics = Graphics.FromImage(projectData.texture);
			using (var newTextureSnoop = new BmpPixelSnoop(projectData.texture))
			{
				projectData.textureSnoop = newTextureSnoop;
			}

			// nałożenie tekstury z szachownicą na pomocniczą kanwę
			Drawing.drawTexture(projectData, projectData.bezierLine[projectData.currentPosition], tmp: true);

			// zwiększenie kąta
			//projectData.angle += projectData.angleDiff;

			// zamiana canvasów; bez znaczenia, bo i tak jest czyszczona, ale nie tworzę nowej chociaż
			(projectData.pictureBox.Image, projectData.bitmapTmp) = (projectData.bitmapTmp, (Bitmap)projectData.pictureBox.Image);
			(projectData.graphics, projectData.graphicsTmp) = (projectData.graphicsTmp, projectData.graphics);
			(projectData.bitmapSnoop, projectData.bitmapSnoopTmp) = (projectData.bitmapSnoopTmp, projectData.bitmapSnoop);

			projectData.pictureBox.Refresh();
		}

	}
}
