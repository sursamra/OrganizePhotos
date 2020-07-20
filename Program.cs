using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Drawing;

namespace OrganizePhotos
{
  enum OrganizationType { Photo, Video,Both,DateInName};

  class Program
  {     

    static void Main(string[] args)
    {       

    string  source = args[0];//@"C:\Samra\Phone_Pics";
      string target = args[1];//@"C:\Samra\Phone_Pics2";
      int a = int.Parse( args[2].ToString());
      OrganizationType oganizationType = (OrganizationType)a;
      bool valid = true;

      if (!Directory.Exists(source))
      {
        Console.WriteLine("Directory {0} does not exists", source);
        valid = false;
      }

      if ( valid && !Directory.Exists(target))
      {
        Console.WriteLine("Directory {0} does not exists . Do you want to create ? Y/N", target);
                string s = Console.ReadLine();
                if (s.ToUpper() == "Y")
                    Directory.CreateDirectory(target);
                else
                    valid = false;
      }

      if (valid)
      {
        switch (oganizationType)
        {
          case OrganizationType.Photo:
            {
              PhotoOrganizer po = new PhotoOrganizer(source,target);
              po.Organize();
              break;
            }
          case OrganizationType.Video:
            {
              VideoOrganizer vo = new VideoOrganizer(source, target);
              vo.Organize();
              break;
            }
                    case OrganizationType.DateInName:
                        {
                            LogOrganizer vo = new LogOrganizer(source, target);
                            vo.FixIssuesFromLog();
                            break;
                        }

                    default:
            {
              PhotoOrganizer po = new PhotoOrganizer(source, target);
              po.Organize();
              VideoOrganizer vo = new VideoOrganizer(source, target);
              vo.Organize();
              break;
            }
        }
        
      }

      Console.WriteLine("Finished ! press any key");
      Console.Read();
    }

    

  
}
}
