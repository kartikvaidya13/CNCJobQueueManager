using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCJobQueueManager
{
    /// <summary>
    /// Enum representing the status of a CNC job.
    /// </summary>
    public enum JobStatus
    {
        Pending, // Job is pending in the queue and has not started processing
        InProgress, // Job is in progress and is being processed by the CNC machine
        Completed, // Job has been completed successfully
        Failed // Job has failed to complete or has been cancelled
    }

    /// <summary>
    /// Represents a CNC job that can be added to the job queue. Includes properties for job such as JobID,
    /// description, status, and created at time.
    /// </summary>
    public class CNCJob
    {
        /// <summary>
        /// Gets or sets the unique identifier for the job.
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        /// Gets or sets the description of the job.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status of the job.
        /// </summary>
        public JobStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the time the job was created at.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public CNCJob(int jobId, string description)
        {
            // Sets JobID when Job object is created
            JobId = jobId; 

            // Sets description when Job object is created
            Description = description; 

            // Set the initial status to Pending when the job is created
            Status = JobStatus.Pending;

            // Set the created at time to the current time when the job is created
            CreatedAt = DateTime.Now; 
        }
    }
}

