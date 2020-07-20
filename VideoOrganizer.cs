using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OrganizePhotos
{
    class VideoOrganizer : Organizer
    {
        public VideoOrganizer(string s, string t) : base(s, t) { }

        protected override string[] FileExtension
        {
            get
            {
                return GetVideoFormats().ToArray();
            }
        }
        private List<string> GetVideoFormats()
        {
            List<string> list = new List<string>(256);
            list.Add("*.webm");
            list.Add("*.mkv");
            list.Add("*.flv");
            list.Add("*.flv");
            list.Add("*.vob");
            list.Add("*.ogv");
            list.Add("*.ogg");
            list.Add("*.drc");
            list.Add("*.gif");
            list.Add("*.gifv");
            list.Add("*.mng");
            list.Add("*.avi");
            list.Add("*.mov");
            list.Add("*.qt");
            list.Add("*.wmv");
            list.Add("*.yuv");
            list.Add("*.rm");
            list.Add("*.rmvb");
            list.Add("*.asf");
            list.Add("*.amv");
            list.Add("*.mp4");
            list.Add("*.m4p");
            list.Add("*.m4v");
            list.Add("*.mpg");
            list.Add("*.mp2");
            list.Add("*.mpeg");
            list.Add("*.mpe");
            list.Add("*.mpv");
            list.Add("*.mpg");
            list.Add("*.mpeg");
            list.Add("*.m2v");
            list.Add("*.m4v");
            list.Add("*.svi");
            list.Add("*.3gp");
            list.Add("*.3g2");
            list.Add("*.mxf");
            list.Add("*.roq");
            list.Add("*.nsv");
            list.Add("*.flv ");
            list.Add("*.f4v ");
            list.Add("*.f4p");
            list.Add("*.f4a ");
            list.Add("*.f4b");
            return list;
        }

        protected override string GetDatedTargetPath(DateTime dt)
        {
            return Path.Combine(TargetDir, dt.Year.ToString(), string.Format("{0}{1}", dt.Month.ToString("00"), "Video"));
        }

        protected override DateTime? GetFileDateTime(string path)
        {
            try
            {
                return File.GetLastWriteTime(path);
            }
            catch
            {
                return null;
            }
        }
    }
}
