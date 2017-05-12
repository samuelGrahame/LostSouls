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
        public World ActiveWorld;

        public bool ToSellSomething = false; // if tosellsomething is true - and when we double click on a inventory item - to sell the item, giving the value of the item to main character in orens, destroying the item. then send back to the console yes - after we double click or if the user says no - we set tosellsomething to false.
        // operators: == Is Equal to the Value returns true or false
        // = Set Equal to the Value: returns the value
        // x = 10;
        // if(x == 10)
      //   {
      //
    ////     }

    //    public void SetValue(ref object value1, object value2)
    //    {
    //        value1 = value2;
    //    }

    //    public new bool Equals(object value1, object value2)
    //    {
    //        return value1 == value2;
    //    }

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
                ActiveWorld = world;
                
                var rawMap = world.MapRawData;

                foreach (var mapItem in world.Map)
                {
                    if(!mapItem.Visible)
                    {
                        rawMap.SetPixel(mapItem.X, mapItem.Y, Color.Black);
                    }else
                    {
                        var color = MapData.GetColor(mapItem);
                        if (rawMap.GetPixel(mapItem.X, mapItem.Y) != color)
                        {
                            rawMap.SetPixel(mapItem.X, mapItem.Y, color);
                        }
                    }
                } 
                rawMap.SetPixel(world.CurrentX, world.CurrentY, Color.White);

                pictureBox1.Image = world.MapRawData;
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
                AddRowToEquipt(dt, livingEntity.Belt);

                dt.AcceptChanges();

                var dt2 = new DataTable();

                dt2.Columns.Add("Name", typeof(string));
                dt2.Columns.Add("Description", typeof(string));
                dt2.Columns.Add("Item", typeof(Item));

                foreach (Item item in livingEntity.Inventory.Items)
                {
                    DataRow dr = dt2.NewRow();
                    string name = item.Name;

                    if(item is ResourceItem)
                    {
                        name = (item as ResourceItem).Quantity + " " + name;
                    }

                    dr["Name"] = name;                   
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
                if(ToSellSomething)
                {
                    int price = 0;
                    if (item is UseItem)
                    {
                        var itemU = (UseItem)item;

                        price = itemU.Price;
                    }else if (item is ToolItem)
                    {
                        var itemU = (ToolItem)item;

                        price = itemU.Price;
                    }
                    else if (item is ResourceItem)
                    {
                        var itemU = (ResourceItem)item;

                        price = itemU.Price * itemU.Quantity;
                    }
                    else if(item is WearableItem)
                    {
                        var itemW = (WearableItem)item;

                        price = (itemW.Power.Strength + 1) * (itemW.Power.Inteligence + 1) * (itemW.Power.Defence + 1);
                    }
                    
                    ActiveHuman.Orens += price;
                    
                    ActiveHuman.Inventory.Items.Remove(item);
                    
                    ToSellSomething = false;

                    Program.WriteLineColor($"You have sold the item for {World.GetOrenLabel(price)}.", ConsoleColor.Yellow, ConsoleColor.Black);
                    Program.WriteLineColor($"You now have {World.GetOrenLabel(ActiveHuman.Orens)}", ConsoleColor.Yellow, ConsoleColor.Black);

                    UpdateStats(ActiveHuman);
                    
                    WriteLineProcess("yes");

                    
                    // then we need to set the value of ToSellSomething to false. // (we set the value to false when we create the variable ToSellSomething)                    
                }
                else
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
                        else if (item is ToolItem)
                        {
                            if (ActiveHuman.Belt != null)
                            {
                                ActiveHuman.Inventory.Items.Add(ActiveHuman.Belt);
                                ActiveHuman.Belt = null;
                            }
                            if(ActiveHuman.GetWearablePower().Inteligence >= ((ToolItem)item).Requirements)
                            {
                                ActiveHuman.Belt = (ToolItem)item;
                            }
                            else
                            {
                                Program.WriteLineColor($"You do not meet the requirements to wear {item.Name} you require {((ToolItem)item).Requirements} Inteligence.", ConsoleColor.Red, ConsoleColor.Black);

                                UpdateStats(ActiveHuman);
                                return;
                            }
                        }
                        ActiveHuman.Inventory.Items.Remove(item);

                        UpdateStats(ActiveHuman);
                    }
                    else
                    {

                    }
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
                    else if (item is ToolItem)
                    {
                        if (ActiveHuman.Belt == item)
                        {
                            ActiveHuman.Inventory.Items.Add(ActiveHuman.Belt);
                            ActiveHuman.Belt = null;
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
            
            PostMessage(GetConsoleWindow(), WM_KEYDOWN, VK_RETURN, 0);
            
            Application.DoEvents();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WriteLineProcess("Attack");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ActiveWorld.CanIMove(ActiveWorld.CurrentX + 1, ActiveWorld.CurrentY))
            {
                WriteLineProcess("East");
            }
            else
            {
                ProcessCommand(ActiveWorld.CurrentX + 1, ActiveWorld.CurrentY);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (ActiveWorld.CanIMove(ActiveWorld.CurrentX, ActiveWorld.CurrentY + 1))
            {
                WriteLineProcess("South");
            }
            else
            {
                ProcessCommand(ActiveWorld.CurrentX, ActiveWorld.CurrentY + 1);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ActiveWorld.CanIMove(ActiveWorld.CurrentX - 1, ActiveWorld.CurrentY))
            {
                WriteLineProcess("West");
            }
            else
            {
                ProcessCommand(ActiveWorld.CurrentX - 1, ActiveWorld.CurrentY);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(ActiveWorld.CanIMove(ActiveWorld.CurrentX, ActiveWorld.CurrentY - 1))
            {
                WriteLineProcess("North");
            }
            else
            {
                ProcessCommand(ActiveWorld.CurrentX, ActiveWorld.CurrentY - 1);                
            }
        }

        public void ProcessMovement(int x, int y)
        {
            if(ActiveWorld.CanIMove(x, y))
            {
                ActiveWorld.CurrentX = x;
                ActiveWorld.CurrentY = y;
            }
            else
            {
                ProcessCommand(x, y);
            }
            UpdateWorld(ActiveWorld);
        }

        public void ProcessCommand(int x, int y)
        {
            if (ActiveHuman.Belt == null)
            {
                return;
            }

            var mapItem = ActiveWorld.GetMapItem(x, y);

            if (mapItem == null)
                return;

            if(mapItem is Tree)
            {
                if(ActiveHuman.Belt is WoodCutters_Axe)
                {
                    mapItem = new Ground() { X = mapItem.X, Visible = true, Y = mapItem.Y } ;

                    ActiveHuman.Inventory.AddOrRemoveResourceItem(typeof(Wood), 1);

                    if(ActiveWorld.Percent(25))
                    {
                        ActiveHuman.Inventory.AddOrRemoveResourceItem(typeof(TreeSeeds), 1);
                    }

                    ActiveWorld.Map[x, y] = mapItem;

                    UpdateWorld(ActiveWorld);

                    UpdateStats(ActiveHuman);
                }
            }
            else if (mapItem is Water)
            {
                if (ActiveHuman.Belt is Fishing_Rod)
                {
                    // some kind of random event - to add fish
                    if (ActiveWorld.Percent(25))
                    {
                        ActiveHuman.Inventory.AddOrRemoveResourceItem(typeof(Fish), 1);
                    }

                    UpdateStats(ActiveHuman);                    
                }else if (ActiveHuman.Belt is Hammer)
                {
                    // do we have wood
                    if(ActiveHuman.Inventory.CanIUseResource(typeof(Wood), 10))
                    {
                        mapItem = new Road() { X = mapItem.X, Visible = true, Y = mapItem.Y };

                        ActiveHuman.Inventory.AddOrRemoveResourceItem(typeof(Wood), -10);
                        
                        ActiveWorld.Map[x, y] = mapItem;

                        UpdateWorld(ActiveWorld);
                        UpdateStats(ActiveHuman);
                    }
                    
                }
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            WriteLineProcess("Outside");
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

        private void button19_Click(object sender, EventArgs e)
        {
            WriteLineProcess("6");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            WriteLineProcess("7");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            WriteLineProcess("8");
        }
    }
}
