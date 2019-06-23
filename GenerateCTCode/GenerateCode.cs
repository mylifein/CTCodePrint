using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCTCode
{
    public class GenerateCode
    {
        private readonly BLL.SelectQuery selectControl = new BLL.SelectQuery();

        public StringBuilder generateCTNumber(string po,string cusNo,string workNo,string macType,string cusMatNo,string officialNo,string verNo)
        {
            StringBuilder ctCode = new StringBuilder();
            DataSet ds = selectControl.getRulesByNo(cusNo,macType);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string ruleType = dr["rule_type"].ToString();
                int ruleLength = Convert.ToInt32(dr["rule_length"].ToString().Trim());
                string ruleValue = dr["rule_value"].ToString().Trim();
                switch (ruleType.Trim())
                {
                    case "T001":
                        string poOrWorkNo = "";
                        if (po != null && po.Trim() != "")
                        {
                            poOrWorkNo = po.Trim();
                        }
                        else
                        {
                            poOrWorkNo = workNo.Trim();
                        }
                        ctCode.Append(poOrWorkNo.Substring(0, ruleLength));
                        break;
                    case "T002":
                        ctCode.Append(GenerateTimeCode(ruleLength));
                        break;
                    case "T003":
                        ctCode.Append(cusMatNo.Trim().Substring(0, ruleLength));
                        break;
                    case "T004":
                        ctCode.Append(officialNo.Trim().Substring(0, ruleLength));
                        break;
                    case "T005":
                        ctCode.Append(verNo.Trim().Substring(0, ruleLength));
                        break;
                    case "T006":
                        ctCode.Append(GenerateRandom(ruleLength));
                        break;
                    case "T007":
                        ctCode.Append(ruleValue);
                        break;

                }
            }
            return ctCode;

        }


      


        /// <summary>
        /// 定义字符数组
        /// </summary>
        private static char[] constant =
       {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };

        /// <summary>
        /// 根据参数长度，产生对应长度的流水号
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>

        public static string GenerateRandom(int length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for(int i = 0; i < length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString().ToUpper();
        }


        /// <summary>
        /// 根据传入参数，获取当前日期的年周别或年度或
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateTimeCode(int length)
        {

            System.Text.StringBuilder timeString = new System.Text.StringBuilder(10);


            switch (length)
            {
                case 2:
                    //若为2位，只取周别
                    timeString.Append(getWeek());
                    break;
                case 3:
                    //若为3位，年周别，分别为1位，2位
                    string yearString = DateTime.Now.Year.ToString();
                    timeString.Append(yearString.Substring(yearString.Length - 1));
                    timeString.Append(getWeek());
                    break;
                case 4:
                    //获得4位，年周别，分别为2位
                    timeString.Append(DateTime.Now.ToString("yy"));
                    string weekString = "";
                    break;
                case 6:
                    //获得6位,获得年、月、日
                    timeString.Append(DateTime.Now.ToString("yyMMdd"));
                    break;
                case 7:
                    //获得7位,获得4位年，2位周别,1位日
                    timeString.Append(DateTime.Now.ToString("yyyy"));
                    timeString.Append(getWeek());
                    timeString.Append(IntConvert(DateTime.Now.Day));
                    break;
            }

            

            return timeString.ToString();
        }

        /// <summary>
        /// 获得星期
        /// </summary>
        /// <returns></returns>
        public static string getWeek()
        {
            GregorianCalendar gc = new GregorianCalendar();
            string weekString = "";
            int week = gc.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (week.ToString().Length < 2)
            {
                weekString = "0" + week.ToString();
            }
            else
            {
                weekString = week.ToString();
            }
            return weekString;
        }

        /// <summary>
        /// 64位Base码
        /// </summary>
        public static Dictionary<int, string> Base64Code = new Dictionary<int, string>() {
            {   0  ,"z"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},
            {   10  ,"a"}, {   11  ,"b"}, {   12  ,"c"}, {   13  ,"d"}, {   14  ,"e"}, {   15  ,"f"}, {   16  ,"g"}, {   17  ,"h"}, {   18  ,"i"}, {   19  ,"j"},
            {   20  ,"k"}, {   21  ,"x"}, {   22  ,"m"}, {   23  ,"n"}, {   24  ,"y"}, {   25  ,"p"}, {   26  ,"q"}, {   27  ,"r"}, {   28  ,"s"}, {   29  ,"t"},
            {   30  ,"u"}, {   31  ,"v"}, {   32  ,"w"}, {   33  ,"x"}, {   34  ,"y"}, {   35  ,"z"}, {   36  ,"A"}, {   37  ,"B"}, {   38  ,"C"}, {   39  ,"D"},
            {   40  ,"E"}, {   41  ,"F"}, {   42  ,"G"}, {   43  ,"H"}, {   44  ,"I"}, {   45  ,"J"}, {   46  ,"K"}, {   47  ,"L"}, {   48  ,"M"}, {   49  ,"N"},
            {   50  ,"O"}, {   51  ,"P"}, {   52  ,"Q"}, {   53  ,"R"}, {   54  ,"S"}, {   55  ,"T"}, {   56  ,"U"}, {   57  ,"V"}, {   58  ,"W"}, {   59  ,"X"},
            {   60  ,"Y"}, {   61  ,"Z"}, {   62  ,"-"}, {   63  ,"_"},
        };

        public static Dictionary<string, int> _Base64Code
        {
            get
            {            

                return Enumerable.Range(0, Base64Code.Count()).ToDictionary(i => Base64Code[i], i => i);
            }
        }

        /// <summary>
        /// 获取32进制数
        /// </summary>
        /// <param name="convertValue"></param>
        /// <returns></returns>
        public static string IntToi32(long convertValue)
        {
            string a = "";
            while (convertValue >= 1)
            {
                int index = Convert.ToInt16(convertValue - (convertValue / 32) * 32);
                a = Base64Code[index] + a;
                convertValue = convertValue / 32;
            }
            return a;
        }

        /// <summary>
        /// 设置日期和月份的Dictionary 
        /// </summary>
        public static Dictionary<int, string> Base32Code = new Dictionary<int, string>() {
            {   0  ,"z"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},
            {   10  ,"A"}, {   11  ,"B"}, {   12  ,"C"}, {   13  ,"D"}, {   14  ,"E"}, {   15  ,"F"}, {   16  ,"G"}, {   17  ,"H"}, {   18  ,"I"}, {   19  ,"J"},
            {   20  ,"K"}, {   21  ,"L"}, {   22  ,"M"}, {   23  ,"N"}, {   24  ,"O"}, {   25  ,"P"}, {   26  ,"Q"}, {   27  ,"R"}, {   28  ,"S"}, {   29  ,"T"},
            {   30  ,"U"}, {   31  ,"V"}
        };


        /// <summary>
        /// 将日期和月份转换
        /// </summary>
        /// <param name="convertValue"></param>
        /// <returns></returns>
        public static string IntConvert(int convertValue)
        {
            return Base32Code[convertValue];
        }

        

    }
}
