using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCJobQueueManager
{
    public enum JobStatus
    {
        Pending,
        InProgress,
        Completed,
        Failed
    }

    public class CNCJob
    {
        public int JobId { get; set; }
        public string Description { get; set; }
        public JobStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public CNCJob(int jobId, string description)
        {
            JobId = jobId;
            Description = description;

            // updates when Job object is created
            Status = JobStatus.Pending;
            CreatedAt = DateTime.Now; 
        }
    }
}

