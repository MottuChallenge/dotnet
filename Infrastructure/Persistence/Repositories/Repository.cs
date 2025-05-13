﻿using Microsoft.EntityFrameworkCore;
using MottuGrid_Dotnet.Infrastructure.Context;

namespace MottuGrid_Dotnet.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MottuGridContext _context;

        private readonly DbSet<T> _dbSet;

        public Repository(MottuGridContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
