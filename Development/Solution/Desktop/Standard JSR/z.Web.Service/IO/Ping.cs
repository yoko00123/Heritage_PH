using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;

namespace z.Web.Service.IO
{
   public class Ping
    {

       public string IPAddress {get;set;}
       public bool Abort { get; set; }

       public Ping(string IPAddress)
       {
           this.IPAddress = IPAddress;
           this.Abort = false;
       }

       public void SetAbort()
       {
           this.Abort = true;
       }

       public void Reply(Action<eStatus, PingEventArgs> action, Action<string, int> ErrorHandler = null)
       {
           var png = new System.Net.NetworkInformation.Ping();
           this.Abort = false;
           int trycount = 0;

       retry:
           try
           {
               while (!Abort)
               {
                   PingReply pr = null;
                   if (this.IPAddress.ToLower().Contains("http"))
                   {
                       var uri = new Uri(this.IPAddress);
                       pr = png.Send(uri.Host);
                   }
                   else
                   {
                       pr = png.Send(this.IPAddress);
                   }

                   switch (pr.Status)
                   {
                       case IPStatus.Success: 
                           action(eStatus.Success, new PingEventArgs() { RoundTrip = pr.RoundtripTime, TryCount = trycount }); 
                           break;
                       default:
                           trycount++;
                           action(eStatus.TimeOut, new PingEventArgs() { RoundTrip = pr.RoundtripTime, TryCount = trycount }); 
                           break;
                   }

                   Thread.Sleep(500);
               }
           }
           catch (Exception ex)
           {
               if (ErrorHandler != null)
               {
                   ErrorHandler(ex.Message, trycount);
                   trycount++;

                   Thread.Sleep(500);
                   goto retry;
               }
               else
               {
                   throw ex;
               }
           }
           finally
           {
               GC.Collect();
           }

       }

       public void Wait(int time = 500)
       {
           System.Threading.Thread.Sleep(time);
       }

       public enum eStatus{
           Success = 0,
           TimeOut = 1,
       }

       public class PingEventArgs
       {
           public long RoundTrip { get; set; }
           public int TryCount { get; set; }
       }

    }
}
