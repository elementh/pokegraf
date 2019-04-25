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
using Pokegraf.Infrastructure.Contract.Dto;
using Pokegraf.Infrastructure.Contract.Service;

namespace Pokegraf.Infrastructure.Implementation.Service
{
    public class IntentDetectionService : IIntentDetectionService
    {
        protected readonly ILogger<IntentDetectionService> Logger;
        protected readonly SessionsClient Client;
        
        public IntentDetectionService(ILogger<IntentDetectionService> logger, IConfiguration configuration)
        {
            Logger = logger;
            
            //TODO: refactor as custom config
            var credentialsJson = JsonConvert.SerializeObject(configuration.GetSection("GoogleCredentials").Get<Dictionary<string, string>>());
            
            var credential = GoogleCredential.FromJson(credentialsJson);
            
            var channel = new Channel(SessionsClient.DefaultEndpoint.Host, SessionsClient.DefaultEndpoint.Port, credential.ToChannelCredentials());
            
            Client = SessionsClient.Create(channel);
        }

        public async Task<IntentDto> GetIntent(string userQuery)
        {
            var session = new SessionName("newagent-9bc74", Guid.NewGuid().ToString());
            
            var formalQuery = new QueryInput
            {
                Text = new TextInput
                {
                    Text = "I want info of pidgey",
                    LanguageCode = "en-us"
                }
            };

            try
            {
                var response = await Client.DetectIntentAsync(session, formalQuery);

            }
            catch (Exception e)
            {
                Logger.LogError(e, "Unhandled error detecting intent");
            }

            
            return new IntentDto();
        }
    }
}