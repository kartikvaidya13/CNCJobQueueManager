using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCJobQueueManager
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listViewJobs;
        private System.Windows.Forms.Button btnAddJob;
        private System.Windows.Forms.Button btnStartProcessing;
        private System.Windows.Forms.Button btnStopProcessing;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listViewJobs = new System.Windows.Forms.ListView();
            this.btnAddJob = new System.Windows.Forms.Button();
            this.btnStartProcessing = new System.Windows.Forms.Button();
            this.btnStopProcessing = new System.Windows.Forms.Button();
            this.SuspendLayout();


            this.listViewJobs.FullRowSelect = true;
            this.listViewJobs.GridLines = true;
            this.listViewJobs.Location = new System.Drawing.Point(12, 12);
            this.listViewJobs.Name = "listViewJobs";
            this.listViewJobs.Size = new System.Drawing.Size(500, 250);
            this.listViewJobs.TabIndex = 0;
            this.listViewJobs.View = System.Windows.Forms.View.Details;
            this.listViewJobs.Columns.Add("Job ID", 60);
            this.listViewJobs.Columns.Add("Description", 200);
            this.listViewJobs.Columns.Add("Status", 100);
            this.listViewJobs.Columns.Add("Created At", 100);


            this.btnAddJob.Location = new System.Drawing.Point(12, 280);
            this.btnAddJob.Name = "btnAddJob";
            this.btnAddJob.Size = new System.Drawing.Size(100, 30);
            this.btnAddJob.TabIndex = 1;
            this.btnAddJob.Text = "Add Job";
            this.btnAddJob.UseVisualStyleBackColor = true;
            this.btnAddJob.Click += new System.EventHandler(this.btnAddJob_Click);


            this.btnStartProcessing.Location = new System.Drawing.Point(130, 280);
            this.btnStartProcessing.Name = "btnStartProcessing";
            this.btnStartProcessing.Size = new System.Drawing.Size(120, 30);
            this.btnStartProcessing.TabIndex = 2;
            this.btnStartProcessing.Text = "Start Processing";
            this.btnStartProcessing.UseVisualStyleBackColor = true;
            this.btnStartProcessing.Click += new System.EventHandler(this.btnStartProcessing_Click);


            this.btnStopProcessing.Location = new System.Drawing.Point(270, 280);
            this.btnStopProcessing.Name = "btnStopProcessing";
            this.btnStopProcessing.Size = new System.Drawing.Size(120, 30);
            this.btnStopProcessing.TabIndex = 3;
            this.btnStopProcessing.Text = "Stop Processing";
            this.btnStopProcessing.UseVisualStyleBackColor = true;
            this.btnStopProcessing.Click += new System.EventHandler(this.btnStopProcessing_Click);


            this.ClientSize = new System.Drawing.Size(524, 321);
            this.Controls.Add(this.btnStopProcessing);
            this.Controls.Add(this.btnStartProcessing);
            this.Controls.Add(this.btnAddJob);
            this.Controls.Add(this.listViewJobs);
            this.Name = "MainForm";
            this.Text = "CNC Job Queue Manager";
            this.ResumeLayout(false);
        }
    }
}

