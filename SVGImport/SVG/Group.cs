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
        public List<SVGElement> Elements { get; set; }

        public Group(XElement group)
        {
            XAttribute id = group.Attribute("id");

            if (id != null)
            {
                this.Name = id.Value;
            }

            this.Elements = new List<SVGElement>();

            List<XElement> elements = new List<XElement>();

            elements = group.Descendants("{http://www.w3.org/2000/svg}line").ToList();
            if (elements != null && elements.Count > 0)
            {
                elements.ForEach(e =>
                {
                    SVGElement element = new SVG.Elements.Line();
                    element.Process(e);
                    this.Elements.Add(element);
                });
            }

            elements.Clear();
            elements = group.Descendants("{http://www.w3.org/2000/svg}rect").ToList();
            if (elements != null && elements.Count > 0)
            {
                elements.ForEach(e =>
                {
                    SVGElement element = new SVG.Elements.Rect();
                    element.Process(e);
                    this.Elements.Add(element);
                });
            }

            elements.Clear();
            elements = group.Descendants("{http://www.w3.org/2000/svg}circle").ToList();
            if (elements != null && elements.Count > 0)
            {
                elements.ForEach(e =>
                {
                    SVGElement element = new SVG.Elements.Circle();
                    element.Process(e);
                    this.Elements.Add(element);
                });
            }

            elements.Clear();
            elements = group.Descendants("{http://www.w3.org/2000/svg}path").ToList();
            if (elements != null && elements.Count > 0)
            {
                elements.ForEach(e =>
                {
                    SVGElement element = new SVG.Elements.Path();
                    element.Process(e);
                    this.Elements.Add(element);
                });
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
            
            this.Elements.ForEach(l => l.Draw());

            ThisApplication.AddLogMessage(4, "<< Draw");
        }
    }
}
