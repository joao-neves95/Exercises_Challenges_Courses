using System;
using System.Windows;


namespace Calculator_Simplified
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal number1 = 0;
        decimal number2 = 0;
        string operation = "";
        decimal result = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            switch (operation)
            {
                case "+":
                    result = number1 + number2;
                    displayCurrentNumberBox.Text = result.ToString();
                    break;
                case "-":
                    result = number1 - number2;
                    displayCurrentNumberBox.Text = result.ToString();
                    break;
                case "/":
                    result = number1 / number2;
                    displayCurrentNumberBox.Text = result.ToString();
                    break;
                case "*":
                    result = number1 * number2;
                    displayCurrentNumberBox.Text = result.ToString();
                    break;
                default:
                    break;
            }
            operation = "";
        }

        // OPERATORS CLICK EVENT HANDLERS:
        private void HandleOperatorLogic(string Operator)
        {
            operation = Operator;
            displayCurrentNumberBox.Text = "0";
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            HandleOperatorLogic("+");
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            HandleOperatorLogic("-");
        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {
            HandleOperatorLogic("/");
        }

        private void btnTimes_Click(object sender, RoutedEventArgs e)
        {
            HandleOperatorLogic("*");
        }

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                number1 *= -1;
                displayCurrentNumberBox.Text = number1.ToString();
            }
            else
            {
                number2 *= -1;
                displayCurrentNumberBox.Text = number2.ToString();
            }
        }

        // NUMBERS CLICK EVENT HANDLERS:
        private void HandleNumberLogic(int Number)
        {
            if (operation == "")
            {
                number1 = (number1 * 10) + Number;
                displayCurrentNumberBox.Text = number1.ToString();
            }
            else
            {
                number2 = (number2 * 10) + Number;
                displayCurrentNumberBox.Text = number2.ToString();
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(1);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(2);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(3);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(4);
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(5);
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(6);
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(7);
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(8);
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(9);
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            HandleNumberLogic(0);
        }
        // End of NUMBERS CLICK EVENT HANDLERS:

        // CLEAR BUTTONS:
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            number1 = 0;
            number2 = 0;
            operation = "";
            displayCurrentNumberBox.Text = "";
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                number1 = 0;
            }
            else
            {
                number2 = 0;
            }
            displayCurrentNumberBox.Text = "0";
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {

            if (operation == "")
            {
                string number = number1.ToString();
                if (number.Length <= 1)
                {
                    number1 = 0;
                }
                else
                {
                    string removedNum = number.Remove(number.Length - 1, 1);
                    number1 = Convert.ToDecimal(removedNum);
                }
                displayCurrentNumberBox.Text = number1.ToString();
            }
            else
            {
                string number = number2.ToString();
                if (number.Length <= 1)
                {
                    number2 = 0;
                }
                else
                {
                    string removedNum = number.Remove(number.Length - 1, 1);
                    number2 = Convert.ToDecimal(removedNum);
                }
                displayCurrentNumberBox.Text = number2.ToString();
            }
        }
    }
}
