using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace OrganizePhotos
{
    /// <summary>
    /// this is only needed if need to process log file later on.
    /// </summary>
    class LogOrganizer : PhotoOrganizer
    {

        public LogOrganizer(string s, string t) : base(s, t) { }
        private static Regex r = new Regex(":");
        public void FixIssuesFromLog()
        {
            string[] sourceFiles = System.IO.File.ReadAllLines(Path.Combine(SourceDir, "Log.txt"));
            sourceFiles = sourceFiles.Select(a => a.Replace("Date taken not found ", "")).ToArray();
            base.OrganizeFiles(sourceFiles);
        }

    }
}
