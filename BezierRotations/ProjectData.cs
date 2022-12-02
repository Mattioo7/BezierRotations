using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierRotations
{
	internal class ProjectData
	{
		public List<Vertex> points { get; set; }
		public bool mouseDown { get; set; } = false;
		public Vertex mousePosition { get; set; }
		public Vertex pressedVertex { get; set; }
		public int RADIUS = 4;
		public Pen Pen { get; set; }
		public PictureBox PictureBox { get; set; }
		public Graphics graphics { get; set; }
		public int numberOfPoints { get; set; }
	}
}
