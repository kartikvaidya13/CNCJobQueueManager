using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNCJobQueueManager;

namespace CNCJobQueueManager
{  
    public partial class MainForm : Form
    {
        private JobQueueManager jobQueueManager;
        private int nextJobId = 1;

        public MainForm()
        {
            InitializeComponent();
            jobQueueManager = new JobQueueManager();
            jobQueueManager.JobUpdated += OnJobUpdated;
        }

        // Event handling for Add Job button click. Queues a new job on the board with the ID and name and adds it to the 
        // CNC Job object. Also calls AddJobToListView to display queued job. 
        private void btnAddJob_Click(object sender, EventArgs e)
        {
            var job = new CNCJob(nextJobId, $"Laser cutting job {nextJobId}");
            nextJobId++; // increment JobID for next Job added to ListView
            jobQueueManager.EnqueueJob(job);
            AddJobToListView(job);
        }

        //Event handling for Start Processing button. Changes status for each job being procressed. 
        private void btnStartProcessing_Click(object sender, EventArgs e)
        {
            Task.Run(() => jobQueueManager.ProcessJobsAsync());
        }

        //Event handling for Stop Processing button. Cancels all processing for jobs to avoid race conditions. 
        private void btnStopProcessing_Click(object sender, EventArgs e)
        {
            jobQueueManager.StopProcessing();
        }

        // Function to add Job to ListView. 
        private void AddJobToListView(CNCJob job)
        {
            var item = new ListViewItem(job.JobId.ToString());
            item.SubItems.Add(job.Description);
            item.SubItems.Add(job.Status.ToString());
            item.SubItems.Add(job.CreatedAt.ToString("HH:mm:ss"));
            item.Tag = job;
            listViewJobs.Items.Add(item);
        }

        // Safely updates UI from any thread. 
        private void OnJobUpdated(CNCJob job)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => UpdateJobInListView(job))); // schedules update asynchronously
            }
            else
            {
                UpdateJobInListView(job);
            }
        }

        // Updates the status of a job in the ListView. 
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

