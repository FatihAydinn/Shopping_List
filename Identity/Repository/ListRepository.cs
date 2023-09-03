using AutoMapper;
using Shopping_List.Data;
using Shopping_List.Models;

namespace Shopping_List.Repository
{
    public class ListRepository /*: IListRepository*/
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        // Constructor Injection
        public ListRepository(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //24.03 - 02:10
        //public async Task<ShoppingListDto> CreateUpdateList(ShoppingListDto listDto)
        //{
        //    ShoppingList list = _mapper.Map<ShoppingList>(listDto);
        //    var listInDb = await _context.ShoppingLists.FirstOrDefault(x => x.ListId ==
        //    listDto.ListHeaders.FirstOrDefault().ListId);
        //}
    }
}
