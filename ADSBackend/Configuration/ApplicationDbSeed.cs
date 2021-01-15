using ADSBackend.Data;
using ADSBackend.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ADSBackend.Configuration
{
    public class ApplicationDbSeed
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbSeed(ApplicationDbContext context)
        {
            _context = context;

        }

        public string GetJson(string seedFile)
        {
            var file = System.IO.File.ReadAllText(Path.Combine("Configuration", "SeedData", seedFile));

            return file;
        }

        public void SeedDatabase<TEntity>(string jsonFile, DbSet<TEntity> dbset, bool preserveOrder = false) where TEntity : class
        {
            var records = JsonConvert.DeserializeObject<List<TEntity>>(GetJson(jsonFile));

            if (records?.Count > 0)
            {
                if (!preserveOrder)
                {
                    _context.AddRange(records);
                    _context.SaveChanges();
                }
                else
                {
                    foreach (var record in records)
                    {
                        dbset.Add(record);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public void SeedDatabaseOrUpdate<TEntity>(string jsonFile, DbSet<TEntity> dbset, string matchingProperty) where TEntity : class
        {
            var records = dbset.ToList();
            if (records == null || records.Count == 0)
            {
                SeedDatabase<TEntity>(jsonFile, dbset, true);
            }
            else
            {
                var precords = JsonConvert.DeserializeObject<List<TEntity>>(GetJson(jsonFile));
                foreach (var rec in precords)
                {

                    var p2 = rec.GetType().GetProperty(matchingProperty).GetValue(rec, null);
                    var exists = records.FirstOrDefault(c => c.GetType().GetProperty(matchingProperty).GetValue(c, null).Equals(p2));

                    if (exists == null)
                    {
                        dbset.Add(rec);
                        _context.SaveChanges();
                    }
                }

            }


        }

        public void SeedDatabase()
        {
            SeedDatabaseOrUpdate<Member>("Members.json", _context.Member, "Nickname");
            SeedDatabaseOrUpdate<Category>("Categories.json", _context.Category, "Name");
            SeedDatabaseOrUpdate<Tag>("Tags.json", _context.Tag, "TagName");
        }



    }
}