using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.J.Details.DataLib
{
    public static class Formula1
    {
        private static List<Racer> racers;

        public static IList<Racer> GetChampions()
        {
            if (racers == null)
            {
                racers = new List<Racer>(40);
                racers.Add(new Racer("Nino", "Farina", "Italy", 33, 5, new int[] { 1950 }, new string[] { "Alfa Romeo" }));
                racers.Add(new Racer("Alberto", "Ascari", "Italy", 32, 10, new int[] { 1952, 1953 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Juan Manuel", "Fangio", "Argentina", 51, 24, new int[] { 1951, 1954, 1955, 1956, 1957 }, new string[] { "Alfa Romeo", "Maserati", "Mercedes", "Ferrari" }));
                racers.Add(new Racer("Mike", "Hawthorn", "UK", 45, 3, new int[] { 1958 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Phil", "Hill", "USA", 48, 3, new int[] { 1961 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("John", "Surtees", "UK", 111, 6, new int[] { 1964 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Jim", "Clark", "UK", 72, 25, new int[] { 1963, 1965 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jack", "Brabham", "Australia", 125, 14, new int[] { 1959, 1960, 1966 }, new string[] { "Cooper", "Brabham" }));
                racers.Add(new Racer("Denny", "Hulme", "New Zealand", 112, 8, new int[] { 1967 }, new string[] { "Brabham" }));
                racers.Add(new Racer("Graham", "Hill", "UK", 176, 14, new int[] { 1962, 1968 }, new string[] { "BRM", "Lotus" }));
                racers.Add(new Racer("Jochen", "Rindt", "Austria", 60, 6, new int[] { 1970 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jackie", "Stewart", "UK", 99, 27, new int[] { 1969, 1971, 1973 }, new string[] { "Matra", "Tyrrell" }));
                racers.Add(new Racer("Emerson", "Fittipaldi", "Brazil", 143, 14, new int[] { 1972, 1974 }, new string[] { "Lotus", "McLaren" }));
                racers.Add(new Racer("James", "Hunt", "UK", 91, 10, new int[] { 1976 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Mario", "Andretti", "USA", 128, 12, new int[] { 1978 }, new string[] { "Lotus" }));
                racers.Add(new Racer("Jody", "Scheckter", "South Africa", 112, 10, new int[] { 1979 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Alan", "Jones", "Australia", 115, 12, new int[] { 1980 }, new string[] { "Williams" }));
                racers.Add(new Racer("Keke", "Rosberg", "Finland", 114, 5, new int[] { 1982 }, new string[] { "Williams" }));
                racers.Add(new Racer("Niki", "Lauda", "Austria", 173, 25, new int[] { 1975, 1977, 1984 }, new string[] { "Ferrari", "McLaren" }));
                racers.Add(new Racer("Nelson", "Piquet", "Brazil", 204, 23, new int[] { 1981, 1983, 1987 }, new string[] { "Brabham", "Williams" }));
                racers.Add(new Racer("Ayrton", "Senna", "Brazil", 161, 41, new int[] { 1988, 1990, 1991 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Nigel", "Mansell", "UK", 187, 31, new int[] { 1992 }, new string[] { "Williams" }));
                racers.Add(new Racer("Alain", "Prost", "France", 197, 51, new int[] { 1985, 1986, 1989, 1993 }, new string[] { "McLaren", "Williams" }));
                racers.Add(new Racer("Damon", "Hill", "UK", 114, 22, new int[] { 1996 }, new string[] { "Williams" }));
                racers.Add(new Racer("Jacques", "Villeneuve", "Canada", 165, 11, new int[] { 1997 }, new string[] { "Williams" }));
                racers.Add(new Racer("Mika", "Hakkinen", "Finland", 160, 20, new int[] { 1998, 1999 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Michael", "Schumacher", "Germany", 287, 91, new int[] { 1994, 1995, 2000, 2001, 2002, 2003, 2004 }, new string[] { "Benetton", "Ferrari" }));
                racers.Add(new Racer("Fernando", "Alonso", "Spain", 177, 27, new int[] { 2005, 2006 }, new string[] { "Renault" }));
                racers.Add(new Racer("Kimi", "Räikkönen", "Finland", 148, 17, new int[] { 2007 }, new string[] { "Ferrari" }));
                racers.Add(new Racer("Lewis", "Hamilton", "UK", 90, 17, new int[] { 2008 }, new string[] { "McLaren" }));
                racers.Add(new Racer("Jenson", "Button", "UK", 208, 12, new int[] { 2009 }, new string[] { "Brawn GP" }));
                racers.Add(new Racer("Sebastian", "Vettel", "Germany", 81, 21, new int[] { 2010, 2011 }, new string[] { "Red Bull Racing" }));
            }

            return racers;
        }


        private static List<Team> teams;
        public static IList<Team> GetContructorChampions()
        {
            if (teams == null)
            {
                teams = new List<Team>()
                {
                    new Team("Vanwall", 1958),
                    new Team("Cooper", 1959, 1960),
                    new Team("Ferrari", 1961, 1964, 1975, 1976, 1977, 1979, 1982, 1983, 1999, 2000, 2001, 2002, 2003, 2004, 2007, 2008),
                    new Team("BRM", 1962),
                    new Team("Lotus", 1963, 1965, 1968, 1970, 1972, 1973, 1978),
                    new Team("Brabham", 1966, 1967),
                    new Team("Matra", 1969),
                    new Team("Tyrrell", 1971),
                    new Team("McLaren", 1974, 1984, 1985, 1988, 1989, 1990, 1991, 1998),
                    new Team("Williams", 1980, 1981, 1986, 1987, 1992, 1993, 1994, 1996, 1997),
                    new Team("Benetton", 1995),
                    new Team("Renault", 2005, 2006 ),
                    new Team("Brawn GP", 2009),
                    new Team("Red Bull Racing", 2010, 2011)
                };
            }
            return teams;
        }

        private static List<Championship> championships;
        public static IEnumerable<Championship> GetChampionships()
        {
            if (championships == null)
            {
                championships = new List<Championship>();
                championships.Add(new Championship
                {
                    Year = 1950,
                    First = "Nino Farina",
                    Second = "Juan Manuel Fangio",
                    Third = "Luigi Fagioli"
                });
                championships.Add(new Championship
                {
                    Year = 1951,
                    First = "Juan Manuel Fangio",
                    Second = "Alberto Ascari",
                    Third = "Froilan Gonzalez"
                });
                championships.Add(new Championship
                {
                    Year = 1952,
                    First = "Alberto Ascari",
                    Second = "Nino Farina",
                    Third = "Piero Taruffi"
                });
                championships.Add(new Championship
                {
                    Year = 1953,
                    First = "Alberto Ascari",
                    Second = "Juan Manuel Fangio",
                    Third = "Nino Farina"
                });
                championships.Add(new Championship
                {
                    Year = 1954,
                    First = "Juan Manuel Fangio",
                    Second = "Froilan Gonzalez",
                    Third = "Mike Hawthorn"
                });
                championships.Add(new Championship
                {
                    Year = 1955,
                    First = "Juan Manuel Fangio",
                    Second = "Stirling Moss",
                    Third = "Eugenio Castellotti"
                });
                championships.Add(new Championship
                {
                    Year = 1956,
                    First = "Juan Manuel Fangio",
                    Second = "Stirling Moss",
                    Third = "Peter Collins"
                });
                championships.Add(new Championship
                {
                    Year = 1957,
                    First = "Juan Manuel Fangio",
                    Second = "Stirling Moss",
                    Third = "Luigi Musso"
                });
                championships.Add(new Championship
                {
                    Year = 1958,
                    First = "Mike Hawthorn",
                    Second = "Stirling Moss",
                    Third = "Tony Brooks"
                });
                championships.Add(new Championship
                {
                    Year = 1959,
                    First = "Jack Brabham",
                    Second = "Tony Brooks",
                    Third = "Stirling Moss"
                });
                championships.Add(new Championship
                {
                    Year = 1960,
                    First = "Jack Brabham",
                    Second = "Bruce McLaren",
                    Third = "Stirling Moss"
                });
                championships.Add(new Championship
                {
                    Year = 1961,
                    First = "Phil Hill",
                    Second = "Wolfgang von Trips",
                    Third = "Stirling Moss"
                });
                championships.Add(new Championship
                {
                    Year = 1962,
                    First = "Graham Hill",
                    Second = "Jim Clark",
                    Third = "Bruce McLaren"
                });
                championships.Add(new Championship
                {
                    Year = 1963,
                    First = "Jim Clark",
                    Second = "Graham Hill",
                    Third = "Richie Ginther"
                });
                championships.Add(new Championship
                {
                    Year = 1964,
                    First = "John Surtees",
                    Second = "Graham Hill",
                    Third = "Jim Clark"
                });
                championships.Add(new Championship
                {
                    Year = 1965,
                    First = "Jim Clark",
                    Second = "Graham Hill",
                    Third = "Jackie Stewart"
                });
                championships.Add(new Championship
                {
                    Year = 1966,
                    First = "Jack Brabham",
                    Second = "John Surtees",
                    Third = "Jochen Rindt"
                });
                championships.Add(new Championship
                {
                    Year = 1967,
                    First = "Dennis Hulme",
                    Second = "Jack Brabham",
                    Third = "Jim Clark"
                });
                championships.Add(new Championship
                {
                    Year = 1968,
                    First = "Graham Hill",
                    Second = "Jackie Stewart",
                    Third = "Dennis Hulme"
                });
                championships.Add(new Championship
                {
                    Year = 1969,
                    First = "Jackie Stewart",
                    Second = "Jackie Ickx",
                    Third = "Bruce McLaren"
                });
                championships.Add(new Championship
                {
                    Year = 1970,
                    First = "Jochen Rindt",
                    Second = "Jackie Ickx",
                    Third = "Clay Regazzoni"
                });
                championships.Add(new Championship
                {
                    Year = 1971,
                    First = "Jackie Stewart",
                    Second = "Ronnie Peterson",
                    Third = "Francois Cevert"
                });
                championships.Add(new Championship
                {
                    Year = 1972,
                    First = "Emerson Fittipaldi",
                    Second = "Jackie Stewart",
                    Third = "Dennis Hulme"
                });
                championships.Add(new Championship
                {
                    Year = 1973,
                    First = "Jackie Stewart",
                    Second = "Emerson Fittipaldi",
                    Third = "Ronnie Peterson"
                });
                championships.Add(new Championship
                {
                    Year = 1974,
                    First = "Emerson Fittipaldi",
                    Second = "Clay Regazzoni",
                    Third = "Jody Scheckter"
                });
                championships.Add(new Championship
                {
                    Year = 1975,
                    First = "Niki Lauda",
                    Second = "Emerson Fittipaldi",
                    Third = "Carlos Reutemann"
                });
                championships.Add(new Championship
                {
                    Year = 1976,
                    First = "James Hunt",
                    Second = "Niki Lauda",
                    Third = "Jody Scheckter"
                });
                championships.Add(new Championship
                {
                    Year = 1977,
                    First = "Niki Lauda",
                    Second = "Jody Scheckter",
                    Third = "Mario Andretti"
                });
                championships.Add(new Championship
                {
                    Year = 1978,
                    First = "Mario Andretti",
                    Second = "Ronnie Peterson",
                    Third = "Carlos Reutemann"
                });
                championships.Add(new Championship
                {
                    Year = 1979,
                    First = "Jody Scheckter",
                    Second = "Gilles Villeneuve",
                    Third = "Alan Jones"
                });
                championships.Add(new Championship
                {
                    Year = 1980,
                    First = "Alan Jones",
                    Second = "Nelson Piquet",
                    Third = "Carlos Reutemann"
                });
                championships.Add(new Championship
                {
                    Year = 1981,
                    First = "Nelson Piquet",
                    Second = "Carlos Reutemann",
                    Third = "Alan Jones"
                });
                championships.Add(new Championship
                {
                    Year = 1982,
                    First = "Keke Rosberg",
                    Second = "Didier Pironi",
                    Third = "John Watson"
                });
                championships.Add(new Championship
                {
                    Year = 1983,
                    First = "Nelson Piquet",
                    Second = "Alain Prost",
                    Third = "Rene Arnoux"
                });
                championships.Add(new Championship
                {
                    Year = 1984,
                    First = "Niki Lauda",
                    Second = "Alain Prost",
                    Third = "Elio de Angelis"
                });
                championships.Add(new Championship
                {
                    Year = 1985,
                    First = "Alain Prost",
                    Second = "Michele Alboreto",
                    Third = "Keke Rosberg"
                });
                championships.Add(new Championship
                {
                    Year = 1986,
                    First = "Alain Prost",
                    Second = "Nigel Mansell",
                    Third = "Nelson Piquet"
                });
                championships.Add(new Championship
                {
                    Year = 1987,
                    First = "Nelson Piquet",
                    Second = "Nigel Mansell",
                    Third = "Ayrton Senna"
                });
                championships.Add(new Championship
                {
                    Year = 1988,
                    First = "Ayrton Senna",
                    Second = "Alain Prost",
                    Third = "Gerhard Berger"
                });
                championships.Add(new Championship
                {
                    Year = 1989,
                    First = "Alain Prost",
                    Second = "Ayrton Senna",
                    Third = "Riccardo Patrese"
                });
                championships.Add(new Championship
                {
                    Year = 1990,
                    First = "Ayrton Senna",
                    Second = "Alain Prost",
                    Third = "Nelson Piquet"
                });
                championships.Add(new Championship
                {
                    Year = 1991,
                    First = "Ayrton Senna",
                    Second = "Nigel Mansell",
                    Third = "Riccardo Patrese"
                });
                championships.Add(new Championship
                {
                    Year = 1992,
                    First = "Nigel Mansell",
                    Second = "Riccardo Patrese",
                    Third = "Michael Schumacher"
                });
                championships.Add(new Championship
                {
                    Year = 1993,
                    First = "Alain Prost",
                    Second = "Ayrton Senna",
                    Third = "Damon Hill"
                });
                championships.Add(new Championship
                {
                    Year = 1994,
                    First = "Michael Schumacher",
                    Second = "Damon Hill",
                    Third = "Gerhard Berger"
                });
                championships.Add(new Championship
                {
                    Year = 1995,
                    First = "Michael Schumacher",
                    Second = "Damon Hill",
                    Third = "David Coulthard"
                });
                championships.Add(new Championship
                {
                    Year = 1996,
                    First = "Damon Hill",
                    Second = "Jacques Villeneuve",
                    Third = "Michael Schumacher"
                });
                championships.Add(new Championship
                {
                    Year = 1997,
                    First = "Jacques Villeneuve",
                    Second = "Heinz-Harald Frentzen",
                    Third = "David Coulthard"
                });
                championships.Add(new Championship
                {
                    Year = 1998,
                    First = "Mika Hakkinen",
                    Second = "Michael Schumacher",
                    Third = "David Coulthard"
                });
                championships.Add(new Championship
                {
                    Year = 1999,
                    First = "Mika Hakkinen",
                    Second = "Eddie Irvine",
                    Third = "Heinz-Harald Frentzen"
                });
                championships.Add(new Championship
                {
                    Year = 2000,
                    First = "Michael Schumacher",
                    Second = "Mika Hakkinen",
                    Third = "David Coulthard"
                });
                championships.Add(new Championship
                {
                    Year = 2001,
                    First = "Michael Schumacher",
                    Second = "David Coulthard",
                    Third = "Rubens Barrichello"
                });
                championships.Add(new Championship
                {
                    Year = 2002,
                    First = "Michael Schumacher",
                    Second = "Rubens Barrichello",
                    Third = "Juan Pablo Montoya"
                });
                championships.Add(new Championship
                {
                    Year = 2003,
                    First = "Michael Schumacher",
                    Second = "Kimi Räikkönen",
                    Third = "Juan Pablo Montoya"
                });
                championships.Add(new Championship
                {
                    Year = 2004,
                    First = "Michael Schumacher",
                    Second = "Rubens Barrichello",
                    Third = "Jenson Button"
                });
                championships.Add(new Championship
                {
                    Year = 2005,
                    First = "Fernando Alonso",
                    Second = "Kimi Räikkönen",
                    Third = "Michael Schumacher"
                });
                championships.Add(new Championship
                {
                    Year = 2006,
                    First = "Fernando Alonso",
                    Second = "Michael Schumacher",
                    Third = "Felipe Massa"
                });
                championships.Add(new Championship
                {
                    Year = 2007,
                    First = "Kimi Räikkönen",
                    Second = "Lewis Hamilton",
                    Third = "Fernando Alonso"
                });
                championships.Add(new Championship
                {
                    Year = 2008,
                    First = "Lewis Hamilton",
                    Second = "Felipe Massa",
                    Third = "Kimi Raikkonen"
                });
                championships.Add(new Championship
                {
                    Year = 2009,
                    First = "Jenson Button",
                    Second = "Sebastian Vettel",
                    Third = "Rubens Barrichello"
                });
                championships.Add(new Championship
                {
                    Year = 2010,
                    First = "Sebastian Vettel",
                    Second = "Fernando Alonso",
                    Third = "Mark Webber"
                });
                championships.Add(new Championship
                {
                    Year = 2011,
                    First = "Sebastian Vettel",
                    Second = "Jenson Button",
                    Third = "Mark Webber"
                });
            }
            return championships;
        }


        private static IList<Racer> moreRacers;
        private static IList<Racer> GetMoreRacers()
        {
            if (moreRacers == null)
            {
                moreRacers = new List<Racer>();
                moreRacers.Add(new Racer("Luigi", "Fagioli", "Italy", starts: 7, wins: 1));
                moreRacers.Add(new Racer("Jose Froilan", "Gonzalez", "Argentina", starts: 26, wins: 2));
                moreRacers.Add(new Racer("Piero", "Taruffi", "Italy", starts: 18, wins: 1));
                moreRacers.Add(new Racer("Stirling", "Moss", "UK", starts: 66, wins: 16));
                moreRacers.Add(new Racer("Eugenio", "Castellotti", "Italy", starts: 14, wins: 0));
                moreRacers.Add(new Racer("Peter", "Collins", "UK", starts: 32, wins: 3));
                moreRacers.Add(new Racer("Luigi", "Musso", "Italy", starts: 24, wins: 1));
                moreRacers.Add(new Racer("Tony", "Brooks", "UK", starts: 38, wins: 6));
                moreRacers.Add(new Racer("Bruce", "McLaren", "New Zealand", starts: 100, wins: 4));
                moreRacers.Add(new Racer("Wolfgang von", "Trips", "Germany", starts: 27, wins: 2));
                moreRacers.Add(new Racer("Richie", "Ginther", "USA", starts: 52, wins: 1));
                moreRacers.Add(new Racer("Jackie", "Ickx", "Belgium", starts: 116, wins: 8));
                moreRacers.Add(new Racer("Clay", "Regazzoni", "Switzerland", starts: 132, wins: 5));
                moreRacers.Add(new Racer("Ronnie", "Peterson", "Sweden", starts: 123, wins: 10));
                moreRacers.Add(new Racer("Francois", "Cevert", "France", starts: 46, wins: 1));
                moreRacers.Add(new Racer("Carlos", "Reutemann", "Argentina", starts: 146, wins: 12));
                moreRacers.Add(new Racer("Gilles", "Villeneuve", "Canada", starts: 67, wins: 6));
                moreRacers.Add(new Racer("Didier", "Pironi", "France", starts: 70, wins: 3));
                moreRacers.Add(new Racer("John", "Watson", "UK", starts: 152, wins: 5));
                moreRacers.Add(new Racer("Rene", "Arnoux", "France", starts: 149, wins: 7));
                moreRacers.Add(new Racer("Elio", "de Angelis", "Italy", starts: 108, wins: 2));
                moreRacers.Add(new Racer("Michele", "Alboreto", "Italy", starts: 194, wins: 5));
                moreRacers.Add(new Racer("Gerhard", "Berger", "Austria", starts: 210, wins: 10));
                moreRacers.Add(new Racer("Riccardo", "Patrese", "Italy", starts: 256, wins: 6));
                moreRacers.Add(new Racer("David", "Coulthard", "UK", starts: 246, wins: 13));
                moreRacers.Add(new Racer("Heinz-Harald", "Frentzen", "Germany", starts: 156, wins: 3));
                moreRacers.Add(new Racer("Eddie", "Irvine", "UK", starts: 147, wins: 4));
                moreRacers.Add(new Racer("Rubens", "Barrichello", "Brazil", starts: 322, wins: 11));
                moreRacers.Add(new Racer("Juan Pablo", "Montoya", "Columbia", starts: 94, wins: 7));
                moreRacers.Add(new Racer("Felipe", "Massa", "Brazil", starts: 152, wins: 11));
                moreRacers.Add(new Racer("Mark", "Webber", "Australia", starts: 176, wins: 7));
            }
            return moreRacers;
        }
    }
}
