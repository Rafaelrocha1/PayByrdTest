using paybyrd.Entities.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace paybyrd.Entities
{
    public class Diff
    {
        public int Id { get; set; }
        public string JsonValue { get; set; }
        public TypeDiff TypeDiff { get; set; }       
    }
}
