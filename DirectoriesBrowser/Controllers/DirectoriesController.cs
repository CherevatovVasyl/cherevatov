using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using DirectoriesBrowser.Models;
using System.Web;

namespace DirectoriesBrowser.Controllers
{

    
    public class DirectoriesController : ApiController
    {
        //api/durectories?path=
        public IHttpActionResult Get(string path = @"c:\")
        { 
            path = HttpUtility.UrlDecode(path);
            
            DirectoryInfoCust dir = new DirectoryInfoCust();

            if (path == "0")
            {
                dir.Drives = Directory.GetLogicalDrives().ToList();
                return Json(dir);
            }
            
            DirectoryInfo DI = new DirectoryInfo(path);
            dir.Name = DI.FullName;

            try
            {
                dir.Files = (from c in DI.GetFiles() select c.Name).ToList();
                dir.Subdirectories = (from c in DI.GetDirectories() select c.Name).ToList();
                dir.Drives = Directory.GetLogicalDrives().ToList();
            }
            catch (UnauthorizedAccessException ex)
            {
                dir.Message = "Error1";
            }
            catch (System.IO.IOException ex)
            {
                dir.Message = "Error2";
            }
            return Json(dir);
        }

        public IHttpActionResult GetFilesCount(string path = @"")
        {
            path = HttpUtility.UrlDecode(path);
            FilesCount fc = new FilesCount(0,0,0);

            List<long> allFilesLenght = GetCount(path);
            foreach (var c in allFilesLenght)
            {
                if (c <= 10000000) fc.Less10MbCount++;
                else if (c > 10000000 && c <= 50000000) fc.More10Less50MbCount++;
                else if (c >= 100000000) fc.More50MbCount++;
            }

            return Json(fc);


        }
        private List<long> GetCount(string path)
        {
            var files = new List<long>();
            DirectoryInfo DI = new DirectoryInfo(path);

            
            files.AddRange(from file in DI.GetFiles("*.*", SearchOption.TopDirectoryOnly) select file.Length);

            try
            {
                foreach (var directory in Directory.GetDirectories(path))
                {

                    files.AddRange(GetCount(directory));
                }   
            }
            catch (UnauthorizedAccessException) { }
            catch (System.ArgumentException) { }            
            
            return files;
        }
    }

    
}
