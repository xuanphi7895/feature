using System.Collections.Generic;
using Core.Enum;
using System;

namespace Core.Entities {
    public class Transaction : BaseEntity {
        public DateTime TransactionDate { set; get; }
        public string ExternalTransactionId { set; get; }
        public decimal Amount { set; get; }
        public decimal Fee { set; get; }
        public string Result { set; get; }
        public string Message { set; get; }
        public TransactionStatus Status { set; get; }
        public string Provider { set; get; }
    }
}