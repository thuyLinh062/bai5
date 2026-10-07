//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace QuanLyThietBi
{
    public enum TypePrinter
    {
        laser,
        phun
    }

    public class Printer : Device
    {
        public TypePrinter TypePrinter {get; set;}
        public int PageCount { get; set; }
        public bool IsColor { get; set; }

        //Constructor
        public Printer(
            string deviceId, 
            string deviceName, 
            int startYears, 
            decimal costPrice, 
            DeviceStatus status, 
            TypePrinter typePrinter, 
            int pageCount, 
            bool isColor) 
            : base(deviceId, deviceName, startYears, costPrice, status)
        {
            if (pageCount < 0)
            {
                throw new ArgumentException("Số trang đã in không được nhỏ hơn 0.");
            }

            this.TypePrinter = typePrinter;
            PageCount = pageCount;
            IsColor = isColor;
        }

        public override decimal CalculateAnnualMaintenanceCost()
        {
            decimal cost = CostPrice * 0.04m;

            //Cộng thêm 500.000 đồng nếu số trang đã in lớn hơn 100.000.
            if(PageCount > 100000)
            {
                cost = cost + 500000;
            }

            //Cộng thêm 300.000 đồng nếu là máy in màu.
            if(IsColor)
            {
                cost += 300000;
            }

            return cost;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Loại máy: {TypePrinter} | Số trang đã in: {PageCount} | Có in màu hay không: {IsColor}";
        }
    }

    public class NetworkPrinter : Printer, INetworkable
    {
        public NetworkPrinter(
            string deviceId, 
            string deviceName, 
            int startYears, 
            decimal costPrice, 
            DeviceStatus status, 
            TypePrinter typePrinter, 
            int pageCount, bool isColor) 
            : base(deviceId, deviceName, startYears, costPrice, status, typePrinter, pageCount, isColor)
        {
        }

        public string IpAddress {get; private set; } = string.Empty;

        public bool IsConnected {get; private set;}

        public void Connect(string ipAddress)
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