using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Password.Domain.VaultObject.ValueObjects
{
    public class PaymentCardData
    {
        public string CardNumber { get; private set; }
        public string ExpiryDate { get; private set; }
        public string Cvv { get; private set; }
        public string FriendlyName { get; private set; }

        public PaymentCardData(string cardNumber, string expiryDate, string cvv, string friendlyName)
        {
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            Cvv = cvv;
            FriendlyName = friendlyName;
        }
    }
}
