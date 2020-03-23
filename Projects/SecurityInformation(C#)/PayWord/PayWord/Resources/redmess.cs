using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayWord.Resources
{
    public class redmess
    {
        payment lpay;
        ucommit ucomm;

        public redmess(payment pw, ucommit ucom)
        {
            lpay = pw;
            ucomm = ucom;
        }

        public payment getlpay()
        {
            return lpay;
        }
        public ucommit getucomm()
        {
            return ucomm;
        }
    }
}
