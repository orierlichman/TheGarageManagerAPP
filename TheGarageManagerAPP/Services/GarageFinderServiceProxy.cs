using TheGarageManagerAPP;
using TheGarageManagerAPP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using TasksManagementApp.Models;

namespace TasksManagementApp.Services
{
    public class GarageFinderServiceProxy
    {
        #region without tunnel
        /*
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "localhost";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110/api/" : $"http://{serverIP}:5110/api/";
        private static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110" : $"http://{serverIP}:5110";
        */
        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "v6zd56b7-7181.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://v6zd56b7-7181.euw.devtunnels.ms/api/";
        private static string ImageBaseAddress = "https://v6zd56b7-7181.euw.devtunnels.ms/";
        #endregion

        public GarageFinderServiceProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        public string GetImagesBaseAddress()
        {
            return GarageFinderServiceProxy.ImageBaseAddress;
        }

        public string GetDefaultProfilePhotoUrl()
        {
            return $"{GarageFinderServiceProxy.ImageBaseAddress}/profileImages/default.png";
        }

        

        public  List<VehicleModels> GetVehicles(int garageId)
        {
            List<VehicleModels> l = new List<VehicleModels>();
            l.Add(new VehicleModels()
            {
                Color = "Blue",
                LicensePlate = "11111",
                CurrentMileage = 90000,
                Manufacturer = "Porche",
                Model = "Y",
                FuelType = "95",
                YearVehicle = 1955,
                ImageURL = "https://cdn.dealeraccelerate.com/ecpch/2/913/49752/790x1024/w/1955-porsche-356-speedster"
            });

            return l;
        }






        public async Task<List<AppointmentModels>?> GetAppointmentsByStatusAsync(int garageId)
        {
            string url = $"{this.baseUrl}getAppointmentsByStatus";
            try
            {
                string json = JsonSerializer.Serialize(garageId);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    // Deserialize into your custom model (AppointmentModels)
                    List<AppointmentModels>? result = JsonSerializer.Deserialize<List<AppointmentModels>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }





    }
}
