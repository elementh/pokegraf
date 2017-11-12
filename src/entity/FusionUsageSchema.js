const FusionUsage = require('../model/FusionUsage').FusionUsage

module.exports = {
  target: FusionUsage,
  columns: {
    firstPokemon: {
      primary: true,
      type: 'varchar'
    },
    secondPokemon: {
      primary: true,
      type: 'varchar'
    },
    timesUsed: {
      type: 'int'
    }
  }
}
