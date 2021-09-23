using MonsterMaker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterMaker.Terminal.Actions
{
    public class CreateMonster : IAction
    {
        private Monster _monster;

        public CreateMonster()
        {
            _monster = new Monster();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
