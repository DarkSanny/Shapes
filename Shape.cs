using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes {
    public abstract class Shape {

        public Point Location { get; set; }
        float angle;
        public float Angle {
            get {
                return angle;
            }
            set {
                angle = value % 360;
            }
        }
        protected SolidBrush BackgroundBrush { get; private set; }
        public Color BackgroundColor {
            get {
                return BackgroundBrush.Color;
            }
            set {
                BackgroundBrush = new SolidBrush(value);
            }
        }
        public Pen BorderPen { get; set; }
        public Color BorderColor {
            get {
                return BorderPen.Color;
            }
            set {
                BorderPen.Color = value;
            }
        }
        public float BorderSize {
            get {
                return BorderPen.Width;
            }
            set {
                BorderPen.Width = value;
            }
        }

        public Shape(Point location) {
            Location = location;
            Angle = 0;
            BackgroundBrush = new SolidBrush(Color.Black);
            BorderPen = new Pen(Color.Black, 0);
        }

        public abstract void DrawShape(Graphics g);
        public abstract bool ContainsPoint(Point point);
        public abstract void Resize(float percent);
        public abstract float Size();

    }
}
