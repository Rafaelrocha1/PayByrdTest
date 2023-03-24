using paybyrd.Entities;
using paybyrd.Entities.Enum;
using System;

namespace paybyrd.Interfaces.Repository
{
    public interface IDiffRepository
    {
       public Diff Save(Diff diff);
        public Diff GetById(int Id, TypeDiff typeDiff);
    }
}
