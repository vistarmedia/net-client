using System.Collections.Generic;
using System.Threading.Tasks;
using VistarClient.Entities;
using VistarClient.Request;
using VistarClient.Utils;

namespace VistarClient {
  public class ApiClientAsync {
    readonly IAdRequestor adRequestor;
    readonly IVistarTaskFactory taskFactory;

    public ApiClientAsync() : this(new AdRequestor(), new VistarTaskFactory()) {

    }

    public ApiClientAsync(IAdRequestor adRequestor, IVistarTaskFactory taskFactory) {
      this.adRequestor = adRequestor;
      this.taskFactory = taskFactory;
    }

    public List<Task<List<Advertisement>>> SubmitAdRequestsAsync(List<AdRequest> requests) {
      var tasks = new List<Task<List<Advertisement>>>();

      foreach(var request in requests) {
        var task = taskFactory.StartNew(adRequestor.RunSubmitAdRequest, request);
        tasks.Add(task);
      }

      return tasks;
    }
  }
}
