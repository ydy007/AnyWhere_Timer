using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.IO;
namespace AnyWhere_AutoTXT
{

    class WriteText
    {

        static public void WriteIntoText(string Location, string wait_filename)
        {
            string TimeFormat;
            TimeFormat = CorrectTimeFormat(); 
            //string wait_filename = "Z_SURF_C_BFAK-REG_" + TimeFormat + "_O_AWS_FTM.txt";
            FileStream aFile = new FileStream(wait_filename, FileMode.Append);
            StreamWriter sw = new StreamWriter(aFile);
            
            //依次写入坐标，时间，空数据
            sw.WriteLine(Location);
            
            sw.Write(TimeFormat);
            sw.WriteLine(@" /// /// /// /// /// /// //// /// /// /// /// //// //// //// //// //// //// /// /// //// /// //// ///// ///// //// ///// //// //// //// //// //// //// //// //// //// //// //// //// //// //// //// //// //// //// //// //// ///// ///// ///// ////
000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000=");//13数据位
            sw.Close();
        }

        static public void TrimOperation_AddN(string wait_filename)
        {
            FileStream aFile = new FileStream(wait_filename, FileMode.Append);
            StreamWriter sw = new StreamWriter(aFile);
            sw.Write("NNNN");
            sw.Close();
        }

        static public void TrimOperation_DeleteN(string wait_filename)
        {
            string str_buffer;
            FileStream aFile = new FileStream(wait_filename, FileMode.Open);
            StreamReader sr = new StreamReader(aFile);
            str_buffer = sr.ReadToEnd();
            sr.Close();
            str_buffer = str_buffer.TrimEnd('N');
            //Console.Write(str_buffer);
            aFile = new FileStream(wait_filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(aFile);
            sw.Write(str_buffer);
            sw.Close();
        }

        static public string CorrectTimeFormat()
        {
            string TimeFormat;
            DateTime Date_UTC = DateTime.UtcNow;

            string timeWrite_day = Date_UTC.Day.ToString();
            string timeWrite_hour = Date_UTC.Hour.ToString();
            string timeWrite_month = Date_UTC.Month.ToString();
            if (Convert.ToInt32(timeWrite_day)<10)
            {
                timeWrite_day = "0" + timeWrite_day;
            }
            if(Convert.ToInt32(timeWrite_hour)<10)
            {
                timeWrite_hour = "0" + timeWrite_hour;
            }
            if(Convert.ToInt32(timeWrite_month)<10)
            {
                timeWrite_month = "0" + timeWrite_month;
            }

            TimeFormat = Date_UTC.Year.ToString() + timeWrite_month + timeWrite_day + timeWrite_hour + "0000";
            return TimeFormat;
        }
    }
}
