using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using AzureSamples.AzureSQL.Models;

namespace AzureSamples.AzureSQL.Controllers
{
    [ApiController]
    [Route("trainingsession/sync")]
    public class TrainingSessionSyncController : ControllerQuery
    {
        public TrainingSessionSyncController(IConfiguration config, ILogger<TrainingSessionSyncController> logger):
            base(config, logger) {}

        public async Task<JsonElement> Get([FromBody]SyncRequest syncRequest)
        {
            // var userId = HttpContext.Request.Headers["UserId"].FirstOrDefault();
            // var fromVersion = Int32.Parse(HttpContext.Request.Headers["FromVersion"].FirstOrDefault() ?? "0");

            var userId = syncRequest.UserId;
            var fromVersion = syncRequest.FromVersion;

            var payload = new {
                userId = userId,
                fromVersion = fromVersion
            };

            var jp = JsonDocument.Parse(JsonSerializer.Serialize(payload));
            
            this._logger.LogInformation($"userId {userId}, fromVersion {fromVersion}");

            return await this.Query("get", this.GetType(), payload: jp.RootElement);
        }
    }
}
