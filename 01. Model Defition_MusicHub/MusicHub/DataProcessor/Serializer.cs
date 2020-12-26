namespace MusicHub.DataProcessor
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context.Albums
                .Where(a => a.ProducerId == producerId)
                .Select(a => new
                {
                    AlbumName = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs
                    .Select(s=>new 
                    {
                        SongName = s.Name,
                        Price = s.Price.ToString("F2"),
                        Writer = s.Writer.Name
                    })
                    .OrderByDescending(s=>s.SongName)
                    .ThenBy(s=>s.Writer)
                    .ToArray(),
                    AlbumPrice = a.Price.ToString("F2")
                })
                .OrderByDescending(a=>a.AlbumPrice)
                .ToArray();

            return JsonConvert.SerializeObject(albums,Formatting.Indented);
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new ExportSongDTO
                {
                    SongName = s.Name,
                    Writer = s.Writer.Name,
                    Performer = context.Performers.Select(p=>p.FirstName+" "+p.LastName).FirstOrDefault(),
                     AlbumProducer = s.Album.Producer.Name,
                     Duration = s.Duration.ToString("c")
                })
                .ToArray()
                .OrderBy(s=>s.SongName)
                .ThenBy(s=>s.Writer)
                .ThenBy(s=>s.Performer)
                .ToArray();


            return XMLConverter.Serialize(songs, "Songs");
        }
    }
}