using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly MyContext _myContext;
        List<Room> resultList = null;
        Room result = null;

        public RoomRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<Room> Get()
        {
            resultList = _myContext.Rooms.ToList();
            return resultList;
        }

        public Room Get(string id)
        {
            result = _myContext.Rooms.FirstOrDefault(x => x.Id == id);
            return result;
        }
    }
}
