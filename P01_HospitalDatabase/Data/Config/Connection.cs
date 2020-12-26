using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Config
{
    public static class Connection
    {
        public static string ConnectionString => @"Server=USERPC\SQLEXPRESS;Database=HospitalDatabase;Integrated Security = true";
    }
}
