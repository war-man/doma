using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace doma.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Int64 steamid = convertAccountIDtoSteamID("137040308");//Convert.ToInt64(4294967295);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private string convertAccountIDtoSteamID(string account_id)
        {
            // convert account_id to Int64
            Int64 x = Convert.ToInt64(account_id);
            string s = Convert.ToString(x, 2); //Convert to binary in a string

            StringBuilder result = new StringBuilder();
            result.Append("1000100000000000000000001");

            int dem_so_luong = 57 - 25 - s.Length;

            // add 0 to result to get enough 57 character
            for (int i = 0; i < dem_so_luong; i++)
            {
                result.Append("0");
            }

            // append the convert number to end result
            result.Append(s);

            BitArray bitarry = new BitArray(result.Length);


            for(int i = 0; i < result.Length; i++)
            {
                int index = result.Length - i - 1;
                if (result[index] == '0')
                {
                    bitarry[i] = false;
                }
                else
                {
                    bitarry[i] = true;
                }
            }       

            return  GetIntFromBitArray(bitarry).ToString();
        }

        private static long GetIntFromBitArray(BitArray bitArray)
        {
            var array = new byte[8];
            bitArray.CopyTo(array, 0);
            return BitConverter.ToInt64(array, 0);
        }
    }
}