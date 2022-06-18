using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities.StaticDetails
{
    public static class OrderStatus
    {
        public const string OrderStatusPending = "Oczekujące na zatwierdzenie";

        public const string OrderStatusInProcess = "Przetwarzane";

        public const string OrderStatusShipped = "Wysłane";

        public const string OrderStatusDelivered = "Dostarczone";

        public const string OrderStatusCancelled = "Anulowane";

        public const string OrderStatusRefunded = "Zwrócone";
    }
}
