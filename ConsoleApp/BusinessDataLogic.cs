using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
  public  class BusinessDataLogic
    {
        private SamuraiContext _ctx;

        public BusinessDataLogic()
        {
            _ctx = new SamuraiContext();
        }
        public BusinessDataLogic(SamuraiContext _ctx)
        {
            this._ctx = _ctx;
        }

        public int InsertSamurai(string[] samurais)
        {

            var samuraiList = new List<Samurai>();
            foreach (var name in samurais)
            {
                samuraiList.Add(new Samurai() { Name = name });
            }
            _ctx.AddRange(samuraiList);
              var dbresult =  _ctx.SaveChanges();
            return dbresult;

        }
    }
}
