using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using Persistence.Data;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly GardeningContext _context;

    public RolRepository(GardeningContext context) : base(context)
    {
       _context = context;
    }
}
