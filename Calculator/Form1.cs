using System;
using System.Collections;
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
    public partial class Calculator : Form
    {

        private bool finalCalc = false; //tracks when equal sign is pressed, used to make sure label is blank after operation
        private int equalOnce = 0;      //helps with flow of control, to prevent the equal sign from being processed twice
        private bool historyPopped = false;     //keeps track whether or not calculator history listbox is active
        private string tempHistory = null;
        private string lastOperation;
        private bool equalPressed = false;

        public Calculator()
        {
            InitializeComponent();
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (display.Text.Equals("0") || equalPressed==true)
            {
                display.Text = "";
                equalPressed = false;
            }

            //ensures that only one decimal can be placed within a number
            if (!(display.Text.Contains(".") && button.Text.Equals("."))){
                display.Text = display.Text + button.Text;

                switch (button.Text) { 
                    case "0":
                        tempHistory = tempHistory + "0";
                        break;
                    case "1":
                        tempHistory = tempHistory + "1";
                        break;
                    case "2":
                        tempHistory = tempHistory + "2";
                        break;
                    case "3":
                        tempHistory = tempHistory + "3";
                        break;
                    case "4":
                        tempHistory = tempHistory + "4";
                        break;
                    case "5":
                        tempHistory = tempHistory + "5";
                        break;
                    case "6":
                        tempHistory = tempHistory + "6";
                        break;
                    case "7":
                        tempHistory = tempHistory + "7";
                        break;
                    case "8":
                        tempHistory = tempHistory + "8";
                        break;
                    case "9":
                        tempHistory = tempHistory + "9";
                        break;
                    case ".":
                        tempHistory = tempHistory + ".";
                        break;
                    default:
                        break;
                
                }
            }
            this.ActiveControl = null;
        }

        
        private void btnHistory_Click(object sender, EventArgs e) 
        {
            if (!historyPopped)
            {
                this.ClientSize = new Size(415, 269);
            }
            else {
                this.ClientSize = new Size(234, 269); 
            }
            //toggles flag on button click
            if (historyPopped) historyPopped = false;
            else historyPopped = true;
            this.ActiveControl = null;
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            
            //ensures backspace clears data properly in display
            if (display.TextLength <= 1)
                display.Text = "0";
            else if (display.TextLength > 0)
                display.Text = display.Text.Substring(0, display.TextLength - 1);

            this.ActiveControl = null;
        }

        private void btnAllClear_Click(object sender, EventArgs e)
        {
            display.Text = "0";
            label1.Text = " ";
            equalOnce = 0;
            this.ActiveControl = null;
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            tempHistory = tempHistory + " * ";
            finalCalc = false;
            equalOnce = 0;
            calculate(button.Text);
            this.ActiveControl = null;
        }
        private void btnDivide_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            tempHistory = tempHistory + " / ";
            finalCalc = false;
            equalOnce = 0;
            calculate(button.Text);
            this.ActiveControl = null;
        }
        private void btnSubtract_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            tempHistory = tempHistory + " - ";
            finalCalc = false;
            equalOnce = 0;
            calculate(button.Text);
            this.ActiveControl = null;
        }
        private void btnAddition_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            tempHistory = tempHistory + " + ";
            finalCalc = false;
            equalOnce = 0;
            calculate(button.Text);
            this.ActiveControl = null;
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            finalCalc = true;
            calculate(button.Text);
            if (equalOnce == 1) {
                tempHistory = tempHistory + " = " + display.Text;
                listBox1.Items.Add(string.Join(Environment.NewLine, tempHistory));
                tempHistory = null;
            }
            equalPressed = true; 
            this.ActiveControl = null;
        }

        private void calculate(string operation) 
        {

            float? labelNum = null;     //holder for number in label after parse

            if (!label1.Text.Equals(" "))
            {
                //splits the label string using a space delimiter
                //ex: label string = 56 + --> label[0] ='56' label[1] ='+'
                string[] sep = { " " };
                string[] label = label1.Text.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                
                //converts display string into a float
                float displayNum = float.Parse(display.Text);
                labelNum = float.Parse(label[0]);

                string tempOp = operation;
                
                if (lastOperation != null && operation!="=")
                    operation = lastOperation;
                

                //depending on which button was pressed in terms of calculator operations
                switch (operation) { 
            
                case "*":
                    
                    float product = (float)labelNum * displayNum;
                    
                    //if = sign was pressed
                    //else
                    if (finalCalc)
                    {
                        display.Text = product.ToString();
                        label1.Text = " ";
                        lastOperation = "=";
                        break;
                    }
                    else 
                    { 
                        label1.Text = product.ToString() + " " + tempOp;
                        lastOperation = tempOp;
                    }

                    display.Text = "0";
                    break;

                case "/":
                    
                    float quotient = (float)labelNum / displayNum;

                    if (finalCalc)
                    {
                        display.Text = quotient.ToString();
                        lastOperation = "=";
                        label1.Text = " ";
                        break;
                    }
                    else 
                    {
                        label1.Text = quotient.ToString() + " " + tempOp;
                        lastOperation = tempOp;
                    }
                        

                    display.Text = "0";
                    break;

                case "%":

                    break;

                case "-":

                    float difference = (float)labelNum - displayNum;

                    if (finalCalc)
                    {
                        display.Text = difference.ToString();
                        lastOperation = "=";
                        label1.Text = " ";
                        break;
                    }
                    else 
                    {
                        label1.Text = difference.ToString() + " " + tempOp;
                        lastOperation = tempOp;
                    }
                       
                    display.Text = "0";
                    break;

                case "+":

                    float sum = (float)labelNum + displayNum;

                    if (finalCalc)
                    {
                        display.Text = sum.ToString();
                        lastOperation = "=";
                        label1.Text = " ";
                        break;
                    }
                    else 
                    {
                        label1.Text = sum.ToString() + " " + tempOp;
                        lastOperation = tempOp;
                    }
                        
                    display.Text = "0";
                    break;


                case "=":

                    if (equalOnce == 0)
                    {
                        equalOnce++;
                        calculate(label[1]);
                    }else
                    {
                        equalOnce++;
                    }

                    break;
            
                }
            }
            else
            {
                if (!operation.Equals("="))
                {
                    label1.Text = display.Text + " " + operation;
                    lastOperation = operation;
                    display.Text = "0";
                }
                else
                { 
                    equalOnce++;
                }
                 
            }         
            
        }

        


    }
}
