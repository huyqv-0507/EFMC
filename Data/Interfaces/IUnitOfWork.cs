using System;

namespace EFMC.Data.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
