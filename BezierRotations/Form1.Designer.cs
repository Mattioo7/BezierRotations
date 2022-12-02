namespace BezierRotations;

partial class form_bezierRotations
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.tableLayoutPanel_main = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel_right = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox_beziersCurve = new System.Windows.Forms.GroupBox();
			this.checkBox_visibleLine = new System.Windows.Forms.CheckBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button_loadLine = new System.Windows.Forms.Button();
			this.button_generate = new System.Windows.Forms.Button();
			this.textBox_numberOfPoints = new System.Windows.Forms.TextBox();
			this.label_numberOfPoints = new System.Windows.Forms.Label();
			this.groupBox_image = new System.Windows.Forms.GroupBox();
			this.checkBox_gray = new System.Windows.Forms.CheckBox();
			this.button_loadImage = new System.Windows.Forms.Button();
			this.pictureBox_image = new System.Windows.Forms.PictureBox();
			this.groupBox_rotationMode = new System.Windows.Forms.GroupBox();
			this.radioButton_filtering = new System.Windows.Forms.RadioButton();
			this.radioButton_naiveRotations = new System.Windows.Forms.RadioButton();
			this.groupBox_animation = new System.Windows.Forms.GroupBox();
			this.radioButton_moving = new System.Windows.Forms.RadioButton();
			this.radioButton_rotation = new System.Windows.Forms.RadioButton();
			this.button_stop = new System.Windows.Forms.Button();
			this.button_start = new System.Windows.Forms.Button();
			this.pictureBox_workingArea = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel_main.SuspendLayout();
			this.tableLayoutPanel_right.SuspendLayout();
			this.groupBox_beziersCurve.SuspendLayout();
			this.groupBox_image.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).BeginInit();
			this.groupBox_rotationMode.SuspendLayout();
			this.groupBox_animation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_workingArea)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel_main
			// 
			this.tableLayoutPanel_main.ColumnCount = 2;
			this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel_main.Controls.Add(this.tableLayoutPanel_right, 1, 0);
			this.tableLayoutPanel_main.Controls.Add(this.pictureBox_workingArea, 0, 0);
			this.tableLayoutPanel_main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel_main.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel_main.Name = "tableLayoutPanel_main";
			this.tableLayoutPanel_main.RowCount = 1;
			this.tableLayoutPanel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel_main.Size = new System.Drawing.Size(1184, 661);
			this.tableLayoutPanel_main.TabIndex = 0;
			// 
			// tableLayoutPanel_right
			// 
			this.tableLayoutPanel_right.ColumnCount = 1;
			this.tableLayoutPanel_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel_right.Controls.Add(this.groupBox_beziersCurve, 0, 0);
			this.tableLayoutPanel_right.Controls.Add(this.groupBox_image, 0, 1);
			this.tableLayoutPanel_right.Controls.Add(this.groupBox_rotationMode, 0, 2);
			this.tableLayoutPanel_right.Controls.Add(this.groupBox_animation, 0, 3);
			this.tableLayoutPanel_right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel_right.Location = new System.Drawing.Point(987, 3);
			this.tableLayoutPanel_right.Name = "tableLayoutPanel_right";
			this.tableLayoutPanel_right.RowCount = 4;
			this.tableLayoutPanel_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel_right.Size = new System.Drawing.Size(194, 655);
			this.tableLayoutPanel_right.TabIndex = 0;
			// 
			// groupBox_beziersCurve
			// 
			this.groupBox_beziersCurve.Controls.Add(this.checkBox_visibleLine);
			this.groupBox_beziersCurve.Controls.Add(this.button4);
			this.groupBox_beziersCurve.Controls.Add(this.button_loadLine);
			this.groupBox_beziersCurve.Controls.Add(this.button_generate);
			this.groupBox_beziersCurve.Controls.Add(this.textBox_numberOfPoints);
			this.groupBox_beziersCurve.Controls.Add(this.label_numberOfPoints);
			this.groupBox_beziersCurve.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox_beziersCurve.Location = new System.Drawing.Point(3, 3);
			this.groupBox_beziersCurve.Name = "groupBox_beziersCurve";
			this.groupBox_beziersCurve.Size = new System.Drawing.Size(188, 157);
			this.groupBox_beziersCurve.TabIndex = 0;
			this.groupBox_beziersCurve.TabStop = false;
			this.groupBox_beziersCurve.Text = "Beziers\'s curve";
			// 
			// checkBox_visibleLine
			// 
			this.checkBox_visibleLine.AutoSize = true;
			this.checkBox_visibleLine.Location = new System.Drawing.Point(6, 72);
			this.checkBox_visibleLine.Name = "checkBox_visibleLine";
			this.checkBox_visibleLine.Size = new System.Drawing.Size(105, 19);
			this.checkBox_visibleLine.TabIndex = 5;
			this.checkBox_visibleLine.Text = "Visible polyline";
			this.checkBox_visibleLine.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(107, 103);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 4;
			this.button4.Text = "Save";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button_loadLine
			// 
			this.button_loadLine.Location = new System.Drawing.Point(6, 103);
			this.button_loadLine.Name = "button_loadLine";
			this.button_loadLine.Size = new System.Drawing.Size(75, 23);
			this.button_loadLine.TabIndex = 3;
			this.button_loadLine.Text = "Load ";
			this.button_loadLine.UseVisualStyleBackColor = true;
			// 
			// button_generate
			// 
			this.button_generate.Location = new System.Drawing.Point(107, 37);
			this.button_generate.Name = "button_generate";
			this.button_generate.Size = new System.Drawing.Size(75, 23);
			this.button_generate.TabIndex = 2;
			this.button_generate.Text = "Generate";
			this.button_generate.UseVisualStyleBackColor = true;
			// 
			// textBox_numberOfPoints
			// 
			this.textBox_numberOfPoints.Location = new System.Drawing.Point(6, 37);
			this.textBox_numberOfPoints.Name = "textBox_numberOfPoints";
			this.textBox_numberOfPoints.Size = new System.Drawing.Size(94, 23);
			this.textBox_numberOfPoints.TabIndex = 1;
			// 
			// label_numberOfPoints
			// 
			this.label_numberOfPoints.AutoSize = true;
			this.label_numberOfPoints.Location = new System.Drawing.Point(6, 19);
			this.label_numberOfPoints.Name = "label_numberOfPoints";
			this.label_numberOfPoints.Size = new System.Drawing.Size(101, 15);
			this.label_numberOfPoints.TabIndex = 0;
			this.label_numberOfPoints.Text = "Number of points";
			// 
			// groupBox_image
			// 
			this.groupBox_image.Controls.Add(this.checkBox_gray);
			this.groupBox_image.Controls.Add(this.button_loadImage);
			this.groupBox_image.Controls.Add(this.pictureBox_image);
			this.groupBox_image.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox_image.Location = new System.Drawing.Point(3, 166);
			this.groupBox_image.Name = "groupBox_image";
			this.groupBox_image.Size = new System.Drawing.Size(188, 157);
			this.groupBox_image.TabIndex = 1;
			this.groupBox_image.TabStop = false;
			this.groupBox_image.Text = "Image";
			// 
			// checkBox_gray
			// 
			this.checkBox_gray.AutoSize = true;
			this.checkBox_gray.Location = new System.Drawing.Point(6, 70);
			this.checkBox_gray.Name = "checkBox_gray";
			this.checkBox_gray.Size = new System.Drawing.Size(85, 19);
			this.checkBox_gray.TabIndex = 2;
			this.checkBox_gray.Text = "Gray colors";
			this.checkBox_gray.UseVisualStyleBackColor = true;
			// 
			// button_loadImage
			// 
			this.button_loadImage.Location = new System.Drawing.Point(6, 39);
			this.button_loadImage.Name = "button_loadImage";
			this.button_loadImage.Size = new System.Drawing.Size(75, 23);
			this.button_loadImage.TabIndex = 1;
			this.button_loadImage.Text = "Load";
			this.button_loadImage.UseVisualStyleBackColor = true;
			// 
			// pictureBox_image
			// 
			this.pictureBox_image.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.pictureBox_image.Location = new System.Drawing.Point(132, 39);
			this.pictureBox_image.Name = "pictureBox_image";
			this.pictureBox_image.Size = new System.Drawing.Size(50, 50);
			this.pictureBox_image.TabIndex = 0;
			this.pictureBox_image.TabStop = false;
			// 
			// groupBox_rotationMode
			// 
			this.groupBox_rotationMode.Controls.Add(this.radioButton_filtering);
			this.groupBox_rotationMode.Controls.Add(this.radioButton_naiveRotations);
			this.groupBox_rotationMode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox_rotationMode.Location = new System.Drawing.Point(3, 329);
			this.groupBox_rotationMode.Name = "groupBox_rotationMode";
			this.groupBox_rotationMode.Size = new System.Drawing.Size(188, 157);
			this.groupBox_rotationMode.TabIndex = 2;
			this.groupBox_rotationMode.TabStop = false;
			this.groupBox_rotationMode.Text = "Rotation mode";
			// 
			// radioButton_filtering
			// 
			this.radioButton_filtering.AutoSize = true;
			this.radioButton_filtering.Location = new System.Drawing.Point(6, 81);
			this.radioButton_filtering.Name = "radioButton_filtering";
			this.radioButton_filtering.Size = new System.Drawing.Size(94, 19);
			this.radioButton_filtering.TabIndex = 1;
			this.radioButton_filtering.TabStop = true;
			this.radioButton_filtering.Text = "With filtering";
			this.radioButton_filtering.UseVisualStyleBackColor = true;
			// 
			// radioButton_naiveRotations
			// 
			this.radioButton_naiveRotations.AutoSize = true;
			this.radioButton_naiveRotations.Location = new System.Drawing.Point(6, 39);
			this.radioButton_naiveRotations.Name = "radioButton_naiveRotations";
			this.radioButton_naiveRotations.Size = new System.Drawing.Size(105, 19);
			this.radioButton_naiveRotations.TabIndex = 0;
			this.radioButton_naiveRotations.TabStop = true;
			this.radioButton_naiveRotations.Text = "Naive rotations";
			this.radioButton_naiveRotations.UseVisualStyleBackColor = true;
			// 
			// groupBox_animation
			// 
			this.groupBox_animation.Controls.Add(this.radioButton_moving);
			this.groupBox_animation.Controls.Add(this.radioButton_rotation);
			this.groupBox_animation.Controls.Add(this.button_stop);
			this.groupBox_animation.Controls.Add(this.button_start);
			this.groupBox_animation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox_animation.Location = new System.Drawing.Point(3, 492);
			this.groupBox_animation.Name = "groupBox_animation";
			this.groupBox_animation.Size = new System.Drawing.Size(188, 160);
			this.groupBox_animation.TabIndex = 3;
			this.groupBox_animation.TabStop = false;
			this.groupBox_animation.Text = "Animation";
			// 
			// radioButton_moving
			// 
			this.radioButton_moving.AutoSize = true;
			this.radioButton_moving.Location = new System.Drawing.Point(6, 65);
			this.radioButton_moving.Name = "radioButton_moving";
			this.radioButton_moving.Size = new System.Drawing.Size(135, 19);
			this.radioButton_moving.TabIndex = 3;
			this.radioButton_moving.TabStop = true;
			this.radioButton_moving.Text = "Moving on the curve";
			this.radioButton_moving.UseVisualStyleBackColor = true;
			// 
			// radioButton_rotation
			// 
			this.radioButton_rotation.AutoSize = true;
			this.radioButton_rotation.Location = new System.Drawing.Point(6, 31);
			this.radioButton_rotation.Name = "radioButton_rotation";
			this.radioButton_rotation.Size = new System.Drawing.Size(70, 19);
			this.radioButton_rotation.TabIndex = 2;
			this.radioButton_rotation.TabStop = true;
			this.radioButton_rotation.Text = "Rotation";
			this.radioButton_rotation.UseVisualStyleBackColor = true;
			// 
			// button_stop
			// 
			this.button_stop.Location = new System.Drawing.Point(107, 106);
			this.button_stop.Name = "button_stop";
			this.button_stop.Size = new System.Drawing.Size(75, 23);
			this.button_stop.TabIndex = 1;
			this.button_stop.Text = "Stop";
			this.button_stop.UseVisualStyleBackColor = true;
			// 
			// button_start
			// 
			this.button_start.Location = new System.Drawing.Point(6, 106);
			this.button_start.Name = "button_start";
			this.button_start.Size = new System.Drawing.Size(75, 23);
			this.button_start.TabIndex = 0;
			this.button_start.Text = "Start";
			this.button_start.UseVisualStyleBackColor = true;
			// 
			// pictureBox_workingArea
			// 
			this.pictureBox_workingArea.BackColor = System.Drawing.Color.White;
			this.pictureBox_workingArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox_workingArea.Location = new System.Drawing.Point(3, 3);
			this.pictureBox_workingArea.Name = "pictureBox_workingArea";
			this.pictureBox_workingArea.Size = new System.Drawing.Size(978, 655);
			this.pictureBox_workingArea.TabIndex = 1;
			this.pictureBox_workingArea.TabStop = false;
			this.pictureBox_workingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_workingArea_MouseDown);
			this.pictureBox_workingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_workingArea_MouseMove);
			// 
			// form_bezierRotations
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1184, 661);
			this.Controls.Add(this.tableLayoutPanel_main);
			this.Name = "form_bezierRotations";
			this.Text = "Bezier Rotations";
			this.tableLayoutPanel_main.ResumeLayout(false);
			this.tableLayoutPanel_right.ResumeLayout(false);
			this.groupBox_beziersCurve.ResumeLayout(false);
			this.groupBox_beziersCurve.PerformLayout();
			this.groupBox_image.ResumeLayout(false);
			this.groupBox_image.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_image)).EndInit();
			this.groupBox_rotationMode.ResumeLayout(false);
			this.groupBox_rotationMode.PerformLayout();
			this.groupBox_animation.ResumeLayout(false);
			this.groupBox_animation.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_workingArea)).EndInit();
			this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel tableLayoutPanel_main;
    private TableLayoutPanel tableLayoutPanel_right;
    private GroupBox groupBox_beziersCurve;
    private GroupBox groupBox_image;
    private PictureBox pictureBox_image;
    private GroupBox groupBox_rotationMode;
    private RadioButton radioButton_filtering;
    private RadioButton radioButton_naiveRotations;
    private GroupBox groupBox_animation;
    private RadioButton radioButton_moving;
    private RadioButton radioButton_rotation;
    private Button button_stop;
    private Button button_start;
    private PictureBox pictureBox_workingArea;
	private CheckBox checkBox_visibleLine;
	private Button button4;
	private Button button_loadLine;
	private Button button_generate;
	private TextBox textBox_numberOfPoints;
	private Label label_numberOfPoints;
	private Button button_loadImage;
	private CheckBox checkBox_gray;
}