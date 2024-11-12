



using PMS.Core.Interfaces;

namespace UnitOfWorkDemo.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employee { get; }
        IEmployeeRecordRepository EmployeeRecord { get; }
        IUserRepository UserRepository { get; }
        Task<int> CompleteAsync();
        int Save();
   
    }
}
