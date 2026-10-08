//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace QuanLyThietBi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=================================================");
            Console.WriteLine("    CHƯƠNG TRÌNH QUẢN LÝ THIẾT BỊ PHÒNG LAB     ");
            Console.WriteLine("=================================================\n");

            //0, Khởi tạo thiết bị theo yêu cầu 
            Console.WriteLine("---> 0. Khởi tạo danh sách thiết bị...");

            //Hai máy tính, trong đó có một máy có GPU rời
            Computer pc01 = new Computer("PC001", "PC Đồ họa 01", 2022, 20000000m, DeviceStatus.Active, 32, "Intel i7", true);
            Computer pc02 = new Computer("PC002", "PC Văn phòng 02", 2020, 10000000m, DeviceStatus.Active, 16, "Intel i5", false);

            // Hai máy in, trong đó một máy đã in trên 100.000 trang
            Printer pr01 = new Printer("PR001", "Máy in HP Laser", 2023, 5000000m, DeviceStatus.Active, TypePrinter.laser, 50000, false);
            NetworkPrinter pr02 = new NetworkPrinter("PR002", "Máy in Canon Mạng", 2018, 12000000m, DeviceStatus.UnderMaintenance, TypePrinter.phun, 120000, true);

            // Một máy chiếu có bóng đèn đã sử dụng trên 3.000 giờ
            Projector pj01 = new Projector("PJ001", "Máy chiếu Sony", 2019, 15000000m, DeviceStatus.Active, 3500, 3200);

            Console.WriteLine("-> Khởi tạo 5 thiết bị thành công.\n");

            //1, Khởi tạo LAB và thêm thiết bị
            Console.WriteLine("=================================================");
            Console.WriteLine("---> 1. Khởi tạo Phòng Lab & Thêm thiết bị...");

            LabRoom lab1 = new LabRoom("LAB01", "Phòng Máy Tính 1", "40 máy");
            LabRoom lab2 = new LabRoom("LAB02", "Phòng Đa Phương Tiện", "30 máy");

            // Thêm thiết bị vào phòng 1
            lab1.AddDevice(pc01);
            lab1.AddDevice(pc02);
            lab1.AddDevice(pr01);

            // Thêm thiết bị vào phòng 2
            lab2.AddDevice(pr02);
            lab2.AddDevice(pj01);
            Console.WriteLine();
            Console.WriteLine();

            //2, Thu thêm thiết bị bị trùng mã 
            Console.WriteLine("=================================================");
            Console.WriteLine("---> 2. Kiểm thử thêm thiết bị bị trùng mã...");
            try
            {
                // Thử thêm lại pc01 (mã PC001 đã có trong lab1)
                Computer duplicatePc = new Computer("PC001", "PC Trùng Mã", 2024, 15000000m, DeviceStatus.Active, 16, "AMD", false);
                lab1.AddDevice(duplicatePc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Bắt lỗi thành công]: {ex.Message}");
            }
            Console.WriteLine();

            //3, In danh sách thiết bị trong từng 
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("---> 3. Danh sách thiết bị trong từng phòng:");
            
            lab1.PrintLabRoomDevices();
            Console.WriteLine();
            lab2.PrintLabRoomDevices();

            //4, Tổng chi phí bảo trì dự kiến của mỗi phòng
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("---> 4.Tổng chi phí bảo trì dự kiến:");

            Console.WriteLine($"---> Tổng chi phí bảo trì {lab1.RoomName} ({lab1.RoomId}): {lab1.CalculateAnnualMaintenanceCost():N0} VNĐ");
            Console.WriteLine($"---> Tổng chi phí bảo trì {lab2.RoomName} ({lab2.RoomId}): {lab2.CalculateAnnualMaintenanceCost():N0} VNĐ\n");

            //5, Liệt kê thiết bị cần bảo trì.
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("---> 5.Các thiết bị cần bảo trì:");
            
            lab1.PrintDevicesNeedingMaintenance();
            Console.WriteLine();
            lab2.PrintDevicesNeedingMaintenance();

            //6, Kết nối mạng cho các đối tượng thực thi INetworkable
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("---> 6.Kết nối mạng cho các đối tượng thực thi INetworkable");
            Console.WriteLine();

            //Kết nối mạng cho device lab1
            Console.WriteLine("=== Kết nối mạng cho thiết bị trong {lab1.RoomName} ({lab1.RoomId}) ===");

            int ipTail = 10;
            foreach (Device dev in lab1.Device)
            {
                if(dev is INetworkable netDev)
                    {
                        netDev.Connect($"180.375.1.{ipTail++}");
                        Console.WriteLine($"Đã kết nối mạng cho: {dev.DeviceName} -> IP: {netDev.IpAddress}");
                        Console.WriteLine();
                    }
            }

            //Kết nối mạng cho device lab2
            Console.WriteLine();
            Console.WriteLine("=== Kết nối mạng cho thiết bị trong {lab2.RoomName} ({lab2.RoomId}) ===");
            foreach (Device dev in lab2.Device)
            {
                if(dev is INetworkable netDev)
                    {
                        netDev.Connect($"180.375.1.{ipTail++}");
                        Console.WriteLine($"Đã kết nối mạng cho: {dev.DeviceName} -> IP: {netDev.IpAddress}");
                        Console.WriteLine();
                    }
            }

            //7,Duyệt các thiết bị mạng thông qua kiểu INetworkable, không phụ thuộc vào lớp cụ thể.
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("---> 7.Các thiết bị mạng: ");

            //Thiết bị mạng của lab1
            Console.WriteLine("=== Thiết bị mạng của {lab1.RoomName} ({lab1.RoomId}) ===");

            foreach (Device dev in lab1.Device)
            {
                if(dev is INetworkable netDev)
                    {
                        Console.WriteLine($"{netDev.ToString()}");
                        Console.WriteLine();
                    }
            }

            //Thiết bị mạng của lab2
            Console.WriteLine();
            Console.WriteLine("=== Thiết bị mạng củag {lab2.RoomName} ({lab2.RoomId}) ===");
            foreach (Device dev in lab2.Device)
            {
                if(dev is INetworkable netDev)
                    {
                        Console.WriteLine($"{netDev.ToString()}");
                        Console.WriteLine();
                    }
            }
        }
    }
}