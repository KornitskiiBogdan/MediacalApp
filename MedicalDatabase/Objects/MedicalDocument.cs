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
        private int _width;
        private int _height;

        public MedicalDocument() : base()
        {
            _image = Array.Empty<byte>();
        }

        public MedicalDocument(Int64 id, string name, long date, byte[] image) : base(id, name)
        {
            _image = image;
            _date = date;
        }

        public MedicalDocument(Int64 id, string name, byte[] image) : this(id, name, DateTime.Now.Ticks, image)
        {
        }

        public long Date
        {
            get => _date;
            set => _date = value;
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
