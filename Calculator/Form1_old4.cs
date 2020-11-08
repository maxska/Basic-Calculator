using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//OBS: hittade symbol för denna knappen EraseButton på:
// https://en.wikipedia.org/wiki/Wingdings :


/// "111x2,,3" kan skrivas in...
/// 



//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
//       måste se till att man inte kan skriva in "1,x2" eller "2+,3"
// eller "2+,3,="
//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&



namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ToggleOperatorButtons(false); //denna måste köras direkt när programmet startas
            CommaButton.Enabled = false; //denna måste köras direkt när programmet startas
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
            ToggleOperatorButtons(false);
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            ioBox.Text += "-";
            ToggleOperatorButtons(false);
        }

        private void MultButton_Click(object sender, EventArgs e)
        {
            ioBox.Text += "x";
            ToggleOperatorButtons(false);
        }

        private void DivButton_Click(object sender, EventArgs e)
        {
            ioBox.Text += "/";
            ToggleOperatorButtons(false);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ioBox.Text = "";
            
            ToggleAllButtons(true);
            ToggleOperatorButtons(false);
            CommaButton.Enabled = false;
        }

        private void EraseButton_Click(object sender, EventArgs e)
        {
            if(ioBox.Text.Length > 0)
            {
                string before = ioBox.Text;

                //////////////////EXCEPTION:
                //////////////////EXCEPTION:
                //////////////////EXCEPTION:
                //får en exception unhandled när jag klickar på typ erasebutton när ioBox är empty

                string after = before.Substring(0, before.Length - 1);

                ioBox.Text = after;

                ////////////-------------------OLD LOCATION:
                ////Om den sista char:en som togs bort är en operator (inte number):
                //if (IsNumber(before[before.Length - 1]) == false)
                //{
                //    ToggleOperatorButtons(true);
                //}





                if(NumberOfOperands(before) > 1)
                {
                    //dont proceed
                }
                else if(NumberOfOperands(before) == 1)
                {
                    //Om den sista char:en som togs bort är en operator (inte number):
                    if (IsNumber(before[before.Length - 1]) == false)
                    {
                        ToggleOperatorButtons(true);
                    }
                }




            }
        }

        private void CommaButton_Click(object sender, EventArgs e)
        {
            ioBox.Text += ",";


            
            //ToggleOperatorButtons(false);
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            //då vi använder olika räknesätt (addidion/subtraktion/mult/div) 
            //  måste jag namnge variablerna något allmänt:
            double first;
            double second;
            char op; //operator
            double result;
            string inText;
            string[] numbers;

            result = 0.0; //tilldelar den ett värde så att den kan visas om en exception körs...

            inText = ioBox.Text;

            op = GetNonNumber(inText); //ger operatorn som står i ioBox

            //metoden Split delar upp en string i en array, 
            // med platsen delad med den char som anges i inparametern:
            numbers = inText.Split(op);

            try
            {
                first = double.Parse(numbers[0  ]);
                second = double.Parse(numbers[1]);

                if (op == '+')
                    result = first + second;
                else if (op == '-')
                    result = first - second;
                else if (op == 'x')
                    result = first * second;
                else if (op == '/')
                    result = first / second;
                else
                    MessageBox.Show("ERROR WHILE CHOOSING OPERATOR....");
            }
            catch
            {
                MessageBox.Show("EXCEPTION: couldnt do double.Parse... ");
            }

            ioBox.Text = $"{result}";

            ToggleAllButtons(false);
        }





        //====================================================================
        //=======================   EXTRA METHODS      =======================
        //====================================================================



        //private bool ContainsOnly()
        //{
        //    return false;
        //}

        private int NumberOfOperands(string inText)
        {
            int counter = 0;

            for (int i = 0; i < inText.Length; i++)
            {
                if( IsNumber(inText[i]) == false )
                {
                    counter++;
                }
            }

            return counter;
        }


        private void ToggleOperatorButtons(bool val)
        {
            PlusButton.Enabled = val;
            MinusButton.Enabled = val;
            MultButton.Enabled = val;
            DivButton.Enabled = val;
        }

        private void ToggleAllButtons(bool val)
        {
            PlusButton.Enabled = val;
            MinusButton.Enabled = val;
            MultButton.Enabled = val;
            DivButton.Enabled = val;

            B0.Enabled = val;
            B1.Enabled = val;
            B2.Enabled = val;
            B3.Enabled = val;
            B4.Enabled = val;
            B5.Enabled = val;
            B6.Enabled = val;
            B7.Enabled = val;
            B8.Enabled = val;
            B9.Enabled = val;

            ResetButton.Enabled = true;

            EraseButton.Enabled = val;
            EqualsButton.Enabled = val;
            CommaButton.Enabled = val;
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
            return '?';
        }

        private bool IsNumber(char c)
        {
            if(c == '0' || c == '1' || c == '2' || c == '3' || c == '4' 
            || c == '5' || c == '6' || c == '7' || c == '8' || c == '9' || c == ',')
            {
                return true;
            }

            return false;
        }

        private void ioBox_TextChanged(object sender, EventArgs e)
        {
            if(ioBox.Text.Length == 0)
            {
                ToggleOperatorButtons(false);
                CommaButton.Enabled = false;
            }
            else if(ioBox.Text.Length == 1)
            {
                ToggleOperatorButtons(true);
                CommaButton.Enabled = true;
            }




            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //       måste se till att man inte kan skriva in "1,x2" eller "2+,3"
            // eller "2+,3,="
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&



            //if( CommaExists(ioBox.Text) )
            //{
            //}

            //if( CommaAndOperatorExists(ioBox.Text) )
            //{ 
            //}


            if (ioBox.Text.Length >= 1)
            {
                if (LastWasNumber(ioBox.Text) && !TwoCommasExist(ioBox.Text))
                {
                    //MessageBox.Show("Line 364");

                    ToggleOperatorButtons(true);
                    CommaButton.Enabled = true;
                }

                if (LastWasOperator(ioBox.Text))
                {
                    CommaButton.Enabled = false;
                }
                
                if (LastWasComma(ioBox.Text))
                {
                    ToggleOperatorButtons(false);
                }

                if(OperatorExists(ioBox.Text))
                {
                    ToggleOperatorButtons(false);
                }

                if(TwoCommasExist(ioBox.Text))
                {
                    CommaButton.Enabled = false;
                }

                if ( CommaExists(ioBox.Text) && !CommaAndOperatorExists(ioBox.Text) )
                {
                    CommaButton.Enabled = false;
                }

                if( OperatorExists(ioBox.Text) && CommaExists(ioBox.Text) )
                {

                    char op = GetNonNumber(ioBox.Text); //hämtar operator 

                    string[] textSplit = ioBox.Text.Split(op);

                    //if(CommaExists( textSplit[0] ))
                    //{

                    //}

                    if(textSplit.Length > 0)
                    {
                        if( CommaExists( textSplit[1] ))
                        {
                            //då finns två av maximalt två utplacerade kommatecken:
                            CommaButton.Enabled = false;
                        }
                    }


                }

            }

            if(ioBox.Text.Length == 0)
            {
                CommaButton.Enabled = false;
            }
            

            

        }







        private bool CommaExists(string inText)
        {
            for(int i = 0; i < inText.Length; i++)
            {
                if(inText[i] == ',')
                {
                    return true;
                }
            }

            return false;
        }

        private bool CommaAndOperatorExists(string inText)
        {
            if( CommaExists(inText) )
            {
                if (OperatorExists(inText))
                {
                    return true;
                }
                
            }
            

            return false;
        }



        private bool OperatorExists(string inText)
        {
            for(int i = 0; i < inText.Length; i++)
            {
                if(IsNumber(inText[i]) == false)
                {
                    //MessageBox.Show("Hello line 366... is not number: " + inText[i]);
                    return true;
                }
            }

            return false;
        }

        private bool LastWasComma(string inText)
        {
            if( inText[ inText.Length - 1 ] == ',' )
            {
                return true;
            }

            return false;
        }

        private bool LastWasOperator(string inText) //Operator: + - x /.  "," är inte en operator
        {
            if ( IsNumber(inText[inText.Length - 1])  == false) //om inte nummer (inkl , ) så är det operator
            {
                return true;
            }

            return false;
        }


        private bool LastWasNumber(string inText) //Last was a Number ("," not included in numbers here)
        {
            if ( IsNumber(inText[inText.Length - 1]) && (LastWasComma(inText) == false) ) //check that its not a ,
            {
                return true;
            }

            return false;
        }

        private bool TwoCommasExist(string inText)
        {
            int counter = 0;

            for(int i = 0; i < inText.Length; i++)
            {
                if(inText[i] == ',')
                {
                    counter++;
                }
            }

            if(counter >= 2)
            {
                return true;
            }

            return false;
        }


    }
}






        //private void label2_Click(object sender, EventArgs e)
        //{

        //}