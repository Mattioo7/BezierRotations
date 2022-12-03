using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BezierRotations
{
	internal static class Animation
	{
		public static void calculateRotations(ProjectData projectData)
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
		}

		public static void naiveRotateMatrix(ProjectData projectData, float angle)
		{
			BmpPixelSnoop newTextureSnoop;// = new BmpPixelSnoop(projectData.textureSnoop);

			for (int i = 0; i < projectData.textureSnoop.Width; i++)
			{
				for (int j = 0; j < projectData.textureSnoop.Height; j++)
				{
					Color color = projectData.textureSnoop.GetPixel(i, j);
					if (color.A != 0)
					{
						Vector2 point = new Vector2(i, j);
						//Vector2 rotatedPoint = naiveRotationForPoint(point.X, point.Y, angle);
						//projectData.textureSnoop.SetPixel((int)rotatedPoint.X, (int)rotatedPoint.Y, color);
					}
				}
			}
		}

		public static (float x, float y) naiveRotationForPoint(float x, float y, float alfa)
		{
			float cos = (float)Math.Cos(alfa);
			float sin = (float)Math.Sin(alfa);
			float[,] invertedMatrix = new float[2, 2] { { cos, sin }, { -sin, cos } };
			float[] vector = new float[2] { x, y };
			float[] result = new float[2];

			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						result[k] = invertedMatrix[i, j] * vector[j];
					}
				}
			}

			return (result[0], result[1]);
		}

		public static void Xshear(ProjectData projectData, float shear, int width, int height)
		{
			for (int y = 0; y < height; y++)
			{
				int ii = (int)Math.Floor(shear * (y + 0.5f));
				float ff = (shear * (y + 0.5f)) - (int)(shear * (y + 0.5f));
				Color prev_left = Color.Black;
				for (int x = 0; x < width; x++)
				{
					Color pixel = projectData.textureSnoop.GetPixel(width - x, y); // ??

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

					projectData.textureSnoop.SetPixel(width - x + ii, y, pixel);
				}
				projectData.textureSnoop.SetPixel(ii, y, prev_left);
			}
		}





	}
}
