﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RosaLogic;
using RosaModel;

namespace LoginForm
{
    public partial class EditEmployeeForm : Form
    {
        private Employee connectedEmployee;
        private Employee employeeToEdit;
        public EditEmployeeForm(Employee connectedEmployee, Employee employeeToEdit)
        {
            InitializeComponent();
            this.connectedEmployee = connectedEmployee;
            this.employeeToEdit = employeeToEdit;
        }

        private void EditEmployeeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void homeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new SwitchForms(connectedEmployee, this, new homeForm(connectedEmployee));
        }

        private void tablesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new SwitchForms(connectedEmployee, this, new tableViewForm(connectedEmployee));
        }

        private void barToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SwitchForms(connectedEmployee, this, new BarKitchenForm(connectedEmployee, "bar"));
        }

        private void kitchenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            new SwitchForms(connectedEmployee, this, new BarKitchenForm(connectedEmployee, "kitchen"));
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            loginForm loginForm = new loginForm();
            loginForm.Show();
            this.Hide();
        }
        private void EditEmployeeForm_Load(object sender, EventArgs e)
        {
            Init();
        }
        private void Init()
        {
            dateOfBirth.MaxDate = DateTime.Now.AddYears(-18);

            txtFirstNName.Text = employeeToEdit.firstName;
            txtLastName.Text = employeeToEdit.lastName;
            txtUsername.Text = employeeToEdit.username;
            txtPassword.Text = employeeToEdit.password;

            if (employeeToEdit.role == Roles.Manager)
                btnManager.Checked = true;
            else if (employeeToEdit.role == Roles.Chef)
                btnChef.Checked = true;
            else if (employeeToEdit.role == Roles.Waiter)
                btnWaiter.Checked = true;
            else
                btnBartender.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new SwitchForms(connectedEmployee, this, new ManagementForm(connectedEmployee));
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            Employee_Service employeeService = new Employee_Service();
            if (string.IsNullOrEmpty(txtUsername.Text))
                txtUsername.Text = employeeToEdit.username;
            if (string.IsNullOrEmpty(txtFirstNName.Text))
                txtFirstNName.Text = employeeToEdit.firstName;
            if (string.IsNullOrEmpty(txtLastName.Text))
                txtLastName.Text = employeeToEdit.lastName;
            if (string.IsNullOrEmpty(txtPassword.Text))
                txtPassword.Text = employeeToEdit.password;

            Roles newRole;
            if (btnManager.Checked)
                newRole = Roles.Manager;
            else if (btnWaiter.Checked)
                newRole = Roles.Waiter;
            else if (btnChef.Checked)
                newRole = Roles.Chef;
            else
                newRole = Roles.Bartender;

           Employee newEmployee = new Employee()
            {
                firstName = txtFirstNName.Text,
                lastName = txtLastName.Text,
                username = txtUsername.Text,
                password = txtPassword.Text,
                role = newRole
            };
            employeeService.EditAccount(employeeToEdit, newEmployee);
            MessageBox.Show("Account eddited succesfully!","Edited",MessageBoxButtons.OK);

        }
    }
}
