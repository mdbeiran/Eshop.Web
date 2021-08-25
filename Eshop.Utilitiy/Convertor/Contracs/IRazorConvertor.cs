namespace Eshop.Utilitiy.Convertor.Contracs
{
   public interface IRazorConvertor
    {
        string RenderPartialViewToString(string controllerName, string partialView, object model);
    }
}
