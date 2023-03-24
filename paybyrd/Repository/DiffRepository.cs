using Microsoft.EntityFrameworkCore;
using paybyrd.Context;
using paybyrd.Entities;
using paybyrd.Entities.Enum;
using System;
using System.Linq;

namespace paybyrd.Interfaces.Repository
{
    public class DiffRepository : IDiffRepository
    {
        private readonly PayByrdContext _context;


        public DiffRepository(PayByrdContext context)
        {
            _context = context;
        }

        public Diff Save(Diff diff)
        {
            try
            {
                var contextDiffValue = _context.Diff.Where(x => x.Id == diff.Id && x.TypeDiff == diff.TypeDiff).FirstOrDefault();
                if (contextDiffValue != null)
                {
                    _context.Remove(contextDiffValue);
                    _context.Diff.Add(diff);
                }
                else
                    _context.Diff.Add(diff);
                _context.SaveChanges();

                return diff;
            }
            catch (Exception e)
            {
                throw;
            }
          
        }
        public Diff GetById(int Id, TypeDiff typeDiff)
        {
            try
            {
                return _context.Diff.Where(x => x.Id == Id && x.TypeDiff == typeDiff).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
