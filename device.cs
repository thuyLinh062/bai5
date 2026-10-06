//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

public abstract class Device
{
    // Các thuộc tính chung

    //Mã thiết bị
    public string deviceId { get; init; }

    //Tên thiết bị
    public string deviceName { get; set; }

    //Năm đưa vào sử dụng
    public int yearsInUse { get; set; }

    //Giá mua
    public double costPrice { get; set; }

    //Trạng thái hoạt động 
    public enum deviceStatus 
    {
        Active,
        UnderMaintenance,
        Retired
    }
    
    protected Device(string deviceId, string deviceName, int yearsInUse, double costPrice, enum deviceStatus)
    {
        if(string.IsNullOrWhiteSpace(deviceId))
        {
            throw new ArgumentException("ID không được để trống");
        }

        if (costPrice <= 0)
        {
            throw new ArgumentException("Giá mua phải lớn hơn 0.");
        }

        //Năm đưa vào sử dụng không được lớn hơn năm hiện tại.
        int currentYear = DateTime.Now.Year;

        if (yearsInUse > currentYear)
        {
            throw new ArgumentException("Năm đưa vào sử dụng không được lớn hơn năm hiện tại.");
        }

        this.deviceId = deviceId;
        this.deviceName = deviceName;
        this.yearsInUse = yearsInUse;
        this.costPrice =costPrice;
        this.deviceStatus = deviceStatus;
    }

    //Cung cấp phương thức trừu tượng tính chi phí bảo trì dự kiến trong một năm.
    public astract decimal CalculateAnnualMaintenanceCost();

    //Ghi đè phương thức biểu diễn thông tin của thiết bị dưới dạng chuỗi
    public override string ToString()
    {
        return $"[Mã: {deviceId}] - Tên: {deviceName} | Chi phí bảo trì/năm: {CalculateAnnualMaintenanceCost():N0} VNĐ";
    }
}