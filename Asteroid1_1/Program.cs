using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroid1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 800;
            form.Height = 600;
            form.Show();
            form.FormClosing += Form_FormClosing;
            Game.Init(form);
            Application.Run();
            form.FormClosing += Form_FormClosing;

        }
        private static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.timer.Stop();
        }
    }
}
