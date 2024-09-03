namespace howto_polygon_geometry
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuTests = new System.Windows.Forms.MenuItem();
            this.mnuTestsClear = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuTestsConvex = new System.Windows.Forms.MenuItem();
            this.mnuTestsPointInPolygon = new System.Windows.Forms.MenuItem();
            this.mnuTestsArea = new System.Windows.Forms.MenuItem();
            this.mnuTestsCentroid = new System.Windows.Forms.MenuItem();
            this.mnuTestsOrientation = new System.Windows.Forms.MenuItem();
            this.mnuTestsReverse = new System.Windows.Forms.MenuItem();
            this.mnuTestsTriangulate = new System.Windows.Forms.MenuItem();
            this.mnuTestsBoundingRectangle = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // MainMenu1
            // 
            this.MainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuTests});
            // 
            // mnuTests
            // 
            this.mnuTests.Index = 0;
            this.mnuTests.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuTestsClear,
            this.menuItem1,
            this.mnuTestsConvex,
            this.mnuTestsPointInPolygon,
            this.mnuTestsArea,
            this.mnuTestsCentroid,
            this.mnuTestsOrientation,
            this.mnuTestsReverse,
            this.mnuTestsTriangulate,
            this.mnuTestsBoundingRectangle});
            this.mnuTests.Text = "&Tests";
            // 
            // mnuTestsClear
            // 
            this.mnuTestsClear.Index = 0;
            this.mnuTestsClear.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.mnuTestsClear.Text = "Clear";
            this.mnuTestsClear.Click += new System.EventHandler(this.mnuTestsClear_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // mnuTestsConvex
            // 
            this.mnuTestsConvex.Enabled = false;
            this.mnuTestsConvex.Index = 2;
            this.mnuTestsConvex.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.mnuTestsConvex.Text = "&Convex";
            this.mnuTestsConvex.Click += new System.EventHandler(this.mnuTestsConvex_Click);
            // 
            // mnuTestsPointInPolygon
            // 
            this.mnuTestsPointInPolygon.Enabled = false;
            this.mnuTestsPointInPolygon.Index = 3;
            this.mnuTestsPointInPolygon.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.mnuTestsPointInPolygon.Text = "&Point in Polygon";
            this.mnuTestsPointInPolygon.Click += new System.EventHandler(this.mnuTestsPointInPolygon_Click);
            // 
            // mnuTestsArea
            // 
            this.mnuTestsArea.Enabled = false;
            this.mnuTestsArea.Index = 4;
            this.mnuTestsArea.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.mnuTestsArea.Text = "&Area";
            this.mnuTestsArea.Click += new System.EventHandler(this.mnuTestsArea_Click);
            // 
            // mnuTestsCentroid
            // 
            this.mnuTestsCentroid.Enabled = false;
            this.mnuTestsCentroid.Index = 5;
            this.mnuTestsCentroid.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.mnuTestsCentroid.Text = "Centroi&d";
            this.mnuTestsCentroid.Click += new System.EventHandler(this.mnuTestsCentroid_Click);
            // 
            // mnuTestsOrientation
            // 
            this.mnuTestsOrientation.Enabled = false;
            this.mnuTestsOrientation.Index = 6;
            this.mnuTestsOrientation.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.mnuTestsOrientation.Text = "&Orientation";
            this.mnuTestsOrientation.Click += new System.EventHandler(this.mnuTestsOrientation_Click);
            // 
            // mnuTestsReverse
            // 
            this.mnuTestsReverse.Enabled = false;
            this.mnuTestsReverse.Index = 7;
            this.mnuTestsReverse.Text = "&Reverse";
            this.mnuTestsReverse.Click += new System.EventHandler(this.mnuTestsReverse_Click);
            // 
            // mnuTestsTriangulate
            // 
            this.mnuTestsTriangulate.Enabled = false;
            this.mnuTestsTriangulate.Index = 8;
            this.mnuTestsTriangulate.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.mnuTestsTriangulate.Text = "&Triangulate";
            this.mnuTestsTriangulate.Click += new System.EventHandler(this.mnuTestsTriangulate_Click);
            // 
            // mnuTestsBoundingRectangle
            // 
            this.mnuTestsBoundingRectangle.Enabled = false;
            this.mnuTestsBoundingRectangle.Index = 9;
            this.mnuTestsBoundingRectangle.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.mnuTestsBoundingRectangle.Text = "Bounding Rectangle";
            this.mnuTestsBoundingRectangle.Click += new System.EventHandler(this.mnuTestsBoundingRectangle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 285);
            this.Menu = this.MainMenu1;
            this.Name = "Form1";
            this.Text = "howto_polygon_geometry";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MainMenu MainMenu1;
        internal System.Windows.Forms.MenuItem mnuTests;
        private System.Windows.Forms.MenuItem mnuTestsClear;
        private System.Windows.Forms.MenuItem menuItem1;
        internal System.Windows.Forms.MenuItem mnuTestsConvex;
        internal System.Windows.Forms.MenuItem mnuTestsPointInPolygon;
        internal System.Windows.Forms.MenuItem mnuTestsArea;
        internal System.Windows.Forms.MenuItem mnuTestsCentroid;
        internal System.Windows.Forms.MenuItem mnuTestsOrientation;
        internal System.Windows.Forms.MenuItem mnuTestsReverse;
        internal System.Windows.Forms.MenuItem mnuTestsTriangulate;
        private System.Windows.Forms.MenuItem mnuTestsBoundingRectangle;
    }
}

