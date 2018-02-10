using CustomLogic.Core.Models;

namespace  CustomLogic.Core.Interfaces.BL
{
    public interface IUpdateRule<T>
    {
        bool Run(T model, IUnitOfWork unitOfWork, Response<T> result);
    }
}

