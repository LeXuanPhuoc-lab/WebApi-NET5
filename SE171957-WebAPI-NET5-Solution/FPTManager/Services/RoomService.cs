using AutoMapper;
using FPTManager.Entities;
using FPTManager.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Services
{
    public class RoomService : IRoomService
    {
        private readonly PRN211DemoADOContext _context;
        private readonly IMapper _mapper;

        public RoomService(PRN211DemoADOContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<RoomResponse> GetRooms()
        {
            var rooms = _context.Rooms.ToList();
            return _mapper.Map<List<RoomResponse>>(rooms);
        }
    }
}
