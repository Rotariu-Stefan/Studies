using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Windows.Forms;
using System.Transactions;

namespace Tema1
{
    public partial class Form1 : Form
    {
        EntitiesCustomer context;
        TransactionScope ts;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            context = new EntitiesCustomer();
            //context.SaveChanges(false);
            ts = new TransactionScope();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            context.Dispose();
            ts.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)      //list c
        {
            /*var econ = context.Connection;
            econ.Open();
            econ.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            var comm = econ.CreateCommand();
            comm.CommandText = "SELECT con.name FROM EntitiesCustomer.CUSTOMER AS con";
            var dr = comm.ExecuteReader(CommandBehavior.SequentialAccess);
            comboCust.DataSource = dr;*/
            IQueryable<CUSTOMER> qc = from c in context.CUSTOMERs select c;
            comboCust.DataSource = qc.ToList();
            comboCust.DisplayMember = "NAME";
        }

        private void button2_Click(object sender, EventArgs e)      //insert c
        {
            CUSTOMER addc = new CUSTOMER();
            addc.CUSTOMERID = Convert.ToInt16(context.CUSTOMERs.ToList().Last().CUSTOMERID + 1);
            addc.NAME = textcname.Text;
            addc.ADRESA = textcadresa.Text;
            context.AddToCUSTOMERs(addc);
            int rowsAffected = context.SaveChanges(false);
            textCh.Text=rowsAffected.ToString() + " changes made to the customers (insert)";
        }

        private void button3_Click(object sender, EventArgs e)      //update c
        {
            CUSTOMER selc = (CUSTOMER)comboCust.SelectedItem;
            selc.NAME = textcname.Text;
            selc.ADRESA = textcadresa.Text;
            int rowsAffected = context.SaveChanges(false);
            textCh.Text=rowsAffected.ToString() + " changes made to the customers (update)";
        }

        private void comboCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            CUSTOMER selc = (CUSTOMER)comboCust.SelectedItem;
            textcid.Text = selc.CUSTOMERID.ToString();
            textcname.Text = selc.NAME;
            textcadresa.Text = selc.ADRESA;
        }

        private void button12_Click(object sender, EventArgs e)     //delete c
        {
            CUSTOMER selc = (CUSTOMER)comboCust.SelectedItem;
            context.DeleteObject(selc);
            int rowsAffected = context.SaveChanges(false);
            textCh.Text=rowsAffected.ToString() + " changes made to the customers (delete)";
        }

        private void button7_Click(object sender, EventArgs e)      //get c orders
        {
            try
            {
                CUSTOMER selc = (CUSTOMER)comboCust.SelectedItem;
                int selcid = selc.CUSTOMERID;

                IQueryable<ORDER> queryo = from o in context.ORDERs where o.CUSTOMERID == selcid select o;
                List<ORDER> selo = queryo.ToList();

                if (selo != null && selo.Count > 0)
                {
                    comboOrder.DataSource = selo;
                    comboOrder.DisplayMember = "ORDERID";
                }
                else
                    comboOrder.DataSource = null;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Invalid Selection !");
            }
        }

        private void button6_Click(object sender, EventArgs e)      //list o
        {
            IQueryable<ORDER> qo = from o in context.ORDERs select o;
            comboOrder.DataSource = qo.ToList();
            comboOrder.DisplayMember = "ORDERID";
        }

        private void comboOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ORDER selo = (ORDER)comboOrder.SelectedItem;
            textoid.Text = selo.ORDERID.ToString();
            textozi.Text = selo.DATA.Day.ToString();
            textoluna.Text = selo.DATA.Month.ToString();
            textoan.Text = selo.DATA.Year.ToString();
            textocid.Text = selo.CUSTOMERID.ToString();
            textovaloare.Text = selo.VALOARE.ToString();
        }

        private void button4_Click(object sender, EventArgs e)      //update o
        {
            short cid = Convert.ToInt16(textocid.Text);
            IQueryable<CUSTOMER> ct = from c in context.CUSTOMERs where c.CUSTOMERID == cid select c;
            if (ct.Count() <= 0)
                MessageBox.Show("Invalid Customer ID!");
            else
            {
                try
                {
                    ORDER selo = (ORDER)comboOrder.SelectedItem;
                    selo.ORDERID = Convert.ToInt16(textoid.Text);
                    DateTime dt = new DateTime(Convert.ToInt32(textoan.Text), Convert.ToInt32(textoluna.Text), Convert.ToInt32(textozi.Text));
                    selo.DATA = dt;
                    selo.CUSTOMERID = cid;
                    selo.VALOARE = Convert.ToDecimal(textovaloare.Text);
                    int rowsAffected = context.SaveChanges(false);
                    textCh.Text=rowsAffected.ToString() + " changes made to the orders (update)";
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Invalid Date!");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)      //insert o
        {
            short cid = Convert.ToInt16(textocid.Text);
            IQueryable<CUSTOMER> ct = from c in context.CUSTOMERs where c.CUSTOMERID == cid select c;
            if (ct.Count() <= 0)
                MessageBox.Show("Invalid Customer ID!");
            else
            {
                try
                {
                    ORDER addo = new ORDER();
                    addo.ORDERID = Convert.ToInt16(context.ORDERs.ToList().Last().ORDERID + 1);
                    DateTime dt = new DateTime(Convert.ToInt32(textoan.Text), Convert.ToInt32(textoluna.Text), Convert.ToInt32(textozi.Text));
                    addo.DATA = dt;
                    addo.CUSTOMERID = cid;
                    addo.VALOARE = Convert.ToDecimal(textovaloare.Text);
                    context.AddToORDERs(addo);
                    int rowsAffected = context.SaveChanges(false);
                    textCh.Text=rowsAffected.ToString() + " changes made to the orders (insert)";
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Invalid Date!");
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)     //delete o
        {
            ORDER selo = (ORDER)comboOrder.SelectedItem;
            context.DeleteObject(selo);
            int rowsAffected = context.SaveChanges(false);
            textCh.Text=rowsAffected.ToString() + " changes made to the order (delete)";
        }

        private void button10_Click(object sender, EventArgs e)     //list od
        {
            IQueryable<ORDERDETAIL> qod = from od in context.ORDERDETAILS select od;
            comboODetails.DataSource = qod.ToList();
            comboODetails.DisplayMember = "PRODUS";
        }

        private void button11_Click(object sender, EventArgs e)     //get o details
        {
            try
            {
                ORDER selo = (ORDER)comboOrder.SelectedItem;
                int seloid = selo.ORDERID;

                IQueryable<ORDERDETAIL> qod = from od in context.ORDERDETAILS where od.ORDERID == seloid select od;
                List<ORDERDETAIL> selod = qod.ToList();

                if (selo != null && selod.Count > 0)
                {
                    comboODetails.DataSource = selod;
                    comboODetails.DisplayMember = "PRODUS";
                }
                else
                    comboODetails.DataSource = null;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Invalid Selection !");
            }
        }

        private void button8_Click(object sender, EventArgs e)      //update od
        {
            try
            {
                decimal oval = Convert.ToDecimal(textodvaloare.Text);
                string oprod = textodprodus.Text;
                ORDERDETAIL selod = (ORDERDETAIL)comboODetails.SelectedItem;
                if (oval != selod.VALOARE)
                    selod.ORDER.VALOARE = selod.ORDER.VALOARE - selod.VALOARE + oval;
                selod.PRODUS = oprod;
                if (selod.ORDERID != Convert.ToInt16(textodid.Text) || selod.SERIAL != Convert.ToInt16(textodserial.Text))
                    throw new InvalidOperationException();

                int rowsAffected = context.SaveChanges(false);
                textCh.Text = rowsAffected.ToString() + " changes made to the orderdetails (update)";
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Invalid orderId/serial !");
            }
        }

        private void button9_Click(object sender, EventArgs e)      //insert od
        {
            short oid = Convert.ToInt16(textodid.Text);
            IQueryable<ORDER> odt = from od in context.ORDERs where od.ORDERID == oid select od;
            if (odt.Count() <= 0)
                MessageBox.Show("Invalid Order ID!");
            else
            {
                odt.First().VALOARE += Convert.ToDecimal(textodvaloare.Text);   //val stuff
                try
                {
                    ORDERDETAIL selod = new ORDERDETAIL();
                    selod.ORDERID = oid;
                    selod.PRODUS = textodprodus.Text;
                    selod.VALOARE = Convert.ToDecimal(textodvaloare.Text);
                    selod.SERIAL = Convert.ToInt16(textodserial.Text);
                    context.AddToORDERDETAILS(selod);
                    int rowsAffected = context.SaveChanges(false);
                    textCh.Text=rowsAffected.ToString() + " changes made to the orderdetails (insert)";
                }
                catch (UpdateException ex)
                {
                    MessageBox.Show("Invalid Key!");
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)     //delete od
        {
            ORDERDETAIL selod = (ORDERDETAIL)comboODetails.SelectedItem;
            selod.ORDER.VALOARE -= selod.VALOARE;
            context.DeleteObject(selod);
            int rowsAffected = context.SaveChanges(false);
            textCh.Text=rowsAffected.ToString() + " changes made to the orderdetails (delete)";
        }

        private void comboODetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            ORDERDETAIL selod = (ORDERDETAIL)comboODetails.SelectedItem;
            textodid.Text = selod.ORDERID.ToString();
            textodprodus.Text = selod.PRODUS;
            textodvaloare.Text = selod.VALOARE.ToString();
            textodserial.Text = selod.SERIAL.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ts.Dispose();
            context.Refresh(RefreshMode.StoreWins, context.CUSTOMERs);
            context.Refresh(RefreshMode.StoreWins, context.ORDERs);
            context.Refresh(RefreshMode.StoreWins, context.ORDERDETAILS);

            textCh.Text = "Rollback done !";
            ts = new TransactionScope();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ts.Complete();
            context.AcceptAllChanges();
            ts.Dispose();

            textCh.Text = "Commit done !";
            ts = new TransactionScope();
        }

    }
}
