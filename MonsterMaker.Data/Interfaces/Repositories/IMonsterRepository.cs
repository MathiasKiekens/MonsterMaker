using MonsterMaker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterMaker.Data.Interfaces.Repositories
{
    public interface IMonsterRepository
    {
        Monster CreateMonster(Monster monster);
        List<Monster> ListMonsters();
        Monster GetMonsterById(Guid id);
        void UpdateMonster(Monster monster);
        void DeleteMonster(Guid id);
    }
}
