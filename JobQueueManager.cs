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
        private ConcurrentQueue<CNCJob> jobQueue = new ConcurrentQueue<CNCJob>();
        private CancellationTokenSource cts = new CancellationTokenSource();

        public event Action<CNCJob> JobUpdated;

        // Adds job to jobQueue, notifies subscribers of job has been updated
        public void EnqueueJob(CNCJob job)
        {
            // notifies subscribers that job has been added to queue
            jobQueue.Enqueue(job);
            JobUpdated?.Invoke(job);
        }

        // Function to process Jobs asynchronously unless cancellation is requested. Notifies subscribers of job processing and 
        // simulates time job is processed. Job status will either be updated to inprogress, completed, or cancelled. 
        public async Task ProcessJobsAsync()
        {
            while (!cts.IsCancellationRequested)
            {
                if (jobQueue.TryDequeue(out CNCJob job))
                {
                    // notifies subscribers that job is in progress
                    job.Status = JobStatus.InProgress; 
                    JobUpdated?.Invoke(job); 

                    //  function will try to update the status of the job  to completed unless cts is cancelled.
                    try 
                    {
                        await Task.Delay(2000, cts.Token); // simulates Job processing in real time
                        job.Status = JobStatus.Completed; // updates status to completed
                    }
                    catch (TaskCanceledException)
                    {
                        job.Status = JobStatus.Failed; // updates status to failed
                    }
                    JobUpdated?.Invoke(job);
                }
                else
                {
                    await Task.Delay(500); // waits for next task
                }
            }
        }

        // Cancels token when invoked
        public void StopProcessing()
        {
            cts.Cancel();
        }
    }
}
