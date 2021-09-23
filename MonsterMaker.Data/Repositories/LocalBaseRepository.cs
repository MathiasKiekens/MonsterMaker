using CsvHelper;
using CsvHelper.Configuration;
using MonsterMaker.Domain;
using MonsterMaker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MonsterMaker.Data.Repositories
{
    public class LocalBaseRepository<T> where T : IEntity
    {
        private readonly string _filePath;
        private readonly CsvConfiguration _csvConfig;

        public LocalBaseRepository(string localFolder = null)
        {
            if (string.IsNullOrEmpty(localFolder))
                localFolder = "D:\\MonsterMaker\\Storage";
            _filePath = $"{localFolder}\\{typeof(T).Name}s.csv";
            Directory.CreateDirectory(localFolder);
            _csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        }

        protected T Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.Id = Guid.NewGuid();
            entity.LastModified = DateTime.UtcNow;

            if (!File.Exists(_filePath))
            {
                using (var writer = File.CreateText(_filePath))
                using (var csv = new CsvWriter(writer, _csvConfig))
                {
                    csv.WriteHeader<T>();
                    csv.NextRecord();
                    csv.WriteRecord(entity);
                }
            }
            else
            {
                var records = List();
                records.Add(entity);

                using (var writer = File.CreateText(_filePath))
                using (var csv = new CsvWriter(writer, _csvConfig))
                {
                    csv.WriteRecords(records);
                }
            }

            return entity;
        }

        protected List<T> List()
        {
            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, _csvConfig))
                return csv.GetRecords<T>().ToList();
        }

        protected T GetById(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));

            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, _csvConfig))
                return csv.GetRecords<T>().SingleOrDefault(x => x.Id == id);
        }

        protected void Update(T entity)
        {
            if (entity.Id == default)
                throw new ArgumentNullException(nameof(entity.Id));


            entity.LastModified = DateTime.UtcNow;
            var entities = List();
            var newList = entities.Select(x => x.Id == entity.Id ? entity : x);

            using (var writer = File.CreateText(_filePath))
            using (var csv = new CsvWriter(writer, _csvConfig))
                csv.WriteRecords(newList);
        }

        protected void Delete(Guid id)
        {
            if (id == default)
                throw new ArgumentNullException(nameof(id));
            var item = GetById(id);
            if (item is ISoftDelete delete)
            {
                delete.Archived = true;
                Update((T) delete);
            }
            else
            {
                var entities = List();
                var newList = entities.Where(x => x.Id != id);

                using (var writer = File.CreateText(_filePath))
                using (var csv = new CsvWriter(writer, _csvConfig))
                    csv.WriteRecords(newList);
            }
        }
    }
}
