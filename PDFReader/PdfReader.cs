using System.Diagnostics;
using SkiaSharp;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Rendering.Skia;

namespace PDFReader
{
    public class PdfReader
    {
        const string folder = @"C:\Users\bkornitsky\Downloads\";
        const string file = "mark4.pdf";

        public void Read()
        {
            string filePath = Path.Combine(folder, file);
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
        }

        public SKBitmap GetBitmapFromPdf()
        {
            try
            {
                using (PdfDocument document = PdfDocument.Open(Path.Combine(folder, file)))
                {
                    document.AddSkiaPageFactory();

                    return document.GetPageAsSKBitmap(1);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new SKBitmap();
            }
        }

        public SKBitmap GetBitmapFromPdf(string filePath)
        {
            using (PdfDocument document = PdfDocument.Open(filePath))
            {
                return document.GetPageAsSKBitmap(0);
            }
        }

    }
}
