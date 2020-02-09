using GlobalHotkeys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ice.MonitorDisabler
{
    public partial class Form1 : Form
    {
        private GlobalHotkey ghk;

        public Form1()
        {
            InitializeComponent();

            ghk = new GlobalHotkey(Modifiers.NoMod, Keys.Escape, this);
            ghk.Register();
            Native.SetMonitorEnable(this.Handle, false);
            this.Shown += (o, e) => this.Hide();
        }

        protected override void WndProc(ref Message m)
        {
            var hotkeyInfo = HotkeyInfo.GetFromMessage(m);
            if (hotkeyInfo != null) HotkeyProc(hotkeyInfo);
            base.WndProc(ref m);
        }

        private void HotkeyProc(HotkeyInfo hotkeyInfo)
        {
            Native.SetMonitorEnable(this.Handle, true);
            Application.Exit();
        }
    }
}