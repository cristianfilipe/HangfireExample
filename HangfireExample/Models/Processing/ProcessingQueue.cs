using HangfireExample.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangfireExample.Models.Processing
{
    public class ProcessingQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? RequestedBy { get; set; }
        public ProcessingQueueStatus ProcessingQueueStatus { get; set; }
        public string? FilePath { get; set; }
        public ICollection<ProcessingQueueLog>? ProcessingQueueLogs { get; set; }
    }
}
