using PDFReader;

namespace PDFReaderApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PdfReader pdfReader = new PdfReader();
            pdfReader.Read();
        }
    }
}
