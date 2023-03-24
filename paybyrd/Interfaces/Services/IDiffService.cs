using paybyrd.Entities;
using paybyrd.Entities.Request;
using paybyrd.Entities.Response;
using System;

namespace paybyrd.Interfaces.Services
{
    public interface IDiffService
    {
        DiffResponse SaveLeft(DiffRequest diff);
        DiffResponse SaveRight(DiffRequest diff);
        DiffDifferencesResponse GetDiff(int Id, bool IgnoreUpperCaseLowerCase);
    }
}
