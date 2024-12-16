namespace Reborn.Middleware;

public class RequestTrackingMeddleware
{
    private readonly RequestDelegate _next;
    private static readonly Dictionary<string, List<DateTime>> UserRequests = new();
    public int UserRequestCount => UserRequests.Count;

    public RequestTrackingMeddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string UserIP = context.Connection.RemoteIpAddress.ToString();
        lock (UserRequests)
        {
            if (!UserRequests.ContainsKey(UserIP))
            {
                UserRequests.Add(UserIP, new List<DateTime>());
            }
            UserRequests[UserIP].Add(DateTime.Now);
            UserRequests[UserIP].RemoveAll( x => x < DateTime.Now.AddYears(-3));
        }
    }

    public static int RequestCount(string UserRequstIP)
    {
        lock (UserRequests)
        {
            return UserRequests[UserRequstIP].Count;
        }
    }
}