using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerateCTCode
{
    public class GenerateCarton
    {

        private readonly static CartonService cartonService = new CartonService();
        private readonly static CodeRuleService codeRuleService = new CodeRuleService();
        private readonly static ProdLineService prodLineService = new ProdLineService();


        public static Carton generateCartonNo(Carton carton)
        {
            StringBuilder cartonNo = new StringBuilder();
            CodeRule codeRule = codeRuleService.queryRuleById(carton.Ruleno);
            if(codeRule.RuleItem != null)
            {
                foreach(RuleItem ruleItem in codeRule.RuleItem){
                    switch (ruleItem.Ruletype)
                    {
                        case "T001":
                            break;
                        case "T002":
                            break;
                        case "T003":
                            break;
                        case "T004":
                            break;
                        case "T005":
                            break;
                        case "T006":
                            break;
                        case "T007":
                            cartonNo.Append(ruleItem.Rulevalue);
                            break;
                        case "T008":
                            cartonNo.Append(carton.SoOrder);
                            break;
                        case "T009":
                            //机种号3-7位
                            cartonNo.Append(carton.Delmatno.Substring(2, 5));
                            break;
                        case "T010":
                            cartonNo.Append(prodLineService.queryProdLineById(carton.ProdLine.ProdlineId).ProdlineName);
                            break;
                        case "T011":
                            //流水码十六进制
                            string maxCarton = cartonService.getMaxCartonNo(cartonNo.ToString());
                            string prefixCarton = cartonNo.ToString();
                            if (maxCarton == null || maxCarton == "")
                            {
                                    string seqCode = "0";
                                    for (int numLength = seqCode.Length; numLength < Convert.ToInt32(ruleItem.Rulelength); numLength++)
                                    {
                                    seqCode += "0";
                                    }
                                cartonNo.Append(seqCode);                               
                            }
                            else
                            {
                                //獲取流水號
                                string subCode = maxCarton.Substring(prefixCarton.Length, Convert.ToInt32(ruleItem.Rulelength));
                                int cartonSeq = convert16CodeTo10(subCode);
                                string carton16Code = Convert16Code(cartonSeq + 1);
                                string seqNo = "";
                                for (int i = carton16Code.Length; i < Convert.ToInt32(ruleItem.Rulelength); i++)
                                {
                                    seqNo += 0;
                                }
                                carton16Code = seqNo + carton16Code;
                                cartonNo.Append(carton16Code);

                            }
                            break;
                        case "T012":
                            String prefix = cartonNo.ToString();
                            int modNum = 0;
                            foreach (char result in prefix.ToCharArray())
                            {
                                string exchangeInt = result.ToString();
                                int numExchange = inverseBase43Code[exchangeInt];
                                modNum += numExchange;
                            }
                            int modReuslt = modNum % 43;
                            string modString = base43Code[modReuslt];
                            cartonNo.Append(modString);
                            break;
                    }
                }
            }
            carton.CartonNo = cartonNo.ToString();
            return carton;
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


    }
}
