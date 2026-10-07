using System;

namespace QuanLyThietBi
{
    public class LabRoom
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public string Capacity { get; set; }
        public List<Device> Device { get; set; }
        public bool GetDevies { get; private set; }

        public LabRoom(string roomId, string roomName, string capacity)
        {
            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
            Device = new List<Device>();
        }

        //Thêm
        public void AddDevice(Device device)
        {
            // Kiểm tra tham số truyền vào có null không.
            if (device == null)
            {
                throw new ArgumentException("Không chấp nhận đối tượng thiết bị null.");
            }

            bool isExisted = Device.Any(d => d.DeviceId == device.DeviceId);

            // Kiểm tra xem có thêm hai thiết bị cùng mã trong cùng một phòng không.
            if (isExisted)
            {
                throw new ArgumentException("Đã tồn tại thiết bị!");
            }

            Device.Add(device);
            Console.WriteLine("Thêm thiết bị vào thành công");
        }

        //Xóa
        public bool RemoveDevice(string deviceId)
        {
            Device? deviceToRemove = Device.FirstOrDefault(d => d.DeviceId == deviceId);

            // Kiểm tra xem có tồn tại ID này không.
            if (deviceToRemove == null)
            {
                throw new ArgumentException("Không tồn tại deviceID này!");
            }

            Device.Remove(deviceToRemove);
            Console.WriteLine("Đã xóa thiết bị thành công!");

            return true;
        }

        //Tìm
        public Device? FindDevice(string deviceId)
        {
            Device? foundDevice = Device.FirstOrDefault(d => d.DeviceId == deviceId);

            if (foundDevice == null)
            {
                throw new ArgumentException("Không tìm thấy thiết bị cần tìm.");
            }

            return foundDevice;
        }

        //Tổng tất cả chi phí bảo chì của device
        public decimal CalculateAnnualMaintenanceCost()
        {
            decimal total = 0;

            foreach (Device dev in Device)
            {
                total += dev.CalculateAnnualMaintenanceCost();
            }

            return total;
        }

        //Cho danh sách các thiết bị cần bảo trì
        public List<Device> GetDevicesRequiringMaintenance()
        {
            List<Device> devices = new List<Device>();

            foreach (Device dev in Device)
            {
                bool isUnderMaintenance = dev.Status == DeviceStatus.UnderMaintenance;
                bool isOldDevice = DateTime.Now.Year - dev.StartYears > 5;

                if (isUnderMaintenance || isOldDevice)
                {
                    devices.Add(dev);
                }
            }

            return devices;
        }
    }
}