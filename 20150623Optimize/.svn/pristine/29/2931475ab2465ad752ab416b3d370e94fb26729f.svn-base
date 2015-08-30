using System.ServiceModel;

namespace IDishMenuAsynUpdate
{
    [ServiceContract]
    public interface IMenuService
    {
        [OperationContract(IsOneWay = true)]
        void Update(long taskId);
    }
}