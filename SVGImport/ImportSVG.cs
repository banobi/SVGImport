using CamBam;
using CamBam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SVGImport
{
    public partial class ImportSVG : Form
    {
        public ImportSVG()
        {
            InitializeComponent();

            label1.Text = TextTranslation.Translate("Drop here the SVG file(s).");
        }

        private static void ProcessSVGFilesImport(List<string> files)
        {
            ThisApplication.AddLogMessage(4, string.Format(">> ProcessSVGFileImport({0})", string.Join(",", files.ToArray())));

            files.ForEach(f =>
            {
                SVGFile svg = new SVGFile(f);

                svg.DrawSVGFile();
            });

            ThisApplication.AddLogMessage(4, "<< ProcessSVGFileImport");
        }

        private void ImportSVG_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            List<string> svgFiles = new List<string>();

            ProcessSVGFilesImport(files.Where(f => f.EndsWith(".svg", StringComparison.InvariantCultureIgnoreCase)).ToList());
        }
    }
}
