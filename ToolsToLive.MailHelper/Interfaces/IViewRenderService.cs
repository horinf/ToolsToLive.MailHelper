using System.Threading.Tasks;

namespace ToolsToLive.MailHelper.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string path, string viewName, object model);
    }
}
