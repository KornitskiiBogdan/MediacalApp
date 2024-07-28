using MedicalDatabase.Objects;
using SkiaSharp;

namespace PDFReader;

public record PdfData(SKBitmap Bitmap, MedicalMark[] Marks, MedicalValue[] Values);