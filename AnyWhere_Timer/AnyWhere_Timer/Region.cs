using System;
using System.Collections.Generic;
using System.Text;

namespace AnyWhere_AutoTXT
{
    class Region
    {
        public string Region_Name;
        public int Station_NUM;
        public int Aver_data1, Aver_data2, Aver_data3, Aver_data4, Aver_data5;
        public string Aver_data1_string, Aver_data2_string, Aver_data3_string, Aver_data4_string,Aver_data5_string;

        public void CopyData()
        {
            Aver_data1_string = DataFormatConvert(Aver_data1);
            Aver_data2_string = DataFormatConvert(Aver_data2);
            Aver_data3_string = DataFormatConvert(Aver_data3);
            Aver_data4_string = DataFormatConvert(Aver_data4);
            Aver_data5_string = DataFormatConvert(Aver_data5);
        }

        public string DataFormatConvert(int Data_int)
        {
            int i;
            string Data_str;
            Data_str=Convert.ToString(Data_int);
            if (Data_str.Length < 4)
            {
                i = 4 - Data_str.Length;
                while (i > 0)
                {
                    Data_str = '0' + Data_str;
                    i--;
                }
            }
            return Data_str;
        }
    }
}
