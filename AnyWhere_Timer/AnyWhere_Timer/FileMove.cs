using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
//System.Diagnostics.Process.Start(Server.MapPath("ah.bat")); 

namespace AnyWhere_AutoTXT
{
    class FileMove
    {
        static public void CopyFile(string SourcePath,string DestPath)
        {
            
            if (!Directory.Exists(DestPath))
                Directory.CreateDirectory(DestPath);
             //先来移动文件
            DirectoryInfo info = new DirectoryInfo(SourcePath);
            FileInfo[] files = info.GetFiles("Z_SURF_C_BFAK-REG_*.txt");
            foreach (FileInfo file in files)
            {
                File.Copy(SourcePath + "\\" + file.Name, DestPath + "\\" + file.Name, true);
                //File.Delete(SourcePath + "\\" + file.Name);
                
                    Console.WriteLine("拷贝文件 {0}\\{2} 至 {1}\\{2}", SourcePath, DestPath, file.Name); 
                    //LostStationSeek.IF_Seek(file.Name);//将拷贝的文件进行补全并生成新的补全文件
            }
        }
        static public void DeleteALLFile(string Path)
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
             //先来移动文件
            DirectoryInfo info = new DirectoryInfo(Path);
            FileInfo[] files = info.GetFiles("Z_SURF_C_BFAK-REG_*.txt");
            foreach (FileInfo file in files)
            {
                File.Delete(Path + "\\" + file.Name);
            }
        }
    }
}
