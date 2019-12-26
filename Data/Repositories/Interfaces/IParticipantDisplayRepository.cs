using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IParticipantDisplayRepository
    {
        List<ParticipantDisplayVM>Get();
    }
}
