using System.Data;
using System.Diagnostics;
using System.Numerics;

namespace BezierRotations;

public partial class form_bezierRotations : Form
{
	ProjectData projectData { get; set; } = new ProjectData();

	public form_bezierRotations()
    {
        InitializeComponent();

        run();
    }

    private void run()
    {
		Bitmap bitmap = new Bitmap(this.pictureBox_workingArea.Size.Width, this.pictureBox_workingArea.Size.Height);
		this.pictureBox_workingArea.Image = bitmap;
		Pen pen = new Pen(BackColor);
        Graphics g = Graphics.FromImage(bitmap);

		projectData.PictureBox = this.pictureBox_workingArea;
		projectData.Pen = pen;
		projectData.graphics = g;
		projectData.numberOfPoints = 10;

		Drawing.generatePointsForBezierCurve(projectData);

		Drawing.drawVertices(projectData);
		Drawing.drawLines(projectData);
		List<Vector2> vector2s = Vertex.ToVector2List(projectData.points);
		projectData.bezierLine = BezierCurve.PointList2(vector2s);
		BezierCurve.drawBezierCurve(projectData);
		//Drawing.drawBezierCurve(projectData);

		// default texture
		string path = Path.Combine(Environment.CurrentDirectory, @"Props\", "board.jpg");
		projectData.texture = new Bitmap(path);
		projectData.texture = new Bitmap(projectData.texture, this.pictureBox_workingArea.Width, this.pictureBox_workingArea.Height);

		using (var textureSnoop = new BmpPixelSnoop(projectData.texture))
		{
			projectData.textureSnoop = textureSnoop;
		}
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
}