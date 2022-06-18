using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.StaticDetails
{
    public static class PaymentMethods
    {
        public const string PaymendMethodOnDelivery = "Płatność przy odbiorze";

        public const string PaymendMethodOnline = "Płatność ekspresowa (karta, p24)";

        public const string PaymentMethodBankTransfer = "Tradycyjny przelew bankowy";

    }
}
