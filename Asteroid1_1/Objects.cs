using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid1_1
{
    abstract class BaseObject: ICollision
    {
        protected Point pos;
        protected Point dir;
        protected Size size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public Rectangle Rect => new Rectangle(pos, size);

        public bool Collision(ICollision obj)
        {
            return (obj.Rect.IntersectsWith(this.Rect));
        }

        abstract public void Draw();
        abstract public void Update();
        
    }
    class Star : BaseObject
    {
        public static Image Image { get; set; }
        public Star() : base(new Point(0, 0), new Point(0, 0), new Size(0, 0)) 
        {
            Image = Image;
        }
        public int Power { get; set; }
        public Star (Point pos,Point dir, Size size):base(pos,dir,size)
        {
            Power = 1;
        }
        public override void Update()
        {
            pos.X = pos.X - dir.X;
            if (pos.X <= 0) pos.X = Game.Width;
           
        }
        public override void Draw()
        {
           
            Game.buffer.Graphics.DrawImage(Image,pos);
        }

    }
    
    class Sky : BaseObject
    {
        private Image img = Image.FromFile("Images\\image.jpg");

        public Sky() : base(new Point(0, 0), new Point(0, 0), new Size(0, 0))
        {
        }

        public Sky(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }
        public override void Update()
        {
            pos.X = pos.X - dir.X;
            if (pos.X < 0 - img.Width) pos.X = Game.Width + img.Width / 2;
        }

        public override void Draw()
        {

            Game.buffer.Graphics.DrawImage(img, pos);
        }
    }
    class Bullet : BaseObject
    {
        int speed;
        private Image img = Image.FromFile("Images\\bullet.png");

       
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            speed = dir.X;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(img, pos);
        }

        public override void Update()
        {
            if (pos.X < Game.Width + size.Width) pos.X = pos.X + speed;
        }
    }
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
