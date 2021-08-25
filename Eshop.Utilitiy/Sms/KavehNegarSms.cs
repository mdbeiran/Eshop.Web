using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Utilitiy.Sms
{
    using Kavenegar;
    using Kavenegar.Models.Enums;

    public class KavehNegarSms
    {
        private KavenegarApi api = new KavenegarApi("6F70574D746D3874684D36723049477138314270686F756D6E38524A6E62386533584442577A6C5063726B3D");
        private string sender = "30004681";
        public void SendSms(string reciever, string text)
        {
            api.Send(sender, reciever, text);
        }

        public void SendVertifySms(string reciever, string text)
        {
            api.VerifyLookup(reciever, text, "Vertify", VerifyLookupType.Sms);
        }

        public void SendActiveCode(string reciever, string text)
        {
            api.VerifyLookup(reciever, text, "SendActiveCode", VerifyLookupType.Sms);
        }
    }
}
