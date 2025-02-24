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

        public void EnqueueJob(CNCJob job)
        {
            jobQueue.Enqueue(job);
            JobUpdated?.Invoke(job);
        }

        public async Task ProcessJobsAsync()
        {
            while (!cts.IsCancellationRequested)
            {
                if (jobQueue.TryDequeue(out CNCJob job))
                {
                    job.Status = JobStatus.InProgress;
                    JobUpdated?.Invoke(job);

                    try
                    {
                        await Task.Delay(2000, cts.Token);
                        job.Status = JobStatus.Completed;
                    }
                    catch (TaskCanceledException)
                    {
                        job.Status = JobStatus.Failed;
                    }
                    JobUpdated?.Invoke(job);
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }

        public void StopProcessing()
        {
            cts.Cancel();
        }
    }
}
