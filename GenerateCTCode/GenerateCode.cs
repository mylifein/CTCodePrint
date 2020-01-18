using DBUtility;
using Model;
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
        private readonly BLL.PrintModelQ printM = new BLL.PrintModelQ();

        //string po,string cusNo,string workNo,string macType,string cusMatNo,string officialNo,string verNo
        public List<CTCode> generateCTNumber(CTCode ctCodeInfo,int printQty)
        {
            bool judgeSerial = false;                   //判断是否需要流水号
            List<CTCode> listCode = new List<CTCode>();
            StringBuilder ctCode = new StringBuilder();
            DataSet ds = selectControl.getRulesByNo(ctCodeInfo.Mactype);
            if(ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string ruleType = dr["rule_type"].ToString();
                    ctCodeInfo.Ruleno = dr["rule_no"].ToString();
                    int ruleLength = Convert.ToInt32(dr["rule_length"].ToString());
                    string ruleValue = dr["rule_value"].ToString();
                    switch (ruleType.Trim())
                    {
                        case "T001":
                            string poOrWorkNo = "";
                            if (ctCodeInfo.Cuspo != null && ctCodeInfo.Cuspo != "")
                            {
                                poOrWorkNo = ctCodeInfo.Cuspo;
                            }
                            else
                            {
                                poOrWorkNo = ctCodeInfo.Workno;
                            }
                            ctCode.Append(poOrWorkNo.Substring(0, ruleLength));
                            break;
                        case "T002":
                            ctCode.Append(GenerateTimeCode(ruleLength));
                            break;
                        case "T003":
                            if (ctCodeInfo.Cusmatno.Length > ruleLength)
                            {
                                ctCode.Append(ctCodeInfo.Cusmatno.Trim().Substring(0, ruleLength));
                            }
                            else
                            {
                                ctCode.Append("");
                            }
                            break;
                        case "T004":
                            ctCode.Append(ctCodeInfo.Offino.Trim().Substring(0, ruleLength));
                            break;
                        case "T005":
                            ctCode.Append(ctCodeInfo.Verno.Trim().Substring(0, ruleLength));
                            break;
                        case "T006":
                            judgeSerial = true;
                            string maxCode = printM.getMaxCTCode(ctCode.ToString(), ctCodeInfo.Delmatno);
                            string prefixCT = ctCode.ToString();
                            if (maxCode == null || maxCode == "")
                            {
                                for (int i = 1; i <= printQty; i++)
                                {
                                    CTCode ctCodeIn = new CTCode();
                                    ctCodeIn = exchangeCT(ctCodeIn, ctCodeInfo);
                                    string seqNo = "";
                                    string tempCT = "";
                                    string seqCode = Convert34Code(i);
                                    for (int numLength = seqCode.Length; numLength < ruleLength; numLength++)
                                    {
                                        seqNo += "0";
                                    }
                                    seqNo += seqCode;
                                    tempCT = prefixCT + seqNo;
                                    ctCodeIn.Ctcode = tempCT;
                                    listCode.Add(ctCodeIn);
                                }
                            }
                            else
                            {
                                //獲取流水號
                                string subCode = maxCode.Substring(maxCode.Length - ruleLength);
                                int ctNo = convert34CodeTo10(subCode);
                                for (int i = 0; i < printQty; i++)
                                {
                                    CTCode ctCodeIn = new CTCode();
                                    ctCodeIn = exchangeCT(ctCodeIn, ctCodeInfo);
                                    ctNo++;
                                    string ct34Code = Convert34Code(ctNo);
                                    string temStr = "";
                                    string tempCT = "";
                                    for (int j = ct34Code.Length; j < ruleLength; j++)
                                    {
                                        temStr += 0;
                                    }
                                    ct34Code = temStr + ct34Code;
                                    tempCT = prefixCT + ct34Code;
                                    ctCodeIn.Ctcode = tempCT;
                                    listCode.Add(ctCodeIn);
                                }

                            }
                            //ctCode.Append(GenerateRandom(ruleLength));
                            break;
                        case "T007":
                            ctCode.Append(ruleValue);
                            break;
                        case "T008":
                            string subOperation = this.extractString(ctCodeInfo.SoOrder, ruleLength);
                            ctCode.Append(subOperation);
                            break;

                    }
                }
            }
            if (!judgeSerial)
            {
                for (int i = 1; i <= printQty; i++)
                {
                    CTCode ctCodeIn = new CTCode();
                    ctCodeIn = exchangeCT(ctCodeIn, ctCodeInfo);
                    ctCodeIn.Ctcode = ctCode.ToString();
                    listCode.Add(ctCodeIn);
                }
            }
            return listCode;

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
                    timeString.Append(getWeek());
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
        /// 设置34进制编码 
        /// </summary>
        public static Dictionary<int, string> Base34Code = new Dictionary<int, string>() {
            {   0  ,"0"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},
            {   10  ,"A"}, {   11  ,"B"}, {   12  ,"C"}, {   13  ,"D"}, {   14  ,"E"}, {   15  ,"F"}, {   16  ,"G"}, {   17  ,"H"}, {   18  ,"J"}, {   19  ,"K"},
            {   20  ,"L"}, {   21  ,"M"}, {   22  ,"N"}, {   23  ,"P"}, {   24  ,"Q"}, {   25  ,"R"}, {   26  ,"S"}, {   27  ,"T"}, {   28  ,"U"}, {   29  ,"V"},
            {   30  ,"W"}, {   31  ,"X"}, { 32,"Y"},{33,"Z" }
        };

        public static Dictionary<string, int> InverseBase34Code = new Dictionary<string, int>() {
            {   "0",0}, {   "1"  ,1}, {   "2"  ,2}, {   "3" ,3}, {   "4",4}, {   "5"  ,5}, {   "6" ,6}, {   "7",7}, {   "8" ,8}, {   "9"  ,9},
            {   "A",10  }, {   "B",11}, {   "C",12 }, {   "D",13  }, {   "E",14  }, {   "F",15  }, {   "G",16 }, {   "H",17 }, {   "J",18  }, {   "K",19  },
            {   "L",20  }, {   "M",21  }, {   "N",22  }, {   "P",23  }, {   "Q",24  }, {   "R",25  }, {   "S",26  }, {   "T",27  }, {   "U",28  }, {   "V",29  },
            {   "W",30  }, {   "X",31  }, { "Y",32},{"Z",33 }
        };

        /// <summary>
        /// 设置日期和月份的Dictionary 
        /// </summary>
        public static Dictionary<int, string> Base32Code = new Dictionary<int, string>() {
            {   0  ,"0"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},
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


        /// <summary>
        /// 将数字转换为34进制
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Convert34Code(int num)
        {
            if (num < 0)
            {
                return "";
            }
            //10进制数所对应的34进制数
            char[] rule = new char[] {'0','1','2','3','4','5','6','7','8','9',
                              'A','B','C','D','E','F','G','H','J','K',
                              'L','M','N','P','Q','R','S','T','U','V',
                              'W','X','Y','Z',};

            //保存除34后的余数
            List<int> list = new List<int>();
            while (num >= 34)
            {
                int a = num % 34;
                num /= 34;
                list.Add(a);
            }
            list.Add(num);

            StringBuilder sb = new StringBuilder();
            //结果要从后往前排
            for (int i = list.Count - 1; i >= 0; i--)
            {
                sb.Append(rule[list[i]]);
            }
            return sb.ToString();
        }

        public static int convert34CodeTo10(string convertCode)
        {
            int tenCode = 0;
            for(int i = 0; i <= convertCode.Length - 1; i++)
            {
                int loop = InverseBase34Code[convertCode[i].ToString()];
                for (int j = 1; j < convertCode.Length - i ; j++)
                {
                    loop *= 34;
                }
                tenCode += loop;
            }
            return tenCode;
        }
        
        public CTCode exchangeCT(CTCode ctCode1, CTCode ctCode2)
        {
            ctCode1.Uuid = Auxiliary.Get_UUID();
            ctCode1.Workno = ctCode2.Workno;
            ctCode1.Cusno = ctCode2.Cusno;
            ctCode1.Cuspo = ctCode2.Cuspo;
            ctCode1.Cusname = ctCode2.Cusname;
            ctCode1.Orderqty = ctCode2.Orderqty;
            ctCode1.Cusmatno = ctCode2.Cusmatno;
            ctCode1.SoOrder = ctCode2.SoOrder;
            ctCode1.Opuser = Auxiliary.loginName;
            ctCode1.Woquantity = ctCode2.Woquantity;
            ctCode1.Completedqty = ctCode2.Completedqty;
            ctCode1.Mactype = ctCode2.Mactype;
            ctCode1.Offino = ctCode2.Offino;
            ctCode1.Delmatno = ctCode2.Delmatno;
            ctCode1.Ruleno = ctCode2.Ruleno;
            ctCode1.Verno = ctCode2.Verno;
            ctCode1.Modelno = ctCode2.Modelno;
            ctCode1.Createtime = DateTime.Now.ToString();
            return ctCode1;
        } 

        public string extractString(string operaField,int ruleLength)
        {
            string result = "";
            if (operaField.Count() < ruleLength)
            {
                result = operaField;
            }else
            {
                result = operaField.Substring(0, ruleLength);
            }
            int fieldLen = result.Count();
            if (fieldLen < ruleLength)
            {
                for(int i = fieldLen; i < ruleLength; i++)
                {
                    result = 0 + result;
                }
            }
            return result;
        }
    }
}
