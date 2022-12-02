using System.Data;

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

		Drawing.generatePointsForBezierCurve(projectData);
        Drawing.drawBezierCurve(projectData);
    }

	private void pictureBox_workingArea_MouseDown(object sender, MouseEventArgs e)
	{
		projectData.pressedVertex = Vertex.findVertex(e, projectData.points);

		if (projectData.pressedVertex != null)
		{
			projectData.mouseDown = true;

			projectData.mousePosition.X = projectData.pressedVertex.X;
			projectData.mousePosition.Y = projectData.pressedVertex.Y;
		}
	}

	private void pictureBox_workingArea_MouseMove(object sender, MouseEventArgs e)
	{
		if (projectData.mouseDown == true && projectData.pressedVertex != null)
		{
			projectData.pressedVertex.X = e.X;
			projectData.pressedVertex.Y = e.Y;


			Drawing.reDraw();
		}
	}
}