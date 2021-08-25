using System;
using System.Net;
using System.Web;
using Eshop.Utilitiy.ZarinPal_Test;

namespace Eshop.Utilitiy.Payment
{
    public class Payments
    {
        public static bool ChargePayment(int amount, int chargeId)
        {
            string key = "کد مربوط به درگاه را در این قسمت قرار دهید";

            ServicePointManager.Expect100Continue = false;

            var zarinPall = new PaymentGatewayImplementationServicePortTypeClient();

            string result = "";

            int Code = zarinPall.PaymentRequest(key, amount,
                "نام شرکت", "ایمیل شرکت", "تلفن شرکت",
                "http://" + HttpContext.Current.Request.Url.Authority + "/OnlinePayment/" + chargeId, out result);

            if (Code == 100)
            {
                //HttpContext.Current.Response.Redirect("https://zarinpal.com/pg/StartPay/" + result);

                // این قطعه کد شامل درگاه تستی زرین پال می باشد
                // در صورت تایید میتوانید این قطعه را کامنت کرده و از کد بالا استفاده کنید که درگاه اصلی می باشد
                HttpContext.Current.Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result);
            }

            return false;

        }

        public static Tuple<int, long> ComingBackPayment(int chargeId, int amount)
        {
            string key = "eedff322-e32f-11e8-8701-005056a205be";

            var request = HttpContext.Current.Request;
            if (request.QueryString["Status"] != "" && request.QueryString["Status"] != null &&
                request.QueryString["Authority"] != "" && request.QueryString["Authority"] != null)
            {
                if (request.QueryString["Status"].ToString().Equals("OK"))
                {
                    long refid = 0;
                    var zarinPall =
                        new PaymentGatewayImplementationServicePortTypeClient();
                    int _status = zarinPall.PaymentVerification(key, request.QueryString["Authority"].ToString(),
                        amount, out refid);
                    Tuple<int, long> returnValue = new Tuple<int, long>(_status, refid);
                    return returnValue;
                }
            }
            Tuple<int, long> zero = new Tuple<int, long>(0, 0);
            return zero;
        }
    }
}
