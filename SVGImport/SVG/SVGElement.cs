using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SVGImport.SVG
{
    public abstract class SVGElement
    {
        public SVGElement()
        {
        }

        public abstract void Process(XElement element);

        public abstract void Draw();
    }
}
