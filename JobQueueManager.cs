using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CNCJobQueueManager
{
    public class JobQueueManager
    {

        // Thread-safe queue to store CNC Jobs
        private ConcurrentQueue<CNCJob> jobQueue = new ConcurrentQueue<CNCJob>();

        // Token source to manage cancellation of job processing
        private CancellationTokenSource cts = new CancellationTokenSource();

        // Event to notify subscribers when job is updated
        public event Action<CNCJob> JobUpdated;

        /// <summary>
        /// Adds a job to the job queue and notifies subscribers that the job has been added
        /// </summary>
        /// <param name="job"></param>
        public void EnqueueJob(CNCJob job)
        {
            // Enqueues the job to the job queue
            jobQueue.Enqueue(job);

            // Notifies subscribers that the job has been added
            JobUpdated?.Invoke(job);
        }

        /// <summary>
        /// Processes jobs asynchronously until cancellation is requested and notifies subscribers of job status updates.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ProcessJobsAsync()
        {
            // Continue processing jobs until cancellation is requested
            while (!cts.IsCancellationRequested)
            {
                // Try to dequeue a job from the job queue
                if (jobQueue.TryDequeue(out CNCJob job))
                {
                    // Update job status to InProgress and notify subscribers
                    job.Status = JobStatus.InProgress;

                    // if job is updated, notify subscribers
                    JobUpdated?.Invoke(job);

                    // Simulate job processing
                    try
                    {
                        // Simulate job processing by delaying for 2 seconds
                        await Task.Delay(2000, cts.Token);

                        // Update job status to Completed and notify subscribers
                        job.Status = JobStatus.Completed; 
                    }
                    catch (TaskCanceledException)
                    {
                        // If the task is cancelled, update job status to Failed and notify subscribers
                        job.Status = JobStatus.Failed; // updates status to failed
                    }

                    // Notify subscribers that the job has been updated
                    JobUpdated?.Invoke(job);
                }
                else
                {
                    // Wait for a short period before checking the queue again for new jobs
                    await Task.Delay(500); 
                }
            }
        }

        /// <summary>
        /// Cancels job processing by requesting cancellation of the token source
        /// </summary>
        public void StopProcessing()
        {
            // request cancellation of the token source
            cts.Cancel();
        }
    }
}
