using System.Diagnostics;
using SkiaSharp;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Rendering.Skia;

namespace PDFReader
{
    public record PdfData(SKBitmap Bitmap);
    public class PdfReader
    {
        public static PdfData Read(string filePath)
        {
            using PdfDocument document = PdfDocument.Open(filePath);
            foreach (Page page in document.GetPages())
            {
                    
                var letters = page.Letters;
                // 1. Extract words
                //var wordExtractor = NearestNeighbourWordExtractor.Instance;

                //var words = wordExtractor.GetWords(letters);

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

            return new PdfData(document.GetPageAsSKBitmap(1));
        }

        public static SKBitmap GetBitmapFromPdf(string filePath)
        {
            using PdfDocument document = PdfDocument.Open(filePath);
            document.AddSkiaPageFactory();

            return document.GetPageAsSKBitmap(1);
        }

    }
}
