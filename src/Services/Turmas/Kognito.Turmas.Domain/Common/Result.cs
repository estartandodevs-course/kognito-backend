using System;

namespace Kognito.Turmas.Domain.Common;

public class Result
{
    public bool Success { get; protected set; }
    public string Message { get; protected set; }
    public List<string> Errors { get; protected set; }

    protected Result()
    {
        Success = true;
        Errors = new List<string>();
    }

    public static Result Ok(string message = "Operação realizada com sucesso")
    {
        return new Result 
        { 
            Success = true, 
            Message = message 
        };
    }

    public static Result Fail(string error)
    {
        return new Result 
        { 
            Success = false, 
            Errors = new List<string> { error } 
        };
    }

    public static Result Fail(List<string> errors)
    {
        return new Result 
        { 
            Success = false, 
            Errors = errors 
        };
    }
}

public class Result<T> : Result
{
    public T Data { get; private set; }

    public static Result<T> Ok(T data, string message = "Operação realizada com sucesso")
    {
        return new Result<T> 
        { 
            Success = true, 
            Data = data, 
            Message = message 
        };
    }

    public new static Result<T> Fail(string error)
    {
        return new Result<T> 
        { 
            Success = false, 
            Errors = new List<string> { error } 
        };
    }

    public new static Result<T> Fail(List<string> errors)
    {
        return new Result<T> 
        { 
            Success = false, 
            Errors = errors 
        };
    }
}