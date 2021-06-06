using System;

namespace AzureSamples.AzureSQL.Models
{
    public class SyncRequest
    {                                     
        public int FromVersion { get; set; }
        public string UserId  { get; set; }
    }      
}