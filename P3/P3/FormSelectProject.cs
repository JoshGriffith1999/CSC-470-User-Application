﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P3
{
    public partial class FormSelectProject : Form
    {

        FakeProjectRepository Repository = new FakeProjectRepository();
        public FormSelectProject()
        {
            InitializeComponent();
        }

        public FormSelectProject(FakeProjectRepository P, List<Project> Projects)
        {

            InitializeComponent();
            Projects = P.GetAll();
            string fillbox = "Hello";
            int i = 0;
           // SelectProjectListBox.DataSource = Projects;

            //MessageBox.Show(fillbox);
            foreach (Project p in Projects)
            {
                i = p.ID;
                fillbox = p.ID + " - " + p.Name;

                SelectProjectListBox.Items.Insert(i,fillbox);
                
            }

        }



        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void SelectProjectButton_Click(object sender, EventArgs e)
        {

        }
        
        public void setRepository(FakeProjectRepository P)
        {
            this.Repository = P;
        }
    }
}
