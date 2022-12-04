using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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


		// points
		projectData.controlPoints = Drawing.generatePointsForBezierCurve(projectData);

		// drawing bezier segments
		Drawing.drawLines(projectData);
		Drawing.drawVertices(projectData);
		
		// bezierLine
		List<Vector2> controlPointsAsVectors = Vertex.ToVector2List(projectData.controlPoints);
		projectData.bezierLine = BezierCurve.PointList2(controlPointsAsVectors);
		projectData.bezierLineDerivatives = BezierCurve.calculateDerivativeForBezierCurve(controlPointsAsVectors);
		BezierCurve.drawBezierCurve(projectData);

		// default texture
		/*string path = Path.Combine(Environment.CurrentDirectory, @"Props\", "board.jpg");
		Bitmap texture = new Bitmap(path);

		projectData.texture = new Bitmap(151, 151);
		Graphics gTexture = Graphics.FromImage(projectData.texture);
		projectData.textureGraphics = gTexture;
		
		this.pictureBox_image.Image = new Bitmap(texture, pictureBox_image.Width, pictureBox_image.Height);
		texture = new Bitmap(texture, 99, 99);
		projectData.textureGraphics.DrawImage(texture, (projectData.texture.Width - texture.Width) / 2, (projectData.texture.Height - texture.Height) / 2, texture.Width, texture.Height);

		using (var textureSnoop = new BmpPixelSnoop(projectData.texture))
		{
			projectData.textureSnoop = textureSnoop;
		}

		// inicjalizacja pomocniczej tekstury
		projectData.textureTmp = new Bitmap(projectData.texture);
		projectData.textureGraphicsTmp = Graphics.FromImage(projectData.textureTmp);
		using (var newTextureSnoop = new BmpPixelSnoop(projectData.textureTmp))
		{
			projectData.textureSnoopTmp = newTextureSnoop;
		}*/
		doLoadImage(newImage: false);

		// draw texture
		//projectData.graphics.DrawImage(projectData.texture, projectData.bezierLine[projectData.currentPosition]);
		Drawing.drawTexture(projectData, projectData.bezierLine[projectData.currentPosition]);

		// animation setup
		timer.Tick += new EventHandler((sender, e) => Animation.doAnimation(sender, e, projectData));
		timer.Interval = 10;
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

	private void radioButton_rotation_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton_rotation.Checked == true)
		{
			projectData.animationType = Animation.AnimationType.Rotation;
		}
	}

	private void radioButton_moving_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton_moving.Checked == true)
		{
			projectData.animationType = Animation.AnimationType.Moving;
		}
	}

	private void radioButton_onlyMoving_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton_onlyMoving.Checked == true)
		{
			projectData.animationType = Animation.AnimationType.OnlyMoving;
		}
	}

	private void radioButton_naiveRotations_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton_naiveRotations.Checked == true)
		{
			projectData.rotationType = Animation.RotationType.Naive;
		}
	}

	private void radioButton_filtering_CheckedChanged(object sender, EventArgs e)
	{
		if (radioButton_filtering.Checked == true)
		{
			projectData.rotationType = Animation.RotationType.Shear;
		}
	}

	private void button_start_Click(object sender, EventArgs e)
	{
		timer.Start();
	}

	private void button_stop_Click(object sender, EventArgs e)
	{
		timer.Stop();
	}

	private void button_generate_Click(object sender, EventArgs e)
	{
		int numberOfPoints = this.textBox_numberOfPoints.Text == "" ? projectData.numberOfPoints : int.Parse(this.textBox_numberOfPoints.Text);
		projectData.numberOfPoints = numberOfPoints;

		run();
	}

	private void checkBox_visibleLine_CheckedChanged(object sender, EventArgs e)
	{
		projectData.visiblePolyline = checkBox_visibleLine.Checked;
		Drawing.reDraw(projectData);
	}

	private void button_loadImage_Click(object sender, EventArgs e)
	{
		doLoadImage(newImage: true);
		Drawing.reDraw(projectData);
	}

	private void doLoadImage(bool newImage = false)
	{
		if (newImage == true)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
			openFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, @"Props\");
			openFileDialog.Title = "Select an image file";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				projectData.path = openFileDialog.FileName;
			}
		}
		
		Bitmap texture = new Bitmap(projectData.path);

		projectData.texture = new Bitmap(151, 151);
		Graphics gTexture = Graphics.FromImage(projectData.texture);
		projectData.textureGraphics = gTexture;

		this.pictureBox_image.Image = new Bitmap(texture, pictureBox_image.Width, pictureBox_image.Height);
		texture = new Bitmap(texture, 99, 99);
		projectData.textureGraphics.DrawImage(texture, (projectData.texture.Width - texture.Width) / 2, (projectData.texture.Height - texture.Height) / 2, texture.Width, texture.Height);

		using (var textureSnoop = new BmpPixelSnoop(projectData.texture))
		{
			projectData.textureSnoop = textureSnoop;
		}

		// inicjalizacja pomocniczej tekstury
		projectData.textureTmp = new Bitmap(projectData.texture);
		projectData.textureGraphicsTmp = Graphics.FromImage(projectData.textureTmp);
		using (var newTextureSnoop = new BmpPixelSnoop(projectData.textureTmp))
		{
			projectData.textureSnoopTmp = newTextureSnoop;
		}

		this.checkBox_gray.Checked = false;
	}

	private void button_loadLine_Click(object sender, EventArgs e)
	{
		using (OpenFileDialog openDialog = new OpenFileDialog())
		{
			List<Vertex> controlPoints = new List<Vertex>();

			openDialog.Filter = "Bezier line files (*.bezier)|*.bezier";
			if (openDialog.ShowDialog() == DialogResult.OK)
			{	
				using (StreamReader stream = new StreamReader(openDialog.OpenFile()))
				{
					try
					{
						while (!stream.EndOfStream)
						{
							var line1 = stream.ReadLine();
							var line2 = stream.ReadLine();
							controlPoints.Add(new Vertex(int.Parse(line1), int.Parse(line2)));
						}

						projectData.controlPoints = controlPoints;
						Drawing.reDraw(projectData);
					}
					catch
					{
						MessageBox.Show("Error load");
						return;
					}
				}
			}
		}
	}

	private void button_saveLine_Click(object sender, EventArgs e)
	{
		using (SaveFileDialog safeDialog = new SaveFileDialog())
		{
			safeDialog.Filter = "Bezier line files (*.bezier)|*.bezier";
			if (safeDialog.ShowDialog() == DialogResult.OK)
			{
				var filename = safeDialog.FileName;

				using (StreamWriter sw = File.CreateText(filename))
				{
					foreach (Vertex v in projectData.controlPoints)
					{
						sw.WriteLine(v.point.X.ToString());
						sw.WriteLine(v.point.Y.ToString());
					}
				}

				MessageBox.Show(text: "File saved", caption: "Save file");
			}
		}
	}

	private void button_grayScale_Click(object sender, EventArgs e)
	{
		Drawing.convertToGrayScale(projectData);
	}

	private void checkBox_gray_CheckedChanged(object sender, EventArgs e)
	{
		if (this.checkBox_gray.Checked)
		{
			Drawing.convertToGrayScale(projectData);
		}
		else
		{
			doLoadImage(newImage: false);
		}
	}
}
