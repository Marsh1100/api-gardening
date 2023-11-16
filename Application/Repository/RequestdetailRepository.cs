using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class RequestdetailRepository : GenericRepository<Requestdetail>, IRequestdetail
{
    private readonly GardeningContext _context;

    public RequestdetailRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }



    

}
