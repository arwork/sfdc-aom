using CustomLogic.Core.Models;

namespace  CustomLogic.Core.Interfaces.DL
{
    internal interface IViewEvent<T>
    {
        Response<T> Run(T model, IUnitOfWork unitOfWork, Response<T> result);
    }
}
