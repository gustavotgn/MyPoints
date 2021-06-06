using AutoMapper;
using Dapper;
using MyPoints.Identity.Data;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Queries;
using MyPoints.Identity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private IdentityContext _context;
        private IMapper _mapper;

        public AddressRepository(IdentityContext identityContext, IMapper mapper)
        {
            this._context = identityContext;
            _mapper = mapper;
        }

        public async Task<AddUserAddressCommandResult> AddAsync(AddUserAddressCommand request)
        {
            var transaction = _context.Connection.BeginTransaction();
            try
            {

                var id = await _context.Connection.ExecuteScalarAsync<int>(@"
                    INSERT INTO Address (Street,City,PostalCode,State,Complements,Number)
                    VALUES(@Street,@City,@PostalCode,@State,@Complements,@Number);
                    SELECT LAST_INSERT_ID();", request);
                
                await _context.Connection.ExecuteAsync(@"
                    UPDATE User SET AddressId = @addressId WHERE Id = @userId;", new { addressId = id, userId = request.UserId});

                transaction.Commit();

                var result = _mapper.Map<AddUserAddressCommandResult>(request);
                result.Id = id;
                return result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }
        public async Task<AddressQueryResult> GetAsync(int id)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<AddressQueryResult>(@"
                SELECT * FROM Address 
                WHERE Id = @id; ", new { id });

        }

        public async Task<AddressQueryResult> GetByUserIdAsync(int userId)
        {
            return await _context.Connection.QueryFirstOrDefaultAsync<AddressQueryResult>(@"
                    SELECT Address.* FROM Address
                        INNER JOIN User ON User.AddressId = Address.Id 
                    WHERE User.Id = @userId;", new { userId });

        }
    }
}
