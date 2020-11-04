using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _91App_Test.Models
{
    public class payMethod
    {
        public void pm(tProduct x)
        {
            if (x.fPayBy != null)
            {
                foreach (var i in x.fPayBy)
                {
                    switch (i)
                    {
                        case 1:
                            x.fPayByArrive = 1;
                            break;
                        case 2:
                            x.fPayByLinePay = 1;
                            break;
                        case 3:
                            x.fPayByJKO = 1;
                            break;
                        case 4:
                            x.fPayByCard = 1;
                            break;
                    }
                }
            }
        }
        public void dm(tProduct x)
        {
            if (x.fDeliveryTo != null)
            {
                foreach (var i in x.fDeliveryTo)
                {
                    switch (i)
                    {
                        //case 1:
                        //    x.ftoHome = 1;
                        //    break;
                        //case 2:
                        //    x.ftoconvenientStore = 1;
                        //    break;
                    }
                }
            }
        }
    }
}