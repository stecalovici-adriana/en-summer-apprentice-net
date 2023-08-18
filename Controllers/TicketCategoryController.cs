using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketManagerSystem.Api.Repositories;

namespace TicketManagerSystem.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketCategoryController : ControllerBase
    {


        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;
        public TicketCategoryController(ITicketCategoryRepository ticketCategoryRepository, IMapper mapper)
        {
            _ticketCategoryRepository = ticketCategoryRepository;
            _mapper = mapper;

        }



    }
}
