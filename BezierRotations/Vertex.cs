using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.DataFormats;

namespace BezierRotations
{
	internal class Vertex
	{
		public PointF PointF { get; set; }
		public Brush brush;

		public Vertex()
		{
			this.PointF = new PointF(0, 0);
			brush = Brushes.Black;
		}

		public Vertex(PointF point)
		{
			PointF = point;
			brush = Brushes.Black;
		}

		public Vertex(PointF point, Brush br)
		{
			PointF = point;
			brush = br;
		}
		
		public Vertex(float x, float y)
		{
			PointF = new PointF(x, y);
			brush = Brushes.Black;
		}

		public Vertex(float x, float y, Brush br)
		{
			PointF = new PointF(x, y);
			brush = br;
		}

		public float X
		{
			set
			{
				this.PointF = new PointF(X, PointF.Y);
			}

			get
			{
				return this.PointF.X;
			}
		}

		public float Y
		{
			set
			{
				this.PointF = new PointF(PointF.X, Y);
			}

			get
			{
				return this.PointF.Y;
			}
		}

		public PointF point => this.PointF;
		public PointF pointF => this.PointF;

		public static PointF[] ToPointFArray(List<Vertex> vertices)
		{
			List<PointF> result = new List<PointF>();
			result.AddRange(vertices.Select(vertex => vertex.PointF));
			return result.ToArray();
		}

		public static Vertex? findVertex(MouseEventArgs e, List<Vertex> vertices)
		{
			Vertex? foundVertex = null;
			int RADIUS = 4;

			foreach (Vertex v in vertices)
			{
				int yDiff = (int)Math.Abs(v.Y - e.Y);
				int xDiff = (int)Math.Abs(v.X - e.X);

				if (yDiff * yDiff + xDiff * xDiff < 4 * (RADIUS + 1) * (RADIUS + 1))
				{
					foundVertex = v;
					break;
				}
			}

			return foundVertex;
		}
	}
}
