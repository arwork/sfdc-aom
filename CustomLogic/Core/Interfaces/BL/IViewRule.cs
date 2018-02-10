using CustomLogic.Core.Models;

namespace  CustomLogic.Core.Interfaces.BL
{
    public interface IViewRule<T>
    {
        bool Run(T model, IUnitOfWork unitOfWork, Response<T> result);
    }
}

