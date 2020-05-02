using System.Drawing;

namespace SnakeAI.WinForms
{
	partial class GameCanvasForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameCanvasForm));
			this.GameTimer = new System.Windows.Forms.Timer(this.components);
			this.Canvas = new System.Windows.Forms.PictureBox();
			this.Background = new System.Windows.Forms.PictureBox();
			this.graph = new System.Windows.Forms.Button();
			this.load = new System.Windows.Forms.Button();
			this.save = new System.Windows.Forms.Button();
			this.IncreaseBut = new System.Windows.Forms.Button();
			this.DecreaseBut = new System.Windows.Forms.Button();
			this.RedLabel = new System.Windows.Forms.Label();
			this.BlueLabel = new System.Windows.Forms.Label();
			this.GenLabel = new System.Windows.Forms.Label();
			this.ScoreLabel = new System.Windows.Forms.Label();
			this.HighscoreLabel = new System.Windows.Forms.Label();
			this.MutationLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Background)).BeginInit();
			this.SuspendLayout();
			// 
			// Canvas
			// 
			this.Canvas.BackColor = System.Drawing.Color.Black;
			this.Canvas.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.Canvas.InitialImage = null;
			this.Canvas.Location = new System.Drawing.Point(440, 40);
			this.Canvas.Margin = new System.Windows.Forms.Padding(4);
			this.Canvas.Name = "Canvas";
			this.Canvas.Size = new System.Drawing.Size(760, 760);
			this.Canvas.TabIndex = 0;
			this.Canvas.TabStop = false;
			this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
			// 
			// Background
			// 
			this.Background.BackColor = System.Drawing.Color.Black;
			this.Background.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Background.Location = new System.Drawing.Point(0, 0);
			this.Background.Name = "Background";
			this.Background.Size = new System.Drawing.Size(1200, 800);
			this.Background.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.Background.TabIndex = 0;
			this.Background.TabStop = false;
			this.Background.Paint += new System.Windows.Forms.PaintEventHandler(this.Graph_Paint);
			// 
			// ScoreLabel
			// 
			this.ScoreLabel.AutoSize = true;
			this.ScoreLabel.BackColor = System.Drawing.Color.Transparent;
			this.ScoreLabel.Font = new System.Drawing.Font("Agency FB", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.ScoreLabel.ForeColor = System.Drawing.Color.Silver;
			this.ScoreLabel.Location = new System.Drawing.Point(120, 725);
			this.ScoreLabel.Margin = new System.Windows.Forms.Padding(0);
			this.ScoreLabel.Name = "ScoreLabel";
			this.ScoreLabel.Size = new System.Drawing.Size(78, 24);
			this.ScoreLabel.TabIndex = 0;
			this.ScoreLabel.Text = "SCORE: {0}";
			// 
			// graph
			// 
			this.graph.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.graph.FlatAppearance.BorderSize = 0;
			this.graph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.graph.Font = new System.Drawing.Font("Agency FB", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.graph.Location = new System.Drawing.Point(349, 15);
			this.graph.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.graph.Name = "graph";
			this.graph.Size = new System.Drawing.Size(100, 30);
			this.graph.TabIndex = 0;
			this.graph.TabStop = false;
			this.graph.Text = "Graph";
			this.graph.UseVisualStyleBackColor = true;
			// 
			// load
			// 
			this.load.BackColor = System.Drawing.Color.Transparent;
			this.load.FlatAppearance.BorderSize = 0;
			this.load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.load.Font = new System.Drawing.Font("Agency FB", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.load.Location = new System.Drawing.Point(248, 15);
			this.load.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.load.Name = "load";
			this.load.Size = new System.Drawing.Size(100, 30);
			this.load.TabIndex = 0;
			this.load.TabStop = false;
			this.load.Text = "Load";
			this.load.UseVisualStyleBackColor = false;
			// 
			// save
			// 
			this.save.FlatAppearance.BorderSize = 0;
			this.save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.save.Font = new System.Drawing.Font("Agency FB", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.save.Location = new System.Drawing.Point(147, 15);
			this.save.Margin = new System.Windows.Forms.Padding(0);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(100, 30);
			this.save.TabIndex = 0;
			this.save.TabStop = false;
			this.save.Text = "Save";
			this.save.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.save.UseVisualStyleBackColor = true;
			// 
			// IncreaseBut
			// 
			this.IncreaseBut.FlatAppearance.BorderSize = 0;
			this.IncreaseBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.IncreaseBut.Font = new System.Drawing.Font("Agency FB", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.IncreaseBut.Location = new System.Drawing.Point(324, 96);
			this.IncreaseBut.Margin = new System.Windows.Forms.Padding(0);
			this.IncreaseBut.Name = "IncreaseBut";
			this.IncreaseBut.Size = new System.Drawing.Size(20, 20);
			this.IncreaseBut.TabIndex = 0;
			this.IncreaseBut.TabStop = false;
			this.IncreaseBut.Text = "+";
			this.IncreaseBut.UseVisualStyleBackColor = true;
			// 
			// DecreaseBut
			// 
			this.DecreaseBut.FlatAppearance.BorderSize = 0;
			this.DecreaseBut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DecreaseBut.Font = new System.Drawing.Font("Agency FB", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DecreaseBut.Location = new System.Drawing.Point(349, 96);
			this.DecreaseBut.Margin = new System.Windows.Forms.Padding(0);
			this.DecreaseBut.Name = "DecreaseBut";
			this.DecreaseBut.Size = new System.Drawing.Size(20, 20);
			this.DecreaseBut.TabIndex = 0;
			this.DecreaseBut.TabStop = false;
			this.DecreaseBut.Text = "-";
			this.DecreaseBut.UseVisualStyleBackColor = true;
			// 
			// HighscoreLabel
			// 
			this.HighscoreLabel.AutoSize = true;
			this.HighscoreLabel.BackColor = System.Drawing.Color.Transparent;
			this.HighscoreLabel.Font = new System.Drawing.Font("Agency FB", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.HighscoreLabel.ForeColor = System.Drawing.Color.Silver;
			this.HighscoreLabel.Location = new System.Drawing.Point(120, 755);
			this.HighscoreLabel.Name = "HighscoreLabel";
			this.HighscoreLabel.Size = new System.Drawing.Size(115, 24);
			this.HighscoreLabel.TabIndex = 0;
			this.HighscoreLabel.Text = "HIGHSCORE: {0}";
			this.HighscoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RedLabel
			// 
			this.RedLabel.AutoSize = true;
			this.RedLabel.BackColor = System.Drawing.Color.Transparent;
			this.RedLabel.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.RedLabel.ForeColor = System.Drawing.Color.Red;
			this.RedLabel.Location = new System.Drawing.Point(120, 685);
			this.RedLabel.Name = "RedLabel";
			this.RedLabel.Size = new System.Drawing.Size(54, 24);
			this.RedLabel.TabIndex = 0;
			this.RedLabel.Text = "RED < 0";
			this.RedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BlueLabel
			// 
			this.BlueLabel.AutoSize = true;
			this.BlueLabel.BackColor = System.Drawing.Color.Transparent;
			this.BlueLabel.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.BlueLabel.ForeColor = System.Drawing.Color.Blue;
			this.BlueLabel.Location = new System.Drawing.Point(200, 685);
			this.BlueLabel.Name = "BlueLabel";
			this.BlueLabel.Size = new System.Drawing.Size(60, 24);
			this.BlueLabel.TabIndex = 0;
			this.BlueLabel.Text = "BLUE > 0";
			this.BlueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GenLabel
			// 
			this.GenLabel.AutoSize = true;
			this.GenLabel.BackColor = System.Drawing.Color.Transparent;
			this.GenLabel.Font = new System.Drawing.Font("Agency FB", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.GenLabel.ForeColor = System.Drawing.Color.Silver;
			this.GenLabel.Location = new System.Drawing.Point(120, 60);
			this.GenLabel.Margin = new System.Windows.Forms.Padding(0);
			this.GenLabel.Name = "GenLabel";
			this.GenLabel.Size = new System.Drawing.Size(66, 24);
			this.GenLabel.TabIndex = 0;
			this.GenLabel.Text = "GEN: {0}";
			this.GenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MutationLabel
			// 
			this.MutationLabel.AutoSize = true;
			this.MutationLabel.BackColor = System.Drawing.Color.Transparent;
			this.MutationLabel.Font = new System.Drawing.Font("Agency FB", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.MutationLabel.ForeColor = System.Drawing.Color.Silver;
			this.MutationLabel.Location = new System.Drawing.Point(120, 90);
			this.MutationLabel.Margin = new System.Windows.Forms.Padding(0);
			this.MutationLabel.Name = "MutationLabel";
			this.MutationLabel.Size = new System.Drawing.Size(136, 24);
			this.MutationLabel.TabIndex = 0;
			this.MutationLabel.Text = "MUTATION RATE: {0}%";
			this.MutationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GameCanvasForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Size = new System.Drawing.Size(1200, 800);
			this.ClientSize = new System.Drawing.Size(Size.Width + 40, Size.Height + 40);
			this.Controls.Add(this.Canvas);
			this.Controls.Add(this.MutationLabel);
			this.Controls.Add(this.GenLabel);
			this.Controls.Add(this.BlueLabel);
			this.Controls.Add(this.RedLabel);
			this.Controls.Add(this.ScoreLabel);
			this.Controls.Add(this.HighscoreLabel);
			this.Controls.Add(this.save);
			this.Controls.Add(this.load);
			this.Controls.Add(this.graph);
			this.Controls.Add(this.IncreaseBut);
			this.Controls.Add(this.DecreaseBut);
			this.Controls.Add(this.Background);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(1);
			//this.Margin = new System.Windows.Forms.Padding(1, 1, 41, 41);
			//this.Padding = new System.Windows.Forms.Padding(1, 1, 41, 41);
			this.Name = "GameCanvasForm";
			this.Text = "SnakeAI.WinForms";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameCanvasForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameCanvasForm_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Background)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.PictureBox Canvas;
		private System.Windows.Forms.Timer GameTimer;
		private System.Windows.Forms.PictureBox Background;
		private System.Windows.Forms.Button graph;
		private System.Windows.Forms.Button load;
		private System.Windows.Forms.Button save;
		private System.Windows.Forms.Button IncreaseBut;
		private System.Windows.Forms.Button DecreaseBut;
		private System.Windows.Forms.Label RedLabel;
		private System.Windows.Forms.Label BlueLabel;
		private System.Windows.Forms.Label ScoreLabel;
		private System.Windows.Forms.Label HighscoreLabel;
		private System.Windows.Forms.Label GenLabel;
		private System.Windows.Forms.Label MutationLabel;
	}
}