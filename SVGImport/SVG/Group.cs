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
    public class Group
    {
        public string Name { get; set; }
        public List<Line> Lines { get; set; }
        public List<Rect> Rects { get; set; }
        public List<Circle> Circles { get; set; }
        public List<Path> Paths { get; set; }

        public Group(XElement group)
        {
            XAttribute id = group.Attribute("id");

            if (id != null)
            {
                this.Name = id.Value;
            }

            this.Lines = new List<Line>();
            this.Rects = new List<Rect>();
            this.Circles = new List<Circle>();
            this.Paths = new List<Path>();

            List<XElement> lines = new List<XElement>();
            List<XElement> rects = new List<XElement>();
            List<XElement> circles = new List<XElement>();
            List<XElement> paths = new List<XElement>();

            lines = group.Descendants("{http://www.w3.org/2000/svg}line").ToList();
            if (lines != null && lines.Count > 0)
            {
                lines.ForEach(l => this.Lines.Add(new SVG.Line(l)));
            }

            rects = group.Descendants("{http://www.w3.org/2000/svg}rect").ToList();
            if (rects != null && rects.Count > 0)
            {
                rects.ForEach(r => this.Rects.Add(new Rect(r)));
            }

            circles = group.Descendants("{http://www.w3.org/2000/svg}circle").ToList();
            if (circles != null && circles.Count > 0)
            {
                circles.ForEach(c => this.Circles.Add(new Circle(c)));
            }

            paths = group.Descendants("{http://www.w3.org/2000/svg}path").ToList();
            if (paths != null && paths.Count > 0)
            {
                paths.ForEach(p => this.Paths.Add(new Path(p)));
            }
        }

        public void Draw()
        {
            ThisApplication.AddLogMessage(4, ">> Draw()");

            if (!CamBam.UI.CamBamUI.MainUI.ActiveView.CADFile.Layers.ContainsKey(this.Name))
            {
                CamBam.UI.CamBamUI.MainUI.ActiveView.CADFile.CreateLayer(this.Name);
            }
            CamBam.UI.CamBamUI.MainUI.ActiveView.CADFile.SetActiveLayer(this.Name);
            
            this.Lines.ForEach(l => l.Draw());
            this.Rects.ForEach(r => r.Draw());
            this.Circles.ForEach(c => c.Draw());
            this.Paths.ForEach(p => p.Draw());

            ThisApplication.AddLogMessage(4, "<< Draw");
        }
    }
}
