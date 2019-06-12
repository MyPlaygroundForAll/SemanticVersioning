using System.Threading.Tasks;

namespace SemanticVersioning.Core
{
    public interface ITestInterface
    {
        Task DoSomething();

        //Test
        Task DoSomethingMore();
    }
}