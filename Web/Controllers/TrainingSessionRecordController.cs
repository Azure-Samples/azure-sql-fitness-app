using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using AzureSamples.AzureSQL.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace AzureSamples.AzureSQL.Controllers
{
    [ApiController]
    [Route("trainingsession/record")]
    public class TrainingSessionRecordController : ControllerBase
    {
        protected readonly ILogger<TrainingSessionSyncController> _logger;
        private readonly IConfiguration _config;
        
        public TrainingSessionRecordController(IConfiguration config, ILogger<TrainingSessionSyncController> logger)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<ActionResult> Post([FromBody]TrainingSession session)
        {
            var procedure = "web.post_trainingsession";

            try
            {
                using(var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"))) {
                    DynamicParameters parameters = new DynamicParameters();

                    var jsonParameters = JsonSerializer.Serialize(session);
                    parameters.Add("Json", jsonParameters);
                                        
                    var qr = await conn.ExecuteScalarAsync<string>(
                        sql: procedure, 
                        param: parameters, 
                        commandType: CommandType.StoredProcedure
                    );                
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot save training sessions");

                return new StatusCodeResult(500);
            }

            return new OkResult();
        }
    }
}
