//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace QuanLyThietBi
{
    public class Projector : Device
    {
        public int Brightness {get; set;} //Tính bằng lumen
        public int LampHours {get; set;}

        //Constructor
        public Projector(
            string deviceId, 
            string deviceName, 
            int startYears, 
            decimal costPrice, 
            DeviceStatus status, 
            int brightness,
            int lampHours)
            : base(deviceId, deviceName, startYears, costPrice, status)
        {
            Brightness = brightness;
            LampHours = lampHours;
        }
        
        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = CostPrice * 0.03m;

            if(LampHours > 3000)
            {
                cost += 1500000m;
            }

            return cost;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Độ sáng (tính bằng lumen): {Brightness} | Số giờ đèn: {LampHours}";
        }
    }
}