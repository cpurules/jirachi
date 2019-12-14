using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    public class PokemonModel {
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
        /// Represents whether this Pokemon is in the user's party or in a box/daycare
        /// </summary>
        public bool InParty { get; set; }
    }
}
