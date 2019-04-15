using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_react.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using Microsoft.EntityFrameworkCore;

namespace dotnet_react.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        
        private static readonly HttpClient client = new HttpClient();
         
        
        public IEnumerable<Transactions> GetAll()
        {
           
           using (var context = new GNBContext())
            {
                Random random = new Random();  
                
                var result = context.Transactions.Select(cl => new Transactions {Id=random.Next() , Sku = cl.Sku, Currency = cl.Currency, Amount = cl.Amount } ).ToList();

                foreach(Transactions row in result)
                {
                    row.Amount = GetRate(row.Currency,"EUR",row.Amount);
                }
                
                 var summary = result.GroupBy(K => K.Sku).Select(cl => new Transactions {Id=random.Next() , Sku = cl.First().Sku, Currency = "EUR", Amount = cl.Sum(c =>c.Amount) } ).OrderBy(cl => cl.Sku).ToList();
                return summary;
            }
        }

         public IEnumerable<Rates> GetAllRates()
        {
           
           using (var context = new GNBContext())
            {
               
                return context.Rates.ToList();
            }
        }

          public IEnumerable<Transactions> GetAll(string id)
        {
            Random random = new Random();  
           using (var context = new GNBContext())
            {
                var result = context.Transactions.Where(b => b.Sku.Contains(id)).Select(cl => new Transactions {Id=random.Next() , Sku = cl.Sku, Currency = cl.Currency, Amount = cl.Amount }).ToList();
                foreach(Transactions row in result)
                {
                    row.Amount = GetRate(row.Currency,"EUR",row.Amount);

                }
                return result;
            }
        }

        [HttpGet("[action]")]
        public IEnumerable<Transactions> refresh()
        {
           
            return GetAll();
        }

        


         [HttpGet("[action]")]
        public IEnumerable<Rates> rates()
        {
           
            return GetAllRates();
        }

        [HttpGet("[action]")]
        public IEnumerable<Transactions> updatex()
        {   ProcessRates().Wait();
            CompleteRateTable().Wait();
            ProcessTransactions().Wait();
            return GetAll();
        }

        [HttpGet("[action]")]
        public IEnumerable<Rates> updRates()
        {   ProcessRates().Wait();
            CompleteRateTable().Wait();
            return GetAllRates();
        }

        [HttpGet("[action]/{id}")]
        public IEnumerable<Transactions> refresh(string id)
        {
           
            return GetAll(id);
        }

        public async Task ProcessTransactions()
        {   
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var serializer = new DataContractJsonSerializer(typeof(List<TransactionsJSON>));
            var streamTask =  client.GetStreamAsync("http://quiet-stone-2094.herokuapp.com/transactions.json");
            var repositories = serializer.ReadObject(await streamTask) as List<TransactionsJSON>;
            using (var context = new GNBContext())
            {
                foreach( var trans in context.Transactions)
                {
                    context.Remove(trans);
                    
                }
                context.SaveChanges();
                foreach(var trans in repositories)
                {
                    var row = new Transactions{Id=0, Sku=trans.Sku, Currency=trans.Currency, Amount = trans.Amount };
                    context.Add(row);
                }
                context.SaveChanges();
            }
        }

        public async Task ProcessRates()
        {   
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var serializer = new DataContractJsonSerializer(typeof(List<RatesJSON>));
            var streamTask =  client.GetStreamAsync("http://quiet-stone-2094.herokuapp.com/rates.json");
            var repositories = serializer.ReadObject(await streamTask) as List<RatesJSON>;
            using (var context = new GNBContext())
            {
                foreach( var trans in context.Rates)
                {
                    context.Remove(trans);
                    
                }
                context.SaveChanges();
                foreach(var trans in repositories)
                {
                    var row = new Rates{Id=0, FromCurr=trans.from, ToCurr=trans.to, Rate=trans.Rate};
                    context.Add(row);
                }
                context.SaveChanges();
            }
        }

        public decimal  GetRate(string fromcurr, string tocurr, decimal amount)
        {
            if(fromcurr.Trim() == tocurr.Trim())
            {

                return amount;

            } 

            using (var context = new GNBContext())
            {
                Rates qry = context.Rates.Where(b => b.FromCurr.Contains(fromcurr)).Where(b => b.ToCurr.Contains(tocurr)).Select(cl => new Rates {FromCurr=cl.FromCurr, ToCurr=cl.ToCurr, Rate=cl.Rate}).FirstOrDefault();
                decimal result;
                if(qry.Rate>1)
                {
                    return result = amount/qry.Rate ;
                }
                else
                {
                    return result = qry.Rate * amount;
                }
                
            }

        }

        public async Task CompleteRateTable(){
            
            using (var context = new GNBContext())
            {
                 string sqlstring = "SELECT pair.Id, pair.FromCurr, pair.ToCurr, pair.rate "+
                                    "FROM "+
                                    "(    SELECT  f.id Id,f.FromCurr FromCurr , t.FromCurr ToCurr, (f.rate/t.rate) rate "+
                                    "    FROM dbo.rates f, dbo.rates t "+
                                    "    WHERE  "+
                                    "    f.ToCurr = t.ToCurr "+
                                    ") pair "+
                                    "LEFT OUTER JOIN rates "+
                                    "ON  "+
                                    "pair.FromCurr = rates.FromCurr "+
                                    "AND pair.ToCurr = rates.ToCurr "+
                                    "WHERE rates.FromCurr IS NULL "+
                                    "and  pair.rate<>1";
           
            var result = context.Rates.FromSql(sqlstring).ToList();
            foreach(Rates trans in result)
            {
                var row = new Rates{Id=0, FromCurr=trans.FromCurr, ToCurr=trans.ToCurr, Rate=Decimal.Round(trans.Rate,2)};

                context.Add(row);
            }
                 context.SaveChanges();
            }
        }
    
        
    }
}
