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
		public PointF pointF { get; set; }
		public Brush brush;

		public Vertex()
		{
			this.pointF = new PointF(0, 0);
			brush = Brushes.Black;
		}

		public Vertex(PointF point)
		{
			pointF = point;
			brush = Brushes.Black;
		}

		public Vertex(PointF point, Brush br)
		{
			pointF = point;
			brush = br;
		}
		
		public Vertex(float x, float y)
		{
			pointF = new PointF(x, y);
			brush = Brushes.Black;
		}

		public Vertex(float x, float y, Brush br)
		{
			pointF = new PointF(x, y);
			brush = br;
		}

		public float X
		{
			set
			{
				this.pointF = new PointF(X, pointF.Y);
			}

			get
			{
				return this.pointF.X;
			}
		}

		public float Y
		{
			set
			{
				this.pointF = new PointF(pointF.X, Y);
			}

			get
			{
				return this.pointF.Y;
			}
		}

		public PointF point => this.pointF;

		public static PointF[] ToPointFArray(List<Vertex> vertices)
		{
			List<PointF> result = new List<PointF>();
			result.AddRange(vertices.Select(vertex => vertex.pointF));
			return result.ToArray();
		}

		public static List<Vector2> ToVector2List(List<Vertex> vertices)
		{
			List<Vector2> result = new List<Vector2>();
			result.AddRange(vertices.Select(vertex => new Vector2(vertex.X, vertex.Y)));
			return result;
		}

		public static Vertex? findVertex(MouseEventArgs e, ProjectData projectData)
		{
			int RADIUS = 4;

			foreach (Vertex v in projectData.controlPoints)
			{
				int yDiff = (int)Math.Abs(v.Y - e.Y);
				int xDiff = (int)Math.Abs(v.X - e.X);

				if (yDiff * yDiff + xDiff * xDiff < 4 * (RADIUS + 1) * (RADIUS + 1))
				{
					return v;
				}
			}

			return null;
		}
	}
}
