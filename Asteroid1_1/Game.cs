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
       // static public BufferedGraphics sky;
        // Свойства
        // Ширина и высота игрового поля
        static public int Width { get; set; }
        static public int Height { get; set; }
        static Game()
        {
        }
        static public BaseObject[] objs;
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
            for (int i = 0; i < objs.Length / 2; i++)
                objs[i] = new BaseObject(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
            Star.Image = Image.FromFile("Images\\square.png");
            for (int i = 15; i < objs.Length; i++)
                objs[i] = new Star(new Point(600, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
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
            
            buffer.Render();
        }
        static public void Update()
        {
            foreach (BaseObject obj in objs) obj.Update();
        }

    }
}
