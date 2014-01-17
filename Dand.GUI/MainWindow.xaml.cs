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
using System.IO;

using Dand.Analyzing;
using Dand.RunTime;

namespace Dand.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.SetError(new TextBlockWriter(ErrorCode));
            //Console.SetOut(new TextBlockWriter(ErrorCode));
        }

        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            Scanner scn = null;
            Parser parser = null;
            MainProgram program = null;
            ErrorCode.Text = "";

            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(MainCode.Text);
                MemoryStream stream = new MemoryStream(byteArray);
                scn = new Scanner(stream);

                parser = new Parser(scn);
                if (parser.Parse())
                {
                    program = parser.program;

                    if (program != null)
                    {
                        TranslatedCode.Text = program.Translate();
                    }
                }
            }
            catch (Exception ee)
            {
                Console.Error.WriteLine(ee.Message);
            }
            finally
            {
                SymbolTable.GetInstance.Clear();
            }

            if (ErrorCode.Text != "")
                ErrorCode.Background = new SolidColorBrush(Colors.Red);
            else
            {
                ErrorCode.Text = "No Error";
                ErrorCode.Background = new SolidColorBrush(Colors.Green);
            }
        }
    }
}
