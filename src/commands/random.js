'use strict'

const { randomIntFromInterval, capitalize, pokemon } = require('../helpers')

// module.exports = function random (ctx, markup, P) {
module.exports = function random (ctx, markup) {
  let rand = randomIntFromInterval(1, 802)
  console.log(`New random request with number: ${rand}, from user: ${ctx.from.username}`)
  // P.getPokemonByName(rand) // with Promise
  //   .then(function (response) {
  //     // console.log(response)
  //     return ctx.replyWithPhoto(`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${rand}.png`, {caption: capitalize(response.name)})
  //   })
  //   .catch(function (error) {
  //     console.log('There was an ERROR: ', error)
  //     return ctx.reply('There was an ERROR, sorry Trainer')
  //   })
  ctx.replyWithPhoto(`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${rand}.png`, {caption: pokemon.getName(rand)})
}

