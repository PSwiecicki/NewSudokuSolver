using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SudokuSolver.VMWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            FieldsTextboxes = new TextBox[9, 9] { { Field0, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8 },
                                                  { Field9, Field10, Field11, Field12, Field13, Field14, Field15, Field16, Field17 },
                                                  { Field18, Field19, Field20, Field21, Field22, Field23, Field24, Field25, Field26 },
                                                  { Field27, Field28, Field29, Field30, Field31, Field32, Field33, Field34, Field35 },
                                                  { Field36, Field37, Field38, Field39, Field40, Field41, Field42, Field43, Field44 },
                                                  { Field45, Field46, Field47, Field48, Field49, Field50, Field51, Field52, Field53 },
                                                  { Field54, Field55, Field56, Field57, Field58, Field59, Field60, Field61, Field62 },
                                                  { Field63, Field64, Field65, Field66, Field67, Field68, Field69, Field70, Field71 },
                                                  { Field72, Field73, Field74, Field75, Field76, Field77, Field78, Field79, Field80 }};
        }

        TextBox[,] FieldsTextboxes;


        private void FieldPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9]+");
            e.Handled = regex.IsMatch(e.Text);
            var field = sender as TextBox;
            var indexes = GetTexboxIndexes(field);
            var conflictedFields = GetConflictFields(indexes[0], indexes[1]);
            if(conflictedFields.Count > 0)
            {
                foreach(var conflictedField in conflictedFields)
                {
                    conflictedField.Background = Brushes.LightCoral;
                }
            }
        }

        private List<TextBox> GetConflictFields(int row, int column)
        {

            List<TextBox> textBoxes = new List<TextBox>();
            for (int i = 0; i < 9; i++)
            {
                if (i == row)
                    continue;
                if (FieldsTextboxes[i, column].Text == FieldsTextboxes[row, column].Text)
                    textBoxes.Add(FieldsTextboxes[i, column]);
            }
            for (int j = 0; j < 9; j++)
            {
                if (j == column)
                    continue;
                if (FieldsTextboxes[row, j].Text == FieldsTextboxes[row, column].Text)
                    textBoxes.Add(FieldsTextboxes[row, j]);
            }
            int square = row / 3 * 3 + column / 3;
            int squareFirstFieldIndexI = square / 3 * 3;
            int squareFirstFieldIndexJ = (square % 3) * 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int checkingI = squareFirstFieldIndexI + i;
                    int checkingJ = squareFirstFieldIndexJ + j;
                    if (checkingI == row && checkingJ == column)
                        continue;
                    if (FieldsTextboxes[checkingI, checkingJ].Text == FieldsTextboxes[row, column].Text)
                        textBoxes.Add(FieldsTextboxes[checkingI, checkingJ]);
                }
            }
            return textBoxes;   
        }

        private int[] GetTexboxIndexes(TextBox textBox)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (FieldsTextboxes[i, j] == textBox)
                        return new int[] { i, j };
            return null;
        }

        private void solve_button_Click(object sender, RoutedEventArgs e)
        {
             
        }
    }
}
