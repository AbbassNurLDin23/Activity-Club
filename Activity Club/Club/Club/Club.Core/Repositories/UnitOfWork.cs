using Club.Core.DataModels;
using Club.Core.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Core.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDataContext _dataContext;
        private ILookupRepository _lookupRepository;
        private IUserRepository _userRepository;
        private IMemberRepository _memberRepository;
        private IGuideRepository _guideRepository;
        private IEventRepository _eventRepository;
        private IAuthorizationRepository _authorizationRepository;


        public UnitOfWork(MyDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ILookupRepository Lookups =>
            _lookupRepository ??= new LookupRepository(_dataContext);

        public IUserRepository Users =>
            _userRepository ??= new UserRepository(_dataContext);

        public IMemberRepository Members =>
            _memberRepository ??= new MemberRepository(_dataContext);

        public IGuideRepository Guides =>
            _guideRepository ??= new GuideRepository(_dataContext);

        public IEventRepository Events =>
            _eventRepository ??= new EventRepository(_dataContext);

        public IAuthorizationRepository Authorizations =>
            _authorizationRepository ??= new AuthorizationRepository(_dataContext);


        public async Task<int> CommitAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
