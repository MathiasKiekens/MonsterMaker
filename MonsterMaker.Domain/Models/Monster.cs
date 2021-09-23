using System;

namespace MonsterMaker.Domain.Models
{
    public class Monster : IEntity, ISoftDelete
    {
        // Generic fields
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public bool Archived { get; set; }

        // Stats
        public int ArmorClass { get; set; }
        public int Hitpoints { get; set; }

        // Calculated fields
        public int ChallengeRating { get; set; }

        public bool Equals(Monster target) =>
            ToString() == target.ToString();

        public override string ToString()
        {
            return $"{Id}{Name}{LastModified}{Archived}{ArmorClass}{Hitpoints}{ChallengeRating}";
        }
    }
}
