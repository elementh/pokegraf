using System;
using Microsoft.Extensions.Hosting;

namespace Pokegraf.Application.Contract.Background
{
    public interface IBackgroundService: IHostedService, IDisposable
    {
    }
}