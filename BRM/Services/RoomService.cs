using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class RoomService : IRoomService
    {
        bool status = false;

        private IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public List<Room> Get()
        {
            return _roomRepository.Get();
        }

        public Room Get(string id)
        {
            return _roomRepository.Get(id);
        }
    }
}
