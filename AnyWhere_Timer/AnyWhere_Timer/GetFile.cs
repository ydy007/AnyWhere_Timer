using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Timers;
using System.Threading;
using System.Diagnostics;
namespace AnyWhere_AutoTXT
{
    class GetFile
    {
        
        static public void ScanFile(string path)
        {
            flag.IF_SEEK_FILE = 0;
            DirectoryInfo ScanFolder = new DirectoryInfo(path);
            FileInfo[] FileTXT = ScanFolder.GetFiles("Z_SURF_C_BFAK-REG_*.txt");
            
            foreach (FileInfo i in FileTXT)
            {
                //标志是否有文件，有的话不需要全部补全
                //System.Console.WriteLine(i.Name);
                //LostStationSeek.IF_Seek(i.Name);
                flag.IF_SEEK_FILE = 1;
                System.Console.WriteLine("在路径{0}搜索到文件:{1}",path,i.Name);
            }  
        }
        

        static public void WaitFor_FileFolder_Empty()
        {
            while (flag.IF_SEEK_FILE == 1)
            {
                GetFile.ScanFile(flag.SourcePath);
                //Console.WriteLine(flag.SourcePath);
                if (flag.IF_SEEK_FILE == 0)
                {
                    Console.WriteLine("原始文件已传输，补全文件拷贝至share");
                    flag.HourAutoWtrite_Flag = 1;
                    FileMove.CopyFile(flag.DestPath, flag.SourcePath);
                    FileMove.DeleteALLFile(flag.DestPath);
                }
                else
                {
                    Console.WriteLine("等待原始文件传输");
                }
                Thread.Sleep(1000);
            }
 
        }

        static public void TikTok()
        {
            DateTime BigBen;
            int BigBenMinute,BigBenSec;
            int SLEEP_TIME = 5000;

            Region[] Area = new Region[10];
            Calculate.Init(Area);

            //
            //Region[] Area = new Region[10];
            //Calculate.Init(Area);
            //Calculate.Search_Sourth_FILE(Area, @"C:\Users\ydy\Desktop\BFAKCopy");
            //Calculate.Compute_Average(Area);

            

            while (true)
            {
                BigBen = DateTime.UtcNow;
                BigBenMinute = Convert.ToInt32(BigBen.Minute.ToString());
                BigBenSec = Convert.ToInt32(BigBen.Second.ToString());
                //Console.WriteLine(BigBenMinute);


                if (BigBenMinute >= 0 && BigBenMinute <=5 && flag.HourAutoWtrite_Flag == 0 && BigBenSec>=40)
                {
                    Console.WriteLine("开始扫描文件");
                    GetFile.ScanFile(flag.SourcePath);
                    
                    

                    if (flag.IF_SEEK_FILE == 1)
                    {

                        
                        FileMove.CopyFile(flag.SourcePath, flag.DestPath);

                        Calculate.Search_Sourth_FILE(Area, flag.DestPath);
                        Calculate.Compute_Average(Area);
                        
                         for (int i = 0; i <= 9; i++)
                        {
                            Area[i].CopyData();
                        }//将源数据进行计算
                        

                        Console.WriteLine("搜索到文件，开始补全");
                        LostStationSeek.AUTO_TXT(flag.DestPath);

                        Calculate.Search_Dest_FILE(Area,flag.DestPath);//将不全的文件进行修改

                        //GetFile.WaitFor_FileFolder_Empty();
                    }
                    else if(flag.IF_SEEK_FILE==0)
                    {
                        Console.WriteLine("未搜索到文件，继续扫描");
                    }

                }
                else if (BigBenMinute > 5 && BigBenMinute <= 8 && flag.HourAutoWtrite_Flag == 0)
                {
                    GetFile.ScanFile(flag.SourcePath);
                    if (flag.IF_SEEK_FILE == 0)
                    {
                        Console.WriteLine("未生成任何文件，全部补全");
                        LostStationSeek.IF_NOT_Seek();
                    }
                }
                else if (BigBenMinute == 9)
                {
                    flag.HourAutoWtrite_Flag = 0;
                    //System.Diagnostics.Process.Start("d:\\cc\\3.bat"); 
                    FileMove.DeleteALLFile(flag.DestPath);
                    Console.WriteLine("系统进行一次初始化");
                    
                    Calculate.Init(Area);//Area数据初始化
                }
                else
                {
                    Console.WriteLine("等待下一个小时的数据");
                }
                Thread.Sleep(SLEEP_TIME);
                Console.WriteLine("现在时刻 {0}小时 {1}分钟 {2}秒", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                Console.WriteLine("时间过了{0}秒 ",SLEEP_TIME/1000);
            }
        }

    }
}
