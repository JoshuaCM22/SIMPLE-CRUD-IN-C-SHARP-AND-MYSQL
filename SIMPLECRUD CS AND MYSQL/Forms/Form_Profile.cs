﻿using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
namespace SIMPLECRUD_CS_AND_MYSQL // Created by: Joshua C. Magoliman
{
    public partial class Form_Profile : Form
    {
        public Form_Profile()
        {
            InitializeComponent();
        }
        private string gender;
        private void Form_Profile_Load(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            txtLastName.Enabled = false;
            txtMiddleName.Enabled = false;
            txtGivenName.Enabled = false;
            radiobtnMale.Enabled = false;
            radiobtnFemale.Enabled = false;
            dtpDateOfBirth.Enabled = false;
            txtAge.Enabled = false;
            txtAddress.Enabled = false;
            btnBrowse.Enabled = false;
            btnTakeAPicture.Enabled = false;
            btnEdit.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnClose.Text = "CLOSE";
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtLastName.Enabled = true;
            txtMiddleName.Enabled = true;
            txtGivenName.Enabled = true;
            radiobtnMale.Enabled = true;
            radiobtnFemale.Enabled = true;
            dtpDateOfBirth.Enabled = true;
            txtAge.Enabled = true;
            txtAddress.Enabled = true;
            btnBrowse.Enabled = true;
            btnTakeAPicture.Enabled = true;
            btnEdit.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnClose.Text = "CANCEL";
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update ?", "ATTENTION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                MemoryStream ms = new MemoryStream();
                pictureboxProfile.Image.Save(ms, pictureboxProfile.Image.RawFormat);
                byte[] imageByte = ms.ToArray();
                try
                {
                    Objects.con.Open();
                    Objects.cmd = new MySqlCommand("sp_UpdateExistingStudent", Objects.con);
                    Objects.cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_LastName", txtLastName.Text));
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_GivenName", txtGivenName.Text));
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_MiddleName", txtMiddleName.Text));
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_GenderId", gender));
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_DateOfBirth", dtpDateOfBirth.Value));
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_Age", txtAge.Text));
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_Address", txtAddress.Text));
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_Photo", imageByte));
                    Objects.cmd.Parameters.Add(new MySqlParameter("param_StudentId", txtStudentId.Text));
                    Objects.cmd.ExecuteNonQuery();
                    MessageBox.Show("SUCCESSFULLY UPDATED", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Objects.con.Close();
                    Reset();
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete ?", "ATTENTION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Objects.con.Open();
                    Objects.cmd = new MySqlCommand("sp_DeleteExistingStudent", Objects.con);
                    Objects.cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    Objects.cmd.Parameters.AddWithValue("param_StudentId", txtStudentId.Text);
                    Objects.cmd.ExecuteNonQuery();
                    MessageBox.Show("SUCCESSFULLY DELETED", "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Objects.con.Close();
                    Reset();
                    var nextForm = Application.OpenForms.OfType<Form_Students>().First();
                    nextForm.GetData();
                    nextForm.lblTotal.Visible = true;
                    nextForm.lblTotal.Text = "TOTAL ENTRY: " + nextForm.dgvStudents.Rows.Count.ToString();
                    this.SendToBack();
                    this.Hide();
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (btnClose.Text == "CLOSE")
            {
                var nextForm = Application.OpenForms.OfType<Form_Students>().First();
                nextForm.btnBack.Visible = true;
                nextForm.lblTotal.Visible = true;
                nextForm.GetData();
                this.Hide();
                this.SendToBack();
            }
            else if (btnClose.Text == "CANCEL")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel ?", "ATTENTION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var nextForm = Application.OpenForms.OfType<Form_Students>().First();
                    nextForm.btnBack.Visible = true;
                    nextForm.lblTotal.Visible = true;
                    nextForm.GetData();
                    this.Hide();
                    this.SendToBack();
                }
            }
        }
        private void radiobtnMale_CheckedChanged(object sender, EventArgs e)
        {
            gender = "1";
        }
        private void radiobtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            gender = "2";
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string pictureLocation = openFileDialog1.FileName.ToString();
                string holder = pictureLocation;
                pictureboxProfile.ImageLocation = pictureLocation;
            }
        }
        private void btnTakeAPicture_Click(object sender, EventArgs e)
        {
            Form_Camera nextForm = new Form_Camera();
            nextForm.HidebtnStudentProfile();
            nextForm.ShowDialog();
        }
        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtMiddleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtGivenName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
