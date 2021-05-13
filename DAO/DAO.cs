using System;
using System.Collections.Generic;
using System.Text;

namespace DAO
{
    public class DAO
    {
        public string connectionString = @"server=DESKTOP-8HAU83C\SQLEXPRESS;database=INFONEW;integrated security=yes;";

        public string ConnString 
        { 
            get
            {
                return this.connectionString;
            }
        }
    }
}
