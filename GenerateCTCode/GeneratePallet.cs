﻿using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GenerateCTCode
{
    public class GeneratePallet
    {

        private readonly static PalletService palletService = new PalletService();
        private readonly static WoBatchService woBatchService = new WoBatchService();


        public static Pallet generatePalletNo(Pallet pallet, CodeRule codeRule)
        {
            StringBuilder palletNo = new StringBuilder();        
            if (codeRule != null)
            {
                foreach (RuleItem ruleItem in codeRule.RuleItem)
                {
                    int ruleInt = ruleItem.Rulelength;
                    switch (ruleItem.Ruletype)
                    {
                        case "T001":
                            string poOrWorkNo = "";
                            if (pallet.Cuspo != null && pallet.Cuspo != "")
                            {
                                poOrWorkNo = pallet.Cuspo;
                            }
                            else
                            {
                                poOrWorkNo = pallet.Workno;
                            }
                            palletNo.Append(poOrWorkNo.Substring(0, ruleInt));
                            break;
                        case "T002":
                            palletNo.Append(GenerateTimeCode(ruleInt));
                            break;
                        case "T003":
                            palletNo.Append(pallet.Cusmatno.Trim().Substring(0, ruleInt));
                            break;
                        case "T004":
                            break;
                        case "T005":
                            break;
                        case "T006":        //流水碼34進制
                            break;
                        case "T007":
                            palletNo.Append(ruleItem.Rulevalue);
                            break;
                        case "T008":
                            string subOperation = extractString(pallet.SoOrder, ruleInt);
                            palletNo.Append(subOperation);
                            break;
                        case "T009":
                            //机种号3-7位
                            palletNo.Append(pallet.Delmatno.Substring(2, 5));
                            break;
                        case "T010":
                            break;
                        case "T011":
                            //流水码十六进制
                            string maxPallet = palletService.queryMaxPalletNo(palletNo.ToString());
                            string prefixPallet = palletNo.ToString();
                            if (maxPallet == null || maxPallet == "")
                            {
                                string seqCode = "1";
                                for (int numLength = seqCode.Length; numLength < Convert.ToInt32(ruleItem.Rulelength); numLength++)
                                {
                                    seqCode = "0" + seqCode;
                                }
                                palletNo.Append(seqCode);
                            }
                            else
                            {
                                //獲取流水號
                                string subCode = maxPallet.Substring(prefixPallet.Length, Convert.ToInt32(ruleItem.Rulelength));
                                int palletSeq = convert16CodeTo10(subCode);
                                string pallet16Code = Convert16Code(palletSeq + 1);
                                string seqNo = "";
                                for (int i = pallet16Code.Length; i < Convert.ToInt32(ruleItem.Rulelength); i++)
                                {
                                    seqNo += 0;
                                }
                                pallet16Code = seqNo + pallet16Code;
                                palletNo.Append(pallet16Code);

                            }
                            break;
                        case "T012":
                            String prefix = palletNo.ToString();
                            int modNum = 0;
                            foreach (char result in prefix.ToCharArray())
                            {
                                string exchangeInt = result.ToString();
                                int numExchange = inverseBase43Code[exchangeInt];
                                modNum += numExchange;
                            }
                            int modReuslt = modNum % 43;
                            string modString = base43Code[modReuslt];
                            palletNo.Append(modString);
                            break;
                        case "T013":                //十進制流水碼
                            string maxPallet10 = palletService.queryMaxPalletNo(palletNo.ToString());
                            string prefixPallet10 = palletNo.ToString();
                            if (maxPallet10 == null || maxPallet10 == "")
                            {
                                string seqCode = "1";
                                for (int numLength = seqCode.Length; numLength < Convert.ToInt32(ruleItem.Rulelength); numLength++)
                                {
                                    seqCode = "0" + seqCode;
                                }
                                palletNo.Append(seqCode);
                            }
                            else
                            {
                                //獲取流水號
                                string subCode10 = maxPallet10.Substring(prefixPallet10.Length, Convert.ToInt32(ruleItem.Rulelength));
                                int palletSeq10 = int.Parse(subCode10);
                                string pallet10Code = (palletSeq10 + 1).ToString();
                                string seqNo = "";
                                for (int i = pallet10Code.Length; i < Convert.ToInt32(ruleItem.Rulelength); i++)
                                {
                                    seqNo += 0;
                                }
                                pallet10Code = seqNo + pallet10Code;
                                palletNo.Append(pallet10Code);

                            }

                            break;
                        case "T015":                //年月日進制表示
                            palletNo.Append(getInsuprTime());
                            break;
                        case "T016":                        //浪潮批次号        


                            string woBatchNo = woBatchService.getBatchNoByWO(pallet.Workno);            //查询工单批次号 
                            string batchSeqNo = "1";
                            if (woBatchNo != null && !woBatchNo.Equals(""))
                            {
                                pallet.BatchNo = woBatchNo;
                                palletNo.Clear();
                                palletNo.Append(woBatchNo);
                            }
                            else
                            {
                                string queryCond = getInsuprTime() + "Q";
                                List<String> batchNos = woBatchService.queryBatchNos(queryCond);
                                if (batchNos != null && batchNos.Count > 0)
                                {
                                    int maxNo = 0;
                                    foreach (string batchNo in batchNos)
                                    {

                                        int tempMathNo = InverseBase34Code[batchNo.Substring(batchNo.Length - 1, 1)];
                                        if (tempMathNo > maxNo)
                                        {
                                            maxNo = tempMathNo;
                                        }
                                    }
                                    batchSeqNo = Base34Code[maxNo + 1];
                                }
                                palletNo.Append(batchSeqNo);
                                pallet.BatchNo = palletNo.ToString();
                                //保存工单批次
                                WoBatch woBatch = new WoBatch();
                                woBatch.BatchNo = pallet.BatchNo;
                                woBatch.Workno = pallet.Workno;
                                woBatchService.saveWoBatch(woBatch);                    //占用工单批次  
                            }
                            break;                                           
                        case "T017":                //批次号                        
                            //string selfBatchNo = cartonService.queryBatchNoWorkNo(cartonNo.ToString(), carton.Workno);
                            //string batchCustom = "1";
                            //if (selfBatchNo != null)
                            //{
                            //    batchCustom = selfBatchNo.Substring(cartonNo.Length, ruleItem.Rulelength);
                            //}
                            //else
                            //{
                            //    selfBatchNo = cartonService.queryInspurMaxBoxNo(cartonNo.ToString(), carton.Cusno);
                            //    if (selfBatchNo != null)
                            //    {
                            //        string tempBatchNo = selfBatchNo.Substring(cartonNo.Length, ruleItem.Rulelength);
                            //        int tempMathNo = int.Parse(tempBatchNo);
                            //        batchCustom = (tempMathNo + 1).ToString();
                            //    }
                            //    string tempZero = "";
                            //    for (int i = batchCustom.Length; i < ruleItem.Rulelength; i++)
                            //    {
                            //        tempZero = 0 + tempZero;
                            //    }
                            //    batchCustom = tempZero + batchCustom;
                            //}
                            //cartonNo.Append(batchCustom);
                            //carton.BatchNo = cartonNo.ToString();
                            break;
                        case "T018":                //年月日進制表示
                            StringBuilder h3cTime = new StringBuilder();
                            string h3cYearString = DateTime.Now.Year.ToString();
                            int h3cYearInt = int.Parse(h3cYearString.Substring(h3cYearString.Length - 2)); //獲取兩位年
                            int h3cInt = DateTime.Now.Month;
                            string h3cDD = DateTime.Now.ToString("dd");                       //获得两位日
                            h3cTime.Append(h3cYearInt);
                            h3cTime.Append(Base33Code[h3cInt]);
                            h3cTime.Append(h3cDD);
                            //             ctCode.Append(inspurTime.ToString());
                            palletNo.Append(h3cTime.ToString());

                            break;
                        case "T019":                //工单十进制流水
                        case "T020":

                            break;
                    }
                }
            }
            pallet.PalletNo = palletNo.ToString();
            return pallet;
        }

        /// <summary>
        /// TODO 定义16進制字符数组
        /// </summary>
        private static char[] constant16 =
       {
        '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'
        };

        /// <summary>
        /// TODO 定义43校驗碼字符数组
        /// </summary>
        private static string[] constant43 =
       {
        "0","1","2","3","4","5","6","7","8","9",
        "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
        "-",".","sp","$","/","+","%"
        };

        /// <summary>
        /// TODO 將數字轉換成16進制數字
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Convert16Code(int num)
        {
            if (num < 0)
            {
                return "";
            }
            //10进制数所对应的16进制数


            //保存除16后的余数
            List<int> list = new List<int>();
            while (num >= 16)
            {
                int a = num % 16;
                num /= 16;
                list.Add(a);
            }
            list.Add(num);

            StringBuilder sb = new StringBuilder();
            //结果要从后往前排
            for (int i = list.Count - 1; i >= 0; i--)
            {
                sb.Append(constant16[list[i]]);
            }
            return sb.ToString();
        }


        /// <summary>
        /// TODO 將16進制數轉換成10進制數
        /// </summary>
        /// <param name="convertCode"></param>
        /// <returns></returns>
        public static int convert16CodeTo10(string convertCode)
        {
            int tenCode = 0;
            for (int i = 0; i <= convertCode.Length - 1; i++)
            {
                int loop = inverseBase16Code[convertCode[i].ToString()];
                for (int j = 1; j < convertCode.Length - i; j++)
                {
                    loop *= 16;
                }
                tenCode += loop;
            }
            return tenCode;
        }

        /// <summary>
        /// TODO 16進制對應碼
        /// </summary>
        public static Dictionary<string, int> inverseBase16Code = new Dictionary<string, int>() {
            {   "0",0}, {   "1"  ,1}, {   "2"  ,2}, {   "3" ,3}, {   "4",4}, {   "5"  ,5}, {   "6" ,6}, {   "7",7}, {   "8" ,8}, {   "9"  ,9},
            {   "A",10  }, {   "B",11}, {   "C",12 }, {   "D",13  }, {   "E",14  }, {   "F",15  }
        };

        /// <summary>
        /// TODO 反轉43位校驗碼
        /// </summary>
        private static Dictionary<string, int> inverseBase43Code = new Dictionary<string, int>() {
            {   "0",0}, {   "1"  ,1}, {   "2"  ,2}, {   "3" ,3}, {   "4",4}, {   "5"  ,5}, {   "6" ,6}, {   "7",7}, {   "8" ,8}, {   "9"  ,9},
            {   "A",10  }, {   "B",11}, {   "C",12 }, {   "D",13  }, {   "E",14  }, {   "F",15  }, {   "G",16 }, {   "H",17 }, {   "I",18  }, {   "J",19  },
            {   "K",20  }, {   "L",21  }, {   "M",22  }, {   "N",23  }, {   "O",24  }, {   "P",25  }, {   "Q",26  }, {   "R",27  }, {   "S",28  }, {   "T",29  },
            {   "U",30  }, {   "V",31  }, { "W",32},{"X",33 }, { "Y",34},{"Z",35 },{ "-",36},{".",37 },{"sp",38 },{ "$",39},{"/",40 },{ "+",41},{"%",42 }
        };

        /// <summary>
        /// TODO 順位43位校驗碼
        /// </summary>
        public static Dictionary<int, string> base43Code = new Dictionary<int, string>() {
            {   0  ,"0"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},
            {   10  ,"A"}, {   11  ,"B"}, {   12  ,"C"}, {   13  ,"D"}, {   14  ,"E"}, {   15  ,"F"}, {   16  ,"G"}, {   17  ,"H"}, {   18  ,"I"}, {   19  ,"J"},
            {   20  ,"K"}, {   21  ,"L"}, {   22  ,"M"}, {   23  ,"N"}, {   24  ,"O"}, {   25  ,"P"}, {   26  ,"Q"}, {   27  ,"R"}, {   28  ,"S"}, {   29  ,"T"},
            {   30  ,"U"}, {   31  ,"V"}, { 32,"W"},{33,"X" },{   34  ,"Y"}, { 35,"Z"},{36,"-" },{   37  ,"."}, { 38,"sp"},{39,"$" },{   40  ,"/"}, { 41,"+"},{42,"%" }
        };


        public static string getInsuprTime()
        {
            StringBuilder inspurTime = new StringBuilder();
            string inspurYearString = DateTime.Now.Year.ToString();
            int inspurYearInt = int.Parse(inspurYearString.Substring(inspurYearString.Length - 2)); //獲取兩位年
            int inspurmonthInt = DateTime.Now.Month;
            string dd2 = DateTime.Now.ToString("dd");                       //获得两位日
            inspurTime.Append(Base33Code[inspurYearInt]);
            inspurTime.Append(Base33Code[inspurmonthInt]);
            inspurTime.Append(dd2);
            return inspurTime.ToString();
        }

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


        public static string extractString(string operaField, int ruleLength)
        {
            string result = "";
            if (operaField.Count() < ruleLength)
            {
                result = operaField;
            }
            else
            {
                result = operaField.Substring(0, ruleLength);
            }
            int fieldLen = result.Count();
            if (fieldLen < ruleLength)
            {
                for (int i = fieldLen; i < ruleLength; i++)
                {
                    result = 0 + result;
                }
            }
            return result;
        }

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

        public static string IntConvert(int convertValue)
        {
            return Base32Code[convertValue];
        }


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
        ///  浪潮年编码 
        /// </summary>
        public static Dictionary<int, string> Base33Code = new Dictionary<int, string>() {
            {   0  ,"0"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},
            {   10  ,"A"}, {   11  ,"B"}, {   12  ,"C"}, {   13  ,"D"}, {   14  ,"E"}, {   15  ,"F"}, {   16  ,"G"}, {   17  ,"H"}, {   18  ,"J"}, {   19  ,"K"},
            {   20  ,"L"}, {   21  ,"M"}, {   22  ,"N"}, {   23  ,"P"}, {   24  ,"Q"}, {   25  ,"R"}, {   26  ,"S"}, {   27  ,"T"}, {   28  ,"U"}, {   29  ,"V"},
            {   30  ,"W"}, {   31  ,"X"},{   32  ,"Y"},{   33  ,"Z"}
        };

        public static Dictionary<string, int> InverseBase34Code = new Dictionary<string, int>() {
            {   "0",0}, {   "1"  ,1}, {   "2"  ,2}, {   "3" ,3}, {   "4",4}, {   "5"  ,5}, {   "6" ,6}, {   "7",7}, {   "8" ,8}, {   "9"  ,9},
            {   "A",10  }, {   "B",11}, {   "C",12 }, {   "D",13  }, {   "E",14  }, {   "F",15  }, {   "G",16 }, {   "H",17 }, {   "J",18  }, {   "K",19  },
            {   "L",20  }, {   "M",21  }, {   "N",22  }, {   "P",23  }, {   "Q",24  }, {   "R",25  }, {   "S",26  }, {   "T",27  }, {   "U",28  }, {   "V",29  },
            {   "W",30  }, {   "X",31  }, { "Y",32},{"Z",33 }
        };


        /// <summary>
        /// 设置34进制编码 
        /// </summary>
        public static Dictionary<int, string> Base34Code = new Dictionary<int, string>() {
            {   0  ,"0"}, {   1  ,"1"}, {   2  ,"2"}, {   3  ,"3"}, {   4  ,"4"}, {   5  ,"5"}, {   6  ,"6"}, {   7  ,"7"}, {   8  ,"8"}, {   9  ,"9"},
            {   10  ,"A"}, {   11  ,"B"}, {   12  ,"C"}, {   13  ,"D"}, {   14  ,"E"}, {   15  ,"F"}, {   16  ,"G"}, {   17  ,"H"}, {   18  ,"J"}, {   19  ,"K"},
            {   20  ,"L"}, {   21  ,"M"}, {   22  ,"N"}, {   23  ,"P"}, {   24  ,"Q"}, {   25  ,"R"}, {   26  ,"S"}, {   27  ,"T"}, {   28  ,"U"}, {   29  ,"V"},
            {   30  ,"W"}, {   31  ,"X"}, { 32,"Y"},{33,"Z" }
        };
    }
}
