using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Globalization;

namespace OrganizePhotos
{
  abstract class Organizer
  {
    public string SourceDir { get; set; }
    public string TargetDir { get; set; }
    protected readonly string[] PhotoExtensions = new string[] { "*.JPG", "*.JPE", "*.BMP", "*.GIF", "*.PNG" };
    protected readonly string[] VideoExtensions = new string[] { "*.3gp", "*.avi", "*.flv", "*.m4v", "*.mp4", "*.mpg", "*.wmv" };

   
    public  Organizer(string s, string t)
    {
      SourceDir = s;
      TargetDir = t;
    }

    protected void Log(string s)
    {
      System.IO.File.AppendAllText(Path.Combine(SourceDir, "Log.txt"), string.Format("{0}\r\n", s));
    }

    protected abstract string[] FileExtension { get; }
    protected abstract DateTime? GetFileDateTime(string f);
    protected abstract string GetDatedTargetPath(DateTime dt);

    protected string GetSortableDate(DateTime dt)
    {
      return string.Format("{0}-{1}-{2} {3}-{4}-{5}", dt.Year, dt.Month.ToString("00"), dt.Day.ToString("00"), dt.Hour.ToString("00"), dt.Minute.ToString("00"), dt.Second.ToString("00"));
    }
        protected DateTime? GetDateFromName(string file)
        {
            DateTime? dt = null;
            string s = "log-bb-2014-02-12-12-06-13-diag";
            Regex r = new Regex(@"\d{4}-\d{2}-\d{2}-\d{2}-\d{2}-\d{2}");
            Match m = r.Match(s);
            if (m.Success)
            {
                dt= DateTime.ParseExact(m.Value, "yyyy-MM-dd-hh-mm-ss", CultureInfo.InvariantCulture);
            }
            return dt;
        }

        public void Organize()
        {
            string[] sourceFiles = FileExtension.SelectMany(f => Directory.GetFiles(SourceDir, f, SearchOption.AllDirectories).Where(d => (new DirectoryInfo(d).Attributes & FileAttributes.Hidden) == 0)).ToArray();
            OrganizeFiles(sourceFiles);
        }
        
            public  void OrganizeFiles(string[] sourceFiles)
    {      
        Dictionary<string, string> dic = new Dictionary<string, string>();
        List<string> list = new List<string>();  // to cache currntly copying files so if more than 1 photo in second then append -1 , -2 etc.
        List<string> listPath = new List<string>();
        List<string> newFilePaths = new List<string>();
        Console.WriteLine("Checking target folder....");
        newFilePaths.AddRange(Directory.GetFiles(TargetDir, "*.*", SearchOption.AllDirectories));

        Console.WriteLine(string.Format("Target folder contains {0} files", newFilePaths.Count));

        listPath.AddRange(Directory.GetDirectories(TargetDir, "*", SearchOption.AllDirectories).Where(d => (new DirectoryInfo(d).Attributes & FileAttributes.Hidden) == 0));

        Console.WriteLine(string.Format("Target folder contains {0} directories", listPath.Count));

        

        int index = 0;
        foreach (string f in sourceFiles)
        {
          index++;
          Console.WriteLine(string.Format("Processing {0} of {1} files {2}", index, sourceFiles.Length, f));

          DateTime? dtnull = GetFileDateTime(f);
                dtnull = dtnull.HasValue?dtnull: GetDateFromName(f);
                
          if (dtnull.HasValue)
          {
            DateTime dt = dtnull.Value;
            string d = GetSortableDate(dt);
            string path = TargetDir;

            //if (mOrganizationType == OrganizationType.Hierarchy)
            {
              path = GetDatedTargetPath(dt);// Path.Combine(TargetDir, dt.Year.ToString(), dt.Month.ToString("00"));

              if (!listPath.Contains(path))
              {
                if (!Directory.Exists(path))
                  Directory.CreateDirectory(path);
                listPath.Add(path);
              }
            }

            int count = list.FindAll(a => a == d).Count;
            string newFile = Path.Combine(TargetDir, Path.Combine(path, d + Path.GetExtension(f)));

            if (count > 0)
            {
              newFile = Path.Combine(path, d + string.Format("-{0}", count) + Path.GetExtension(f));
            }

            if (!newFilePaths.Contains(newFile))
              try
              {
                File.Copy(f, newFile);
              }
              catch (Exception ex)
              {
                Log(string.Format(string.Format("Could not copy file {0} as ", f, ex.Message)));
              }

            else
              Log(string.Format("File already exists {0}", f));

            newFilePaths.Add(newFile);

            list.Add(d);
          }
            else
            {
                Log(string.Format("Date taken not found {0}", f));
            }

            }
    }
    }
      
  }

