using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
       static SamuraiContext _ctx = new SamuraiContext();
        public Program()
        {
            
        }
        static void Main(string[] args)
        {
            //AddSamurai();
            //InsertMutipleSamurai();
            //RetrieveAndUpdate();
            //RetrieveAndDelete();
            //InsertBattles();
            //GetSamurais();
            //InsertNewSamuraiWithQuote();
            // AddQuoteToExistingSamuraiWhileTracked(3);
            //AddQouteToExistingSamuraiNotTracked(3, "a legendary  quote");
            //EagerLoadingSamuraiWithQuoutes();
            //ProjectSamuraisWithQuotes();
            //ExplicitLoadQuotes();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();
            //JoinBattleAndSamurai();
            // AddNewHorseToSamuraiUsingId();
            //AddNewHorseToSamuraiObject();
            //ReplaceAHorse();
            //QuerySamuraiBattleStats();
            //QueryUsingRawSql();
            //QueryUsingFromRawSqlStoreProc();

        }
        
        #region addsamurai

        private static void AddSamurai()
        {
            var samurai = new Samurai()
            {
                Name = "Mattew"
            };
            _ctx.Add(samurai);
            _ctx.SaveChanges();


        }
        #endregion
        #region insertSamurai
        private static void GetSamurais()
        {
            //var samurai = _ctx.Samurais.FirstOrDefault(s => s.Id == 1);
            //Console.WriteLine(samurai.Name);
            var samurai = _ctx.Samurais.ToList(); //when use to list you load everyting in memory first
            foreach (var item in samurai)
            {
                Console.Write($"{item.Name} ");
            }
        }

        #endregion
        #region insertMultipleSamurai
        private static void InsertMutipleSamurai()
        {
            var samurai = new Samurai { Name = "Valeriu" };
            var  samurai2 = new Samurai { Name = "Marius" };

            _ctx.Samurais.AddRange(samurai, samurai2);
            _ctx.SaveChanges();

        }
        #endregion

        
        private static void RetrieveAndUpdate()
        {
            var samurais = _ctx.Samurais.Take(2).ToList();
            samurais.ForEach(s => s.Name += "The Samurai");
            _ctx.SaveChanges();

        }
        private static void RetrieveAndDelete()
        {
            var samurai =_ctx.Samurais.Where(s => s.Name.Contains("The Samurai")).ToList();
            _ctx.Samurais.RemoveRange(samurai);
            _ctx.SaveChanges();
        }
        private static void InsertBattles()
        {
            _ctx.Battles.Add(new Battle
            {
                Name = "The short battle",
                StartDate = new DateTime(2010, 01, 01),
                EndDate = new DateTime(2012, 02, 02),

            });
            _ctx.SaveChanges();
        }
        private static void InsertNewSamuraiWithQuote()
        {
            var samurai = new Samurai()
            {
                Name = "Jack",
                Qoutes = new List<Quote>() { new Quote { Text = "What a Quote" } },       
            };
            _ctx.Samurais.Add(samurai);
            _ctx.SaveChanges();
        }
        private static void AddQuoteToExistingSamuraiWhileTracked(int samuraiId)
        {
            var samurai = _ctx.Samurais.Find(samuraiId);
            
                if(samurai !=null)
            {
                samurai.Name = "Leonard";
                _ctx.Samurais.Update(samurai);
                _ctx.SaveChanges();
            }
        }
        private static void AddQouteToExistingSamuraiNotTracked(int samuraiId, string text)
        {
            var samurai = _ctx.Samurais.Find(samuraiId);
            if(samurai !=null)
            {
                var qoute = new Quote
                {
                    SamuraiId = samuraiId,
                    Text = text
                };

            using(var ctx = new SamuraiContext())
                {
                    ctx.Quotes.Update(qoute);
                    ctx.SaveChanges();

                }
            }
                
                
            
        }
        private static void EagerLoadingSamuraiWithQuoutes()
        {
            var samurai = _ctx.Samurais.Include(s => s.Qoutes).ToList();

        }
        private static void ProjectSamuraisWithQuotes()
        {
            var samuraiWithQoute = _ctx.Samurais.Select(s => new { name = s.Name, qoute = s.Qoutes.Where( q => q.Text.Contains("a"))}).ToList();

        }
        private static void ExplicitLoadQuotes()
        {
            var samurai = _ctx.Samurais.FirstOrDefault(s => s.Name.Contains("Jack"));
            _ctx.Entry(samurai).Collection(s => s.Qoutes).Load();
            _ctx.Entry(samurai).Reference(s => s.Clan).Load();
        }
        private static void FilteringWithRelatedData()
        {
            var samurai = _ctx.Samurais.Where(s => s.Qoutes.Any(q => q.Text.Contains("a")));
        }
        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _ctx.Samurais.Include(s => s.Qoutes).FirstOrDefault(s => s.Id == 3);
              var qoute = samurai.Qoutes[0];
            qoute.Text = "Life is full of joy";
            _ctx.SaveChanges();
        }
        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _ctx.Samurais.Include(s => s.Qoutes).FirstOrDefault(s => s.Id == 3);
            var qoute = samurai.Qoutes[0];
            qoute.Text = "Life is full of joy and meaning";
            using (var newCtx = new SamuraiContext())
            {
                newCtx.Entry(qoute).State = EntityState.Modified;
                newCtx.SaveChanges();
            }
        }
        private static void JoinBattleAndSamurai()
        {
            var join = new SamuraiBattle { SamuraiId = 3, BattleId = 1 };
            _ctx.Add(join);
            _ctx.SaveChanges();
        }
        private static void RemoveJoinBattleSamurai()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 3 };
            _ctx.Remove(join);
            _ctx.SaveChanges();
        }
        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattle = _ctx.Samurais
                .Include(s => s.samuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(samurai => samurai.Id == 3);

            var samuraiWithBattleCleaner = _ctx.Samurais.Where(s => s.Id == 3)
                .Select(s => new
                {
                    Samurai = s,
                    Battles = s.samuraiBattles.Select(s => s.Battle)
                })
                .FirstOrDefault();
        }
        private static void AddNewHorseToSamuraiUsingId()
        {
            var horse = new Horse() { Name = "Peter", SamuraiId = 3 };
            _ctx.Add(horse);
            _ctx.SaveChanges();
        }
        private static void AddNewHorseToSamuraiObject()
        {
            var samurai = _ctx.Samurais.Find(4);
            samurai.Horse = new Horse() { Name = "John" };
            _ctx.SaveChanges();
        }
        private static void ReplaceAHorse()
        {
            var samurai = _ctx.Samurais.Include(a => a.Horse).FirstOrDefault(s => s.Id == 4);
            samurai.Horse = new Horse() { Name = "Andrei" };
            _ctx.SaveChanges();
        }
        private static void GetSamuraiWithHorse()
        {
            var samurai = _ctx.Samurais.Include(h => h.Horse).ToList();
        }
        private static void GetHorseWithSamurai()
        {
            var hourseWithSamurai = _ctx.Samurais
                .Where(s => s.Horse != null)
                .Select(s => new { Hourse = s.Horse, Samurai = s })
                .ToList();
        }
        private static void GetClanWithSamurai()
        {
            var samuraiforClan = _ctx.Samurais.Where(s => s.Clan.Id == 3).ToList();
        }
        private static void QuerySamuraiBattleStats()
        {
            var stats = _ctx.SamuraiBattlesStats.ToList();
        }
        private static void QueryUsingRawSql()
        {
            var name = "John";
            var samurai = _ctx.Samurais.FromSqlRaw("select * from samurais").ToList();

            var samurais = _ctx.Samurais.FromSqlInterpolated($"select * from samurais where Name = {name}");

            var samurais2 = _ctx.Samurais.FromSqlRaw("select id, name , clanId from samurais").Include(s => s.Qoutes).ToList();
        }
        private static void QueryUsingFromRawSqlStoreProc()
        {
            var text = "life";

            var samurais = _ctx.Samurais.FromSqlRaw($"EXEC dbo.SamuraisWhoSaidAWord {text}").ToList();
        }
        private static void ExecuteSomeRawSQL()
        {
            var samuraiid = 1002;
            var x = _ctx.Database.ExecuteSqlRaw($"Exec deletequotesforsamurai {0}", samuraiid);
        }

    }
}
