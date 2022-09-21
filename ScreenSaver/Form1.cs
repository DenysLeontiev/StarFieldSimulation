using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaver
{
	public partial class Form1 : Form
	{
		class Star
		{
			public float X { get; set; }
			public float Y { get; set; }
			public float Z { get; set; }
		}

		Star[] stars = new Star[15000];
		Random random = new Random();
		Graphics graphics; //Об'єкт для створення графічних зображень


		public Form1()
		{
			InitializeComponent();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			graphics.Clear(Color.Black); // замальовуємо екран

			foreach (var star in stars)
			{
				DarwStar(star);
				MoveStar(star);
			}

			pictureBox1.Refresh(); // перемальовує себе(обновляє)
		}

		private void MoveStar(Star star)
		{
			star.Z -= 10f;
			if(star.Z < 1)
			{
				star.X = random.Next(-pictureBox1.Width, pictureBox1.Width);
				star.Y = random.Next(-pictureBox1.Height, pictureBox1.Height);
				star.Z = random.Next(1, pictureBox1.Height);
			}
		}

		private void DarwStar(Star star)
		{
			float starSize = Map(star.Z, 0, pictureBox1.Width, 7, 0);

			float x = Map(star.X / star.Z, 0, 1, 0, pictureBox1.Width) + pictureBox1.Width / 2;
			float y = Map(star.Y / star.Z, 0, 1, 0, pictureBox1.Height) + pictureBox1.Height / 2;

			graphics.FillEllipse(Brushes.White, x, y, starSize, starSize); // draws circles
		}

		private float Map(float n, float start1, float stop1, float start2, float stop2)
		{
			return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);  //Bitmap - an image file format that can be used to create and store computer graphics

			graphics = Graphics.FromImage(pictureBox1.Image); 

			for (int i = 0; i < stars.Length; i++)
			{
				stars[i] = new Star
				{
					X = random.Next(-pictureBox1.Width, pictureBox1.Width),
					Y = random.Next(-pictureBox1.Height, pictureBox1.Height),
					Z = random.Next(1, pictureBox1.Height)
				};

				timer1.Start(); // calls timer1_Tick() every interval we set in timer Interval properties
			}
		}
	}
}
