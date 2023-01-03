using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITY
{
    public class Track
    {
        public int TrackID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Time { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string File { get; set; }
        public IFormFile MediaFile { get; set; }
        public string FilePath { get; set; }
        public bool IsFeaturing { get; set; }
        public bool IsSocial { get; set; }
        public bool IsReservable { get; set; }
        public bool IsReserved { get; set; }
    }
}
