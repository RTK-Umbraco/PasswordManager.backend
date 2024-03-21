using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.PaymentCards.Domain.Operations
{
    public sealed class OperationDataConstants
    {
        #region CreatePaymentCard
        public const string CreatePaymentCardUserId = "createPaymentCardUserId";
        public const string CreatePaymentCardCardNumber = "createPaymentCardCardNumber";
        public const string CreatePaymentCardCardholderName = "createPaymentCardCardholderName";
        public const string CreatePaymentCardExpirationDate = "createPaymentCardExpirationDate";
        #endregion

        #region UpdatePaymentCard
        public const string CurrentPaymentCardCardNumber = "currentPaymentCardCardNumber";
        public const string CurrentPaymentCardCardholderName = "currentPaymentCardCardholderName";
        public const string CurrentPaymentCardExpirationDate = "currentPaymentCardExpirationDate";

        public const string NewPaymentCardCardNumber = "newPaymentCardCardNumber";
        public const string NewPaymentCardCardholderName = "newPaymentCardCardholderName";
        public const string NewPaymentCardExpirationDate = "newPaymentCardExpirationDate";
        #endregion
    }
}
