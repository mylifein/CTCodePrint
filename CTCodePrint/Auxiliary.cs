using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Common
{
    public static class Auxiliary
    {
        public static string Get_SQLConnStr()
        {
            string SQLConnStr = ConfigurationManager.ConnectionStrings["ConnectionMySqlString"].ConnectionString.ToString();     //讀<connectionStrings>節中的ConnectionString鍵的值
            return SQLConnStr;
        }

        public static string Get_UserName()
        {
            string username = ConfigurationManager.AppSettings["UserName"].ToString().Trim();             //讀<appSettings>
            return username.Trim();
        }

        public static string Get_Password()
        {
            string password = ConfigurationManager.AppSettings["Password"].ToString().Trim();
            return password.Trim();
        }

        //自定義方法,判斷輸入的是不是整數或小數
        public static bool isNumber(string aaa)
        {
            //Regex reg = new Regex("^[0-9]+$");     //對象實例化  //只能是0-9的數字字符

            Regex reg = new Regex("^\\d+(\\.\\d+)?$");              //成功     //整數及小數的字符
            Match ma = reg.Match(aaa);
            if (ma.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //自定義方法,判斷輸入的是不是整數或小數
        public static bool isNumber2(string aaa)
        {
            Regex reg = new Regex("^[0-9]+$");     //對象實例化  //只能是0-9的數字字符

            //Regex reg = new Regex("^\\d+(\\.\\d+)?$");              //成功     //整數及小數的字符
            Match ma = reg.Match(aaa);
            if (ma.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDate(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }

        //得到一個字符串的字節數,如果是得到字符個數,用.Length屬性即可.
        public static int GetBytes(string str)
        {
            int totalBytes = System.Text.Encoding.Default.GetBytes(str).Length;
            return totalBytes;
        }

        //根據字節長度,返回字符串
        public static string GetStringByBytes(string str, int byteLen)
        {
            int totalBytes=0;
            string totalStr = "";
            string currentStr;


            //按字符個數循環
            for (int i = 0; i < str.Length; i++)
            {
                currentStr = str.Substring(i, 1);         //當前取到的字符
                totalStr += currentStr;                   //累加字符
                totalBytes += GetBytes(currentStr);       //累加字節數

                //判斷字節數是否大於或等於指定的字節數
                if (totalBytes >= byteLen)
                {
                    break;                                //跳出for循環
                }

            }

            return totalStr;
        }
    }
}
