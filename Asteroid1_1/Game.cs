using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid1_1
{
    static class Game
    {
        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;
        static public int Width { get; set; }
        static public int Height { get; set; }
        public static Bullet bullet;
        static Game()
        {
        }
        static public BaseObject[] objs;
        static Random rnd = new Random();
        public static Sky sky;
        static public System.Windows.Forms.Timer timer;
        static public void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            if (Width > 1000 || Height > 1000) throw new ArgumentOutOfRangeException();
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            timer = new Timer();
             timer.Interval = 100;
             timer.Tick += Timer_Tick;
             timer.Start();

        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        static public void Load()
        {
            sky = new Sky(new Point(0, 0), new Point(1, 0), new Size(5, 5));
           // Star.Image = Image.FromFile("Images\\image.jpg");
            objs = new BaseObject[30];
           Star.Image = Image.FromFile("Images\\square.png");
            bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(100, 80));
            for (int i = 0; i < objs.Length; i++)
                objs[i] = new Star(new Point(rnd.Next(i, 1000), rnd.Next(i,1000)), new Point(2, 0), new Size(5, 5));
        }
        static public void Draw()
        {
            // Проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);
            // buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            sky.Draw();
            foreach (BaseObject obj in objs)
                obj.Draw();
            bullet.Draw();
            buffer.Render();
        }
        static public void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
            
            bullet.Update();
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].Update();
                if (objs[i].Collision(bullet))
                {
                    bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(100, 80));
                    objs[i] = new Star(new Point(1000, 200), new Point(15 + rnd.Next(1, 10), 30 + rnd.Next(1, 10)), new Size(Star.Image.Width, Star.Image.Height));
                }
            }

        }

    }
}
