using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using Grpc.Auth;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
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
        protected readonly IDistributedCache Cache;
        
        public IntentDetectionService(ILogger<IntentDetectionService> logger, IConfiguration configuration, IDistributedCache cache)
        {
            Logger = logger;
            Configuration = configuration;
            Cache = cache;

            //TODO: refactor as custom config
            var credentialsJson = JsonConvert.SerializeObject(configuration.GetSection("GOOGLE_CREDENTIALS").Get<Dictionary<string, string>>());
            
            var credential = GoogleCredential.FromJson(credentialsJson);
            
            var channel = new Channel(SessionsClient.DefaultEndpoint.Host, SessionsClient.DefaultEndpoint.Port, credential.ToChannelCredentials());
            
            Client = SessionsClient.Create(channel);
        }

        public async Task<Result<IntentDto>> GetIntent(DetectIntentQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var key = $"intent:{query.Text.Trim().ToLower().GetHashCode()}";
            var rawCachedIntent = await Cache.GetStringAsync(key, cancellationToken);

            if (rawCachedIntent != null)
            {
                var cachedIntent = JsonConvert.DeserializeObject<IntentDto>(rawCachedIntent);
                
                return Result<IntentDto>.Success(cachedIntent);
            }
            
            try
            {
                var session = new SessionName(Configuration["GOOGLE_CREDENTIALS:project_id"], Guid.NewGuid().ToString());
                var response = await Client.DetectIntentAsync(session, query.ToQueryInput(), cancellationToken);

                var intent = response.ToIntentDto();
                
                await Cache.SetStringAsync(key, JsonConvert.SerializeObject(intent), cancellationToken);
                
                return Result<IntentDto>.Success(response.ToIntentDto());
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error detecting intent");

                return Result<IntentDto>.UnknownError(new List<string> {e.Message});
            }
        }
    }
}