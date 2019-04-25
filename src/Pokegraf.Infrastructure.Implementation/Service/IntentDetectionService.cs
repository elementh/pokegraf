using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using Grpc.Auth;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pokegraf.Common.Result;
using Pokegraf.Infrastructure.Contract.Dto;
using Pokegraf.Infrastructure.Contract.Model;
using Pokegraf.Infrastructure.Contract.Service;
using Pokegraf.Infrastructure.Implementation.Mapping.Extension;

namespace Pokegraf.Infrastructure.Implementation.Service
{
    public class IntentDetectionService : IIntentDetectionService
    {
        protected readonly ILogger<IntentDetectionService> Logger;
        protected readonly SessionsClient Client;
        protected readonly IConfiguration Configuration;
        
        public IntentDetectionService(ILogger<IntentDetectionService> logger, IConfiguration configuration)
        {
            Logger = logger;
            Configuration = configuration;

            //TODO: refactor as custom config
            var credentialsJson = JsonConvert.SerializeObject(configuration.GetSection("GoogleCredentials").Get<Dictionary<string, string>>());
            
            var credential = GoogleCredential.FromJson(credentialsJson);
            
            var channel = new Channel(SessionsClient.DefaultEndpoint.Host, SessionsClient.DefaultEndpoint.Port, credential.ToChannelCredentials());
            
            Client = SessionsClient.Create(channel);
        }

        public async Task<Result<IntentDto>> GetIntent(DetectIntentQuery query)
        {
            try
            {
                var session = new SessionName(Configuration["GoogleCredential:project_id"], Guid.NewGuid().ToString());
                var response = await Client.DetectIntentAsync(session, query.ToQueryInput());

                return Result<IntentDto>.Success(response.ToIntentDto());
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error detecting intent");

                return null;
            }
        }
    }
}