using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        List<Room> Get();
        Room Get(string id);
    }
}
