﻿using Data.Context;
using Data.Models;
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
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly MyContext _myContext;
        bool status = false;
        List<Participant> resultList = null;
        List<ParticipantVM> resultListVM = null;
        Participant result = null;

        public ParticipantRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public bool Delete(string id)
        {
            var delete = _myContext.Participants.Find(id);
            var backparticipant = _myContext.Employees.Find(id);
            //backparticipant.is_participant = false;
            delete.is_deleted = true;
            _myContext.Entry(delete).State = EntityState.Modified;
            _myContext.Entry(backparticipant).State = EntityState.Modified;
            var result = _myContext.SaveChanges();
            //_myContext.Participants.Remove(delete);
            //var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }

        public List<Participant> Get()
        {
            resultList = _myContext.Participants.ToList();
            return resultList;
        }

        public List<ParticipantVM> Get(string id)
        {
            try
            {
                resultListVM = _myContext.ParticipantVMs.FromSql($"call sp_retrieve_detail_batch_class({id})").ToList();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return resultListVM;
        }

        public Participant GetById(string id)
        {
            result = _myContext.Participants.Find(id);
            return result;
        }

        public bool Insert(InsertParticipantVM insertParticipantVM)
        {
            for (int i = 0; i < insertParticipantVM.id.ToArray().Length; i++)
            {
                string id = insertParticipantVM.id[i];
                var get = _myContext.Participants.Where(x => x.id == id).ToList();
                if (get.Count > 0)
                {
                    return false;
                }
                else
                {
                    var participants = new Participant()
                    {
                        id = insertParticipantVM.id[i],
                        batch_class = insertParticipantVM.batch_class
                    };
                    _myContext.Participants.Add(participants);
                    var pullParticipant = _myContext.Employees.Find(participants.id);
                    //pullParticipant.is_participant = true;
                    _myContext.Entry(pullParticipant).State = EntityState.Modified;
                }
            }
            var result = _myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;
        }
    }
}
