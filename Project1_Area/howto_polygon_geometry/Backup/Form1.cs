using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace howto_polygon_geometry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int PT_RAD = 2;
        const int PT_WID = PT_RAD * 2 + 1;

        private List<PointF> m_Points = new List<PointF>();

        // Draw the polygon.
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the lines.
            if (m_Points.Count >= 3)
            {
                // Draw the polygon.
                e.Graphics.DrawPolygon(Pens.Blue, m_Points.ToArray());
            }
            else if (m_Points.Count == 2)
            {
                // Draw the line.
                e.Graphics.DrawLines(Pens.Blue, m_Points.ToArray());
            }

            // Draw the points.
            if (m_Points.Count > 0)
            {
                foreach (PointF pt in m_Points)
                {
                    e.Graphics.FillRectangle(Brushes.White, pt.X - PT_RAD, pt.Y - PT_RAD, PT_WID, PT_WID);
                    e.Graphics.DrawRectangle(Pens.Black, pt.X - PT_RAD, pt.Y - PT_RAD, PT_WID, PT_WID);
                }
            }

            // Enable menu items appropriately.
            EnableMenus();
        }

        // Enable menu items appropriately.
        private void EnableMenus()
        {
            bool enabled = (m_Points.Count >= 3);
            mnuTestsConvex.Enabled = enabled;
            mnuTestsPointInPolygon.Enabled = enabled;
            mnuTestsArea.Enabled = enabled;
            mnuTestsCentroid.Enabled = enabled;
            mnuTestsOrientation.Enabled = enabled;
            mnuTestsReverse.Enabled = enabled;
            mnuTestsTriangulate.Enabled = enabled;
            mnuTestsBoundingRectangle.Enabled = enabled;
        }

        // Remove all points.
        private void mnuTestsClear_Click(object sender, EventArgs e)
        {
            m_Points = new List<PointF>();
            EnableMenus();
            this.Invalidate();
        }

        // Save a new point.
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            m_Points.Add(new PointF(e.X, e.Y));

            // Redraw.
            this.Invalidate();
        }

        // See if the polygon is convex.
        private void mnuTestsConvex_Click(object sender, EventArgs e)
        {
            // Make a Polygon.
            Polygon pgon = new Polygon(m_Points.ToArray());

            if (pgon.PolygonIsConvex())
            {
                MessageBox.Show("The polygon is convex", "Convex",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("The polygon is not convex", "Not Convex",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // See if the mouse's current position is inside the polygon.
        private void mnuTestsPointInPolygon_Click(object sender, EventArgs e)
        {
            // Get the current mouse position.
            Point pt = Cursor.Position;

            // Convert into form coordinates.
            pt = this.PointToClient(pt);

            Rectangle rect = new Rectangle(pt.X - 3, pt.Y - 3, 7, 7);
            using (Graphics gr = this.CreateGraphics())
            {
                // Make a Polygon.
                Polygon pgon = new Polygon(m_Points.ToArray());

                // See if the point is in the polygon.
                if (pgon.PointInPolygon(pt.X, pt.Y))
                {
                    gr.FillEllipse(Brushes.Lime, rect);
                }
                else
                {
                    gr.FillEllipse(Brushes.Red, rect);
                }
                gr.DrawEllipse(Pens.Black, rect);
            }
        }

        // Find the polygon's area.
        private void mnuTestsArea_Click(object sender, EventArgs e)
        {
            // Make a Polygon.
            Polygon pgon = new Polygon(m_Points.ToArray());

            MessageBox.Show("Area: " + pgon.PolygonArea().ToString(), "Area",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Display the centroid.
        private void mnuTestsCentroid_Click(object sender, EventArgs e)
        {
            // Make a Polygon.
            Polygon pgon = new Polygon(m_Points.ToArray());

            PointF pt = pgon.FindCentroid();

            Rectangle rect = new Rectangle((int)pt.X - 3, (int)pt.Y - 3, 7, 7);
            using (Graphics gr = this.CreateGraphics())
            {
                gr.FillEllipse(Brushes.Yellow, rect);
                gr.DrawEllipse(Pens.Black, rect);
            }
        }

        // See if the polygon is oriented clockwise or counterclockwise.
        private void mnuTestsOrientation_Click(object sender, EventArgs e)
        {
            // Make a Polygon.
            Polygon pgon = new Polygon(m_Points.ToArray());

            if (pgon.PolygonIsOrientedClockwise())
            {
                MessageBox.Show("Clockwise", "Clockwise",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Counterclockwise", "Counterclockwise",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Reverse the polygon's orientation.
        private void mnuTestsReverse_Click(object sender, EventArgs e)
        {
            m_Points.Reverse();
        }

        // Triangulate the polygon.
        private void mnuTestsTriangulate_Click(object sender, EventArgs e)
        {
            // Make a Polygon.
            Polygon pgon = new Polygon(m_Points.ToArray());

            // Triangulate.
            List<Triangle> triangles = pgon.Triangulate();

            // Draw the triangles.
            using (Graphics gr = this.CreateGraphics())
            {
                foreach (Triangle tri in triangles)
                {
                    gr.DrawPolygon(Pens.Red, tri.Points);
                }

                // Redraw the polygon.
                if (m_Points.Count >= 3)
                {
                    // Draw the polygon.
                    gr.DrawPolygon(Pens.Blue, m_Points.ToArray());
                }
            }
        }

        // Find the polygon's bounding rectangle.
        private void mnuTestsBoundingRectangle_Click(object sender, EventArgs e)
        {
            // Make a Polygon.
            Polygon pgon = new Polygon(m_Points.ToArray());

            // Make sure it's oriented counter-clockwise.
            if (pgon.PolygonIsOrientedClockwise())
            {
                Array.Reverse(pgon.Points);
            }

            // Find the polygon's bounding rectangle.
            PointF[] pts = pgon.FindSmallestBoundingRectangle();

            // Draw it.
            using (Graphics gr = this.CreateGraphics())
            {
                gr.DrawPolygon(Pens.Orange, pts);
            }
        }
    }
}
