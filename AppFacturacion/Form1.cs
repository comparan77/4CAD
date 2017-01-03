using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ConsoleAppCasc.facturacion;
using System.Diagnostics;

namespace AppFacturacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel files | *.xls;*.xlsx"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                button1.Visible = false;
                String path = dialog.FileName; // get name of file
                //using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                //{
                    
                //}
                string result = Path.GetTempFileName().Replace("\\", "/").Replace(".tmp", ".xlsx");
                facturacionCtrl.procesaFacturacion(path, result);
                LinkLabel.Link lnk = new LinkLabel.Link();
                lnk.LinkData = "http://cascserver.ddns.net:82";
                linkLabel1.Links.Add(lnk);
                linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
                linkLabel1.Visible = true;
            }
        }

        void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
