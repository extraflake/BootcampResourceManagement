using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IParticipantRepository
    {
        List<Participant> Get();
        List<ParticipantVM> Get(string id);
        Participant GetById(string id);
        bool Insert(InsertParticipantVM insertParticipantVM);
        bool Delete(string id);
    }
}
