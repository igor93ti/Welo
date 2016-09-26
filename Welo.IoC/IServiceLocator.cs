using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Welo.IoC
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }
}
