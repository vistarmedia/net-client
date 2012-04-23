using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VistarClient.Request;
using VistarClient.Entities;
using VistarClient;
using System.Threading.Tasks;

namespace ConsoleApplication1 {
  class Program {
    public static void Main(string[] args) {
      Console.WriteLine("Running AdRequestTest...\r\n");
      RunAdRequestTest(int.Parse(args[0]));
    }

    static void RunAdRequestTest(int count) {
      var requests = new List<AdRequest>();

      for (var i = 0; i < count; i++) {
        var request = new AdRequest {
          NetworkId = "24ba0582-7648-48b2-a7f4-0af3783b55f0",
          ApiKey = "eb7d6e26-5930-4fef-a3c7-aa023f31cefd",
          DeviceId = "VistarDisplay0",
          DisplayTime = DateTime.Now.ToFileTimeUtc(),
          NumberOfScreens = 1,
          DirectConnection = false,
          DisplayAreas = new List<DisplayArea> {
             new DisplayArea{
             Id = "display-test-" + i.ToString(),
             Width = 1280,
             Height = 720,
             AllowAudio = true,
             SupportedMedia = new List<string> {
               "image/gif", 
               "image/jpeg", 
               "image/png",
               "application/x-shockwave-flash", 
               "video/x-flv",
               "video/mp4"
             }
           }
         },
          DeviceAttributes = new List<DeviceAttribute> {
            new DeviceAttribute {
             Name = "test_me",
             Value = "test_value"
            }
          }
        };
        requests.Add(request);
      }

      Console.WriteLine("I am main in thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

      var client = new ApiClientAsync();

      var startTime = DateTime.Now;

      var rtn = client.SubmitAdRequestsAsync(requests);
      Console.WriteLine("I was main in thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

      Task.WaitAll(rtn.ToArray());

      Console.WriteLine("Last id: {0}", rtn[rtn.Count - 1].Result[0].Id);

      var endTime = DateTime.Now;

      Console.WriteLine("Result for {0} requests: {1}", count, endTime - startTime);
      Console.ReadKey();
    }
  }
}
