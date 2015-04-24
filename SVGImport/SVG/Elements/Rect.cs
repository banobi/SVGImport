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
    public class Rect : SVGElement
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double RX { get; set; }
        public double RY { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public Rect()
        {
        }

        public override void Process(XElement element)
        {
            XAttribute rx = element.Attribute("rx");
            XAttribute ry = element.Attribute("ry");

            this.X = Convert.ToDouble(element.Attribute("x").Value, SVGFile.Provider);
            this.Y = Convert.ToDouble(element.Attribute("y").Value, SVGFile.Provider);

            this.Width = Convert.ToDouble(element.Attribute("width").Value, SVGFile.Provider);
            this.Height = Convert.ToDouble(element.Attribute("height").Value, SVGFile.Provider);

            if (rx != null)
            {
                this.RX = Convert.ToDouble(rx, SVGFile.Provider);
            }
            if (ry != null)
            {
                this.RY = Convert.ToDouble(ry, SVGFile.Provider);
            }
        }

        public override void Draw()
        {
            Point3F point = new Point3F(this.X, this.Y, 0);

            PolyRectangle rect = new PolyRectangle(point, this.Width, this.Height);

            rect.CornerRadius = this.RX;

            ThisApplication.AddLogMessage(3, string.Format("Add new rect ({0}, {1}) W={2} H={3}", this.X, this.Y, this.Width, this.Height));

            CamBam.UI.CamBamUI.MainUI.InsertEntity(rect);
        }
    }
}
