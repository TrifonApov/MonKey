namespace MonKey.Data.Common
{
    public static class EntityValidation
    {
        public static class Account
        {
            public const int AccountNameMinLength = 3;
            public const int AccountNameMaxLength = 15;
            public const int DescriptionNameMinLength = 5;
            public const int DescriptionNameMaxLength = 200;
        }

        public static class Category
        {
            public const int CategoryNameMinLength = 3;
            public const int CategoryNameMaxLength = 15;

            public const int IconUrlNameMaxLength = 2048;
        }

        public static class Transaction
        {
            public const string MoneyPrecisionType = "decimal(14,2)";
            public const string ExchangeRatePrecisionType = "decimal(18,8)";
        }

        public static class Currency
        {
            public const int CurrencyNameMinLength = 3;
            public const int CurrencyNameMaxLength = 15;
            public const int FixedCurrencyCodeLength = 3;
        }
    }
}
