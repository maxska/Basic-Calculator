using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //====================================================================
        //=======================   NUMBER BUTTONS     =======================
        //====================================================================


        private void B0_Click(object sender, EventArgs e)
        {
            //if(ioBox.Text.Length != 0) 

            //Ska jag fixa så att man inte kan skriva flera nollor om den 
            // siffran som precis skrevs är en nolla, dvs man inte kan skriva
            // 00000, men kan kan skriva 10000


            ioBox.Text += 0;
        }

        private void B1_Click(object sender, EventArgs e)
        {
            ioBox.Text += 1;
        }  

        private void B2_Click(object sender, EventArgs e)
        {
            ioBox.Text += 2;
        }

        private void B3_Click(object sender, EventArgs e)
        {
            ioBox.Text += 3;
        }

        private void B4_Click(object sender, EventArgs e)
        {
            ioBox.Text += 4;
        }

        private void B5_Click(object sender, EventArgs e)
        {
            ioBox.Text += 5;
        }

        private void B6_Click(object sender, EventArgs e)
        {
            ioBox.Text += 6;
        }

        private void B7_Click(object sender, EventArgs e)
        {
            ioBox.Text += 7;
        }

        private void B8_Click(object sender, EventArgs e)
        {
            ioBox.Text += 8;
        }

        private void B9_Click(object sender, EventArgs e)
        {
            ioBox.Text += 9;
        }

        //====================================================================
        //=======================   NUMBER BUTTONS     =======================
        //====================================================================


        private void PlusButton_Click(object sender, EventArgs e)
        {
            ioBox.Text += "+";
            DisableOperatorButtons();
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            ioBox.Text += "-";
            DisableOperatorButtons();
        }

        private void MultButton_Click(object sender, EventArgs e)
        {
            ioBox.Text += "x";
            DisableOperatorButtons();
        }

        private void DivButton_Click(object sender, EventArgs e)
        {
            ioBox.Text += "/";
            DisableOperatorButtons();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ioBox.Text = "";
        }



        //====================================================================
        //=======================   EXTRA METHODS      =======================
        //====================================================================



        private bool ContainsOnly()
        {
            return false;
        }

        private void DisableOperatorButtons()
        {
            PlusButton.Enabled = false;
            MinusButton.Enabled = false;
            MultButton.Enabled = false;
            DivButton.Enabled = false;
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            double first;
            double second;

            string s = ioBox.Text;

            char splitter = GetNonNumber(s);

            string[] numbers = s.Split(splitter);


            try
            {
                first = double.Parse(numbers[0]);
                second = double.Parse(numbers[1]);

                MessageBox.Show("first = " + first);
                MessageBox.Show("second = " + second);
            }
            catch
            {
                MessageBox.Show("EXCEPTION: couldnt do double.Parse... ");
            }
        }


        private char GetNonNumber(string text)
        {
            char current;

            for(int i = 0; i < text.Length; i++)
            {
                current = text[i];

                if(IsNumber(current) == false)
                {
                    return current;
                }
            }

            MessageBox.Show("ERROR: couldn't return char in getNonNumber()... returning '?'...");
            return ('?');
        }

        private bool IsNumber(char c)
        {
            if(c == '0' || c == '1' || c == '2' || c == '3' || c == '4' 
            || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
            {
                return true;
            }

            return false;
        }

        
    }
}


// måste ta hänsyn till att användaren skriver in . ist för , och att användaren kanske
//   skriver in * ist för x, eller vilken godtycklig bokstav/tecken som helst...

// eller, fråga föreläsaren om man verkligen behöver ta input från keyboard.... (?)




        //private void label2_Click(object sender, EventArgs e)
        //{

        //}