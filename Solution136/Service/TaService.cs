﻿namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class TaService
    {
        private readonly ITaRepository repository;

        public TaService(ITaRepository repository)
        {
            this.repository = repository;
        }

        public void InsertTa(Ta ta, ref List<string> errors)
        {
            if (ta == null)
            {
                errors.Add("Ta cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(ta.FirstName))
            {
                errors.Add("Ta first name cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(ta.LastName))
            {
                errors.Add("Ta last name cannot be null");
                throw new ArgumentException();
            }

            if (this.repository.IsNotDuplicateTa(ta, ref errors))
            {
                this.repository.AddTa(ta, ref errors);
            }
        }

        public void UpdateTa(Ta ta, ref List<string> errors)
        {
            if (ta == null)
            {
                errors.Add("Ta cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(ta.FirstName))
            {
                errors.Add("Ta first name cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(ta.LastName))
            {
                errors.Add("Ta last name cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(ta.TaId.ToString()))
            {
                errors.Add("Taid be null");
                throw new ArgumentException();
            }

            this.repository.AddTa(ta, ref errors);
        }

        public Ta GetTaBy(string ta_id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(ta_id))
            {
                errors.Add("Invalid ta_id");
                throw new ArgumentException();
            }

            return this.repository.FindTaById(int.Parse(ta_id), ref errors);
        }

        public void DeleteTa(string ta_id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(ta_id))
            {
                errors.Add("Invalid ta_id");
                throw new ArgumentException();
            }

            this.repository.RemoveTa(int.Parse(ta_id), ref errors);
        }

        public List<Ta> GetTaList(ref List<string> errors)
        {
            return this.repository.GetTaList(ref errors);
        }      
    }
}