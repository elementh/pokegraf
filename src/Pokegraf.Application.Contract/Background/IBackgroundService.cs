using System;

namespace Pokegraf.Application.Contract.Background
{
    public interface IBackgroundService: IHostedService, IDisposable
    {
    }
}