// Hızlı, synchronous, direct calls
using System;
using System.Collections.Generic;

public interface IGameCommand<TRequest, TResult>
{
    TResult Execute(TRequest request);
}

public class GameCommandExecutor
{
    private Dictionary<Type, object> _commands = new();

    public void Register<TReq, TRes>(IGameCommand<TReq, TRes> command)
    {
        _commands[typeof(IGameCommand<TReq, TRes>)] = command;
    }

    public TRes Execute<TReq, TRes>(TReq request)
    {
        var command = _commands[typeof(IGameCommand<TReq, TRes>)] as IGameCommand<TReq, TRes>;
        return command.Execute(request);
    }
}

