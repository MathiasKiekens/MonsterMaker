using CsvHelper;
using CsvHelper.Configuration;
using MonsterMaker.Data.Interfaces.Repositories;
using MonsterMaker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MonsterMaker.Data.Repositories
{
    public class LocalMonsterRepository : LocalBaseRepository<Monster>, IMonsterRepository
    {
        public LocalMonsterRepository(string localFolder = null) : base(localFolder)
        { }

        public Monster CreateMonster(Monster monster) => Create(monster);

        public Monster GetMonsterById(Guid id) => GetById(id);

        public List<Monster> ListMonsters() => List().Where(x => !x.Archived).ToList();

        public void UpdateMonster(Monster monster) => Update(monster);
        public void DeleteMonster(Guid id) => Delete(id);
    }
}
