//Họ và Tên: Trần Thùy Linh
//MSSV: 202418935
using System;

namespace QuanLyThietBi
{
    public interface INetworkable
    {
        string IpAddress {get;}
        bool IsConnected {get;}
        
        void Connect (string ipAddress);
        void Disconnect();
    }
}