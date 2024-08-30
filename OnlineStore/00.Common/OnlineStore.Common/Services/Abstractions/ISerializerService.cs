namespace OnlineStore.Common.Services.Abstractions
{
    public interface ISerializerService
    {
        string Serialize<TInput>(TInput input);
        TOutput Deserialize<TOutput>(string input);
        string SerializeCamelCase<TInput>(TInput input);
    }
}
