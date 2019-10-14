﻿namespace Pokegraf.Application.Contract.Core.Action.Update
{
    public interface IUpdateAction : IBotAction
    {
        Telegram.Bot.Types.Update Update { get; set; }
    }
}