
namespace Blog.Business.Lang
{
    public class LangService<T>
    {
        public string Message(LangEnums lang)
        {
            return typeof(T).Name + lang.ToString();
        }
    }
}
