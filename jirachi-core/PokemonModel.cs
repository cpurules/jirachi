using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class PokemonModel {
        // Constructor for Gen 1 Party Data
        public PokemonModel(int nationalDexNumber, string nickname, int level, int xp, int currentHP, List<MoveModel> moveset, StatusType status, int otID, PokemonLocation location, List<StatModel> stats) {
            Generation = 1;
            this.NationalDexNumber = nationalDexNumber;
            this.Nickname = nickname;
            this.Level = level;
            this.XP = xp;
            this.CurrentHP = currentHP;
            this.Moveset = moveset;
            this.HeldItem = null;
            this.Status = status;
            this.Pokerus = null;
            this.Personality = 0;
            this.OTID = otID;
            this.Language = PokemonLanguage.English;
            this.Ability = 0;
            this.Location = location;
            this.Stats = stats;
        }

        // Constructor for Gen 1 Box Data - coming soon...

        public PokemonModel(int nationalDexNumber, string nickname, int level, int xP, int currentHP, List<MoveModel> moveset, ItemModel heldItem, StatusType status, PokerusModel pokerus, int personality, int oTID, PokemonLanguage language, int ability, PokemonLocation location) {
            NationalDexNumber = nationalDexNumber;
            Nickname = nickname;
            Level = level;
            XP = xP;
            CurrentHP = currentHP;
            Moveset = moveset;
            HeldItem = heldItem;
            Status = status;
            Pokerus = pokerus;
            Personality = personality;
            OTID = oTID;
            Language = language;
            Ability = ability;
            Location = location;
        }

        public int Generation { get; set; }
        /// <summary>
        /// Represents the national dex number of this Pokemon.
        /// </summary>
        public int NationalDexNumber { get; set; }
        /// <summary>
        /// Represents this Pokemon's nickname
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// Represents this Pokemon's current (actual) level
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Represents this Pokemon's current XP earned
        /// </summary>
        public int XP { get; set; }
        /// <summary>
        /// Represents this Pokemon's current HP
        /// </summary>
        public int CurrentHP { get; set; }
        /// <summary>
        /// Represents this Pokemon's moveset
        /// </summary>
        public List<MoveModel> Moveset { get; set; }
        /// <summary>
        /// Represents this Pokemon's held item
        /// </summary>
        public ItemModel HeldItem { get; set; }
        /// <summary>
        /// Represents the non-volatile (i.e. out-of-battle) status condition of this Pokemon
        /// </summary>
        public StatusType Status { get; set; }
        /// <summary>
        /// Represents this Pokemon's Pokerus infection
        /// </summary>
        public PokerusModel Pokerus { get; set; }
        /// <summary>
        /// Represents this Pokemon's personality
        /// </summary>
        public int Personality { get; set; }
        /// <summary>
        /// Represents this Pokemon's OT ID
        /// </summary>
        public int OTID { get; set; }
        /// <summary>
        /// Represents the language of the game this Pokemon came from
        /// </summary>
        public PokemonLanguage Language { get; set; }
        /// <summary>
        /// Represents this Pokemon's ability
        /// </summary>
        public int Ability { get; set; }
        /// <summary>
        /// Represents where this Pokemon lives
        /// </summary>
        public PokemonLocation Location { get; set; }
        /// <summary>
        /// Represents this Pokemon's stats
        /// </summary>
        public List<StatModel> Stats { get; set; }

        public override bool Equals(object obj) {
            return obj is PokemonModel model &&
                   Generation == model.Generation &&
                   NationalDexNumber == model.NationalDexNumber &&
                   Nickname == model.Nickname &&
                   Level == model.Level &&
                   XP == model.XP &&
                   CurrentHP == model.CurrentHP &&
                   EqualityComparer<List<MoveModel>>.Default.Equals(Moveset, model.Moveset) &&
                   EqualityComparer<ItemModel>.Default.Equals(HeldItem, model.HeldItem) &&
                   Status == model.Status &&
                   EqualityComparer<PokerusModel>.Default.Equals(Pokerus, model.Pokerus) &&
                   Personality == model.Personality &&
                   OTID == model.OTID &&
                   Language == model.Language &&
                   Ability == model.Ability &&
                   Location == model.Location &&
                   EqualityComparer<List<StatModel>>.Default.Equals(Stats, model.Stats);
        }

        public override int GetHashCode() {
            var hashCode = 597855731;
            hashCode = hashCode * -1521134295 + Generation.GetHashCode();
            hashCode = hashCode * -1521134295 + NationalDexNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nickname);
            hashCode = hashCode * -1521134295 + Level.GetHashCode();
            hashCode = hashCode * -1521134295 + XP.GetHashCode();
            hashCode = hashCode * -1521134295 + CurrentHP.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<MoveModel>>.Default.GetHashCode(Moveset);
            hashCode = hashCode * -1521134295 + EqualityComparer<ItemModel>.Default.GetHashCode(HeldItem);
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<PokerusModel>.Default.GetHashCode(Pokerus);
            hashCode = hashCode * -1521134295 + Personality.GetHashCode();
            hashCode = hashCode * -1521134295 + OTID.GetHashCode();
            hashCode = hashCode * -1521134295 + Language.GetHashCode();
            hashCode = hashCode * -1521134295 + Ability.GetHashCode();
            hashCode = hashCode * -1521134295 + Location.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<StatModel>>.Default.GetHashCode(Stats);
            return hashCode;
        }
    }
}
