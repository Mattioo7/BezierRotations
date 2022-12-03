using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BezierRotations
{
	internal class ProjectData
	{
		public List<Vertex> points { get; set; }

		public bool mouseDown { get; set; } = false;

		public Vertex? pressedVertex { get; set; }

		public int RADIUS { get; set; } = 8;

		public Pen Pen { get; set; }

		public PictureBox PictureBox { get; set; }

		public Graphics graphics { get; set; }

		public int numberOfPoints { get; set; } = 10;

		public List<Vector2> bezierLine { get; set; }
		public Bitmap texture { get; set; }

		public BmpPixelSnoop textureSnoop { get; set; }
	}
}
