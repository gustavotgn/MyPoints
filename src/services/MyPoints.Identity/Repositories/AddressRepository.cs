using AutoMapper;
using Dapper;
using MyPoints.Identity.Data;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Entities;
using MyPoints.Identity.Domain.Queries;
using MyPoints.Identity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Repositories
{
    public class AddressRepository : BaseRepository<UserAddress,Guid>, IAddressRepository
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public AddressRepository(ApplicationDbContext context, IMapper mapper): base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AddressQueryResult> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExistsByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
