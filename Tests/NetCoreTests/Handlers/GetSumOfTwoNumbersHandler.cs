using NetCoreCQRS.Handlers;

namespace NetCoreTests.Handlers
{
    public class GetSumOfTwoNumbersHandler: BaseHandler
    {
        public int Handle(int leftNumber, int rightNumber)
        {
            return leftNumber + rightNumber;
        }
    }
}
