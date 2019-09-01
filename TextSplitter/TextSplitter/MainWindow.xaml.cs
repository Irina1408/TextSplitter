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

namespace TextSplitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Parameters parameters;

        public MainWindow()
        {
            InitializeComponent();
            parameters = new Parameters();
            this.DataContext = parameters;
        }


        private void ButtonSplit_OnClick(object sender, RoutedEventArgs e)
        {
            string inputTextString = GetText(inputText);
            if (!string.IsNullOrEmpty(inputTextString) && parameters.SeparateByCount > 0)
            {
                if (parameters.SeparateByParts)
                {
                    resultTexts.Children.Clear();

                    int symbolsInPart = inputTextString.Length/parameters.SeparateByCount;

                    for (int i = 0; i < parameters.SeparateByCount; i++)
                    {
                        string text = i + 1 == parameters.SeparateByCount 
                            ? inputTextString.Substring(symbolsInPart * i) 
                            : inputTextString.Substring(symbolsInPart * i, symbolsInPart);

                        var txt = new RichTextBox()
                        {
                            Document = new FlowDocument(new Paragraph(new Run(text))),
                            MinHeight = 30
                        };

                        var rd = new RowDefinition();
                        resultTexts.RowDefinitions.Add(rd);
                        Grid.SetRow(txt, resultTexts.RowDefinitions.IndexOf(rd));
                        resultTexts.Children.Add(txt);
                    }
                }
            }
        }

        private static string GetText(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string text = textRange.Text;
            return text;
        }
    }
}
