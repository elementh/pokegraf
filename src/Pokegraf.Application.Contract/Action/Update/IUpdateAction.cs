﻿namespace Pokegraf.Application.Contract.Action.Update
{
    public interface IUpdateAction : IBotAction
    {
        Telegram.Bot.Types.Update Update { get; set; }
    }
}