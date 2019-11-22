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
			this.ScoreLabel = new System.Windows.Forms.Label();
			this.GameTimer = new System.Windows.Forms.Timer(this.components);
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.Canvas = new System.Windows.Forms.PictureBox();
			this.GameOverPic = new System.Windows.Forms.PictureBox();
			this.GameOverLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GameOverPic)).BeginInit();
			this.SuspendLayout();
			// 
			// ScoreLabel
			// 
			this.ScoreLabel.AutoSize = true;
			this.ScoreLabel.Font = new System.Drawing.Font("Rockwell", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ScoreLabel.ForeColor = System.Drawing.Color.ForestGreen;
			this.ScoreLabel.Location = new System.Drawing.Point(671, 104);
			this.ScoreLabel.Name = "ScoreLabel";
			this.ScoreLabel.Size = new System.Drawing.Size(44, 50);
			this.ScoreLabel.TabIndex = 2;
			this.ScoreLabel.Text = "0";
			this.ScoreLabel.Click += new System.EventHandler(this.label1_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::SnakeAI.WinForms.Properties.Resources.score1;
			this.pictureBox2.Location = new System.Drawing.Point(659, 10);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(148, 55);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 1;
			this.pictureBox2.TabStop = false;
			// 
			// Canvas
			// 
			this.Canvas.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.Canvas.BackgroundImage = global::SnakeAI.WinForms.Properties.Resources.grass00;
			this.Canvas.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.Canvas.InitialImage = null;
			this.Canvas.Location = new System.Drawing.Point(18, 16);
			this.Canvas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Canvas.Name = "Canvas";
			this.Canvas.Size = new System.Drawing.Size(576, 522);
			this.Canvas.TabIndex = 0;
			this.Canvas.TabStop = false;
			this.Canvas.Click += new System.EventHandler(this.Canvas_Click);
			this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
			// 
			// GameOverPic
			// 
			this.GameOverPic.BackColor = System.Drawing.Color.Maroon;
			this.GameOverPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.GameOverPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.GameOverPic.Image = global::SnakeAI.WinForms.Properties.Resources.GO;
			this.GameOverPic.Location = new System.Drawing.Point(135, 182);
			this.GameOverPic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.GameOverPic.Name = "GameOverPic";
			this.GameOverPic.Size = new System.Drawing.Size(289, 89);
			this.GameOverPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.GameOverPic.TabIndex = 3;
			this.GameOverPic.TabStop = false;
			// 
			// GameOverLabel
			// 
			this.GameOverLabel.AutoSize = true;
			this.GameOverLabel.BackColor = System.Drawing.Color.White;
			this.GameOverLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.GameOverLabel.Location = new System.Drawing.Point(157, 313);
			this.GameOverLabel.Name = "GameOverLabel";
			this.GameOverLabel.Size = new System.Drawing.Size(0, 17);
			this.GameOverLabel.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(621, 182);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(331, 344);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			//this.groupBox1.Enter += new System.EventHandler(this.GroupBox1_Enter);
			// 
			// GameCanvasForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(964, 538);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.GameOverLabel);
			this.Controls.Add(this.GameOverPic);
			this.Controls.Add(this.ScoreLabel);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.Canvas);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "GameCanvasForm";
			this.Text = "Snake";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameCanvasForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameCanvasForm_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GameOverPic)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox Canvas;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label ScoreLabel;
		private System.Windows.Forms.Timer GameTimer;
		private System.Windows.Forms.PictureBox GameOverPic;
		private System.Windows.Forms.Label GameOverLabel;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}