using System;
using System.Collections.Generic;

namespace SamuraiApp.Domain
{
    public class Samurai
    {
        public Samurai()
        {
            Qoutes = new List<Quote>();
            samuraiBattles = new List<SamuraiBattle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Qoutes { get; set; }
        public Clan Clan { get; set; }
        public List<SamuraiBattle> samuraiBattles { get; set; }
        public Horse Horse { get; set; }

    }
}
