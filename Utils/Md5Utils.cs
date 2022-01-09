using System.Security.Cryptography;
using System.Text;

namespace Test.CAP.Utils;


public static class Md5Utils
{
    public static string GetMD5String(string str)
    {
        return GetByteToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str)));
    }

    private static string GetByteToString(IEnumerable<byte> data)
    {
        return new StringBuilder().Append(data, f => f.ToString("x2")).ToString();
    }

    private static StringBuilder Append(this StringBuilder stringBuilder, IEnumerable<byte> vs, Func<byte, string> func)
    {
        foreach (var item in vs)
            stringBuilder.Append(func.Invoke(item));
        return stringBuilder;
    }
}