using System;
using System.Threading.Tasks;

namespace GraphqlCoreDemo.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
        IProductRepository Product { get; }
        IStoreRepository Store { get; }
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private IProductRepository _productRepository;
        private IStoreRepository _storeRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IProductRepository Product
        {
            get
            {
                return _productRepository ?? (_productRepository = new ProductRepository(_context));
            }
        }

        public IStoreRepository Store
        {
            get
            {
                return _storeRepository ?? (_storeRepository = new StoreRepository(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
