using Data.Context;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class ParticipantDisplayRepository : IParticipantDisplayRepository
    {
        private readonly MyContext _myContext;
        List<ParticipantDisplayVM> resultListVM = null;

        public ParticipantDisplayRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public List<ParticipantDisplayVM> Get()
        {
            resultListVM = _myContext.ParticipantDisplayVMs.FromSql($"call sp_retrieve_participant_display").ToList(); 
            return resultListVM;
        }
    }
}
