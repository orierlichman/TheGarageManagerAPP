using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheGarageManagerAPP.Models;


namespace TheGarageManagerApp.Services
{
    public class TheGarageManagerWebAPIProxy
    {
        #region without tunnel
        /*
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "localhost";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5055/api/" : $"http://{serverIP}:5055/api/";
        private static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5055" : $"http://{serverIP}:5055";
        */
        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "l4dfcs7m-5055.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://l4dfcs7m-5055.euw.devtunnels.ms/api/";
        private static string ImageBaseAddress = "https://l4dfcs7m-5055.euw.devtunnels.ms/";
        #endregion

        public TheGarageManagerWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        public string GetImagesBaseAddress()
        {
            return TheGarageManagerWebAPIProxy.ImageBaseAddress;
        }

        public string GetDefaultProfilePhotoUrl()
        {
            return $"{TheGarageManagerWebAPIProxy.ImageBaseAddress}/profileImages/default.png";
        }


        public async Task<UserModels?> LoginAsync(LoginInfoModels userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    UserModels? result = JsonSerializer.Deserialize<UserModels>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }





        //This methos call the Register web API on the server and return the AppUser object with the given ID
        //or null if the call fails
        public async Task<UserModels?> Register(UserModels user, string rashamHavarot)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}register?rashamHavarot={rashamHavarot}";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    UserModels? result = JsonSerializer.Deserialize<UserModels>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UserStatusModels>?> GetUserStatusAsync()
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetUserStatuses";
            try
            {
                //Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<UserStatusModels>? result = JsonSerializer.Deserialize<List<UserStatusModels>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<List<GaragePartsModels>?> GetAllGaragePartsAsync()
        {
            // Set URI to the specific function API
            string url = $"{this.baseUrl}getAllGarageParts";

            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List<GaragePartsDTO>
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<GaragePartsModels> result = JsonSerializer.Deserialize<List<GaragePartsModels>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return null if something goes wrong
                return null;
            }
        }



        public async Task<UserModels?> UpdatUser(UserModels user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updateUser";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    UserModels? result = JsonSerializer.Deserialize<UserModels>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<AppointmentModels?> AddAppointmentAsync(AppointmentModels appointmentModels)
        {
            // הגדרת ה-URI של ה-API
            string url = $"{this.baseUrl}addAppointment";

            try
            {
                // המרת ה-DTO של הפגישה ל-JSON
                string json = JsonSerializer.Serialize(appointmentModels);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // קריאת ה-API ב-PostAsync
                HttpResponseMessage response = await client.PostAsync(url, content);

                // בדיקה אם הקריאה הצליחה
                if (response.IsSuccessStatusCode)
                {
                    // אם הקריאה הצליחה, נקרא את התשובה
                    string resContent = await response.Content.ReadAsStringAsync();

                    // המרת התשובה ל-AppointmentDTO
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    AppointmentModels? result = JsonSerializer.Deserialize<AppointmentModels>(resContent, options);
                    return result;
                }
                else
                {
                    // במקרה של שגיאה, מחזירים null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה כללית
                return null;
            }
        }




        public async Task<List<AppointmentModels>?> GetAppointmentsAsync()
        {
            // הגדרת ה-URI של ה-API
            string url = $"{this.baseUrl}getAppointments";

            try
            {
                // קריאת ה-API ב-GetAsync
                HttpResponseMessage response = await client.GetAsync(url);

                // בדיקה אם הקריאה הצליחה
                if (response.IsSuccessStatusCode)
                {
                    // אם הקריאה הצליחה, נקרא את התשובה
                    string resContent = await response.Content.ReadAsStringAsync();

                    // המרת התשובה לרשימה של Models.Appointment
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<AppointmentModels>? result = JsonSerializer.Deserialize<List<AppointmentModels>>(resContent, options);
                    return result;
                }
                else
                {
                    // במקרה של שגיאה, מחזירים null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה כללית
                return null;
            }
        }



        public async Task<List<AppointmentStatusModels>?> GetAppointmentStatusesAsync()
        {
            // הגדרת ה-URI של ה-API
            string url = $"{this.baseUrl}getAppointmentStatuses";

            try
            {
                // קריאת ה-API ב-GetAsync
                HttpResponseMessage response = await client.GetAsync(url);

                // בדיקה אם הקריאה הצליחה
                if (response.IsSuccessStatusCode)
                {
                    // אם הקריאה הצליחה, נקרא את התשובה
                    string resContent = await response.Content.ReadAsStringAsync();

                    // המרת התשובה לרשימה של Models.AppointmentStatus
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<AppointmentStatusModels>? result = JsonSerializer.Deserialize<List<AppointmentStatusModels>>(resContent, options);
                    return result;
                }
                else
                {
                    // במקרה של שגיאה, מחזירים null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה כללית
                return null;
            }
        }



        public async Task<AppointmentModels> UpdateAppointmentStatusAsync(AppointmentModels updateModel)
        {
            // הגדרת ה-URI של ה-API
            string url = $"{this.baseUrl}updateAppointmentStatus";

            try
            {
                // המרת ה-Appointment Model ל-JSON
                string json = JsonSerializer.Serialize(updateModel);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // שליחת הבקשה ב-POST עם ה-Model
                HttpResponseMessage response = await client.PostAsync(url, content);

                // בדיקה אם הקריאה הצליחה
                if (response.IsSuccessStatusCode)
                {
                    // אם הקריאה הצליחה, נקרא את התשובה
                    string resContent = await response.Content.ReadAsStringAsync();

                    // המרת התשובה ל-Appointment Model
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    AppointmentModels? result = JsonSerializer.Deserialize<AppointmentModels>(resContent, options);
                    return result;
                }
                else
                {
                    // במקרה של שגיאה, מחזירים null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה כללית
                return null;
            }
        }




        public async Task<CarRepairModels?> AddCarRepairAsync(CarRepairModels carRepairModels)
        {
            // הגדרת ה-URI של ה-API
            string url = $"{this.baseUrl}addCarRepair";

            try
            {
                // המרת ה-DTO של תיקון הרכב ל-JSON
                string json = JsonSerializer.Serialize(carRepairModels);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // קריאת ה-API ב-PostAsync
                HttpResponseMessage response = await client.PostAsync(url, content);

                // בדיקה אם הקריאה הצליחה
                if (response.IsSuccessStatusCode)
                {
                    // אם הקריאה הצליחה, נקרא את התשובה
                    string resContent = await response.Content.ReadAsStringAsync();

                    // המרת התשובה ל-CarRepairDTO
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    CarRepairModels? result = JsonSerializer.Deserialize<CarRepairModels>(resContent, options);
                    return result;
                }
                else
                {
                    // במקרה של שגיאה, מחזירים null
                    return null;
                }
            }
            catch (Exception ex)
            {
                // במקרה של שגיאה כללית
                return null;
            }
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



        public async Task<bool> TransferPendingAppointmentsAsync()
        {
            try
            {
                // 1. כתובת לשרת אפליקציה 2 שמחזיר את התורים עם סטטוס Pending
                string urlApp2 = $"{this.baseUrl}getPendingAppointments"; 

                HttpResponseMessage responseApp2 = await client.GetAsync(urlApp2);
                if (!responseApp2.IsSuccessStatusCode)
                    return false;

                string jsonAppointments = await responseApp2.Content.ReadAsStringAsync();

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<AppointmentModels>? pendingAppointments =
                    JsonSerializer.Deserialize<List<AppointmentModels>>(jsonAppointments, options);

                if (pendingAppointments == null || pendingAppointments.Count == 0)
                    return false;

                // 2. כתובת לשרת אפליקציה 1 שמכניס את התורים (insertAppointments)
                string urlApp1 = $"{this.baseUrl}insertAppointments";

                string jsonToSend = JsonSerializer.Serialize(pendingAppointments);
                StringContent content = new StringContent(jsonToSend, Encoding.UTF8, "application/json");

                HttpResponseMessage responseApp1 = await client.PostAsync(urlApp1, content);
                return responseApp1.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // ניתן לרשום ל-log או לטפל בשגיאה אחרת
                return false;
            }
        }



    }

}
