using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class ParticipantService : IParticipantService
    {
        private IParticipantRepository _participantRepository;

        public ParticipantService(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            else
            {
                return _participantRepository.Delete(id);
            }
        }

        public List<Participant> Get()
        {
            return _participantRepository.Get();
        }

        public List<ParticipantVM> Get(string id)
        {
            return _participantRepository.Get(id);
        }

        public bool Insert(InsertParticipantVM insertParticipantVM)
        {
            if (string.IsNullOrWhiteSpace(insertParticipantVM.id.ToString()))
            {
                return false;
            }
            else
            {
                return _participantRepository.Insert(insertParticipantVM);
            }
        }
    }
}
