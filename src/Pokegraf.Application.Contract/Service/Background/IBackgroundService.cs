using System;
using Microsoft.Extensions.Hosting;

namespace Pokegraf.Application.Contract.Service.Background
{
    public interface IBackgroundService: IHostedService, IDisposable
    {
    }
}