using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNCJobQueueManager;

namespace CNCJobQueueManager
{
    /// <summary>
    /// Main form for the CNC Job Queue Manager application.
    /// </summary>
    public partial class MainForm : Form
    {
        // Manages the job queue and processing
        private JobQueueManager jobQueueManager;

        // Keeps track of the next job ID to be assigned
        private int nextJobId = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            jobQueueManager = new JobQueueManager();
            jobQueueManager.JobUpdated += OnJobUpdated;
        }

        /// <summary>
        /// Handles the Add Job button click event. Queues a new job and adds it to the list view.
        /// </summary>
        /// <param name="sender">The source of the event (the Add Job button).</param>
        /// <param name="e">The event data.</param>
        private void btnAddJob_Click(object sender, EventArgs e)
        {
            var job = new CNCJob(nextJobId, $"Laser cutting job {nextJobId}");
            nextJobId++; // increment JobID for next Job 
            jobQueueManager.EnqueueJob(job);
            AddJobToListView(job);
        }

        /// <summary>
        /// Handles the Start Processing button click event, Starts processing jobs asynchronously.
        /// </summary>
        /// <param name="sender">The source of the event (the Start Processing button).</param>
        /// <param name="e">The event data.</param>
        private void btnStartProcessing_Click(object sender, EventArgs e)
        {
            Task.Run(() => jobQueueManager.ProcessJobsAsync());
        }

        /// <summary>
        /// Handles the Stop Processing button click event. Stops processing jobs. 
        /// </summary>
        /// <param name="sender">The source of the event (the Stop Processing button).</param>
        /// <param name="e">The event data.</param>
        private void btnStopProcessing_Click(object sender, EventArgs e)
        {
            jobQueueManager.StopProcessing();
        }

        /// <summary>
        /// Adds a job to the list view.
        /// </summary>
        /// <param name="job">The job to be added to the list view.</param>
        private void AddJobToListView(CNCJob job)
        {
            var item = new ListViewItem(job.JobId.ToString());
            item.SubItems.Add(job.Description);
            item.SubItems.Add(job.Status.ToString());
            item.SubItems.Add(job.CreatedAt.ToString("HH:mm:ss"));
            item.Tag = job;
            listViewJobs.Items.Add(item);
        }

        /// <summary>
        /// Handles job updates and safely updates the UI from any thread. 
        /// </summary>
        /// <param name="job">The job that was updated.</param>
        private void OnJobUpdated(CNCJob job)
        {
            if (this.InvokeRequired)
            {
                // Schedule the updste asynchronously if called from a different thread.
                this.BeginInvoke(new Action(() => UpdateJobInListView(job)));
            }
            else
            {
                UpdateJobInListView(job);
            }
        }

        /// <summary>
        /// Updates the job status in the list view.
        /// </summary>
        /// <param name="job">The job to be added to the list view</param>
        private void UpdateJobInListView(CNCJob job)
        {
            foreach (ListViewItem item in listViewJobs.Items)
            {
                if (((CNCJob)item.Tag).JobId == job.JobId)
                {
                    item.SubItems[2].Text = job.Status.ToString();
                    break;
                }
            }
        }
    }
}

