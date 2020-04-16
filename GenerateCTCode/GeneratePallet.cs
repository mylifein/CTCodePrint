using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerateCTCode
{
    public class GeneratePallet
    {

        private readonly static PalletService palletService = new PalletService();



        public static Pallet generatePalletNo(Pallet pallet)
        {
            StringBuilder palletNo = new StringBuilder();
            palletNo.Append("CS");                                  //機箱CS
           
            System.DateTime currentTime = System.DateTime.Now;                      //獲取當前時間
            int year = currentTime.Year;
            int month = currentTime.Month;
            int day = currentTime.Day;                                              //獲得日期
            string dayString = "";
            if (day.ToString().Length < 2)
            {
                dayString = "0" + day;
            }
            else
            {
                dayString = day.ToString();
            }
            string yearString =  year.ToString().Substring(2, 2);                   //獲取兩位年
            palletNo.Append(inverseYearCode[yearString]);
            palletNo.Append(inverseMonthCode[month.ToString()]);                    //獲取月
            palletNo.Append(dayString);                                             //添加日期
            palletNo.Append("Q");                                                   //添加固定字段
            Pallet maxPallet = palletService.queryPalletByPrefix(palletNo.ToString());             //查詢批次號
            int batchNo = 1;                                                                     //計算批次號
            if(maxPallet != null)
            {
                if (maxPallet.WoNo.Equals(pallet.WoNo))
                {
                    string prefixNo = maxPallet.PalletNo.Substring(palletNo.Length,1);
                    batchNo = Convert.ToInt32(prefixNo);
                }else
                {
                    string prefixNo = maxPallet.PalletNo.Substring(palletNo.Length + 1, 1);
                    batchNo = Convert.ToInt32(prefixNo) + 1;
                }
            }
            palletNo.Append(batchNo);
            pallet.BatchNo = palletNo.ToString();                                                 //添加批次號
            Pallet palletSeq = palletService.queryPalletByPrefix(palletNo.ToString());            //根據棧板號前綴查詢
            if(palletSeq == null)
            {
                palletNo.Append("0001");
            }else
            {
                string seqString = palletSeq.PalletNo.Substring(palletNo.Length, 4);
                int seqInt = Convert.ToInt32(seqString) + 1;                                            //查詢流水號
                if(seqInt.ToString().Length < 4)
                {
                    string temSeq = "";
                    for(int i = seqInt.ToString().Length; i < 4; i++)
                    {
                        temSeq += 0;
                    }
                    temSeq = temSeq + seqInt;
                    palletNo.Append(temSeq);
                }else
                {
                    palletNo.Append(seqInt);
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

        /// <summary>
        /// 轉換年
        /// </summary>
        public static Dictionary<string, string> inverseYearCode = new Dictionary<string, string>() {
            {   "10","A"}, {   "11"  ,"B"}, {   "12"  ,"C"}, {   "13" ,"D"}, {   "14","E"}, {   "15"  ,"F"}, {   "16" ,"G"}, {   "17","H"}, {   "18" ,"J"}, {   "19"  ,"K"},
            {   "20","L"  }, {   "21","M"}, {   "22","N" }, {   "23","P"  }, {   "24","R" }, {   "25","S"  }, {   "26","T"  }, {   "27","V"  }, {   "28","W"  }, {   "29","X"  },
            {   "30","Y"  },{   "31","Z"  }
        };


        public static Dictionary<string, string> inverseMonthCode = new Dictionary<string, string>() {
            {   "1","1"}, {   "2"  ,"2"}, {   "3"  ,"3"}, {   "4" ,"4"}, {   "5","5"}, {   "6"  ,"6"}, {   "7" ,"7"}, {   "8","8"}, {   "9" ,"9"}, {   "10"  ,"A"},
            {   "11","B"  }, {   "12","C"}
        };
    }
}
