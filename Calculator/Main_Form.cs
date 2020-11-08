using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//##########################################
//Calculator
//##########################################


namespace Calculator
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();

            ToggleOperatorButtons(false); //denna måste köras direkt när programmet startas
            CommaButton.Enabled = false; //denna måste köras direkt när programmet startas
            EqualsButton.Enabled = false;
        }

        //====================================================================
        //=======================   NUMBER BUTTONS     =======================
        //====================================================================

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



        //==========================================================================================
        //=======================   OPERATOR BUTTONS     ===========================================
        //==========================================================================================

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




        //==========================================================================================
        //=======================   OTHER BUTTONS     ===========================================
        //==========================================================================================

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
                string before = ioBox.Text; //skapar en string med värdet före funktionen anropats...

                //skaper en string som innehåller allt som before för, utom sista char:en:
                string after = before.Substring(0, before.Length - 1);  

                ioBox.Text = after; //uppdaterar ioBox till "after"-texten

                if(NumberOfOperators(before) > 1)
                {
                    //dont proceed
                }
                else if(NumberOfOperators(before) == 1)
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
            // med platsen delad av den char som anges i inparametern, op:
            numbers = inText.Split(op);

            try
            {
                //MessageBox.Show("OP ============== " + op);
                //MessageBox.Show("NUMBERS ===== " + numbers[0] + ",,,,, " + numbers[1] );

                first = double.Parse( numbers[0] );
                second = double.Parse( numbers[1] );

                //Vi kollar var op har fått för värde, och beräknar result utifrån operatorn op:
                if (op == '+')
                    result = first + second;
                else if (op == '-')
                    result = first - second;
                else if (op == 'x')
                    result = first * second;
                else if (op == '/')
                    result = first / second;
                else if (op == '?') //op fås från GetNonNumber(), som returnerar '?' om ingen operator hittades:
                {
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

            ioBox.Text = $"{result}"; //upptaterar texten i ioBox.
            ToggleAllButtons(false); //sätter .Enabled på alla buttons förutom EqualsButton, till false.
        }




        //==========================================================================================
        //=======================     EVENTS       ===========================================
        //==========================================================================================

        //denna funktion dubbelkollar en del saker varje gång ioBox uppdateras...
        // Det har främst att göra med att vissa knappar inte ska kunna klickas 
        //  under vissa förhållanden:
        private void ioBox_TextChanged(object sender, EventArgs e)
        {
            //om ioBox är tomt så ska man inte kunna klicka på en operator eller komma-tecken:
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


            //kontrollera enbart följande egenskaper OM det faktiskt finns text i ioBox:
            if (ioBox.Text.Length >= 1)
            {
                //Sista tecket är en siffra OCH två kommatecken existerar EJ:
                if (LastWasNumber(ioBox.Text) && !TwoCommasExist(ioBox.Text))
                {
                    ToggleOperatorButtons(true); 
                    CommaButton.Enabled = true; //då ska man kunna sätta in ","
                }

                if (LastWasOperator(ioBox.Text))
                {
                    CommaButton.Enabled = false; //då ska man INTE kunna sätta in ","
                    EqualsButton.Enabled = false; //då ska man INTE kunna klicka på "="
                }

                if (LastWasComma(ioBox.Text))
                {
                    ToggleOperatorButtons(false); //då ska man INTE kunna sätta klicka på operatorer, behövs en till siffra
                    EqualsButton.Enabled = false; //då ska man INTE kunna klicka på "="
                    CommaButton.Enabled = false; //då ska man INTE kunna sätta in ","
                }

                if (OperatorExists(ioBox.Text)) //man ska alltid bara kunna skriva in högst en operator:
                {
                    ToggleOperatorButtons(false);
                }

                if (TwoCommasExist(ioBox.Text)) //om två st "," existerar:
                {
                    CommaButton.Enabled = false; //då ska man INTE kunna sätta in "," för vi har alltid högst två operander.
                }

                //om ett "," existerar OCH det finns INTE (både) "," och operator 
                // då innebär det att det finns "," men inte finns operator
                //  vilket innebär att användaren skrivit enligt "123,45" exempelvis:
                if (CommaExists(ioBox.Text) && !CommaAndOperatorExists(ioBox.Text)) 
                {
                    CommaButton.Enabled = false; //då ska man ITNE kunna klicka på ","
                }




                if( LastWasComma(ioBox.Text) == false ) //om sista var "," ska man INTE kunna klicka på "="
                {
                    EqualsButton.Enabled = false;

                }

                if (LastWasOperator(ioBox.Text) == false) //om sista var en operator ska man INTE kunna klicka på "="
                {
                    EqualsButton.Enabled = false; 

                }

                if ( LastWasNumber(ioBox.Text) == true) //om sista var en siffra SKA man kunna klicka på "="
                {
                    EqualsButton.Enabled = true;

                }

                
                if (OperatorExists(ioBox.Text) && CommaExists(ioBox.Text)) //om operator existerar och ",":
                {
                    char op = GetNonNumber(ioBox.Text); //hämtar operator 
                    string f; //operand 1 (first)
                    string s; //operand 2 (second)

                    string[] textSplit = ioBox.Text.Split(op);

                    try
                    {
                        f = textSplit[0];
                        s = textSplit[1];

                        if (textSplit.Length > 0)
                        {
                            if (CommaExists(s)) //denna kollar om det finns "," i andra operanden
                            {
                                //då finns två (av maximalt två) kommatecken utplacerat:
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




        //====================================================================
        //=======================   EXTRA FUNCTIONS      =======================
        //====================================================================

        //returnerar antalet operatorer:
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


        //Sätter alla operator-butotns till värdet av val (true/false):
        private void ToggleOperatorButtons(bool val)
        {
            PlusButton.Enabled = val;
            MinusButton.Enabled = val;
            MultButton.Enabled = val;
            DivButton.Enabled = val;
        }

        //Sätter alla operator-butotns till värdet av val (true/false):
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


        //returnerar char:en av första bokstaven som är icke-tal och inte heller är en ",":
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

            //MessageBox.Show(" XYZ ");

            //MessageBox.Show("ERROR: couldn't return char in getNonNumber()... returning '?'...");
            return '?';
        }



        //returnerar true om den char som skickats in (c) är ett tal eller en ",":
        private bool IsNumber(char c) //including "," (comma)
        {
            if(c == '0' || c == '1' || c == '2' || c == '3' || c == '4' 
            || c == '5' || c == '6' || c == '7' || c == '8' || c == '9' || c == ',')
            {
                return true;
            }

            return false;
        }



        //returnerar true om "," finns inuti string:en inText:
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


        //returnerar true om BÅDE "," OCH en operator finns i string:en inText:
        private bool CommaAndOperatorExists(string inText) 
        {
            if( CommaExists(inText) ) //testar först så att "," existerar:
            {
                if (OperatorExists(inText)) //testar sen om operator existerar:
                {
                    return true;
                }   
            }

            return false;
        }


        //returnerar true om det finns en operator i inText:
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


        //Returnerar true om den sista char:en (den längst till höger i ioBox) är en ",":
        private bool LastWasComma(string inText)
        {
            //kollar om på sista plats i inText finns ett ",", returnar true om ja:
            if ( inText[ inText.Length - 1 ] == ',') 
            {
                return true;
            }

            return false;
        }


        //returnerar true om den sista char:en (den längst till höger i ioBox) är en operaotor (+ - x /):
        private bool LastWasOperator(string inText) //Operator: + - x /.  "," är inte en operator
        {
            //kollar om på sista plats i inText finns en ICKE-siffra (inte heller ","):
            if ( IsNumber(inText[inText.Length - 1])  == false) //om inte nummer (inkl , ) så är det operator
            {
                return true;
            }

            return false;
        }


        //returnerar true om den sista char:en (den längst till höger i ioBox) är ett tal (0-9=
        // OBS: "," är inte inkluderat i tal här:
        private bool LastWasNumber(string inText) 
        {
            //kollar om på sista plats i inText är en siffra, som samtidigt INTE är ett kommatecken:
            if ( IsNumber(inText[inText.Length - 1]) && (LastWasComma(inText) == false) ) //check that its not a ,
            {
                return true;//
            }

            return false;
        }


        //returnerar true om två st "," finns ioBox. Om ja så kan CommaButton sättas till .Enabled = false
        private bool TwoCommasExist(string inText)
        {
            int counter = 0; //räknar antalet ","

            for(int i = 0; i < inText.Length; i++)
            {
                if(inText[i] == ',')
                {
                    counter++;
                }
            }

            if(counter >= 2) //om counter är 2 (eller större) returneras true.
            {
                return true;
            }

            return false;
        }
    }






}
