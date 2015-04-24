using CamBam;
using CamBam.CAD;
using CamBam.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SVGImport.SVG.Elements
{
    public class Circle : SVGElement
    {
        public double CenterX { get; set; }
        public double CenterY { get; set; }

        public double Radius { get; set; }

        public Circle()
        {
        }

        public override void Process(XElement element)
        {
            this.CenterX = Convert.ToDouble(element.Attribute("cx").Value, SVGFile.Provider);
            this.CenterY = Convert.ToDouble(element.Attribute("cy").Value, SVGFile.Provider);

            this.Radius = Convert.ToDouble(element.Attribute("r").Value, SVGFile.Provider);
        }

        public override void Draw()
        {
            Point2F point = new Point2F(this.CenterX, this.CenterY);

            CamBam.CAD.Circle rect = new CamBam.CAD.Circle(point, this.Radius);

            ThisApplication.AddLogMessage(3, string.Format("Add new circle ({0}, {1}) Radius={2}", this.CenterX, this.CenterY, this.Radius));

            CamBam.UI.CamBamUI.MainUI.InsertEntity(rect);
        }
    }
}
