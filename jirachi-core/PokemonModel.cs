using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class PokemonModel {
        // Constructor for Gen 1 Party Data
        public PokemonModel(int nationalDexNumber, string nickname, int level, int xp, int currentHP, List<MoveModel> moveset, StatusType status, int otID, PokemonLanguage pokemonLanguage, PokemonLocation location) {
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
            this.Language = Language;
            this.Ability = 0;
            this.Location = location;
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
    }
}
