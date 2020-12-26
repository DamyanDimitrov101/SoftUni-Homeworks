using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data.Common
{
    public static class CommonData
    {
        public const int CustomerNameMaxLen = 100;
        public const int CustomerEmailMaxLen = 80;


        public const int ProductNameMaxLen = 50;
        public const int ProductDescriptionMaxLen = 250;

        public const int StoreNameMaxLen = 80;

        public const string ConnectionString = @"Server=USERPC\SQLEXPRESS;Database=SalesDatabase;Integrated Security=true";
    }
}
