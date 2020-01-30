using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
namespace SIMPLECRUD_CS_AND_MYSQL // Created by: Joshua C. Magoliman 
{
    public partial class Form_Students : Form
    {
        public Form_Students()
        {
            InitializeComponent();
        }
        private void Form_Students_Load(object sender, EventArgs e)
        {
            GetData();
            cmbboxFilter.Text = "None";
        }
        public void GetData()
        {
            txtSearch.Clear();
            btnBack.Visible = true;
            Objects.da = new MySqlDataAdapter();
            Objects.dt = new DataTable();
            Objects.ds = new DataSet();
            try
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("vw_getallstudents", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.TableDirect;
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
                Objects.da.Update(Objects.dt);
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
               Objects.con.Close();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_Main nextForm = new Form_Main();
            nextForm.Show();
        }
        private void btnAddNewStudent_Click(object sender, EventArgs e)
        {
            this.Close();
            var nextForm = new Form_AddNewStudent();
            nextForm.ShowDialog();
        }
        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {   
            Objects.con.Open();
            DataGridViewRow row = dgvStudents.CurrentRow;
            btnBack.Visible = false;
            lblTotal.Visible = false;
            var nextForm = new Form_Profile();
            nextForm.txtStudentId.Text = row.Cells[0].Value.ToString();
            nextForm.txtLastName.Text = row.Cells[1].Value.ToString();
            nextForm.txtMiddleName.Text = row.Cells[2].Value.ToString();
            nextForm.txtGivenName.Text = row.Cells[3].Value.ToString();
            string gettingGender = row.Cells["Gender"].Value.ToString();
            if (gettingGender == "Male")
            {
                nextForm.radiobtnMale.Checked = true;
            }
            else if (gettingGender == "Female")
            {
                nextForm.radiobtnFemale.Checked = true;
            }
            nextForm.dtpDateOfBirth.Value = (DateTime)row.Cells[5].Value;
            nextForm.txtAge.Text = row.Cells[6].Value.ToString();
            nextForm.txtAddress.Text = row.Cells[7].Value.ToString();
            byte[] arrImage = (byte[])(row.Cells["Photo"].Value);
            MemoryStream mstream = new MemoryStream(arrImage);
            nextForm.pictureboxProfile.Image = Image.FromStream(mstream);
            Objects.con.Close();
            nextForm.ShowDialog();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Objects.da = new MySqlDataAdapter();
            Objects.dt = new DataTable();
            Objects.ds = new DataSet();
            if (txtSearch.Text == "")
            {
                GetData();
            }
            else if (cmbboxFilter.Text == "Student ID")
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("sp_GetStudentByStudentId", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Objects.da.SelectCommand.Parameters.AddWithValue("param_StudentId", txtSearch.Text);
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
            }
            else if (cmbboxFilter.Text == "Last Name")
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("sp_GetStudentByLastName", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Objects.da.SelectCommand.Parameters.AddWithValue("param_LastName", "%" + txtSearch.Text + "%");
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
            }
            else if (cmbboxFilter.Text == "Given Name")
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("sp_GetStudentByGivenName", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Objects.da.SelectCommand.Parameters.AddWithValue("param_GivenName", "%" + txtSearch.Text + "%");
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
            }
            else if (cmbboxFilter.Text == "Middle Name")
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("sp_GetStudentByMiddleName", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Objects.da.SelectCommand.Parameters.AddWithValue("param_MiddleName", "%" + txtSearch.Text + "%");
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
            }
            else if (cmbboxFilter.Text == "Gender")
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("sp_GetStudentByGender", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Objects.da.SelectCommand.Parameters.AddWithValue("param_Gender", txtSearch.Text + "%");
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
            }
            else if (cmbboxFilter.Text == "Date of Birth")
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("sp_GetStudentByDateOfBirth", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Objects.da.SelectCommand.Parameters.AddWithValue("param_DateOfBirth", txtSearch.Text);
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
            }
            else if (cmbboxFilter.Text == "Age")
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("sp_GetStudentByAge", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Objects.da.SelectCommand.Parameters.AddWithValue("param_Age", txtSearch.Text);
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
            }
            else if (cmbboxFilter.Text == "Address")
            {
                Objects.con.Open();
                Objects.da = new MySqlDataAdapter("sp_GetStudentByAddress", Objects.con);
                Objects.da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Objects.da.SelectCommand.Parameters.AddWithValue("param_Address", "%" + txtSearch.Text + "%");
                Objects.da.Fill(Objects.ds);
                dgvStudents.DataSource = Objects.ds.Tables[0];
                dgvStudents.Columns[8].Visible = false;
                dgvStudents.Columns[0].Width = 200;
                dgvStudents.Columns[1].Width = 200;
                dgvStudents.Columns[2].Width = 200;
                dgvStudents.Columns[3].Width = 200;
                dgvStudents.Columns[4].Width = 100;
                dgvStudents.Columns[5].Width = 130;
                dgvStudents.Columns[6].Width = 60;
                dgvStudents.Columns[7].Width = 400;
            }
            this.lblTotal.Text = "TOTAL ENTRY: " + dgvStudents.Rows.Count.ToString();
            Objects.da.Update(Objects.dt);
            Objects.con.Close();
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string stringHolder = cmbboxFilter.Text;
            switch (stringHolder)
            {
                case "None":
                    if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "Student ID":
                    if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "Last Name":
                    if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "Given Name":
                    if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "Middle Name":
                    if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "Gender":
                    if (Char.IsLetter(e.KeyChar) || Char.IsControl(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "Date of Birth":
                    if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
                case "Age":
                    if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }
        private void cmbboxFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsControl(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar))
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
