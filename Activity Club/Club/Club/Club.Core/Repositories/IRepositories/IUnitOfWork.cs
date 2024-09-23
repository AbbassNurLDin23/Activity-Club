using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ILookupRepository Lookups { get; }
        public IUserRepository Users { get; }
        public IMemberRepository Members { get; }
        public IGuideRepository Guides { get; }
        public IEventRepository Events { get; }
        public IAuthorizationRepository Authorizations { get; }

        Task<int> CommitAsync();
    }
}
