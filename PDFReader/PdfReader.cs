using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using MedicalDatabase;
using MedicalDatabase.Objects;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using UglyToad.PdfPig.Rendering.Skia;
using UglyToad.PdfPig.Util;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia.Controls;

namespace PDFReader
{
    public class PdfReader
    {
        //Алгоритм схожести слов https://habr.com/ru/articles/341148/
        //private readonly Dictionary<string, MedicalMark> _stringToMark;
        private readonly List<string> _markTitle = new List<string>()
        {
            "тест", "исследование"
        };

        private readonly List<string> _valueMarkTitle = new List<string>()
        {
            "результат", "значение"
        };

        private readonly List<string> _unitMarkTitle = new List<string>(){
            "единица измерения", "единицы", "ед. изм."
        };

        public PdfReader(MedicalRepository repository)
        {
            //_stringToMark = repository.Reader.ReadMarks().ToDictionary(x => x.Name);
        }
        public PdfData Read(string filePath)
        {
            using PdfDocument document = PdfDocument.Open(filePath);
            List<MedicalMark> marks = new();
            List<MedicalValue> values = new();
            foreach (Page page in document.GetPages())
            {
                var letters = page.Letters;
                // 1. Extract words
                var wordExtractor = NearestNeighbourWordExtractor.Instance;
                
                int order = 0;
                var words = wordExtractor.GetWords(letters).ToArray();
                //Пдфка с анализами в начале или в конце содержит всякую хрень которая нам не нужна, поэтому нужно проверять
                //и начало и конец на содержание полезной инфы. В зависимости от этого мы будет строить наш поиск
                //Всегда сначала идет шапка с анализами ее и будет искать

                //bool foundInStart = false;
                //bool foundInEnd = false;

                PdfTitle pdfTitle = new PdfTitle();

                for (int i = 0; i < words.Length; i++)
                {
                    var w = words[i];
                    var row = FindRow(words, w, i);
                    if (row is not null)
                    {
                        pdfTitle.AddRow(row);
                    }
                    
                }

                var title = pdfTitle.GetTitle(); 
                for (int i = 0; i < words.Length; i++)
                {

                }

                //Сначала пытаемся найти структуру документы как идет в формате таблицы

                //// 2. Segment page
                //var pageSegmenter = DocstrumBoundingBoxes.Instance;

                //var textBlocks = pageSegmenter.GetBlocks(words);

                //var readingOrder = UnsupervisedReadingOrderDetector.Instance;
                //var orderedTextBlocks = readingOrder.Get(textBlocks);


                //foreach (var text in orderedTextBlocks)
                //{
                //    Console.Write(text.Text);
                //}
                //string pageText = page.Text;

                //foreach (Word word in page.GetWords())
                //{
                //    Console.Write(word.Text);
                //}

                foreach (var letter in letters)
                {
                    Console.Write(letter.Value);
                }
            }

            document.AddSkiaPageFactory();

            return null;
        }

        private RowDocument? FindRow(Word[] words, Word word, int countViewsWords)
        {
            if (string.IsNullOrEmpty(word?.Text) || string.IsNullOrWhiteSpace(word?.Text))
            {
                return null;
            }
            if (_markTitle.Contains(word.Text, StringComparer.OrdinalIgnoreCase))
            {
                return new RowDocument(countViewsWords, Title.Mark);
            }
            else if (_valueMarkTitle.Contains(word.Text, StringComparer.OrdinalIgnoreCase))
            {
                return new RowDocument(countViewsWords, Title.Value);
            }
            else
            {
                if (_unitMarkTitle.Contains(word.Text, StringComparer.OrdinalIgnoreCase))
                {
                    return new RowDocument(countViewsWords, Title.Unit);
                }
                else
                {
                    for (int j = countViewsWords + 1; j < words.Length; j++)
                    {
                        var nextWord = words[j];
                        if (string.IsNullOrEmpty(nextWord.Text) || string.IsNullOrWhiteSpace(nextWord.Text))
                        {
                            continue;
                        }
                        if (countViewsWords < words.Length - 1
                            && _unitMarkTitle.Contains($"{word.Text} {nextWord.Text}", Tools.StringComparer.OrdinalIgnoreCase))
                        {
                            return new RowDocument(j, Title.Unit);
                        }
                    }

                }
            }

            return null;
        }

    }
}
