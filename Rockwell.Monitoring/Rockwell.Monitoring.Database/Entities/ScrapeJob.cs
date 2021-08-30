using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rockwell.Monitoring.Database.Entities
{
    [Table("scrape-jobs")]
    public class ScrapeJob
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        [StringLength(500)]
        public string Url { get; set; }

        [Required]
        [StringLength(100)]
        public string CronExpression { get; set; }

        public ICollection<ExecutionResult> ExecutionResults { get; set; }
    }
}
