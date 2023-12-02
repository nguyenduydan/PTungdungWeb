using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClass.Model;

namespace WebThucHanh.App_Start
{
    public static class SessionConfig
    {
        //1. Lưu session cho User
        public static void SetUser(Users user)
        {
            // Lưu vào session
            HttpContext.Current.Session["user"] = user;
        }

        //2. Lấy session cho User
        public static Users GetUser()
        {
            //Lấy vào session
            return (Users)HttpContext.Current.Session["user"];
        }

    }
}