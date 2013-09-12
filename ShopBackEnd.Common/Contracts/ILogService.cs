using log4net;

namespace ShopBackEnd.Common
{
    public interface ILogService
    {
        ILog Logger();
    }
}
