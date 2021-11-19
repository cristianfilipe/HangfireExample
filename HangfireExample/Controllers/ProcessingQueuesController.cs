using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HangfireExample.Models;
using HangfireExample.Models.Processing;
using Hangfire;
using HangfireExample.Services;

namespace HangfireExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessingQueuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ProcessingQueueService _processingQueueService;

        public ProcessingQueuesController(ApplicationDbContext context, ProcessingQueueService processingQueueService)
        {
            _context = context;
            _processingQueueService = processingQueueService;
        }

        // GET: api/ProcessingQueues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessingQueue>>> GetProcessingQueue()
        {
            return await _context.ProcessingQueue.ToListAsync();
        }

        // GET: api/ProcessingQueues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessingQueue>> GetProcessingQueue(int id)
        {
            var processingQueue = await _context.ProcessingQueue.FindAsync(id);

            if (processingQueue == null)
            {
                return NotFound();
            }

            return processingQueue;
        }

        // PUT: api/ProcessingQueues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcessingQueue(int id, ProcessingQueue processingQueue)
        {
            if (id != processingQueue.Id)
            {
                return BadRequest();
            }

            _context.Entry(processingQueue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessingQueueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProcessingQueues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProcessingQueue>> PostProcessingQueue(ProcessingQueue processingQueue)
        {
            _context.ProcessingQueue.Add(processingQueue);
            await _context.SaveChangesAsync();

            BackgroundJob.Enqueue(() => _processingQueueService.StartQueueProcessing(processingQueue));

            return CreatedAtAction("GetProcessingQueue", new { id = processingQueue.Id }, processingQueue);
        }

        // DELETE: api/ProcessingQueues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcessingQueue(int id)
        {
            var processingQueue = await _context.ProcessingQueue.FindAsync(id);
            if (processingQueue == null)
            {
                return NotFound();
            }

            _context.ProcessingQueue.Remove(processingQueue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcessingQueueExists(int id)
        {
            return _context.ProcessingQueue.Any(e => e.Id == id);
        }
    }
}
