namespace BezierRotations;

public partial class form_bezierRotations : Form
{
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

		Drawing drawing = new Drawing(pen, this.pictureBox_workingArea, g);

        drawing.drawBezierCurve(10);
    }

    private void pictureBox_workingArea_MouseClick(object sender, MouseEventArgs e)
    {

    }
}