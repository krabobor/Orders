using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using w1.Models;

namespace w1.Service
{
    public class DBInitialization
    {
        public static DefaultContext InitializeDB()
        {
            return new DefaultContext();
        }
    }
}