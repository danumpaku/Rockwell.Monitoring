using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rockwell.Monitoring.Database.Entities
{
    [Table("execution-results")]
    public class ExecutionResult
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime ExecutionTime { get; set; }

        [ForeignKey(nameof(ScrapeJobId))]
        public long ScrapeJobId { get; set; }
        public ScrapeJob ScrapeJob { get; set; }

        public int ResponseCode { get; set; }

        [StringLength(1000)]
        public string Result { get; set; }

        [StringLength(500)]
        public string ErrorMessage { get; set; }
    }
}
