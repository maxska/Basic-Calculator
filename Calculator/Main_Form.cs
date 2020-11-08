using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Calculator
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();

            ToggleOperatorButtons(false); // needs to be run immediately when the program starts
            CommaButton.Enabled = false; // needs to be run immediately when the program starts
            EqualsButton.Enabled = false;
        }

        // ####################################################################
        // ####################       NUMBER BUTTONS       ####################
        // ####################################################################

        // "B" is short for "Button"
        private void B0_Click(object sender, EventArgs e)
        {
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



        // ####################################################################
        // ###################      OPERATOR BUTTONS      #####################
        // ####################################################################

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




        // ####################################################################
        // #####################      OTHER BUTTONS      ######################
        // ####################################################################

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ioBox.Text = "";
            
            ToggleAllButtons(true);
            ToggleOperatorButtons(false);
            CommaButton.Enabled = false;
            EqualsButton.Enabled = false;
        }

        private void EraseButton_Click(object sender, EventArgs e)
        {
            if(ioBox.Text.Length > 0)
            {
                // Before will be the value before this function was called:
                string before = ioBox.Text; 

                // Creates a string that contains everything as the variable 
                // before contains, except the last char:
                string after = before.Substring(0, before.Length - 1);

                // Updates ioBox to the after-text:
                ioBox.Text = after; 

                if(NumberOfOperators(before) > 1)
                {
                    // Don't proceed
                }
                else if(NumberOfOperators(before) == 1)
                {
                    // If the last char that was removed is an operator (not a number):
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
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            //då vi använder olika räknesätt (addidion/subtraktion/mult/div) 
            //  måste jag namnge variablerna något allmänt:

            // Because the calculator uses addition, subtraction,
            // multiplication and division, I need to name the 
            // variables first and second, instead of something like 
            // numerator and denominator, which would only be a good
            // varaible name in the case of division:
            double first;
            double second;
            // Operator:
            char op; 
            double result;
            string inText;
            string[] numbers;

            // Assigns result to a value so that it can be shown if an exception is thrown:
            result = 0.0;

            inText = ioBox.Text;

            // Gets the operator that is in ioBox
            op = GetNonNumber(inText);

            //metoden Split delar upp en string i en array, 
            // med platsen delad av den char som anges i inparametern, op:

            // The method Split splits a string to an array. It splits the 
            // string where the char op occurs.
            numbers = inText.Split(op);

            try
            {
                first = double.Parse(numbers[0]);
                second = double.Parse(numbers[1]);

                // Checks the value of op, and uses operand to get result:
                if (op == '+')
                    result = first + second;
                else if (op == '-')
                    result = first - second;
                else if (op == 'x')
                    result = first * second;
                else if (op == '/')
                    result = first / second;
                else if (op == '?')
                { 
                    // op is obtained from GetNonNumber, which returns '?' if no 
                    // operand was found: 
                    MessageBox.Show("COULDN'T get Operator...");
                    result = 0.0;
                }
                else
                    MessageBox.Show("ERROR when trying to choose operator...");
            }
            catch
            {
                MessageBox.Show("EXCEPTION: didn't find 2 operands...");
            }

            // Updates the text in ioBox:
            ioBox.Text = $"{result}";
            // Sets .Enabled on all buttons except EqualsButton, to false.
            ToggleAllButtons(false);
        }




        // ####################################################################
        // ##########################    EVENTS    ############################
        // ####################################################################


        /*
         * Checks a few things each time ioBox is updated.
         * It's mostly about making sure that certain buttons shouldn't be able
         * to be clicked during certain condition.
         */
        private void ioBox_TextChanged(object sender, EventArgs e)
        {
            // If ioBox is empty, the user shouln't be able to click on an 
            // operator button or the comma button:
            if (ioBox.Text.Length == 0) 
            {
                ToggleOperatorButtons(false);
                CommaButton.Enabled = false;
                EqualsButton.Enabled = false;

            }
            else if (ioBox.Text.Length == 1)
            {
                ToggleOperatorButtons(true);
                CommaButton.Enabled = true;
            }


            // Checks the following properties only if there's actually 
            // something in the ioBox:
            if (ioBox.Text.Length >= 1)
            {
                // The last charactes is a number AND two commas do not exist 
                // in the ioBox:
                if (LastWasNumber(ioBox.Text) && !TwoCommasExist(ioBox.Text))
                {
                    ToggleOperatorButtons(true);
                    // Then the user should be able to input a comma:
                    CommaButton.Enabled = true;
                }

                if (LastWasOperator(ioBox.Text))
                {
                    // Then the user should not be able to input a comma or
                    // click the calculate button:
                    CommaButton.Enabled = false; 
                    EqualsButton.Enabled = false; 
                }

                if (LastWasComma(ioBox.Text))
                {
                    // Then the user shouldn't be able to click operator
                    // buttons, because another number is needed for that.
                    // Also shouldnät be able to click the calculate button
                    // not insert a comma:
                    ToggleOperatorButtons(false);
                    EqualsButton.Enabled = false;
                    CommaButton.Enabled = false; 
                }

                // The user should always only be able to insert at most one
                // operator:
                if (OperatorExists(ioBox.Text)) 
                {
                    ToggleOperatorButtons(false);
                }

                if (TwoCommasExist(ioBox.Text))
                {
                    // If two commas exist, then the user shouldn't be able
                    // to insert another comma:
                    CommaButton.Enabled = false; 
                }

                // If a ioBox contains a comma, and there isn't both a comma 
                // and an operator, then that means that there is a comma, 
                // but there is no operator, which means that the user has
                // entered (for instance) 123,45:
                if (CommaExists(ioBox.Text) && !CommaAndOperatorExists(ioBox.Text)) 
                {
                    // Then the user shouldn't be able to add another comma:
                    CommaButton.Enabled = false; 
                }

                // If the last character is a comma, then the user shouldn't
                // be able to click the calculate button:
                if (LastWasComma(ioBox.Text) == false)
                {
                    EqualsButton.Enabled = false;
                }

                // If the last character is an operator, then the user 
                // shouldn't be able to click the calculate button:
                if (LastWasOperator(ioBox.Text) == false)
                {
                    EqualsButton.Enabled = false; 
                }

                // If the last character is a number, then the user should be
                // able to click calculate:
                if (LastWasNumber(ioBox.Text) == true)
                {
                    EqualsButton.Enabled = true;
                }

                // If an operator and a comma is in the ioBox:
                if (OperatorExists(ioBox.Text) && CommaExists(ioBox.Text))
                {
                    char op = GetNonNumber(ioBox.Text);
                    string f; // operand 1 (first)
                    string s; // operand 2 (second)

                    string[] textSplit = ioBox.Text.Split(op);

                    try
                    {
                        f = textSplit[0];
                        s = textSplit[1];

                        if (textSplit.Length > 0)
                        {
                            // Checks if there is a comma in the second 
                            // operand (s):
                            if (CommaExists(s))
                            {
                                // Then there's two (of two maximum) commas in
                                // the ioBox:
                                CommaButton.Enabled = false;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("EXCEPTION - Couldn't parse double on line 352 ca...");
                    }
                }
            }
        }




        // ####################################################################
        // ###################        EXTRA FUNCTIONS        ##################
        // ####################################################################


        /*
         * Returns the number of operands
         */
        private int NumberOfOperators(string inText)
        {
            int counter = 0; //räknar antalet operander som hittas i inText.

            for (int i = 0; i < inText.Length; i++)
            {
                if( IsNumber(inText[i]) == false ) //om inte nummer (inte heller ",") så är det en operand:
                {
                    counter++;
                }
            }

            return counter;
        }


        /*
         * Sets .Enabled ofall operator buttons to the value of val (true or 
         * false)
         */
        private void ToggleOperatorButtons(bool val)
        {
            PlusButton.Enabled = val;
            MinusButton.Enabled = val;
            MultButton.Enabled = val;
            DivButton.Enabled = val;
        }


        /*
         * Sets .Enabled of all buttons to the value of val (true or false):
         */
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


        /*
         * Returns the char of the first character that is a non-number and
         * isn't a comma:
         */
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

            return '?';
        }


        /*
         * Returns true if the char that is sent in (c) is a number or a comma
         */
        private bool IsNumber(char c) //including "," (comma)
        {
            if(c == '0' || c == '1' || c == '2' || c == '3' || c == '4' 
            || c == '5' || c == '6' || c == '7' || c == '8' || c == '9' || c == ',')
            {
                return true;
            }

            return false;
        }


        /*
         * Returns true if a comma exists in the string inText
         */
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


        /*
         * Returns true if both comma and an operator is in the string inText
         */
        private bool CommaAndOperatorExists(string inText) 
        {
            if (CommaExists(inText)) // checks so that a comma exists
            {
                if (OperatorExists(inText)) // checks so that an operator exists
                {
                    return true;
                }   
            }

            return false;
        }


        /*
         * Returns true if there is an operator in inText
         */
        private bool OperatorExists(string inText)
        {
            for(int i = 0; i < inText.Length; i++)
            {
                if(IsNumber(inText[i]) == false)
                {
                    return true;
                }
            }

            return false;
        }


        /*
         * Returns true if the last char (the one to the right in inText) is a
         * comma
         */
        private bool LastWasComma(string inText)
        {
            // Checks if the char on the last index in inText is a comma:
            if (inText[ inText.Length - 1 ] == ',') 
            {
                return true;
            }

            return false;
        }


        /*
         * Returns true if the last char is an operator
         */
        private bool LastWasOperator(string inText)
        {
            // Checks if the last char in inText is a non-number, and not a 
            // comma. If it is not a number nor a comma, it's an operator:
            if (IsNumber(inText[inText.Length - 1]) == false)
            {
                return true;
            }

            return false;
        }


        /*
         * Returns true if the last char is a number (0-9). Observe that a
         * comma does not count as a number
         */
        private bool LastWasNumber(string inText) 
        {
            // Checks if the last char in inText is a number and is not a comma:
            if (IsNumber(inText[inText.Length - 1]) && (LastWasComma(inText) == false)) 
            {
                return true;
            }

            return false;
        }


        /*
         * Returns true if there are two commas in inText.
         * Can be used so that CommaButton.Enabled can be set to false in
         * that case.
         */
        private bool TwoCommasExist(string inText)
        {
            // Counter for the number of commas:
            int counter = 0;

            for(int i = 0; i < inText.Length; i++)
            {
                if(inText[i] == ',')
                {
                    counter++;
                }
            }

            // If there are two or more commas:
            if(counter >= 2)
            {
                return true;
            }

            return false;
        }
    }






}
