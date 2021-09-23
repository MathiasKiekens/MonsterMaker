using CsvHelper;
using MonsterMaker.Data.Interfaces.Repositories;
using MonsterMaker.Data.Repositories;
using MonsterMaker.Domain.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Xunit;

namespace MonsterMaker.Data.Tests
{
    public class LocalMonsterRepositoryTests : IDisposable
    {
        private readonly IMonsterRepository _monsterRepository;
        private const string TESTFOLDER = "D:\\MonsterMaker\\Test";
        public LocalMonsterRepositoryTests()
        {
            _monsterRepository = new LocalMonsterRepository("D:\\MonsterMaker\\Test");
        }

        public void Dispose()
        {
            File.Delete($"{TESTFOLDER}\\Monsters.csv");
            GC.SuppressFinalize(this);  
        }
        
        [Fact]
        public void ShouldCreateMonster()
        {
            Monster testSubject = new Monster
            {
                Name = "GMF Test",
                LastModified = DateTime.UtcNow,
                Archived = false,
                ArmorClass = 15,
                Hitpoints = 25,
                ChallengeRating = 1
            };

            var created = _monsterRepository.CreateMonster(testSubject);
            var read = _monsterRepository.GetMonsterById(created.Id);

            Assert.True(created.Equals(read));

            _monsterRepository.DeleteMonster(read.Id);
        }

        [Fact]
        public void ShouldAppendSecondEntry()
        {
            Monster testSubject = new Monster
            {
                Name = "GMF Test",
                LastModified = DateTime.UtcNow,
                Archived = false,
                ArmorClass = 15,
                Hitpoints = 25,
                ChallengeRating = 1
            };
            _monsterRepository.CreateMonster(testSubject);

            Monster append = new Monster
            {
                Name = "GMF Test Append",
                LastModified = DateTime.UtcNow,
                Archived = false,
                ArmorClass = 15,
                Hitpoints = 25,
                ChallengeRating = 1
            };
            _monsterRepository.CreateMonster(append);

            var list = _monsterRepository.ListMonsters();
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void ShouldDeleteMonster()
        {
            Monster testSubject = new Monster
            {
                Name = "GMF Delete",
                LastModified = DateTime.UtcNow,
                Archived = false,
                ArmorClass = 10,
                Hitpoints = 50,
                ChallengeRating = 3
            };

            var created = _monsterRepository.CreateMonster(testSubject);

            _monsterRepository.DeleteMonster(created.Id);

            var all = _monsterRepository.ListMonsters();
            var deleted = all.SingleOrDefault(x => x.Id == created.Id);

            Assert.Null(deleted);
        }

        [Fact]
        public void ShouldSoftDeleteMonster()
        {
            Monster testSubject = new Monster
            {
                Name = "GMF Delete",
                LastModified = DateTime.UtcNow,
                Archived = false,
                ArmorClass = 10,
                Hitpoints = 50,
                ChallengeRating = 3
            };

            var created = _monsterRepository.CreateMonster(testSubject);

            _monsterRepository.DeleteMonster(created.Id);

            var all = _monsterRepository.ListMonsters();
            var deleted = all.SingleOrDefault(x => x.Id == created.Id);

            Assert.Null(deleted);

            using (var reader = new StreamReader($"{TESTFOLDER}\\Monsters.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var amountOfRecords = csv.GetRecords<Monster>().Count();
                Assert.Equal(1, amountOfRecords);
            }
        }
    }
}
