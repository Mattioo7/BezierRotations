using System.Data;
using System.Diagnostics;
using System.Numerics;
using Timer = System.Windows.Forms.Timer;

namespace BezierRotations;

public partial class form_bezierRotations : Form
{
	ProjectData projectData { get; set; } = new ProjectData();
	Timer timer = new Timer();

	public form_bezierRotations()
    {
        InitializeComponent();

        run();
    }

	public void run()
    {
		// pictureBox
		Bitmap bitmap = new Bitmap(this.pictureBox_workingArea.Size.Width, this.pictureBox_workingArea.Size.Height);
		this.pictureBox_workingArea.Image = bitmap;
		projectData.pictureBox = this.pictureBox_workingArea;
		using (var bitmapSnoop = new BmpPixelSnoop((Bitmap)projectData.pictureBox.Image))
		{
			projectData.bitmapSnoop = bitmapSnoop;
		}
		
		// bitmapTmp
		Bitmap bitmapTmp = new Bitmap(this.pictureBox_workingArea.Size.Width, this.pictureBox_workingArea.Size.Height);
		projectData.bitmapTmp = bitmapTmp;
		using (var bitmapSnoopTmp = new BmpPixelSnoop((Bitmap)projectData.bitmapTmp))
		{
			projectData.bitmapSnoopTmp = bitmapSnoopTmp;
		}

		// pen
		Pen pen = new Pen(BackColor);
		projectData.pen = pen;

		// graphics
        Graphics g = Graphics.FromImage(bitmap);
		projectData.graphics = g;
        Graphics gTmp = Graphics.FromImage(bitmapTmp);
		projectData.graphicsTmp = gTmp;

		
		// numberOfPoints
		projectData.numberOfPoints = 6;

		// points
		projectData.points = Drawing.generatePointsForBezierCurve(projectData);

		// drawing bezier segments
		Drawing.drawLines(projectData);
		Drawing.drawVertices(projectData);
		
		// bezierLine
		List<Vector2> vector2s = Vertex.ToVector2List(projectData.points);
		projectData.bezierLine = BezierCurve.PointList2(vector2s);
		BezierCurve.drawBezierCurve(projectData);

		// default texture
		string path = Path.Combine(Environment.CurrentDirectory, @"Props\", "board.jpg");
		projectData.texture = new Bitmap(151, 151);
		Graphics gTexture = Graphics.FromImage(projectData.texture);
		projectData.textureGraphics = gTexture;


		Bitmap texture = new Bitmap(path);
		texture = new Bitmap(texture, 99, 99);
		projectData.textureGraphics.DrawImage(texture, (projectData.texture.Width - texture.Width) / 2, (projectData.texture.Height - texture.Height) / 2, texture.Width, texture.Height);

		projectData.textureTable = new List<(int x, int y)>();

		using (var textureSnoop = new BmpPixelSnoop(projectData.texture))
		{
			projectData.textureSnoop = textureSnoop;
		}

		// draw texture
		projectData.graphics.DrawImage(projectData.texture, 500, 100);

		// animation setup
		timer.Tick += new EventHandler((sender, e) => Animation.naiveRotationAnimation(sender, e, projectData));
		timer.Interval = 1000;
		timer.Start();
		timer.Enabled = true;
	}

	private void pictureBox_workingArea_MouseDown(object sender, MouseEventArgs e)
	{
		projectData.pressedVertex = Vertex.findVertex(e, projectData);

		if (projectData.pressedVertex != null)
		{
			projectData.mouseDown = true;

			Debug.WriteLine("Vertex position: {0}, {1}", projectData.pressedVertex.X, projectData.pressedVertex.Y);
		}
		else
		{
			Debug.WriteLine("Miss");
		}
	}

	private void pictureBox_workingArea_MouseMove(object sender, MouseEventArgs e)
	{
		if (projectData.mouseDown == true && projectData.pressedVertex != null)
		{
			/*projectData.pressedVertex.X = e.X;
			projectData.pressedVertex.Y = e.Y;*/

			projectData.pressedVertex.pointF = new PointF(e.X, e.Y);


			Debug.WriteLine("Mouse position: {0}, {1}", e.X, e.Y);
			Debug.WriteLine("Vertex position: {0}, {1}", projectData.pressedVertex.X, projectData.pressedVertex.Y);

			Drawing.reDraw(projectData);
		}
	}

	private void pictureBox_workingArea_MouseUp(object sender, MouseEventArgs e)
	{
		if (projectData.mouseDown == true)
		{
			Drawing.reDraw(projectData);
		}

		projectData.mouseDown = false;
	}

	private void button_redraw_Click(object sender, EventArgs e)
	{
		Animation.naiveRotateMatrix(projectData);
		Debug.WriteLine("Redraw");
	}
}