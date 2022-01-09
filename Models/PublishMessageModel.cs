using Test.CAP.Utils;

namespace Test.CAP.Models;

public class PublishMessageModel<T>
{
    public PublishMessageModel(LoginUser user, T data)
    {
        User = user;
        Data = data;
    }

    public LoginUser User { get; set; }

    public T Data { get; set; }
}