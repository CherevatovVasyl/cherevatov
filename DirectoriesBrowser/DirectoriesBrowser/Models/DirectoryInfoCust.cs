using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DirectoriesBrowser.Models
{
    public class DirectoryInfoCust
    {
        public string Message { get; set; }

        public List<string> Drives { get; set; }

        public string Name { get; set; }
        public List<string> Subdirectories { get; set; }
        public List<string> Files { get; set; }



    }

    public class FilesCount
    {
        public int Less10MbCount { get; set; }
        public int More10Less50MbCount { get; set; }
        public int More50MbCount { get; set; }

        public FilesCount(int range1, int range2, int range3)
        {
            Less10MbCount = range1;
            More10Less50MbCount = range2;
            More50MbCount = range3;

        }
    }
}