using System.Threading.Tasks;

namespace SemanticVersioning.Core
{
    public class TestClass: ITestInterface
    {
        //Do Nothing
        public Task DoSomething()
        {
            throw new System.NotImplementedException();
        }

        public Task DoSomethingMore()
        {
            throw new System.NotImplementedException();
        }
    }
}