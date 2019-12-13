using System;
using System.Collections.Generic;
using System.Text;

namespace jirachi_core {
    class PokedexEntryModel {
        /// <summary>
        /// Represents the National Dex ID of this Pokemon's dex entry
        /// </summary>
        public int NationalDexId { get; set; }
        /// <summary>
        /// Represents whether or not this Pokemon has been seen
        /// </summary>
        public bool SeenPokemon { get; set; }
        /// <summary>
        /// Represents whether or not this Pokmeon has been owned
        /// </summary>
        public bool OwnedPokemon { get; set; }
    }
}
