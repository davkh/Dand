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
    public class TextBlockWriter : TextWriter
    {
        TextBlock _output = null;

        public TextBlockWriter(TextBlock output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.Text += value.ToString();
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
