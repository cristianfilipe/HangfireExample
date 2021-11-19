using HangfireExample.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangfireExample.Models.Processing
{
    public class ProcessingQueueLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? LogMessage { get; set; }
        public LogType LogType { get; set; }
        public int ProcessingQueueId { get; set; }

        [ForeignKey("ProcessingQueueId")]
        public ProcessingQueue ProcessingQueue { get; set; }
    }
}
