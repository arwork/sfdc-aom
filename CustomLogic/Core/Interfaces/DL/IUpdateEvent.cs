using CustomLogic.Core.Models;

namespace  CustomLogic.Core.Interfaces.DL
{
    internal interface IUpdateEvent<T>
    {
        bool Run(T model, IUnitOfWork unitOfWork, Response<T> result);
    }
}
