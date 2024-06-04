
namespace WebApp.Notifications;

public interface INotificationHub {
    public Task Notification(string user, string message);
}