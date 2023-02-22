using AutoMapper;
using CollectionsAndLinq.DAL.Context;
using Microsoft.EntityFrameworkCore;


namespace CollectionsAndLinq.BL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly CollectionsAndLinqContext _context;
        private protected readonly IMapper _mapper;

        public BaseService(CollectionsAndLinqContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
