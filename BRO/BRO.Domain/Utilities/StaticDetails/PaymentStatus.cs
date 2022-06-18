using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.StaticDetails
{
    public static class PaymentStatus
    {
        public const string PaymentStatusPending = "Oczekujące";

        public const string PaymentStatusApproved = "Zatwierdzone";

        public const string PaymentStatusCanceled = "Anulowane";

        public const string PaymentStatusRefunded = "Zwrócone";
    }
}
