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
  class PhotoOrganizer : Organizer
  {
    public PhotoOrganizer(string s, string t):base(s,t){}
    private static Regex r = new Regex(":");
    protected override string[] FileExtension
    {
      get
      {
                return GetImageFormats().ToArray();
      }     
    }
        private List<string> GetImageFormats()
        {
        
            List<string> list = new List<string>(572);
            list.Add("*.ANI");
            list.Add("*.ANIM");
            list.Add("*.APNG");
            list.Add("*.ART");
            list.Add("*.BMP");
            list.Add("*.BPG");
            list.Add("*.BSAVE");
            list.Add("*.CAL");
            list.Add("*.CIN");
            list.Add("*.CPC");
            list.Add("*.CPT");
            list.Add("*.DDS");
            list.Add("*.DPX");
            list.Add("*.ECW");
            list.Add("*.EXR");
            list.Add("*.FITS");
            list.Add("*.FLIC");
            list.Add("*.FLIF");
            list.Add("*.FPX");
            list.Add("*.GIF");
            list.Add("*.HDRi");
            list.Add("*.HEVC");
            list.Add("*.ICER");
            list.Add("*.ICNS");
            list.Add("*.ICO ");
            list.Add("*.CUR");
            list.Add("*.ICS");
            list.Add("*.ILBM");
            list.Add("*.JBIG");
            list.Add("*.JBIG2");
            list.Add("*.JNG");
            list.Add("*.JPEG");
            list.Add("*.JPG");
            list.Add("*.JPEG 2000");
            list.Add("*.JPEG-LS");
            list.Add("*.JPEG XR");
            list.Add("*.KRA");
            list.Add("*.MNG");
            list.Add("*.MIFF");
            list.Add("*.NRRD");
            list.Add("*.ORA");
            list.Add("*.PAM");
            list.Add("*.PBM");
            list.Add("*.PGM ");
            list.Add("*.PPM ");
            list.Add("*.PNM");
            list.Add("*.PCX");
            list.Add("*.PGF");
            list.Add("*.PICtor");
            list.Add("*.PNG");
            list.Add("*.PSD ");
            list.Add("*.PSB");
            list.Add("*.PSP");
            list.Add("*.QTVR");
            list.Add("*.RAS");
            list.Add("*.RBE ");
            list.Add("*.○ JPEG-HDR");
            list.Add("*.○ Logluv TIFF");
            list.Add("*.SGI");
            list.Add("*.TGA");
            list.Add("*.TIFF ");
            // list.Add("*.○ TIFF");
            list.Add("*.EP");
            list.Add("*.○ TIFF");
            list.Add("*.IT");
            list.Add("*.UFO");
            list.Add("*.UFP");
            list.Add("*.WBMP");
            list.Add("*.WebP");
            list.Add("*.XBM");
            list.Add("*.XCF");
            list.Add("*.XPM");
            list.Add("*.XWD");
            list.Add("*.Raw");
            list.Add("*.CIFF");
            list.Add("*.DNG");
            //list.Add("*.Vector");
            list.Add("*.AI");
            list.Add("*.CDR");
            list.Add("*.CGM");
            list.Add("*.DXF");
            list.Add("*.EVA");
            list.Add("*.EMF");
            // list.Add("*.Gerber");
            list.Add("*.HVIF");
            list.Add("*.IGES");
            list.Add("*.PGML");
            list.Add("*.SVG");
            list.Add("*.VML");
            list.Add("*.WMF");
            list.Add("*.Xar");
             list.Add("*.Compound");
            list.Add("*.CDF");
            list.Add("*.DjVu");
            list.Add("*.EPS");
            list.Add("*.PDF");
            list.Add("*.PICT");
            list.Add("*.PS");
            list.Add("*.SWF");
            list.Add("*.XAML");


            return list;

        }

        protected override string GetDatedTargetPath(DateTime dt)
    {
      return Path.Combine(TargetDir, dt.Year.ToString(), dt.Month.ToString("00"));
    }
    
    protected override DateTime? GetFileDateTime(string path)
    {
      try
      {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        using (Image myImage = Image.FromStream(fs, false, false))
        {
          if (myImage.PropertyIdList.Any(x => x == 36867))
          {
            PropertyItem propItem = myImage.GetPropertyItem(36867);
            if (propItem == null || propItem.Value == null)
              return null;

            string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
            return DateTime.Parse(dateTaken);
          }
          return null;
        }
      }
      catch
      {
        return null;
      }
    }
  }
}
