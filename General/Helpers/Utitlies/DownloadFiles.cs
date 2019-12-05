using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace General.Helpers.Utilities
{
    //این کلاس برای زمانیست که اطلاعات فایل ها صرفا در پوشه و نه در بانک اطلاعات و پوشه ثبت میگردند
    public class DownloadFiles
    {
        public List<Models.RegisterViewModel> GetFiles()
        {
            List<Models.RegisterViewModel> lstFiles = new List<Models.RegisterViewModel>();
            DirectoryInfo dirInfo = new DirectoryInfo(HostingEnvironment.MapPath("~/MyFiles"));
            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {
                lstFiles.Add(new Models.RegisterViewModel()
                {
                    //UserName = i=i-1,
                    //Pic = dirInfo.FullName + @"\" + item.Name
                });
                i = i + 1;
            }
            return lstFiles;
        }

    }
}