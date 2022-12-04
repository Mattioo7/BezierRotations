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
		// bezier curve
		public int numberOfPoints { get; set; }
		public List<Vertex> points { get; set; }
		public List<Vector2> bezierLine { get; set; }

		
		// mouse
		public bool mouseDown { get; set; } = false;
		public Vertex? pressedVertex { get; set; }


		// canva
		public PictureBox pictureBox { get; set; }
		public Graphics graphics { get; set; }
		public BmpPixelSnoop bitmapSnoop { get; set; }

		// canvaTmp
		public Bitmap bitmapTmp { get; set; }
		public Graphics graphicsTmp { get; set; }
		public BmpPixelSnoop bitmapSnoopTmp { get; set; }


		// texture
		public Bitmap texture { get; set; }
		public Graphics textureGraphics { get; set; }
		public BmpPixelSnoop textureSnoop { get; set; }
		public List<(int x, int y)> textureTable { get; set; }

		// textureTmp
		public Bitmap textureTmp { get; set; }
		public Graphics textureGraphicsTmp { get; set; }
		public BmpPixelSnoop textureSnoopTmp { get; set; }


		// others
		public int RADIUS { get; set; } = 8;
		public Pen pen { get; set; }
		public float angle { get; set; } = -0.1f;
		public float angleDiff { get; set; } = -0.1f;


	}
}
