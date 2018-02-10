using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// I think I over complicated...
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal currentDisplayNumber = 0;

        private List<decimal> AllNumbers = new List<decimal>();
        private List<string> AllOperators = new List<string>();

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
                    case "+":
                        result += AllNumbers[NumIndex];
                        NumIndex++;
                        break;
                    case "-":
                        result -= AllNumbers[NumIndex];
                        NumIndex++;
                        break;
                    case "/":
                        result /= AllNumbers[NumIndex];
                        NumIndex++;
                        break;
                    case "*":
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
                allCalculation.Append(AllOperators[i]);
            }

            displayAllCalculation.Text = allCalculation.ToString();
        }

        // OPERATORS CLICK EVENT HANDLERS:
        private void HandleOperatorLogic(string Operator)
        {
            AllNumbers.Add(currentDisplayNumber);
            AllOperators.Add(Operator);
            currentDisplayNumber = 0;
            displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
            DisplayAllCalculation();
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
            if (AllNumbers.Count == 0)
            {
                return;
            }
            else if (currentDisplayNumber.ToString().Length == 1)
            {
                currentDisplayNumber = 0;
                displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
            }
            else
            {
                StringBuilder currNum = new StringBuilder();
                currNum.Append(currentDisplayNumber.ToString());
                currNum.Remove(currNum.Length - 1, 1);

                currentDisplayNumber = Convert.ToDecimal(currNum.ToString());
                displayCurrentNumberBox.Text = currentDisplayNumber.ToString();
            }
        }
    }
}
