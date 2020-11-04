using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _91App_Test.Models
{
    public class serialNum
    {
        dbOrderListEntities db = new dbOrderListEntities();
        public string getNum()
        {
            string result = "";
            var x = (from t in db.tShippingOrder
                     select t).Count();
            if (x == 0)
            {
                result = DateTime.Now.ToString("yyyyMMdd") + "0001";
            }
            else
            {
                var y = (from t in db.tShippingOrder
                         select t.fShippingOrderId).Max();
                int today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int frontEight = int.Parse(y.Substring(0, 8));
                int nextNum = int.Parse(y.Substring(y.Length - 4, 4)) + 1;
                if (today > frontEight)
                {
                    result = DateTime.Now.ToString("yyyyMMdd") + "0001";
                }
                else
                {
                    if (nextNum < 10)
                    {
                        result = today + "000" + nextNum;
                    }
                    if (nextNum >= 10 && nextNum < 100)
                    {
                        result = today + "00" + nextNum;
                    }
                    if (nextNum >= 100 && nextNum < 1000)
                    {
                        result = today + "0" + nextNum;
                    }
                    if (nextNum >= 1000 && nextNum < 10000)
                    {
                        result = (today + nextNum).ToString();
                    }
                }
            }
            return result;
        }
    }
}