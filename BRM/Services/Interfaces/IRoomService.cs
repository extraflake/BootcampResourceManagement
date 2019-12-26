using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IRoomService
    {
        List<Room> Get();
        Room Get(string id);
    }
}
