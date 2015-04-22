using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;

using CamBam;
using CamBam.Geom;
using CamBam.CAD;
using CamBam.UI;
using System.Threading;
using System.Reflection;
using System.ComponentModel;
using CamBam.Util;

namespace SVGImport
{
    public class SVGImport
    {
        static ImportSVG form = new ImportSVG();

        public static void InitPlugin(CamBamUI ui)
        {
            ThisApplication.TopWindow.Load += TopWindow_Load;
        }

        static void TopWindow_Load(object sender, EventArgs e)
        {
            ToolStripMenuItem svgMenu = new ToolStripMenuItem();
            svgMenu.Text = TextTranslation.Translate("Import SVG file(s)");
            svgMenu.Click += svgMenu_Click;

            CamBamUI.MainUI.Menus.mnuPlugins.DropDownItems.Add(svgMenu);
        }

        static void svgMenu_Click(object sender, EventArgs e)
        {
            if (form == null || (form != null && form.IsDisposed))
            {
                form = new ImportSVG();
            }
            if (!form.Visible)
            {
                form.Show();
            }
        }
    }
}
