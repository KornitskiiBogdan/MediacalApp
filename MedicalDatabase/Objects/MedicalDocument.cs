using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDatabase.Objects
{
    public class MedicalDocument : MedicalNamedElementBase
    {
        private long _date;
        private byte[] _image;
        private Int64 _width;
        private Int64 _height;

        public MedicalDocument() : base()
        {
            _image = Array.Empty<byte>();
        }

        public MedicalDocument(Int64 id, string name, long date, Int64 width, Int64 height, byte[] image) : base(id, name)
        {
            _image = image;
            _width = width;
            _height = height;
            _date = date;
        }

        public MedicalDocument(Int64 id, string name, Int64 width, Int64 height, byte[] image) : this(id, name, DateTime.Now.Ticks, width, height, image)
        {
        }

        public long Date
        {
            get => _date;
            set => _date = value;
        }

        public Int64 Width
        {
            get => _width;
            set => _width = value;
        }

        public Int64 Height
        {
            get => _height;
            set => _height = value;
        }

        public byte[] Image
        {
            get => _image;
            set => _image = value;
        }

        public DateTime GetDateTime()
        {
            return new DateTime(Date);
        }

    }
}
