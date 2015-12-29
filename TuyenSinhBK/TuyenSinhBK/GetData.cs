using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TuyenSinhBK
{
    class GetData
    {
        private string sbd, khuvuc, doituong, uutien;
        private string[] diemchinh = new string[13]; //tương ứng 13 môn thi
        private string[,] nguyenvong = new string[4, 2]; //4 hàng ứng với 4 NV, cột 1 là mã NV, cột 2 là tổ hợp môn xét tuyển

        public string Sbd
        {
            get { return sbd; }
        }
        public string khuVuc
        {
            get { return khuvuc; }
        }
        public string doiTuong
        {
            get { return doituong; }
        }
        public string uuTien
        {
            get { return uutien; }
        }
        public string[] diemChinh
        {
            get { return diemchinh; }
            }
        public string[,] nguyenVong
        {
            get { return nguyenvong; }
        }

        //Đọc dữ liệu
        public void getData(int stt)
        {
            //Đọc file dangkynv-bk
            string file1 = @"D:\C#\20151_Lập trình HĐT\TuyenSinhBK\TuyenSinhBK\Data input\dangkynv-bk.txt";
            string[] readlinefile1 = File.ReadAllLines(file1); //đọc tất cả các dòng, đưa vào mảng
            string[] arrfile1 = readlinefile1[stt].Split('"'); //bỏ ký tự ", đưa vào mảng mới

            //Xử lý file
            int n = arrfile1.Length;
            for (int i = 0; i < n; i++)
            {
                if (arrfile1[i] == ",")
                {
                    //arrfile1[i] = arrfile1[i + 1];
                    for (int j = i; j < n - 1; j++)
                    {
                        arrfile1[j] = arrfile1[j + 1]; //bỏ ký tự ',' và đẩy các phần tử lên
                    }
                    n--; //sau mỗi vòng for số phần tử mảng giảm đi 1
                }
            }
            //đưa dữ liệu vào mảng nguyện vọng
            n--;
            int dem = 0;
            for (int i = 2; i < n; i++)
            {
                int j = 0;
                if (i % 2 == 0)
                    nguyenvong[dem, j] = arrfile1[i];
                else
                {
                    nguyenvong[dem, j + 1] = arrfile1[i];
                    dem++;
                }
            }

            //Đọc file csdl-bk
            string file2 = @"D:\C#\20151_Lập trình HĐT\TuyenSinhBK\TuyenSinhBK\Data input\csdl-bk.txt";
            string[] readlinefile2 = File.ReadAllLines(file2);
            string[] arrfile2 = readlinefile2[stt].Split(',');

            //Gán dữ liệu cho các thuộc tính
            int m = arrfile2.Length;
            sbd = arrfile2[0];
            khuvuc = arrfile2[3];
            doituong = arrfile2[4];
            uutien = arrfile2[5];
            for (int i = 6; i < m; i++)
                diemchinh[i - 6] = arrfile2[i];
        }
    }
}
