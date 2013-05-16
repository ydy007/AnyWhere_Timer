using System;
using System.Collections.Generic;
using System.Text;
//using System.Timers;
using System.IO;
using System.Threading;

namespace AnyWhere_AutoTXT
{
    class Program
    {
        //public void WriteIntoText(string Location,StreamWriter sw);
        
        static void Main(string[] args)
        {


            flag.HourAutoWtrite_Flag = 0;
            flag.IF_SEEK_FILE = 0;
            //string Location = "V5928 334548 1084622 11610 00000 4";
            //string filename = Location + ".txt";

            //LostStationSeek.IF_Seek("Z_SURF_C_BFAK-REG_20130117150000_O_AWS_FTM.txt");
            //坐标比对功能测试

            //测试等待文件夹空
            //flag.IF_SEEK_FILE = 1;
            //GetFile.WaitFor_FileFolder_Empty();
            GetFile.TikTok();
            
            //成功运行代码，待恢复
            
            /*
            while (true)
            {
                
                WriteText.WriteIntoText(Location);
                System.Threading.Thread.Sleep(60000);
                //一分钟输出一遍
            }
            */



        }


        
    }
}
