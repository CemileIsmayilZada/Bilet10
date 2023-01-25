using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Utilities
{
    public static class Helper
    {
        public static bool DeleteFile(params string[] paths)
        {
            string resultPath = string.Empty;
            foreach (var item in paths)
            {
                resultPath = Path.Combine(resultPath, item);
            }
            if (File.Exists(resultPath))
            {
                File.Delete(resultPath);
                return true;
            }
            return false;
        }
    }
}
