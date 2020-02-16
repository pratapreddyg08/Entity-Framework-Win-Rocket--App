using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkWinRocketApp
{
    public partial class Form1 : Form
    {
        Rocket model = new Rocket();
        public Form1()
        {
            InitializeComponent();
        }
        public void Clear()
        {
            //txtId.Text = txtName.Text = txtVehicleType.Text = txtPurpose = "";
            foreach (Control c in this.Controls)
                if (c is TextBox)
                    (c as TextBox).Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisplayDataGridview();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            model.Id = Convert.ToInt32(txtId.Text.Trim());
            model.Name = txtName.Text.Trim();
            model.VehicleType = txtVehicleType.Text.Trim();
            model.Purpose = txtPurpose.Text.Trim();
            using (EFDBEntities db = new EFDBEntities())
            {
                db.Rockets.Add(model);
                db.SaveChanges();
                DisplayDataGridview();
            }
            Clear();
            MessageBox.Show("Record Succcessfully Inserted..");
        }
        void DisplayDataGridview()
        {
            dataGridView1.AutoGenerateColumns = false;
            using (EFDBEntities db =new EFDBEntities())
            {
                dataGridView1.DataSource = db.Rockets.ToList<Rocket>();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            model.Id = Convert.ToInt32(txtId.Text.Trim());
            model.Name = txtName.Text.Trim();
            model.VehicleType = txtVehicleType.Text.Trim();
            model.Purpose = txtPurpose.Text.Trim();
            using (EFDBEntities db = new EFDBEntities())
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            Clear();
            DisplayDataGridview();
            MessageBox.Show("Record Succcessfully Updated..");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Delete this Record?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                using (EFDBEntities db = new EFDBEntities())
                {
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Detached)
                        db.Rockets.Attach(model);
                    db.Rockets.Remove(model);
                    db.SaveChanges();
                    DisplayDataGridview();
                    Clear();
                    MessageBox.Show("Record Deleted Successfully");
                }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != 0)
            {
                using (EFDBEntities db = new EFDBEntities())
                {
                    txtId.Text = Convert.ToString(model.Id);
                    txtName.Text = model.Name;
                    txtVehicleType.Text = model.VehicleType;
                    txtPurpose.Text = model.Purpose;

                }
            }
        }
    }
}
