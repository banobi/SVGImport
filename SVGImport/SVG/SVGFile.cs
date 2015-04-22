using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using CamBam;
using SVGImport.SVG;
using System.Globalization;
using CamBam.CAD;
using CamBam.Geom;

namespace SVGImport
{
    public class SVGFile
    {
        public string FileName { get; set; }
        public List<Group> Groups { get; set; }

        public static NumberFormatInfo Provider { get; set; }

        public SVGFile(string fileName)
        {
            ThisApplication.AddLogMessage(4, string.Format(">> SVGFile({0})", fileName));

            try
            {
                this.FileName = fileName;
                this.Groups = new List<Group>();

                SVGFile.Provider = new NumberFormatInfo();
                SVGFile.Provider.NumberDecimalSeparator = ".";

                ProcessSVGFile();
            }
            catch (Exception ex)
            {
                ThisApplication.AddLogMessage(0, string.Format("Exception: {0} StackTrace: {1}", ex.Message, ex.StackTrace));
            }

            ThisApplication.AddLogMessage(4, "<< SVGFile");
        }

        private void ProcessSVGFile()
        {
            ThisApplication.AddLogMessage(4, string.Format(">> ProcessSVGFile({0})", this.FileName));

            List<XElement> groups = new List<XElement>();

            XDocument XD = XDocument.Load(this.FileName);
            XElement SVG_Element = XD.Root;

            groups = SVG_Element.Descendants("{http://www.w3.org/2000/svg}g").ToList();
            if (groups != null && groups.Count > 0)
            {
                ProcessGroups(groups);
            }
            else
            {
                this.Groups.Add(new Group(SVG_Element));
            }

            ThisApplication.AddLogMessage(4, "<< ProcessSVGFile");
        }

        private void ProcessGroups(List<XElement> groups)
        {
            ThisApplication.AddLogMessage(4, ">> DrawSVGFile()");

            groups.ForEach(g =>
            {
                Group group = new Group(g);
                this.Groups.Add(group);
            });

            ThisApplication.AddLogMessage(4, "<< DrawSVGFile");
        }

        public void DrawSVGFile()
        {
            ThisApplication.AddLogMessage(4, ">> DrawSVGFile()");

            this.Groups.ForEach(g =>
            {
                g.Draw();
            });

            ThisApplication.AddLogMessage(4, "<< DrawSVGFile");
        }
    }
}