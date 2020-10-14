using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid1_1
{
    class BaseObject
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
        public virtual void Draw()
        {
            Game.buffer.Graphics.DrawEllipse(Pens.White, pos.X, pos.Y, size.Width, size.Height);
        }
        public virtual void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X < Game.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y < Game.Height) dir.Y = -dir.Y;
        }
    }
    class Star : BaseObject
    {
        public static Image Image { get; set; }
        public Star() : base(new Point(0, 0), new Point(0, 0), new Size(0, 0)) 
        {
            Image = Image;
        }
        public Star (Point pos,Point dir, Size size):base(pos,dir,size)
        {

        }
        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0) dir.X = Game.Width + 20;
           /* pos.Y = pos.Y + dir.Y;
            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X < Game.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y < Game.Height) dir.Y = -dir.Y;*/
        }
        public override void Draw()
        {
            //Game.buffer.Graphics.DrawLine(Pens.White, pos.X, pos.Y,pos.X +size.Width,pos.Y+ size.Height);
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
}
