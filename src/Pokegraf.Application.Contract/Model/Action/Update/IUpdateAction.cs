﻿namespace Pokegraf.Application.Contract.Model.Action.Update
{
    public interface IUpdateAction : IBotAction
    {
        Telegram.Bot.Types.Update Update { get; set; }
    }
}