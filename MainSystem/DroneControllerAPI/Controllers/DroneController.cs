using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DroneControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/drone
        [HttpPost]
        public string Post()
        {
            Debug.WriteLine("Received post");

            byte[] buffer = ReadToEnd(Request.Body);

            Debug.WriteLine(buffer.Length);

            Image img;
            using (var ms = new MemoryStream(buffer))
            {
                img = Image.FromStream(ms);
                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "image.jpeg");
                Debug.WriteLine("Saving image in: " + imgPath);
                img.Save(imgPath);
                GetLicensePlate(getBase64String(img));
            }

            Debug.WriteLine("Ending post");
            return "succ";
        }

        private string getBase64String(Image img)
        {
            using (MemoryStream m = new MemoryStream())
            {
                img.Save(m, img.RawFormat);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        private void GetLicensePlate(string base64)
        {
            string secret_key = "sk_e78b13b44b250ebc3d9f61ff";
            string url = string.Format("https://api.openalpr.com/v2/recognize_bytes?recognize_vehicle=1&country=eu&secret_key={0}", secret_key);

            Thread t = new Thread(async () =>
            {
                var client = new HttpClient();
                StringContent content = new StringContent(base64);
                HttpResponseMessage reponse = await client.PostAsync(url, content);
                byte[] res = await reponse.Content.ReadAsByteArrayAsync();
                string json = Encoding.UTF8.GetString(res);

                string plate = (string)JObject.Parse(json)["results"][0]["plate"];

                Debug.WriteLine(plate);
            });
            t.Start();
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
