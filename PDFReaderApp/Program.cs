using PDFReader;

namespace PDFReaderApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();
            reader.Read();
        }
    }
}
