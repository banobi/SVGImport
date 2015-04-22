using CamBam;
using CamBam.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SVGImport.SVG
{
    public class Line
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }

        public double X2 { get; set; }
        public double Y2 { get; set; }

        public Line(XElement rect)
        {
            this.X1 = Convert.ToDouble(rect.Attribute("x1").Value, SVGFile.Provider);
            this.Y1 = Convert.ToDouble(rect.Attribute("y1").Value, SVGFile.Provider);

            this.X2 = Convert.ToDouble(rect.Attribute("x2").Value, SVGFile.Provider);
            this.Y2 = Convert.ToDouble(rect.Attribute("y2").Value, SVGFile.Provider);
        }

        public void Draw()
        {
            CamBam.CAD.Line line = new CamBam.CAD.Line(new Point2F(this.X1, this.Y1), new Point2F(this.X2, this.Y2));

            ThisApplication.AddLogMessage(3, string.Format("Add new line ({0}, {1}) ({2}, {3})", this.X1, this.Y1, this.X2, this.Y2));

            CamBam.UI.CamBamUI.MainUI.InsertEntity(line);
        }
    }
}
