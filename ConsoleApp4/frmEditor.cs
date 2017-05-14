using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp4
{
    public partial class frmEditor : Form
    {
        public frmEditor()
        {
            InitializeComponent();
        }
        private MapData RawData;
        private void frmEditor_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LoadData(new DataTable(), Database.Weapons);
            dataGridView2.DataSource = LoadData(new DataTable(), Database.HeadWear);
            dataGridView3.DataSource = LoadData(new DataTable(), Database.ChestWear);
            dataGridView4.DataSource = LoadData(new DataTable(), Database.LegWear);
            dataGridView5.DataSource = LoadData(new DataTable(), Database.FeetWear);

            try
            {
                RawData = Database.LoadMap();
            }
            catch (Exception)
            {
                
            }
            
            if (RawData == null)
                RawData = new MapData();

            pictureBox1.Image = RawData.RawData;

            UpdateMap();
        }

        public DataTable LoadData(DataTable dataTable, IList objects)
        {
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Strength", typeof(int));
            dataTable.Columns.Add("Inteligence", typeof(int));
            dataTable.Columns.Add("Defence", typeof(int));            
            
            foreach (var item in objects)
            {
                var wearableItem = (WearableItem)item;
                DataRow dr = dataTable.NewRow();

                dr["Name"] = wearableItem.Name;
                dr["Description"] = wearableItem.Description;
                dr["Strength"] = wearableItem.Power.Strength;
                dr["Inteligence"] = wearableItem.Power.Inteligence;
                dr["Defence"] = wearableItem.Power.Defence;

                dataTable.Rows.Add(dr);
            }

            dataTable.AcceptChanges();

            return dataTable;
        }

        public void PopulateDataToList<T>(DataTable dataTable, List<T> list)
        {
            dataTable.AcceptChanges();

            foreach (DataRow dr in dataTable.Rows)
            {
                var tt = Activator.CreateInstance<T>();
                WearableItem wearableItem = tt as WearableItem;
                
                wearableItem.Name = (dr["Name"] + "").Replace(',', ';');
                wearableItem.Description = (dr["Description"] + "").Replace(',', ';');

                
                int a = 0;
                int b = 0;
                int c = 0;

                int.TryParse((dr["Strength"] + ""), out a);
                int.TryParse((dr["Inteligence"] + ""), out b);
                int.TryParse((dr["Defence"] + ""), out c);

                wearableItem.Power = new Ability() { Strength = a, Inteligence = b, Defence = c };
                
                list.Add(tt);                
            }
        }

        private void frmEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.Weapons = new List<WeaponItem>();
            Database.HeadWear = new List<HeadItem>();
            Database.ChestWear = new List<ChestItem>();
            Database.LegWear = new List<LegsItem>();
            Database.FeetWear = new List<FeetItem>();            

            PopulateDataToList(dataGridView1.DataSource as DataTable, Database.Weapons);
            PopulateDataToList(dataGridView2.DataSource as DataTable, Database.HeadWear);
            PopulateDataToList(dataGridView3.DataSource as DataTable, Database.ChestWear);
            PopulateDataToList(dataGridView4.DataSource as DataTable, Database.LegWear);
            PopulateDataToList(dataGridView5.DataSource as DataTable, Database.FeetWear);

            Database.SaveData();

            RawData.Locations = new List<LocationSpot>();

            DataTable dt = dataGridView6.DataSource as DataTable;

            if(dt != null)
            {
                dt.AcceptChanges();

                foreach (DataRow dr in dt.Rows)
                {
                    var ls = dr["LocationSpot"] as LocationSpot;
                    ls.Name = (dr["Name"] + "");

                    RawData.Locations.Add(ls);
                }
            }
            
            Database.SaveMap(RawData);
        }

        private void createArmorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmArmor armor = new frmArmor())
            {
                if(armor.ShowDialog() == DialogResult.OK)
                {
                    AddRow(dataGridView2.DataSource as DataTable, (armor.txtName.Text + " Helmet").ToTitleCase(), armor.txtDescription.Text, (int)(armor.txtStr.Value * 0.6m), (int)(armor.txtInt.Value * 0.6m), (int)(armor.intDef.Value * 0.6m));
                    AddRow(dataGridView3.DataSource as DataTable, (armor.txtName.Text + " Body").ToTitleCase(), armor.txtDescription.Text, (int)armor.txtStr.Value, (int)armor.txtInt.Value,  (int)armor.intDef.Value);
                    AddRow(dataGridView4.DataSource as DataTable, (armor.txtName.Text + " Leggings").ToTitleCase(), armor.txtDescription.Text, (int)(armor.txtStr.Value * 0.8m), (int)(armor.txtInt.Value * 0.8m), (int)(armor.intDef.Value * 0.8m));
                    AddRow(dataGridView5.DataSource as DataTable, (armor.txtName.Text + " Boots").ToTitleCase(), armor.txtDescription.Text, (int)(armor.txtStr.Value * 0.4m), (int)(armor.txtInt.Value * 0.4m), (int)(armor.intDef.Value * 0.4m));                    
                }
            }
        }

        public void AddRow(DataTable dt, string name, string desc, int str, int inte, int def)
        {
            DataRow dr = dt.NewRow();

            dr["Name"] = name;
            dr["Description"] = desc;
            dr["Strength"] = str;
            dr["Inteligence"] = inte;
            dr["Defence"] = def;

            dt.Rows.Add(dr);

            dt.AcceptChanges();
        }

        private void mapSaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                    ImageFormat format = ImageFormat.Png;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string ext = System.IO.Path.GetExtension(sfd.FileName);
                        switch (ext)
                        {
                            case ".jpg":
                                format = ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = ImageFormat.Bmp;
                                break;
                        }
                        pictureBox1.Image.Save(sfd.FileName, format);
                    }
                }                
            }
        }

        private void mapLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog sfd = new OpenFileDialog())
            {
                sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                ImageFormat format = ImageFormat.Png;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(sfd.FileName);
                    RawData.RawData = new Bitmap(pictureBox1.Image);
                    RawData.Locations = new List<LocationSpot>();
                    RawData.GetMapData();

                    UpdateMap();
                }
            }
        }

        public void UpdateMap()
        {
            dataGridView6.Columns.Clear();

            DataTable dt = new DataTable();

            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("X", typeof(int));
            dt.Columns.Add("Y", typeof(int));
            dt.Columns.Add("LocationSpot", typeof(LocationSpot));

            for (int i = 0; i < RawData.Locations.Count; i++)
            {
                DataRow dr = dt.NewRow();

                dr["Name"] = RawData.Locations[i].Name;
                dr["X"] = RawData.Locations[i].X;
                dr["Y"] = RawData.Locations[i].Y;
                dr["LocationSpot"] = RawData.Locations[i];

                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();
            dataGridView6.DataSource = null;
            dataGridView6.DataSource = dt;

            dataGridView6.Columns["LocationSpot"].Visible = false;
            dataGridView6.Columns["X"].ReadOnly = true;
            dataGridView6.Columns["Y"].ReadOnly = true;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DataTable dt = dataGridView6.DataSource as DataTable;

            if (dt != null && (pictureBox1.Image as Bitmap) != null)
            {
                dt.AcceptChanges();

                float TimesX = pictureBox1.Width / (pictureBox1.Image as Bitmap).Width;
                float TimesY = pictureBox1.Height /(pictureBox1.Image as Bitmap).Height;

                foreach (DataRow dr in dt.Rows)
                {                    
                    var ls = dr["LocationSpot"] as LocationSpot;
                    var name = (dr["Name"] + "");

                    e.Graphics.DrawString(name, this.Font, Brushes.White, ls.X * TimesX, ls.Y * TimesY);
                }
            }
        }

        private void dataGridView6_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox1.Refresh();
        }
    }
}
