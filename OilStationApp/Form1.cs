using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OilStationApp
{
    public partial class Form1 : Form
    {
        double OilPrice = 0;
        double CafePrice = 0;
        double TotalPrice = 0;
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(193, 180, 174);
            List<Oil> Oils = new List<Oil>
            {
                new Oil
                {
                    Name = "AI-92",
                    Price = 1
                },
                new Oil
                {
                    Name = "AI-95",
                    Price = 2
                },
                new Oil
                {
                    Name = "AI-98",
                    Price = 2.3
                },
                new Oil
                {
                    Name = "Diesel",
                    Price = 0.8
                },
            };
            oilsCmbBx.DataSource = Oils;
            oilsCmbBx.DisplayMember = nameof(Oil.Name);
            Oil oil = oilsCmbBx.SelectedItem as Oil;
            priceLbl.Text = oil.Price.ToString();
        }

        private void oilsCmbBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Oil oil = oilsCmbBx.SelectedItem as Oil;
            priceLbl.Text = oil.Price.ToString();
        }

        private void litrRdBtn_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in chooseGrpBx.Controls)
            {
                if (item is RadioButton rb)
                {
                    if (rb.Checked)
                    {
                        if (rb.Text == "litr")
                        {
                            litreMTxtb.Enabled = true;
                            priceMTxtb.Enabled = false;
                            priceMTxtb.Text = string.Empty;
                        }
                        else if (rb.Text == "price")
                        {
                            priceMTxtb.Enabled = true;
                            litreMTxtb.Enabled = false;
                            litreMTxtb.Text = string.Empty;
                        }
                    }
                }
            }
        }

        private void oilsumBtn_Click(object sender, EventArgs e)
        {
            Oil oil = oilsCmbBx.SelectedItem as Oil;
            double price = oil.Price;
            double sumPrice = 0;
            foreach (var item in chooseGrpBx.Controls)
            {
                if (item is RadioButton rb)
                {
                    if (rb.Checked)
                    {
                        if (rb.Text == "litr")
                        {
                            sumPrice = 0;
                            double Litre = double.Parse(litreMTxtb.Text);
                            sumPrice += Litre * price;
                        }
                        else if (rb.Text == "price")
                        {
                            sumPrice = 0;
                            double oilprice = double.Parse(priceMTxtb.Text);
                            sumPrice += oilprice;
                        }
                    }
                }
            }
            oilpriceLbl.Text = sumPrice.ToString();
            finishpriceLbl.Text = (double.Parse(oilpriceLbl.Text) + double.Parse(cafePriceLbl.Text)).ToString();
        }

        private void food1ChckBx_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in cafeGrpBx.Controls)
            {
                if (item is CheckBox chckBx)
                {
                    if (chckBx.Checked)
                    {
                        if (chckBx.Text == food01ChckBx.Text)
                        {
                            food1countMtxtb.Enabled = true;
                        }
                        else if (chckBx.Text == food2ChckBx.Text)
                        {
                            food2countMtxtb.Enabled = true;
                        }
                        else if (chckBx.Text == food3ChckBx.Text)
                        {
                            food3countMtxtb.Enabled = true;
                        }
                        else if (chckBx.Text == food4ChckBx.Text)
                        {
                            food4countMtxtb.Enabled = true;
                        }
                    }
                    else
                    {
                        if (chckBx.Text == food01ChckBx.Text)
                        {
                            food1countMtxtb.Text = string.Empty;
                            food1countMtxtb.Enabled = false;
                        }
                        else if (chckBx.Text == food2ChckBx.Text)
                        {
                            food2countMtxtb.Text = string.Empty;
                            food2countMtxtb.Enabled = false;
                        }
                        else if (chckBx.Text == food3ChckBx.Text)
                        {
                            food3countMtxtb.Text = string.Empty;
                            food3countMtxtb.Enabled = false;
                        }
                        else if (chckBx.Text == food4ChckBx.Text)
                        {
                            food4countMtxtb.Text = string.Empty;
                            food4countMtxtb.Enabled = false;
                        }
                    }
                }
            }
        }

        private void cafesumBtn_Click(object sender, EventArgs e)
        {
            double sumprice = 0;
            foreach (var item in cafeGrpBx.Controls)
            {
                if (item is CheckBox chckBx)
                {
                    if (chckBx.Checked)
                    {
                        if (chckBx.Text == food01ChckBx.Text)
                        {
                            sumprice += double.Parse(food1countMtxtb.Text) * double.Parse(food1priceLbl.Text);
                        }
                        else if (chckBx.Text == food2ChckBx.Text)
                        {
                            sumprice += double.Parse(food2countMtxtb.Text) * double.Parse(food2priceLbl.Text);
                        }
                        else if (chckBx.Text == food3ChckBx.Text)
                        {
                            sumprice += double.Parse(food3countMtxtb.Text) * double.Parse(food03priceLbl.Text);
                        }
                        else if (chckBx.Text == food4ChckBx.Text)
                        {
                            sumprice += double.Parse(food4countMtxtb.Text) * double.Parse(food4priceLbl.Text);
                        }
                    }
                }
            }
            cafePriceLbl.Text = sumprice.ToString();
            finishpriceLbl.Text = (double.Parse(oilpriceLbl.Text) + double.Parse(cafePriceLbl.Text)).ToString();

        }

        private void givebillBtn_Click(object sender, EventArgs e)
        {
            Random random = new Random();   
            int fileRandom = random.Next(100000);
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A7, 10, 10, 5, 0);
            string PDFpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/" + "Bill" + fileRandom.ToString() + ".pdf";
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(PDFpath, FileMode.Create));
            doc.Open();
            Paragraph paraghraph = new Paragraph();
            Oil oil = oilsCmbBx.SelectedItem as Oil;
            double oilprice = double.Parse(oilpriceLbl.Text);
            paraghraph.Add("                        BILL\n");
            paraghraph.Add($"\n");
            paraghraph.Add($" Oil Station :\n");
            paraghraph.Add($" Oil : {oil.Name}\n");
            paraghraph.Add($" Oil price per litre : {oil.Price} AZN\n");
            paraghraph.Add($" Oil litre : {oilprice / oil.Price}\n");
            paraghraph.Add($" Oil price  : {oilprice} AZN\n");

            paraghraph.Add($"\nMini-Cafe : \n");
            paraghraph.Add("Name           Price   Count  Total\n");

            foreach (var item in cafeGrpBx.Controls)
            {
                if (item is CheckBox chckBx)
                {
                    if (chckBx.Checked)
                    {
                        double price = 0;
                        if (chckBx.Text == food01ChckBx.Text)
                        {
                            price = double.Parse(food1priceLbl.Text) * double.Parse(food1countMtxtb.Text);
                            paraghraph.Add($"{food01ChckBx.Text.PadRight(15)}{food1priceLbl.Text.PadRight(8)}  {food1countMtxtb.Text.PadRight(8)}{price}\n");
                        }
                        else if (chckBx.Text == food2ChckBx.Text)
                        {
                            price = double.Parse(food2priceLbl.Text) * double.Parse(food2countMtxtb.Text);
                            paraghraph.Add($"{food2ChckBx.Text.PadRight(15)}{food2priceLbl.Text.PadRight(8)}  {food2countMtxtb.Text.PadRight(8)}{price}\n");
                        }
                        else if (chckBx.Text == food3ChckBx.Text)
                        {
                            price = double.Parse(food03priceLbl.Text) * double.Parse(food3countMtxtb.Text);
                            paraghraph.Add($"{food3ChckBx.Text.PadRight(14)}{food03priceLbl.Text.PadRight(8)}  {food3countMtxtb.Text.PadRight(8)}{price}\n");
                        }
                        else if (chckBx.Text == food4ChckBx.Text)
                        {
                            price = double.Parse(food4priceLbl.Text) * double.Parse(food4countMtxtb.Text);
                            paraghraph.Add($"{food4ChckBx.Text.PadRight(15)} {food4priceLbl.Text.PadRight(8)}  {food4countMtxtb.Text.PadRight(8)}{price}\n");
                        }
                    }
                }
            }
            paraghraph.Add($"Cafe Total Price : {cafePriceLbl.Text} AZN\n");

            paraghraph.Add($"---------------------------------------------\n");
            paraghraph.Add($"Total Price : {double.Parse(finishpriceLbl.Text)} AZN\n");


            doc.Add(paraghraph);
            doc.Close();

        }
    }
}
