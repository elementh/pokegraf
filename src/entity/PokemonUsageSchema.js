const PokemonUsageSchema = require('../model/PokemonUsageSchema').PokemonUsageSchema

module.exports = {
  target: PokemonUsageSchema,
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
