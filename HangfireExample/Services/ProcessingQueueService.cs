using HangfireExample.Enumerators;
using HangfireExample.Models;
using HangfireExample.Models.Processing;

namespace HangfireExample.Services
{
    public class ProcessingQueueService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProcessingQueueService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void StartQueueProcessing(ProcessingQueue processingQueue)
        {
            //Added as an Example to show how the Queue works.
            Thread.Sleep(TimeSpan.FromSeconds(30));

            AddProcessingLog(processingQueue, "Starting processing");

            UpdateProcessingStatus(processingQueue, ProcessingQueueStatus.Processing);

            AddProcessingLog(processingQueue, "Log Example: 30 seconds waiting started");
            //Added as an Example to show how the Queue works.
            Thread.Sleep(TimeSpan.FromSeconds(30));
            AddProcessingLog(processingQueue, "Log Example: 30 seconds waiting finished");

            AddProcessingLog(processingQueue, "Processing succeded");

            UpdateProcessingStatus(processingQueue, ProcessingQueueStatus.Processed);
        }

        private void AddProcessingLog(ProcessingQueue processingQueue, string message)
        {
            var processingQueueLog = new ProcessingQueueLog
            {
                ProcessingQueueId = processingQueue.Id,
                LogMessage = message,
                LogType = LogType.Information,
                Date = DateTime.Now
            };
            _applicationDbContext.Add(processingQueueLog);
            _applicationDbContext.SaveChanges();
        }

        private void UpdateProcessingStatus(ProcessingQueue processingQueue, ProcessingQueueStatus processingQueueStatus)
        {
            processingQueue.ProcessingQueueStatus = processingQueueStatus;
            _applicationDbContext.Update(processingQueue);
            _applicationDbContext.SaveChanges();
        }
    }
}
