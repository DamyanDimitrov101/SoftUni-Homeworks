namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ImportDtos;
    using Newtonsoft.Json;
    using ProductShop.XMLTools;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter 
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone 
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong 
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var writersDTOs = JsonConvert.DeserializeObject<WriterDTO[]>(jsonString);

            List<Writer> writers = new List<Writer>();

            foreach (var wr in writersDTOs)
            {
                if (!IsValid(wr))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                Writer writerValid = 
                            new Writer
                            {
                                Name = wr.Name,
                                Pseudonym = wr.Pseudonym,
                            };

                sb.AppendLine(string.Format(SuccessfullyImportedWriter,writerValid.Name));
                writers.Add(writerValid);
            }

            context.Writers.AddRange(writers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {   
            StringBuilder sb = new StringBuilder();

            List<Producer> producers = new List<Producer>();


            var producersDTOs = JsonConvert.DeserializeObject<ProducerDTO[]>(jsonString);

            foreach (var pr in producersDTOs)
            {
                if (!IsValid(pr))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var producerValid = new Producer()
                {
                    Name = pr.Name,
                    Pseudonym = pr.Pseudonym,
                    PhoneNumber = pr.PhoneNumber
                };

                bool IsValidAlbums = true;
                foreach (var alb in pr.AlbumsDTOs)
                {
                    if (!IsValid(alb))
                    {
                        IsValidAlbums = false;
                        break;
                    }

                    DateTime dateTime;
                    bool isValidDateTime = DateTime.TryParseExact(alb.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

                    if (!isValidDateTime)
                    {
                        IsValidAlbums = false;
                        break;
                    }

                    var album = new Album()
                    {
                         Name = alb.Name,
                         ReleaseDate = dateTime
                    };

                    producerValid.Albums.Add(album);

                }

                if (IsValidAlbums==false)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                producers.Add(producerValid);
                sb.AppendLine(
                    producerValid.PhoneNumber!=null 
                    ?
                    string.Format(SuccessfullyImportedProducerWithPhone, producerValid.Name, producerValid.PhoneNumber, producerValid.Albums.Count) 
                    : 
                    string.Format(SuccessfullyImportedProducerWithNoPhone, producerValid.Name, producerValid.Albums.Count));
            }

            context.Producers.AddRange(producers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            List<Song> songs = new List<Song>();

            var songDTOs = XMLConverter.Deserializer<SongDTO>(xmlString, "Songs");


            foreach (var song in songDTOs)
            {
                if (!IsValid(song))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                TimeSpan timeSpan;
                bool isValidDuration = TimeSpan.TryParseExact(song.Duration, "c", CultureInfo.InvariantCulture, out timeSpan);

                if (!isValidDuration)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime dateTime;
                bool isValidCreatedOn = DateTime.TryParseExact(song.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

                if (!isValidCreatedOn)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (song.Price <0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                object genre;
                bool isValidGenre = Enum.TryParse(typeof(Genre), song.Genre, out genre);

                if (!isValidGenre)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Genre genreValid = (Genre)genre;

                Album album = context.Albums.FirstOrDefault(a => a.Id == song.AlbumId);

                if (album==null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var writer = context.Writers.FirstOrDefault(w => w.Id == song.WriterId);

                if (writer==null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Song songValid = new Song()
                {
                    Name = song.Name,
                    Duration = timeSpan,
                    Price = song.Price,
                    Genre = genreValid,
                    CreatedOn = dateTime,
                    Album = album,
                    Writer = writer
                };


                songs.Add(songValid);
                sb.AppendLine(string.Format(SuccessfullyImportedSong, songValid.Name, songValid.Genre, songValid.Duration));
            }

            context.Songs.AddRange(songs);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {

            StringBuilder sb = new StringBuilder();

            List<Performer> performers = new List<Performer>();

            var songPerformerDTOs = XMLConverter.Deserializer<SongPerformerDTO>(xmlString, "Performers");


            foreach (var sp in songPerformerDTOs)
            {
                if (!IsValid(sp))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (sp.NetWorth<0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                Performer songPerformerValid = new Performer()
                {
                     FirstName = sp.FirstName,
                     LastName = sp.LastName,
                     Age = sp.Age,
                     NetWorth = sp.NetWorth
                };

                List<Song> songs = new List<Song>();

                bool isValidSongs = true;
                foreach (var s in sp.Song_PerformerDTOs)
                {
                    if (!IsValid(s))
                    {
                        isValidSongs = false;
                        break;
                    }

                    Song songValid = context.Songs.FirstOrDefault(x => x.Id == s.Id);

                    if (songValid==null)
                    {
                        isValidSongs = false;
                        break;
                    }

                    SongPerformer songPerformer = new SongPerformer()
                    {
                         Performer = songPerformerValid,
                         Song = songValid
                    };

                    songPerformerValid.PerformerSongs.Add(songPerformer);
                }

                if (!isValidSongs)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                performers.Add(songPerformerValid);

                sb.AppendLine(string.Format(SuccessfullyImportedPerformer,songPerformerValid.FirstName, songPerformerValid.PerformerSongs.Count));
            }

            context.Performers.AddRange(performers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }



        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}