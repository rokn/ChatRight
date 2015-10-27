using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRight
{
    public partial class MainTextBox : RichTextBox
    {
        private GraphicsPath shape;
        private Pen borderPen;
        public int BorderRadius;
        private PaintEventArgs paintArgs;

        public MainTextBox()
            : this(new Size())
        {
        }

        public MainTextBox(Size startingSize)
        {
            InitializeComponent();
            Size = startingSize;
            UpdateShape();
            borderPen = new Pen(Color.Black, BorderRadius);
            this.BorderStyle = BorderStyle.None;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            paintArgs = pe;
            base.OnPaint(pe);
            pe.Graphics.DrawPath(borderPen, shape);
        }

        public void UpdateShape()
        {
            shape = MainForm.CreateRoundRectangle(new Rectangle(0, 0, Width, Height), 10, RectangleCorners.All);
            this.Region = new Region(shape);
        }
    }
}