namespace KinoBot;


//Response Movie
public class RootMovie
{
    public List<docs> docs { get; set; }
    public int total { get; set; }
    public int limit { get; set; }
    public int page { get; set; }
    public int pages { get; set; }
}
public class genres
{
    public string name { get; set; }
}

public class countries
{
    public string name { get; set; }
}

public class externalId
{
    public string imdb { get; set; }
    public int tmdb { get; set; }
    public string kpHD { get; set; }
}

public class logo
{
    public string? url { get; set; }
}

public class poster
{
    public string? url { get; set; }
    public string? previewUrl { get; set; }
}

public class backdrop
{
    public string? url { get; set; }
    public string? previewUrl { get; set; }
}

public class rating
{
    public float? kp { get; set; }
    public float? imdb { get; set; }
    public float? filmCritics { get; set; }
    public float? russianFilmCritics { get; set; }
    public object? await { get; set; }
}

public class votes
{
    public int? kp { get; set; }
    public int? imdb { get; set; }
    public int? filmCritics { get; set; }
    public int? russianFilmCritics { get; set; }
    public int? await { get; set; }
}

public class docs
{
    public List<object> internalNames { get; set; }
    public string? name { get; set; }
    public string? alternativeName { get; set; }
    public string? enName { get; set; }
    public int? year { get; set; }
    public List<genres> genres { get; set; }
    public List<countries> countries { get; set; }
    public List<object> releaseYears { get; set; }
    public int? id { get; set; }
    public externalId? externalId { get; set; }
    public List<object> names { get; set; }
    public string? type { get; set; }
    public string? description { get; set; }
    public string? shortDescription { get; set; }
    public logo? logo { get; set; }
    public poster? poster { get; set; }
    public backdrop? backdrop { get; set; }
    public rating? rating { get; set; }
    public votes? votes { get; set; }
    public int? movieLength { get; set; }
    public float? internalRating { get; set; }
    public int? internalVotes { get; set; }
    public bool? isSeries { get; set; }
    public bool? ticketsOnSale { get; set; }
    public object? totalSeriesLength { get; set; }
    public object? seriesLength { get; set; }
    public string? ratingMpaa { get; set; }
    public int? ageRating { get; set; }
    public object? top10 { get; set; }
    public object? top250 { get; set; }
    public int? typeNumber { get; set; }
    public object? status { get; set; }
}  


//Response person
public class Doc
{
    public int id { get; set; }
    public string name { get; set; }
    public string enName { get; set; }
    public string photo { get; set; }
    public string sex { get; set; }
    public int growth { get; set; }
    public DateTime? birthday { get; set; }
    public string death { get; set; }
    public int age { get; set; }
}

public class RootPerson
{
    public List<Doc> docs { get; set; }
    public int total { get; set; }
    public int limit { get; set; }
    public int page { get; set; }
    public int pages { get; set; }
}

// RootRandMovie
    public class Audience
    {
        public int count { get; set; }
        public string country { get; set; }
    }

    public class Backdrop
    {
        public string url { get; set; }
        public string previewUrl { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
    }

    public class ExternalId
    {
        public string kpHD { get; set; }
        public string imdb { get; set; }
        public int tmdb { get; set; }
    }

    public class Fees
    {
        public Russia russia { get; set; }
        public Usa usa { get; set; }
        public World world { get; set; }
    }

    public class Genre
    {
        public string name { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public Logo logo { get; set; }
        public string url { get; set; }
    }

    public class Logo
    {
        public string url { get; set; }
        public string previewUrl { get; set; }
    }

    public class Name
    {
        public string name { get; set; }
        public string language { get; set; }
        public string type { get; set; }
    }

    public class Person
    {
        public int id { get; set; }
        public string photo { get; set; }
        public string name { get; set; }
        public string enName { get; set; }
        public string description { get; set; }
        public string profession { get; set; }
        public string enProfession { get; set; }
    }

    public class Poster
    {
        public string url { get; set; }
        public string previewUrl { get; set; }
    }

    public class Premiere
    {
        public object country { get; set; }
        public object cinema { get; set; }
        public object bluray { get; set; }
        public object dvd { get; set; }
        public DateTime? digital { get; set; }
        public DateTime? russia { get; set; }
        public DateTime? world { get; set; }
    }

    public class Rating
    {
        public double kp { get; set; }
        public double imdb { get; set; }
        public double filmCritics { get; set; }
        public double russianFilmCritics { get; set; }
        public object await { get; set; }
    }

public class RootRandMovie
    {
        public int id { get; set; }
        public ExternalId externalId { get; set; }
        public string name { get; set; }
        public string alternativeName { get; set; }
        public string enName { get; set; }
        public List<Name> names { get; set; }
        public string type { get; set; }
        public int typeNumber { get; set; }
        public int year { get; set; }
        public string description { get; set; }
        public string shortDescription { get; set; }
        public string slogan { get; set; }
        public object status { get; set; }
        public Rating rating { get; set; }
        public Votes votes { get; set; }
        public int? movieLength { get; set; }
        public object totalSeriesLength { get; set; }
        public object seriesLength { get; set; }
        public object ratingMpaa { get; set; }
        public int? ageRating { get; set; }
        public Poster poster { get; set; }
        public Backdrop backdrop { get; set; }
        public List<Genre> genres { get; set; }
        public List<Country> countries { get; set; }
        public List<Person> persons { get; set; }
        public Premiere premiere { get; set; }
        public Watchability watchability { get; set; }
        public object top10 { get; set; }
        public int? top250 { get; set; }
        public bool isSeries { get; set; }
        public List<Audience> audience { get; set; }
        public bool ticketsOnSale { get; set; }
        public List<string> lists { get; set; }
        public object networks { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public Fees fees { get; set; }
        public Videos videos { get; set; }
        public Logo logo { get; set; }
    }

    public class Russia
    {
        public int value { get; set; }
        public string currency { get; set; }
    }

    public class Trailer
    {
        public string url { get; set; }
        public string name { get; set; }
        public string site { get; set; }
        public string type { get; set; }
    }

    public class Usa
    {
        public int value { get; set; }
        public string currency { get; set; }
    }

    public class Videos
    {
        public List<Trailer> trailers { get; set; }
    }

    public class Votes
    {
        public float? kp { get; set; }
        public float? imdb { get; set; }
        public float? filmCritics { get; set; }
        public float? russianFilmCritics { get; set; }
        public int await { get; set; }
    }

    public class Watchability
    {
        public List<Item> items { get; set; }
    }

    public class World
    {
        public int value { get; set; }
        public string currency { get; set; }
    }

