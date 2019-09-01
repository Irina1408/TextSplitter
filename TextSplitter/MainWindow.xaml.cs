using System;
using System.Collections.Generic;
using System.IO;
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
            // check on general erorrs
            if (parameters.SeparateByCount < 1)
            {
                MessageBox.Show("Count is incorrect.", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // if user want separate text file
            if (parameters.SeparateFile)
            {
                // check on errors
                if (string.IsNullOrEmpty(parameters.FilePath))
                {
                    MessageBox.Show("File path is empty. Please, write file path and try again.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (parameters.SeparateByParts)
                {
                    var fileLines = LoadTextFromFile(parameters.FilePath);

                    if (fileLines == null || fileLines.Count == 0)
                    {
                        MessageBox.Show("File does not exists or empty.", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    WriteToFilesByPartsCount(fileLines, parameters.ResFileAlias, parameters.SeparateByCount, parameters.ResFileExtension);
                }
                else
                {
                    WriteToFilesByLines(parameters.FilePath, parameters.ResFileAlias, parameters.SeparateByCount, parameters.ResFileExtension);
                }
            }
            /*
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
            }*/
            MessageBox.Show("Separating is successfully completed!", "Complete", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private static string GetText(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string text = textRange.Text;
            return text;
        }

        private List<string> LoadTextFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            var fileLines = new List<string>();

            using (var sr = new StreamReader(parameters.FilePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
            }

            return fileLines;
        }

        private void WriteToFilesByPartsCount(List<string> lines, string fileAlias, int partsCount, string resFileExtension)
        {
            int wroteLines = 0;

            for (int iPart = 0; iPart < partsCount; iPart++)
            {
                // create new file name
                string newFileName = fileAlias + (iPart + 1).ToString("D3") + "." + resFileExtension;

                // if file exists, remove it
                if(File.Exists(newFileName))
                    File.Delete(newFileName);

                int iLinesFrom = (int)Math.Floor((double)(lines.Count / partsCount * iPart));
                int linesCount = (int)Math.Floor((double)(lines.Count / partsCount * (iPart + 1))) - iLinesFrom;

                if (iPart + 1 == partsCount)
                    linesCount += lines.Count - wroteLines - linesCount;

                File.WriteAllLines(newFileName, lines.GetRange(iLinesFrom, linesCount));

                wroteLines += linesCount;
            }
        }

        private void WriteToFilesByLines(string filePath, string fileAlias, int linesCount, string resFileExtension)
        {
            if (!File.Exists(filePath)) return;

            var fileLines = new List<string>();

            int iFile = 1;

            using (var sr = new StreamReader(parameters.FilePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    fileLines.Add(line);

                    if (fileLines.Count == linesCount)
                    {
                        // create new file name
                        string newFileName = fileAlias + iFile.ToString("D3") + "." + resFileExtension;

                        // if file exists, remove it
                        if (File.Exists(newFileName))
                            File.Delete(newFileName);

                        File.WriteAllLines(newFileName, fileLines);

                        iFile += 1;
                        fileLines.Clear();
                    }
                }

                if (fileLines.Count > 0)
                {
                    // create new file name
                    string newFileName = fileAlias + iFile.ToString("D3") + "." + resFileExtension;

                    // if file exists, remove it
                    if (File.Exists(newFileName))
                        File.Delete(newFileName);

                    File.WriteAllLines(newFileName, fileLines);

                    fileLines.Clear();
                }
            }
        }
    }
}
