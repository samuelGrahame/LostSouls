using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp4
{
    public partial class frmStats : Form
    {
        public Humanoid ActiveHuman;

        public frmStats()
        {
            InitializeComponent();
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        delegate void VoidWithAbility(Humanoid livingEntity);
        delegate void VoidWithWorld(World livingEntity);

        public void UpdateWorld(World world)
        {
            if (this.InvokeRequired)
            {
                VoidWithWorld d = new VoidWithWorld(UpdateWorld);
                this.Invoke(d, new object[] { world });
            }
            else
            {
                var img = pictureBox1.Image;

                if (img != null)
                    img.Dispose();

                pictureBox1.Image = null;

                var rawMap = new Bitmap(world.MapRawData);

                foreach (var mapItem in world.Map)
                {
                    if(!mapItem.Visible)
                    {
                        rawMap.SetPixel(mapItem.X, mapItem.Y, Color.Black);
                    }
                }

                rawMap.SetPixel(world.CurrentX, world.CurrentY, Color.White);
                pictureBox1.Image = rawMap;
            }
        }

        public void UpdateStats(Humanoid livingEntity)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.InvokeRequired)
            {
                VoidWithAbility d = new VoidWithAbility(UpdateStats);
                this.Invoke(d, new object[] { livingEntity });
            }
            else
            {
                ActiveHuman = livingEntity;

                var x = livingEntity.GetWearablePower();
                
                this.labelStrength.Text = "Strength: " + livingEntity.Power.Strength + " / " + x.Strength;
                this.labelInteligence.Text = "Inteligence: " + livingEntity.Power.Inteligence + " / " + x.Inteligence;
                this.labelDefence.Text = "Defence: " + livingEntity.Power.Defence + " / " + x.Defence;
                this.labelHealth.Text = "Health: " + livingEntity.Health;
                this.labelXP.Text = $"XP: {livingEntity.CombatLevel.XP} / {livingEntity.CombatLevel.XPToLevelUp}";
                this.labelName.Text = "Name: " + livingEntity.Name;
                this.labelRace.Text = "Race: " + livingEntity.Race.ToString("G");
                this.labelGender.Text = "Gender: " + livingEntity.Gender.ToString("G");
                if (livingEntity.Orens == 1)
                {
                    this.labelOrens.Text = "Oren: " + livingEntity.Orens;
                }
                else
                {
                    this.labelOrens.Text = "Orens: " + livingEntity.Orens;
                }

                this.labelPoints.Text = livingEntity.AvailablePowerPoints.ToString();
                this.labelLevel.Text = "Level: " + livingEntity.CombatLevel.Value;
                UpdatePointButtons();

                dataEquipt.Columns.Clear();
                dataEquipt.DataSource = null;

                var dt = new DataTable();

                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Strength", typeof(int));
                dt.Columns.Add("Inteligence", typeof(int));
                dt.Columns.Add("Defence", typeof(int));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Item", typeof(Item));

                AddRowToEquipt(dt, livingEntity.Head);
                AddRowToEquipt(dt, livingEntity.Chest);
                AddRowToEquipt(dt, livingEntity.Legs);
                AddRowToEquipt(dt, livingEntity.Feet);
                AddRowToEquipt(dt, livingEntity.Hands);
                
                dt.AcceptChanges();

                var dt2 = new DataTable();

                dt2.Columns.Add("Name", typeof(string));
                dt2.Columns.Add("Description", typeof(string));
                dt2.Columns.Add("Item", typeof(Item));

                foreach (Item item in livingEntity.Inventory.Items)
                {
                    DataRow dr = dt2.NewRow();

                    dr["Name"] = item.Name;                   
                    dr["Description"] = item.Description;
                    dr["Item"] = item;

                    dt2.Rows.Add(dr);
                }
                
                dt2.AcceptChanges();


                dataEquipt.DataSource = dt;
                dataInventory.DataSource = dt2;

                HideAndReadOnlyDT(dataEquipt);
                HideAndReadOnlyDT(dataInventory);
            }
        }

        public void HideAndReadOnlyDT(DataGridView dt)
        {
            if (dt == null)
                return;

            
            foreach (DataGridViewColumn column in dt.Columns)
            {
                column.ReadOnly = true;
                if (column.DataPropertyName == "Item")
                    column.Visible = false;
            }
        }

        public void AddRowToEquipt(DataTable dt, WearableItem item)
        {
            if (item == null)
                return;
            DataRow dr = dt.NewRow();

            dr["Name"] = item.Name;
            dr["Description"] = item.Description;

            dr["Strength"] = item.Power.Strength;
            dr["Inteligence"] = item.Power.Inteligence;
            dr["Defence"] = item.Power.Defence;
            dr["Item"] = item;

            if (item is HeadItem)
            {
                dr["Location"] = "Head";
            }else if (item is ChestItem)
            {
                dr["Location"] = "Chest";
            }
            else if (item is LegsItem)
            {
                dr["Location"] = "Legs";
            }
            else if (item is FeetItem)
            {
                dr["Location"] = "Feet";
            }
            else
            {
                dr["Location"] = "Hands";
            }
            
            dt.Rows.Add(dr);
        }

        public void UpdatePointButtons()
        {
            bool pointsEnabled = ActiveHuman.AvailablePowerPoints > 0;
            button1.Enabled = pointsEnabled;
            button2.Enabled = pointsEnabled;
            button3.Enabled = pointsEnabled;
        }
        
        private void frmStats_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdatePointButtons();
            if(button1.Enabled)
            {
                ActiveHuman.Power.Strength += 1;
                ActiveHuman.AvailablePowerPoints -= 1;

                UpdateStats(ActiveHuman);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdatePointButtons();
            if (button2.Enabled)
            {
                ActiveHuman.Power.Inteligence += 1;
                ActiveHuman.AvailablePowerPoints -= 1;

                UpdateStats(ActiveHuman);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdatePointButtons();
            if (button3.Enabled)
            {
                ActiveHuman.Power.Defence += 1;
                ActiveHuman.AvailablePowerPoints -= 1;

                UpdateStats(ActiveHuman);
            }
        }

        private void dataEquipt_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void dataInventory_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }
        
        public void Equipt()
        {
            if (dataInventory.SelectedRows.Count == 0)
                return;

            var item = (Item)dataInventory.SelectedRows[0].Cells["Item"].Value;

            if (ActiveHuman.Inventory.Items.Contains(item))
            {
                if (item is WearableItem)
                {
                    var typeofValue = item.GetType();
                    if (item is HeadItem)
                    {
                        if (ActiveHuman.Head != null)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Head);
                            ActiveHuman.Head = null;
                        }

                        ActiveHuman.Head = (HeadItem)item;
                    }
                    else if (item is ChestItem)
                    {
                        if (ActiveHuman.Chest != null)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Chest);
                            ActiveHuman.Chest = null;
                        }
                        ActiveHuman.Chest = (ChestItem)item;
                    }
                    else if (item is LegsItem)
                    {
                        if (ActiveHuman.Legs != null)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Legs);
                            ActiveHuman.Legs = null;
                        }
                        ActiveHuman.Legs = (LegsItem)item;
                    }
                    else if (item is FeetItem)
                    {
                        if (ActiveHuman.Feet != null)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Feet);
                            ActiveHuman.Feet = null;
                        }
                        ActiveHuman.Feet = (FeetItem)item;
                    }
                    else if (item is WeaponItem)
                    {
                        if (ActiveHuman.Hands != null)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Hands);
                            ActiveHuman.Hands = null;
                        }
                        ActiveHuman.Hands = (WeaponItem)item;
                    }
                    ActiveHuman.Inventory.Items.Remove(item);

                    UpdateStats(ActiveHuman);
                }
                else
                {

                }
            }
            else
            {
                UpdateStats(ActiveHuman);
            }
        }
        public void Unequipt()
        {
            if (dataEquipt.SelectedRows.Count == 0)
                return;

            var item = (Item)dataEquipt.SelectedRows[0].Cells["Item"].Value;

            if (!ActiveHuman.Inventory.Items.Contains(item))
            {
                if (item is WearableItem)
                {
                    var typeofValue = item.GetType();
                    if (item is HeadItem)
                    {
                        if (ActiveHuman.Head == item)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Head);
                            ActiveHuman.Head = null;
                        }
                    }
                    else if (item is ChestItem)
                    {
                        if (ActiveHuman.Chest == item)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Chest);
                            ActiveHuman.Chest = null;
                        }
                    }
                    else if (item is LegsItem)
                    {
                        if (ActiveHuman.Legs == item)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Legs);
                            ActiveHuman.Legs = null;
                        }
                    }
                    else if (item is FeetItem)
                    {
                        if (ActiveHuman.Feet == item)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Feet);
                            ActiveHuman.Feet = null;
                        }
                    }
                    else if (item is WeaponItem)
                    {
                        if (ActiveHuman.Hands == item)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Hands);
                            ActiveHuman.Hands = null;
                        }
                    }

                    UpdateStats(ActiveHuman);
                }
                else
                {

                }
            }
            else
            {
                UpdateStats(ActiveHuman);
            }
        }
        
        private void dataEquipt_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Unequipt();

        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        const int VK_RETURN = 0x0D;
        const int WM_KEYDOWN = 0x100;

        private void dataInventory_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Equipt();
        }

        public void WriteLineProcess(string value)
        {
            Program.ExternalResponce = value;
            var hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            SetForegroundWindow(GetConsoleWindow());

            PostMessage(hWnd, WM_KEYDOWN, VK_RETURN, 0);
            
            Application.DoEvents();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WriteLineProcess("Attack");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WriteLineProcess("East");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WriteLineProcess("South");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WriteLineProcess("West");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WriteLineProcess("North");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            WriteLineProcess("Forest");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            WriteLineProcess("Yes");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            WriteLineProcess("No");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            WriteLineProcess("0");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            WriteLineProcess("1");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            WriteLineProcess("2");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            WriteLineProcess("3");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            WriteLineProcess("flee");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            WriteLineProcess("4");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            WriteLineProcess("5");
        }
    }
}
