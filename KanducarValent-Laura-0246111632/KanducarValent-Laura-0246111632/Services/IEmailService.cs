using Rad.Model;
namespace KanducarValent_Laura_0246111632.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Contact contactModel);
    }
}
