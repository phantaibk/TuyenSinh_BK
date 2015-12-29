using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace TuyenSinhBK
{
    class Process
    {
        private double[] diemchinh = new double[13]; //điểm tương ứng 13 môn thi
        private double diemcong, khuvuc, doituong, uutien; //điểm cộng = tổng (khu vực + đối tượng)
        private double[] diemxet = new double[4]; //điểm xét cuối cùng, ứng với 4 nguyện vọng
        private string[,] nguyenvong = new string[4, 2]; //4 hàng ứng với 4 NV, cột 1 là mã NV, cột 2 là tổ hợp môn xét tuyển
        private string sbd;

        //Điền giá trị cho các thuộc tính
        public void addData(int stt)
        {
            GetData add = new GetData();
            add.getData(stt);
            //Điểm thi các môn
            for (int i = 0; i < 13; i++)
            {
                if (add.diemChinh[i] == "NA")
                    diemchinh[i] = 0;
                else
                    diemchinh[i] = double.Parse(add.diemChinh[i]);
            }
            //Tính tổng Điểm cộng
            switch (add.khuVuc)
            {
                case "\"KV1\"":
                    khuvuc = 1.5;
                    break;
                case "\"KV2-NT\"":
                    khuvuc = 1;
                    break;
                case "\"KV2\"":
                    khuvuc = 0.5;
                    break;
                case "\"KV3\"":
                    khuvuc = 0;
                    break;
            }
            switch (add.doiTuong)
            {
                case "\"Khong\"":
                    doituong = 0;
                    break;
                case "\"NDT1\"":
                    doituong = 2;
                    break;
                case "\"NDT2\"":
                    doituong = 1;
                    break;
            }
            diemcong = khuvuc + doituong;
            //Ưu tiên
            switch (add.uuTien)
            {
                case "\"Khong\"":
                    uutien = 0;
                    break;
                case "\"UT\"":
                    uutien = 1;
                    break;
            }
            //Nguyện vọng
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 2; j++)
                    nguyenvong[i, j] = add.nguyenVong[i, j];
            //Số báo danh
            sbd = add.Sbd;
            sbd = sbd.Trim('"');
        }

        //Điểm từng môn
        private double diem;
        public double diemMon(string mon)
        {
            switch (mon)
            {
                case "Toan":
                    diem = diemchinh[0];
                    break;
                case "Van":
                    diem = diemchinh[1];
                    break;
                case "Ly":
                    diem = diemchinh[2];
                    break;
                case "Hoa":
                    diem = diemchinh[3];
                    break;
                case "Sinh":
                    diem = diemchinh[4];
                    break;
                case "Su":
                    diem = diemchinh[5];
                    break;
                case "Dia":
                    diem = diemchinh[6];
                    break;
                case "Anh":
                    diem = diemchinh[7];
                    break;
                case "Nga":
                    diem = diemchinh[8];
                    break;
                case "Phap":
                    diem = diemchinh[9];
                    break;
                case "Trung":
                    diem = diemchinh[10];
                    break;
                case "Duc":
                    diem = diemchinh[11];
                    break;
                case "Nhat":
                    diem = diemchinh[12];
                    break;
            }
            return diem;
        }

        //Xử lý
        public void Processing()
        {
            for (int i = 0; i < 4; i++)
            {
                if (nguyenvong[i, 1] == null)
                    diemxet[i] = 0;
                else
                {
                    string[] str = nguyenvong[i, 1].Split(',');
                    if (str[3] == "0")
                    {
                        diemxet[i] = (diemMon(str[0]) + diemMon(str[1]) + diemMon(str[2])) / 3 + diemcong / 3 + uutien;
                    }
                    else
                    {
                        diemxet[i] = (diemMon(str[0]) * 2 + diemMon(str[1]) + diemMon(str[2])) / 4 + diemcong / 3 + uutien;
                    }
                }
            }
        }

        //Kết nối với SQLite
        public void connectSQL(string sql)
        {
            //Tạo một kết nối
            SQLiteConnection conn = new SQLiteConnection(@"Data Source = D:\C#\20151_Lập trình HĐT\TuyenSinhBK\Database\32");
            conn.Open();
            //Tạo một đối tượng giữ lệnh cần thực thi
            SQLiteCommand cmd = new SQLiteCommand();
            //Gán kết nối
            cmd.Connection = conn;
            //Gán lệnh SQL
            cmd.CommandText = sql;
            try
            {
                //Thực hiện câu lệnh SQL
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write("Connect to SQLite fail... Please check again!");
            }
            cmd.Dispose(); //Giải phóng bộ nhớ
            cmd = null;
        }

        //Import kết quả vào bảng nvxt
        public void importData()
        {
            for (int k = 0; k < 4; k++)
            {
                if (diemxet[k] > 0)
                {
                    string sql = "INSERT INTO nvxt VALUES('" + sbd + "','" + (k + 1) + "','" + nguyenvong[k, 0] + "','" + Math.Round(diemxet[k], 3) + "')";
                    connectSQL(sql);
                }
            }
        }
    }
}
