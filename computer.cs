//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace QuanLyThietBi
{
    public class Computer : Device, INetworkable
    {
        public int RamCapacity {get; set;}
        public string ProcessorType {get; set;}
        public bool HasDiscreteGpu {get; set;}

        public string IpAddress {get; private set;} = string.Empty;
        public bool IsConnected {get; private set;}

        //Constructor
        public Computer(
            string deviceId, 
            string deviceName, 
            int startYears, 
            decimal costPrice, 
            DeviceStatus status, 
            int ramCapacity, 
            string processorType, 
            bool hasDiscreteGpu) 
            : base(deviceId, deviceName, startYears, costPrice, status)
        {
            RamCapacity = ramCapacity;
            ProcessorType = processorType;
            HasDiscreteGpu = hasDiscreteGpu;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            //5% giá mua
            decimal cost = CostPrice*0.05m;

            //Cộng thêm 2% giá mua nếu có GPU rời
            if(HasDiscreteGpu)
            {
                cost += CostPrice*0.02m;
            }

            //Nếu thiết bị đã sử dụng trên 5 năm, cộng thêm 1% giá mua
            int currentYear = DateTime.Now.Year;
            if((currentYear - StartYears) > 5 )
            {
                cost += CostPrice*0.01m;
            }

            return cost;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Dung lượng RAM: {RamCapacity} | Loại bộ xử lý: {ProcessorType} | Có GPU rời hay không: {HasDiscreteGpu}";
        }

        public void Connect (string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ArgumentException("Địa chỉ IP không được rỗng.");
            }

            if(IsConnected)
            {
                throw new InvalidOperationException("Thiết bị đã được kết nối.");
            }

            IpAddress = ipAddress;
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
            IpAddress = string.Empty;
        }
    }
}