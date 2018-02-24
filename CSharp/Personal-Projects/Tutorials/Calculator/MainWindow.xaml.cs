using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal currentDisplayNumber = 0;

        private List<decimal> AllNumbers = new List<decimal>();
        private List<Operator> AllOperators = new List<Operator>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private decimal CalculateResult()
        {
            decimal result = AllNumbers[0];
            int NumIndex = 1;

            for (int currentOperator = 0; currentOperator < AllOperators.Count; currentOperator++)
            {
                switch (AllOperators[currentOperator])
                {
                    case Operator.Plus:
                        result += AllNumbers[NumIndex];
                        NumIndex++;
                        break;
                    case Operator.Minus:
                        result -= AllNumbers[NumIndex];
                        NumIndex++;
                        break;
                    case Operator.Divide:
                        result /= AllNumbers[NumIndex];
                        NumIndex++;
                        break;
                    case Operator.Times:
                        result *= AllNumbers[NumIndex];
                        NumIndex++;
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        // EQUAL BUTTON CLICK EVENT LOGIC:
        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            AllNumbers.Add(currentDisplayNumber);
            currentDisplayNumber = CalculateResult();
            displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
            displayAllCalculation.Text = "";
            AllNumbers.Clear();
            AllOperators.Clear();
        }
        private void DisplayAllCalculation()
        {
            StringBuilder allCalculation = new StringBuilder();
            for (int i = 0; i < AllNumbers.Count; i++)
            {
                allCalculation.Append(AllNumbers[i].ToString());
                switch (AllOperators[i])
                {
                    case Operator.Plus:
                        allCalculation.Append("+");
                        break;
                    case Operator.Minus:
                        allCalculation.Append("-");
                        break;
                    case Operator.Divide:
                        allCalculation.Append("/");
                        break;
                    case Operator.Times:
                        allCalculation.Append("*");
                        break;
                    default:
                        break;
                }
            }

            displayAllCalculation.Text = allCalculation.ToString();
        }

        // OPERATORS CLICK EVENT HANDLERS:
        private void HandleOperatorLogic(Operator Operator)
        {
            AllNumbers.Add(currentDisplayNumber);
            AllOperators.Add(Operator);
            currentDisplayNumber = 0;
            displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
            DisplayAllCalculation();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            HandleOperatorLogic(Operator.Plus);
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            HandleOperatorLogic(Operator.Minus);
        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {
            HandleOperatorLogic(Operator.Divide);
        }

        private void btnTimes_Click(object sender, RoutedEventArgs e)
        {
            HandleOperatorLogic(Operator.Times);
        }

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e)
        {
            currentDisplayNumber *= -1;
            displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
        }

        // NUMBERS CLICK EVENT HANDLERS:
        private void HandleNumberLogic(int Number)
        {
            currentDisplayNumber = (currentDisplayNumber * 10) + Number;
            displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
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
            currentDisplayNumber = 0;
            displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
            displayAllCalculation.Text = "";
            AllNumbers.Clear();
            AllOperators.Clear();
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            currentDisplayNumber = 0;
            displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            string currentNumber = currentDisplayNumber.ToString();

            if (currentNumber.Length == 1)
            {
                currentDisplayNumber = 0;
                displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
            }
            else
            {
                string removedNum = currentNumber.Remove(currentNumber.Length - 1, 1);

                currentDisplayNumber = Convert.ToDecimal(removedNum);
                displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
            }
        }
    }
}
