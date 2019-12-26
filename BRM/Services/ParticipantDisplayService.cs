using BRM.Services.Interfaces;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class ParticipantDisplayService : IParticipantDisplayService
    {
        private IParticipantDisplayRepository _participantDisplayRepository;

        public ParticipantDisplayService (IParticipantDisplayRepository participantDisplayRepository)
        {
            _participantDisplayRepository = participantDisplayRepository;
        }

        public List<ParticipantDisplayVM> Get()
        {
            return _participantDisplayRepository.Get();
        }
    }
}
