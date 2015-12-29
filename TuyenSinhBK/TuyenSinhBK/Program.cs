using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenSinhBK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Tuyển Sinh Bách Khoa";
            Console.WriteLine("--> Program is running...");
            double second_begin = DateTime.Now.Second;
            double minute_begin = DateTime.Now.Minute;

            int stt = 3200; //Số thứ tự
            int n = 100; //Số sinh viên cần xử lý
            Process pro = new Process();
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0}% ", (int)(((i + 1) * 100) / n));
                pro.addData(stt + i); //Nhận dữ liệu
                pro.Processing();     //Xử lý
                pro.importData();     //Xuất dữ liệu
            }
            Console.WriteLine("--> Complete!");
            //Tính thời gian chạy chương trình
            double second_end = DateTime.Now.Second;
            double minute_end = DateTime.Now.Minute;
            Console.WriteLine("Time running: {0} seconds", (minute_end * 60 + second_end) - (minute_begin * 60 + second_begin));
            Console.ReadLine();
        }
    }
}
