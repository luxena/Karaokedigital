using ENTITY;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaokedigital.Models
{
    public class TrackModel
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

		public void MapFromTrack(Track track)
        {
             TrackID = track.TrackID;
             Title = track.Title;
             Author = track.Author;
             Time = track.Time;
             Year = track.Year;
             Genre = track.Genre;
             File = track.File;
             MediaFile = track.MediaFile;
             FilePath = track.FilePath;
             IsFeaturing = track.IsFeaturing;
			 IsSocial = track.IsSocial;
			 IsReservable = track.IsReservable;
             IsReserved = track.IsReserved;
    }

        public Track MapIntoTrack()
        {
            return new Track{
                TrackID = TrackID,
                Title = Title,
                Author = Author,
                Time = Time,
                Year = Year,
                Genre = Genre,
                File = File,
                MediaFile = MediaFile,
                FilePath = FilePath,
                IsFeaturing = IsFeaturing,
				IsSocial = IsSocial,
			    IsReservable = IsReservable,
                IsReserved = IsReserved
            };
        }
    }
}
