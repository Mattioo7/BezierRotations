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
		public int numberOfPoints { get; set; } = 5;
		public List<Vertex> controlPoints { get; set; }
		public List<Vector2> bezierLine { get; set; }
		public List<Vector2> bezierLineDerivatives { get; set; }
		public int currentPosition { get; set; } = 0;
		public bool visiblePolyline { get; set; } = true;


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
		public string path { get; set; } = Path.Combine(Environment.CurrentDirectory, @"Props\", "board.jpg");
		public Bitmap texture { get; set; }
		public Graphics textureGraphics { get; set; }
		public BmpPixelSnoop textureSnoop { get; set; }

		// textureTmp
		public Bitmap textureTmp { get; set; }
		public Graphics textureGraphicsTmp { get; set; }
		public BmpPixelSnoop textureSnoopTmp { get; set; }

		// textureTmp
		public Bitmap textureTmp2 { get; set; }
		public Graphics textureGraphicsTmp2 { get; set; }
		public BmpPixelSnoop textureSnoopTmp2 { get; set; }

		// GUI handlers
		public Animation.AnimationType animationType { get; set; }
		public Animation.RotationType rotationType { get; set; }
		

		// others
		public int RADIUS { get; set; } = 8;
		public Pen pen { get; set; }
		public float angle { get; set; } = -0.1f;
		public float angleDiff { get; set; } = -0.1f;

	}
}
