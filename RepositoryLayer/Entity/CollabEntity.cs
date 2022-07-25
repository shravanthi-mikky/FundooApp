using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }
        [ForeignKey("user")]
        public long UserId { get; set; }
        [ForeignKey("notes")]
        public long NoteId { get; set; }

        [JsonIgnore]
        public virtual UserEntity user { get; set; }
        [JsonIgnore]
        public virtual NoteEntity notes { get; set; }
    }
}
