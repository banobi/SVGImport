using CamBam;
using CamBam.CAD;
using CamBam.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SVGImport.SVG
{
    public enum PointType
    {
        M = 0,
        L
    };

    public class Path
    {
        public string PathData { get; set; }
        public List<PathPoint> Points { get; set; }
        public bool Closed { get; set; }

        public Path(XElement rect)
        {
            this.PathData = rect.Attribute("d").Value;
            this.Points = new List<PathPoint>();
            this.Closed = false;

            string[] path = this.PathData.Split(' ');
            PointType pointType = PointType.M;

            foreach (string element in path)
            {
                switch (element)
                {
                    case "M":
                        pointType = PointType.M;
                        break;
                    case "L":
                        pointType = PointType.L;
                        break;
                    case "z":
                    case "Z":
                        this.Closed = true;

                        break;
                    default:
                        string elem = element;

                        if (elem.StartsWith("M"))
                        {
                            pointType = PointType.M;

                            elem = elem.Substring(1);
                        }
                        else if (elem.StartsWith("L"))
                        {
                            pointType = PointType.L;
                            elem = elem.Substring(1);
                        }

                        if (elem.IndexOf(",") >= 0)
                        {
                            string[] xAndY = elem.Split(',');

                            double x = Convert.ToDouble(xAndY[0], SVGFile.Provider);
                            double y = Convert.ToDouble(xAndY[1], SVGFile.Provider);

                            PathPoint point = new PathPoint();

                            point.P = new Point3F(x, y, 0);

                            point.PType = pointType;

                            this.Points.Add(point);
                        }

                        break;
                }
            }
        }

        public void Draw()
        {
            Polyline poly = new Polyline();

            for (int i = 0; i < this.Points.Count; i++)
            {
                PathPoint point = this.Points[i];

                switch (point.PType)
                {
                    case PointType.M:
                    case PointType.L:
                        poly.Add(point.P);

                        break;
                }
            }

            poly.Closed = this.Closed;

            CamBam.UI.CamBamUI.MainUI.InsertEntity(poly);
        }
    }

    public class PathPoint
    {
        public Point3F P { get; set; }
        public PointType PType { get; set; }
    }
}
