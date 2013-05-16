using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AnyWhere_AutoTXT
{
    class LostStationSeek
    {
        static public void AUTO_TXT(string Path)
        {
            flag.HourAutoWtrite_Flag = 1;
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            //先来移动文件
            DirectoryInfo info = new DirectoryInfo(Path);
            FileInfo[] files = info.GetFiles("Z_SURF_C_BFAK-REG_*.txt");
            foreach (FileInfo file in files)
            {
                LostStationSeek.IF_Seek(file.Name);//将拷贝的文件进行补全并生成新的补全文件
            }
        }

        static public void IF_Seek(string CheckFile)
        {
            string StationFile = "StationGps.txt";
            //string CheckFile = "test_file.txt";
            string str_Gps;
            string BFAK_TXT_buffer;
            
            FileStream File_gps = new FileStream(StationFile, FileMode.Open);
            StreamReader reader_gps = new StreamReader(File_gps);
            FileStream Check_temp = new FileStream(flag.DestPath + "\\" + CheckFile, FileMode.Open);
            StreamReader BFAK_TXT_buffer_Reader = new StreamReader(Check_temp);
            
            BFAK_TXT_buffer = BFAK_TXT_buffer_Reader.ReadToEnd();//将备份文件读入缓冲池

            BFAK_TXT_buffer_Reader.Close();
            Check_temp.Close();

            File.Delete(flag.DestPath + "\\" + CheckFile);//删除备份文件，后面的代码会补录的数据生成同名文档

            //测试str_buffer.TrimEnd('N');
            //WriteText.TrimOperation_DeleteN(CheckFile);
            while (reader_gps.Peek() > 0)
            {
                str_Gps = reader_gps.ReadLine();
                

                if (BFAK_TXT_buffer.Contains(str_Gps))
                {
                    System.Console.WriteLine("已存在站点:{0}",str_Gps);
                    //已找到坐标
                }
                else 
                {
                    System.Console.WriteLine("对此站点进行补全:{0}",str_Gps);
                    //没有包含的坐标，需要填补空数据
                    WriteText.WriteIntoText(str_Gps, flag.DestPath + "\\" + CheckFile);
                }
            }

            WriteText.TrimOperation_AddN(flag.DestPath + "\\" + CheckFile);//补全NNNN结尾
            reader_gps.Close();
        }
        static public void IF_NOT_Seek()
        {
            //当传输大面积出问题，未能在五分钟之内上传文本时

            string TimeFormat = WriteText.CorrectTimeFormat();
            string StationFile = "StationGps.txt";
            string wait_filename =flag.DestPath+"\\"+"Z_SURF_C_BFAK-REG_" + TimeFormat + "_O_AWS_FTM.txt";
            FileStream File_gps = new FileStream(StationFile, FileMode.Open);
            StreamReader reader_gps = new StreamReader(File_gps);

            
            String str_Gps;
            while ((str_Gps = reader_gps.ReadLine()) != null)
            {
                WriteText.WriteIntoText(str_Gps, wait_filename);
                System.Console.WriteLine("Have Complete:{0}", str_Gps);
            }

            flag.HourAutoWtrite_Flag = 1;
            WriteText.TrimOperation_AddN(wait_filename);//补全NNNN结尾
            reader_gps.Close();
        }
    }
}
