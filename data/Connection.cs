using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace Forum.Application.data
{
    public class Connection
    {
        public ForumDBContext GetContext()
        {
            ForumDBContext sdc = new ForumDBContext(ConfigurationManager.ConnectionStrings["ForumDBContext"].ToString());
            return sdc;
        }

    }
}