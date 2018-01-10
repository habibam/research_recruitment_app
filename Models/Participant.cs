using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace loginregister.Models
{
    public class Participant : BaseEntity
    {
        [Key]
        public int ParticipantId { get; set; }

        
        public string SubjectId { get; set; }

        public int StudyId { get; set; }

        public int CategoryId {get; set;}
        public Study Study { get; set; }

    }
}