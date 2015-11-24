using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    public class ImageMedia
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public byte[] Buffer { get; set; }

        public ImageMedia(string fileName, string mediaType, byte[] imagebuffer)
        {
            FileName = fileName;
            MediaType = mediaType;
            Buffer = imagebuffer;
        }
        
    }
}
