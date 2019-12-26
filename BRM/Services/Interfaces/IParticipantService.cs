using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services.Interfaces
{
    public interface IParticipantService
    {
        List<Participant> Get();
        List<ParticipantVM> Get(string id);
        bool Insert(InsertParticipantVM insertParticipantVM);
        bool Delete(string id);
    }
}
