using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saaristo_Asg8_Sales
{
    public partial class Form1 : Form
    {
        const int SALES_COUNT = 25;
        double[] arraySalesData = new double[SALES_COUNT];
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {

            clearSalesDataLabels();

            // Get the sales values.
            double smallestSale = getSmallestSale();
            double largestSale = getLargestSale();
            double totalSales = getTotalSales();
            double averageSale = totalSales / SALES_COUNT; 


            // Display the sales data
            textBoxSV.Text = smallestSale.ToString("c");
            textBoxLV.Text = largestSale.ToString("c");
            textBoxAvg.Text = averageSale.ToString("c");
            textBoxTotal.Text = totalSales.ToString("c");

            // Get the commission data.
            double commissionRate = (double)numericUpDownComishPercent.Value;


            // Calculate and display SMALLEST commission and net sale.
            double smallestCommission = calculateCommission(smallestSale, commissionRate);
            double smallestNetSale = smallestSale - smallestCommission;
            textBoxSVC.Text = smallestCommission.ToString("c");
            textBoxSVNS.Text = smallestNetSale.ToString("c");

            // Calculate and display LARGEST commission and net sale
            double largestCommission = calculateCommission(largestSale, commissionRate);
            double largestNetSale = largestSale - largestCommission;
            textBoxLVC.Text = largestCommission.ToString("c");
            textBoxLVNS.Text = largestNetSale.ToString("c");

            // Calculate and display AVERAGE commission and net sale
            double avgCommission = (largestCommission + smallestCommission) / 2;
            double avgNetSale = averageSale - avgCommission;
            textBoxAvgC.Text = avgCommission.ToString("c");
            textBoxAvgNS.Text = avgNetSale.ToString("c");

            // Calculate and display TOTAL commission and net sale
            double totalCommission = largestCommission + smallestCommission;
            double totalNetSale = totalSales - totalCommission;
            textBoxTotalC.Text = totalCommission.ToString("c");
            textBoxTotalNS.Text = totalNetSale.ToString("c");
        }

       // private double calculateAverage(double a, double b)
        //{
          //  throw new NotImplementedException();
            //double avg = arraySalesData.Sum() / 2;
           // return avg;
    //    }
    

        private double calculateCommission(double a, double b)
        {
            double calculatedCommission = a * (b /100);
            return calculatedCommission;
        }

        private double getTotalSales()
        {
            double sum = arraySalesData.Sum();
            return sum;
        }

        private double getLargestSale()
        {
            double largestSale = arraySalesData.Max();
            return largestSale;
        }

        private double getSmallestSale()
        {
            double smallestSale = arraySalesData.Min();
            return smallestSale;
        }

        private void clearSalesDataLabels()
        {
            textBoxAvg.Text = null;
            textBoxAvgC.Text = null;
            textBoxAvgNS.Text = null;
            textBoxLV.Text = null;
            textBoxLVC.Text = null;
            textBoxLVNS.Text = null;
            textBoxSV.Text = null;
            textBoxSVC.Text = null;
            textBoxSVNS.Text = null;
            textBoxTotal.Text = null;
            textBoxTotalC.Text = null;
            textBoxTotalNS.Text = null; 

        }

        private void loadSalesDataIntoArray()
        {
            StreamReader inputReader = new StreamReader("sales.txt");

            for (int i = 0; i < SALES_COUNT; i++)
            {
                if (!inputReader.EndOfStream)
                    arraySalesData[i] = double.Parse(inputReader.ReadLine());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadSalesDataIntoArray();
            loadArrayIntoListbox();

            groupBoxSalesData.Text = "Sales Data: " + listBoxSalesDataInp.Items.Count.ToString() + " Records";
        }

        private void loadArrayIntoListbox()
        {
            for(int X=0; X < arraySalesData.Length; X++)
            {
                listBoxSalesDataInp.Items.Add(arraySalesData[X]);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
