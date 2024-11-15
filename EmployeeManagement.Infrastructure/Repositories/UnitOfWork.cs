﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Core.Interfaces;
using PMS.Infrastructure.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Interfaces;

namespace UnitOfWorkDemo.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextClass _dbContext;
        public IEmployeeRepository Employee { get; }
        public IEmployeeRecordRepository EmployeeRecord { get; }
        public IUserRepository UserRepository { get; }

        private readonly IMapper _mapper;

        public UnitOfWork(DbContextClass dbContext, IMapper mapper,
                            IEmployeeRepository EmployeeRepository, IEmployeeRecordRepository EmployeeRecordRepository, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            Employee = EmployeeRepository;
            EmployeeRecord = EmployeeRecordRepository;
            UserRepository = userRepository;
        }
  
        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

      
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

    }
}
