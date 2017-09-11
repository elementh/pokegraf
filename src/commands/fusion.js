'use strict'

const { randomIntFromInterval } = require('../helpers')

module.exports = function fusion (ctx, markup) {
  let firstPokemon = randomIntFromInterval(1, 151)
  let secondPokemon = randomIntFromInterval(1, 151)

  while (firstPokemon === secondPokemon) {
    secondPokemon = randomIntFromInterval(1, 151)
  }
  console.log(`New fusion request with numbers: ${firstPokemon} and ${secondPokemon}, from user: ${ctx.from.username}`)

  return ctx.replyWithPhoto(`http://images.alexonsager.net/pokemon/fused/${firstPokemon}/${firstPokemon}.${secondPokemon}.png`)
}
