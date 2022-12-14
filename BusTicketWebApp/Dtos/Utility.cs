using BusTicketWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BusTicketWebApp.Dtos
{
    public class Utility
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

        public static List<PaymentMethod> GetPaymentMethods()
        {

            return new List<PaymentMethod>
                {
                    new PaymentMethod{Id=1,PaymentMethodName="Payment Method 1"},
                    new PaymentMethod{Id=2,PaymentMethodName="Payment Method 2"},
                };
        }

        public static List<TicketType> GetTicketTypes()
        {

            return new List<TicketType>
                {
                    new TicketType{Id=1,TypeName="day Pass"},
                };
        }

        public static List<int> GetAdultNumbers()
        {

            return new List<int>
                {
                   1,2,3,4,5,6,7,8,9,10
                };
        }
        public static List<int> GetChildNumbers()
        {

            return new List<int>
                {
                   0,1,2,3,4,5,6,7,8,9
                };
        }
        public static string GetCustomDate(string strReceivedDate)
        {
            string strReturnDate = "";
            if (!string.IsNullOrEmpty(strReturnDate))
            {
                DateTime strTmpReceivedDate = Convert.ToDateTime(strReceivedDate);
                int intReceivedDay = strTmpReceivedDate.Day;
                string strReceivedMonth = getAbbreviatedName(strTmpReceivedDate.Month, strReceivedDate);
                strReturnDate = intReceivedDay + " " + strReceivedMonth + " " + strTmpReceivedDate.Year;
            }
            else
            {
                return "Please send valid date!";
            }

            return strReturnDate;
        }
        static string getAbbreviatedName(int month, string strReceivedDate)
        {
            DateTime date = new DateTime(Convert.ToDateTime(strReceivedDate).Year, month, 1);

            return date.ToString("MMM");
        }
    }
}