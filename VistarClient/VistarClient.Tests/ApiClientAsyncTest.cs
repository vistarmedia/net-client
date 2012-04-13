using NUnit.Framework;
using RestSharp.Serializers;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;
using VistarClient;
using VistarClient.Utils;
using VistarClient.Request;
using System.Threading.Tasks;
using System.Collections.Generic;
using VistarClient.Entities;
using System;

namespace VistarClient.Tests {
  public class ApiClientAsyncTest {
    [Test]
    public void SubmitAdRequestsAsync() {
      var mockery = new MockRepository();
      var taskFactory = mockery.StrictMock<IVistarTaskFactory>();
      var adRequestor = mockery.StrictMock<IAdRequestor>();

      var adRequests = new List<AdRequest> {
        new AdRequest(),
        new AdRequest(),
        new AdRequest()
      };

      var expectedTasks = new List<Task<List<Advertisement>>> {
        new Task<List<Advertisement>>(null),
        new Task<List<Advertisement>>(null),
        new Task<List<Advertisement>>(null)
      };

      using(mockery.Record()) {
        Expect.Call(taskFactory.StartNew(adRequestor.RunSubmitAdRequest, adRequests[0])).Return(expectedTasks[0]);
        Expect.Call(taskFactory.StartNew(adRequestor.RunSubmitAdRequest, adRequests[1])).Return(expectedTasks[1]);
        Expect.Call(taskFactory.StartNew(adRequestor.RunSubmitAdRequest, adRequests[2])).Return(expectedTasks[2]);
      }

      using(mockery.Playback()) {
        var client = new ApiClientAsync(adRequestor, taskFactory);
        var tasks = client.SubmitAdRequestsAsync(adRequests);
        Assert.AreEqual(expectedTasks, tasks);
      }
    }
  }
}

