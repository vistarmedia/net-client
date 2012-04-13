using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using VistarClient.Entities;
using VistarClient.Request;

namespace VistarClient.Utils {
  public interface IVistarTaskFactory {
    Task<List<Advertisement>> StartNew(Func<AdRequest, List<Advertisement>> method, AdRequest adRequest);
  }

  public class VistarTaskFactory : IVistarTaskFactory {
    internal VistarTaskFactory() {

    }

    public Task<List<Advertisement>> StartNew(Func<AdRequest, List<Advertisement>> method, AdRequest adRequest) {
      return Task.Factory.StartNew(() => method(adRequest));
    }
  }
}