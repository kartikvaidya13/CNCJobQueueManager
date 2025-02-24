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
        private void btnAddJob_Click(object sender, EventArgs e)
        {
            var job = new CNCJob(nextJobId, $"Laser cutting job {nextJobId}");
            nextJobId++;
            jobQueueManager.EnqueueJob(job);
            AddJobToListView(job);
        }

        private void btnStartProcessing_Click(object sender, EventArgs e)
        {
            Task.Run(() => jobQueueManager.ProcessJobsAsync());
        }

        private void btnStopProcessing_Click(object sender, EventArgs e)
        {
            jobQueueManager.StopProcessing();
        }

        private void AddJobToListView(CNCJob job)
        {
            var item = new ListViewItem(job.JobId.ToString());
            item.SubItems.Add(job.Description);
            item.SubItems.Add(job.Status.ToString());
            item.SubItems.Add(job.CreatedAt.ToString("HH:mm:ss"));
            item.Tag = job;
            listViewJobs.Items.Add(item);
        }

        private void OnJobUpdated(CNCJob job)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => UpdateJobInListView(job)));
            }
            else
            {
                UpdateJobInListView(job);
            }
        }

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

