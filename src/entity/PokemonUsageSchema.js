const PokemonUsage = require('../model/PokemonUsage').PokemonUsage

module.exports = {
  target: PokemonUsage,
  columns: {
    pokemon: {
      primary: true,
      type: 'varchar'
    },
    timesUsed: {
      type: 'int'
    }
  }
}
