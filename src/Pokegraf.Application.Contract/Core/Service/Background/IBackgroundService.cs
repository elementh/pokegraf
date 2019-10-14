using System;
using Microsoft.Extensions.Hosting;

namespace Pokegraf.Application.Contract.Core.Service.Background
{
    public interface IBackgroundService: IHostedService, IDisposable
    {
    }
}