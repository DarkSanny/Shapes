using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes {
    class Rectangle : Shape {

        Size size;

        public Rectangle(Point location, Size size) : base(location) {
            this.size = size;
        }

        public override bool ContainsPoint(Point point) {
            return point.X >= Location.X - size.Width / 2 && 
                point.X <= Location.X + size.Width/2 &&
                point.Y >= Location.Y - size.Height / 2 && 
                point.Y <= Location.Y + size.Height / 2;
        }

        public override void DrawShape(Graphics g) {
            g.TranslateTransform(Location.X, Location.Y);
            g.RotateTransform(Angle);
            g.FillRectangle(BackgroundBrush, -size.Width / 2, -size.Height / 2, size.Width, size.Height);
            g.DrawRectangle(BorderPen, -size.Width / 2, -size.Height / 2, size.Width, size.Height);
            g.RotateTransform(-Angle);
            g.TranslateTransform(-Location.X, -Location.Y);
        }

        public override void Resize(float delta) {
            size.Width = (int)(size.Width + delta);
            size.Height = (int)(size.Height + delta);
        }

        public override float Size() {
            return (float) Math.Sqrt((size.Width / 2) * (size.Width / 2) + 
                                     (size.Height / 2) * (size.Height / 2));
        }
    }
}
