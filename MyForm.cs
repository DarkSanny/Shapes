using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Shapes {
    class MyForm : Form{

        LinkedList<Shape> list;
        Shape selectedShape;

        public MyForm() {
            Init();
            InitMouse();
            DoubleBuffered = true;
        }

        private void Render() {
            Paint += (sender, args) => {
                foreach (var shape in list)
                    shape.DrawShape(args.Graphics);
            };
        }

        private void InitMouse() {
            bool isDragging = false;
            MouseDown += (sender, args) => {
                if (args.Button == MouseButtons.Left) isDragging = true;
            };
            MouseUp += (sender, args) => {
                if (args.Button == MouseButtons.Left) isDragging = false;
            };
            MouseMove += (sender, args) => {
                if (!isDragging) {
                    selectedShape = null;
                    return;
                }
                if (selectedShape == null) {
                    var selects = list.Where(i => i.ContainsPoint(args.Location));
                    if (selects.Count() == 0) {
                        isDragging = false;
                        return;
                    }
                    selectedShape = selects.Last();
                    list.ShapeUp(selectedShape);
                }
                selectedShape.Location = args.Location;
            };
            MouseWheel += (sender, args) => {
                var selectedShape = SelectShape(args.Location);
                if (selectedShape == null) return;
                list.ShapeUp(selectedShape);
                if (args.Delta > 0) selectedShape.Resize(2f);
                else if (args.Delta < 0) selectedShape.Resize(-2f);
            };
            Point? startLocation = null;
            MouseDown += (sender, args) => {
                if (args.Button == MouseButtons.Right)
                    if (startLocation == null) {
                        startLocation = args.Location;
                        selectedShape = SelectShape(args.Location);
                    }
            };
            MouseUp += (sender, args) => {
                //if (args.Button == MouseButtons.Right)
                    
            };
        }

        private Shape SelectShape(Point location) {
            var selected = list.Where(i => i.ContainsPoint(location));
            if (selected.Count() == 0) return null;
            return selected.Last();
        }

        private void Init() {
            list = new LinkedList<Shape>();
            list.Add(new Rectangle(new Point(40, 40), new Size(20, 30)));
            list.Add(new Rectangle(new Point(40, 40), new Size(20, 30)) { BackgroundColor = Color.Pink});
            Render();
            var render = new Timer() { Interval = 1 };
            render.Tick += (sender, args) => Invalidate();
            render.Start();
        }

    }
}
