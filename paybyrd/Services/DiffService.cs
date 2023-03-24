using AutoMapper;
using paybyrd.Context;
using paybyrd.Entities;
using paybyrd.Entities.Enum;
using paybyrd.Entities.Request;
using paybyrd.Entities.Response;
using paybyrd.Interfaces.Repository;
using paybyrd.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace paybyrd.Services
{
    public class DiffService : IDiffService
    {
        private readonly PayByrdContext _context;
        private readonly IMapper _mapper;
        private readonly IDiffRepository _diffRepository;

        public DiffService(PayByrdContext context, IMapper mapper, IDiffRepository diffRepository)
        {
            _context = context;
            _mapper = mapper;
            _diffRepository = diffRepository;
        }
        public DiffResponse SaveLeft(DiffRequest diffRequest)
        { 
            try
            {
                var diff =_mapper.Map<Diff>(diffRequest);
                diff.TypeDiff = Entities.Enum.TypeDiff.Left;
                var diffReturn = _diffRepository.Save(diff);
                return _mapper.Map<DiffResponse>(diffReturn);

            }
            catch ( Exception e)
            {

                throw new Exception("Erro ao salvar valor a ser comparada a esquerda.");
            }
           
        }

        public DiffResponse SaveRight(DiffRequest diffRequest)
        {
            try
            {
                var diff = _mapper.Map<Diff>(diffRequest);
                diff.TypeDiff = Entities.Enum.TypeDiff.Right;
                var diffReturn = _diffRepository.Save(diff);

                return _mapper.Map<DiffResponse>(diffReturn);
            }
            catch
            {
                throw new Exception("Erro ao salvar valor a ser comparada a direita.");
            }
        }

        public DiffDifferencesResponse GetDiff(int Id, bool IgnoreUpperCaseLowerCase)
        {
            try
            {


                var diffReturnLeft = _diffRepository.GetById(Id, TypeDiff.Left);
                var diffReturnRight = _diffRepository.GetById(Id, TypeDiff.Right);

                Validation(diffReturnLeft, diffReturnRight, out DiffDifferencesResponse diffDifferencesResponse);

                if (diffDifferencesResponse.Equals == false)
                    return diffDifferencesResponse;


                diffDifferencesResponse.Result = "Equals";

                VerificarDiff(diffReturnLeft, diffReturnRight, diffDifferencesResponse, IgnoreUpperCaseLowerCase);

                return diffDifferencesResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DiffDifferencesResponse VerificarDiff(Diff Left, Diff Right, DiffDifferencesResponse diffDifferencesResponse, bool IgnoreUpperCaseLowerCase)
        {
            List<Differences> differences = new List<Differences>();
            bool different = false;
            int length = 0;
            int startIndex = 0;
            string textLeft = Left.JsonValue;
            string textRight = Right.JsonValue;
            if (IgnoreUpperCaseLowerCase)
            {
                textLeft = textLeft.ToUpper();
                textRight = textRight.ToUpper();
            }
            for (int i = 0; i < textLeft.Length; i++)
            {
                if (textLeft[i] != textRight[i])
                {
                    if (different == false)
                    {
                        different = true;
                        startIndex = i;
                    }
                    length++;
                }
                else
                {
                    if (different)
                    {
                        diffDifferencesResponse.Result = "Different";
                        diffDifferencesResponse.Equals = false;
                        differences.Add(new Differences()
                        {
                            length = length,
                            startIndex = startIndex
                        });
                    }
                    startIndex = i;
                    different = false;
                    length = 0;
                }
            }
            if (different)
            {
                differences.Add(new Differences()
                {
                    length = length,
                    startIndex = startIndex
                });
            }

            diffDifferencesResponse.Differences = differences;
            return diffDifferencesResponse;
        }
        private bool Validation(Diff Left, Diff Right, out DiffDifferencesResponse diffDifferencesResponse)
        {
            try
            {


                DiffDifferencesResponse diffDifference = new DiffDifferencesResponse();
                diffDifferencesResponse = diffDifference;


                diffDifference.Differences = new List<Differences>();
                if (Left == null)
                    throw new Exception("Necessário informar um valor a ser comparado a esquerda.");
                if (Right == null)
                    throw new Exception("Necessário informar um valor a ser comparado a direita.");
                diffDifferencesResponse.Id = Left.Id;
                if (Right.JsonValue.Length != Left.JsonValue.Length)
                {
                    diffDifferencesResponse.Equals = false;
                    diffDifferencesResponse.Result = "Different size";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
    }
}
