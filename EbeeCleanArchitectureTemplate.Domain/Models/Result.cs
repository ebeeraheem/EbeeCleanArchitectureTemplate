using System.Text.Json.Serialization;

namespace EbeeCleanArchitectureTemplate.Domain.Models;

public class Result<T> : Result
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Value { get; }

    [JsonConstructor]
    private Result(int statusCode, bool isSuccess, T value, Error error) : base(statusCode, isSuccess, error)
    {
        StatusCode = statusCode;
        Value = value;
    }

    public static Result<T> Success(T value, int statusCode = 200) =>
        new(statusCode, true, value, null!);
    public static new Result<T> Failure(int statusCode, Error error) =>
        new(statusCode, false, default!, error);
}

public class Result
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsFailure => !IsSuccess;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error Error { get; }

    [JsonConstructor]
    protected Result(int statusCode, bool isSuccess, Error error)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success(int statusCode = 200) =>
        new(statusCode, true, null!);
    public static Result Failure(int statusCode, Error error) => new(statusCode, false, error);
}
