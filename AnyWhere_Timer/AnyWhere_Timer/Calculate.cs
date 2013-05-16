using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AnyWhere_AutoTXT
{
    class Calculate
    {

        /// <summary>
        /// 数据位置70-73 75-78 80-83 85-88 90-93
        /// </summary>

        static public void Search_Sourth_FILE(Region[] Area,string SourcePath)
        {
            //先来移动文件
            DirectoryInfo info = new DirectoryInfo(SourcePath);
            FileInfo[] files = info.GetFiles("Z_SURF_C_BFAK-REG_*.txt");
            foreach (FileInfo file in files)
            {
                Get_Data(Area, SourcePath + "\\" + file.Name);
            }
        }

        static public void Search_Dest_FILE(Region[] Area, string DestPath)
        {
            //先来移动文件
            DirectoryInfo info = new DirectoryInfo(DestPath);
            FileInfo[] files = info.GetFiles("Z_SURF_C_BFAK-REG_*.txt");
            foreach (FileInfo file in files)
            {
                Set_Data(Area, DestPath + "\\" + file.Name);
            }
        }

        static public void Get_Data(Region[] Area, string SourthFILE)
        {
            int AreaID;
            string str="",PointID;
            FileStream DataFile = new FileStream(SourthFILE, FileMode.Open);
            StreamReader DataReader = new StreamReader(DataFile);
            while (DataReader.Peek() > 0 )
            {
                str = DataReader.ReadLine();//获取站号
                PointID= str.Substring(1, 2);
                AreaID = Station_belong(PointID, Area);

                Console.WriteLine("属于区域：{0}", Station_belong(PointID, Area));
                //查找此站点区域属性

                str = DataReader.ReadLine();//获取五元素数据行
                
                if (AreaID <= 9 && AreaID >= 0 )
                {
                    if (!str.Substring(70, 24).Contains("////"))//确保站点收集到温度
                    {
                        
                    Area[AreaID].Aver_data1 += Convert.ToInt32(str.Substring(70, 4));
                    Area[AreaID].Aver_data2 += Convert.ToInt32(str.Substring(75, 4));
                    Area[AreaID].Aver_data3 = Convert.ToInt32(str.Substring(80, 4));
                    Area[AreaID].Aver_data4 += Convert.ToInt32(str.Substring(85, 4));
                    Area[AreaID].Aver_data5 = Convert.ToInt32(str.Substring(90, 4));
                    Area[AreaID].Station_NUM++;
                    }
                    
                }

                //将五元素数据归到某一区域，更新此区域的平均值

                DataReader.ReadLine();//向下读取一行
            }
            DataReader.Close();
            
        }

        static public void Set_Data(Region[] Area,string DestFILE)
        {
            int AreaID;
            string str = "", PointID;
            string str_buffer="";
            FileStream DataFile = new FileStream(DestFILE, FileMode.Open);
            StreamReader DataReader = new StreamReader(DataFile);
            
            while (DataReader.Peek() > 0)
            {
                str = DataReader.ReadLine();
                Console.WriteLine(str);
                str = str.Insert(Convert.ToInt32(str.Length), "\r\n");
                str_buffer += str;
                
                if (str!="NNNN"&&DataReader.Peek() > 0)
                {
                    AreaID = Calculate.Station_belong(str.Substring(1, 2), Area);
                    
                    str = DataReader.ReadLine();
                    if (Area[AreaID].Station_NUM > 0)
                    {
                        str = str.Remove(70, 4);
                        str = str.Insert(70, Area[AreaID].Aver_data1_string);
                        str = str.Remove(75, 4);
                        str = str.Insert(75, Area[AreaID].Aver_data2_string);
                        str = str.Remove(80, 4);
                        str = str.Insert(80, Area[AreaID].Aver_data3_string);
                        str = str.Remove(85, 4);
                        str = str.Insert(85, Area[AreaID].Aver_data4_string);
                        str = str.Remove(90, 4);
                        str = str.Insert(90, Area[AreaID].Aver_data5_string);
                        str = str.Insert(94, " ");
                        str = str.Insert(Convert.ToInt32(str.Length), "\r\n");
                    }
                    str_buffer += str;
                }
                if (DataReader.Peek() > 0)
                {
                    str = DataReader.ReadLine();
                    //str.Insert(str.Length, "\n");
                    str = str.Insert(Convert.ToInt32(str.Length), "\r\n");
                    str_buffer += str;
                }
            }


            DataReader.Close();

            DataFile = new FileStream(DestFILE, FileMode.Create);
            StreamWriter sw=new StreamWriter(DataFile);

            //Console.WriteLine("{0}", str_buffer);

            sw.WriteLine(str_buffer);
            sw.Close();
        }

        static public void Compute_Average(Region[] Area)
        {
            for (int i = 0; i <= 9 && Area[i].Station_NUM > 0; i++)
            {
                Area[i].Aver_data1 = Area[i].Aver_data1 / Area[i].Station_NUM;
                Area[i].Aver_data2 = Area[i].Aver_data2 / Area[i].Station_NUM;
                //Area[i].Aver_data3 = Area[i].Aver_data3 / Area[i].Station_NUM;
                Area[i].Aver_data4 = Area[i].Aver_data4 / Area[i].Station_NUM;
                //Area[i].Aver_data5 = Area[i].Aver_data5 / Area[i].Station_NUM;
            }
        }
        static public int Station_belong(string PointID,Region []Area)
        {
                int i = 0;
                while (i <= 9)
                {
                    //Console.WriteLine("站点{0}区域{1}", PointID, Area[i].Region_Name);
                    if (Area[i].Region_Name.Equals(PointID))
                    {
                        return i;
                    }
                    i++;     
                }
                return 10;
            
        }
        static public void Init(Region []Area)
        {
            
            for (int i = 0; i < 10; i++)
            {
                Area[i]=new Region();
                Area[i].Region_Name = Convert.ToString(50 + i);
                
                Area[i].Station_NUM = 0;
                Area[i].Aver_data1 = 0;
                Area[i].Aver_data2 = 0;
                Area[i].Aver_data3 = 0;
                Area[i].Aver_data4 = 0;
                Area[i].Aver_data5 = 0;

            }
        }

    }
}
