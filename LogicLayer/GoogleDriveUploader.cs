//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace LogicLayer
//{
//    using System;
//    using System.Net.Http;
//    using System.Net.Http.Headers;
//    using System.IO;

//    class GoogleDriveUploader
//    {
//        static string accessToken = "YOUR_ACCESS_TOKEN";
//        static string filePath = @"C:\path\to\file.txt";

//        public static void UploadFile()
//        {
//            using (var client = new HttpClient())
//            {
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

//                var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
//                fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain"); // Set the MIME type accordingly

//                var request = new HttpRequestMessage(HttpMethod.Post, "https://www.googleapis.com/upload/drive/v3/files?uploadType=media")
//                {
//                    Content = fileContent
//                };

//                var response = client.SendAsync(request).Result;
//                if (response.IsSuccessStatusCode)
//                {
//                    Console.WriteLine("File uploaded successfully.");
//                }
//                else
//                {
//                    Console.WriteLine("Failed to upload file.");
//                }
//            }
//        }
//    }

//}
