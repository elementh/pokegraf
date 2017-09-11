'use strict'

const { randomIntFromInterval } = require('../helpers')

module.exports = function random (ctx, markup, P) {
  let rand = randomIntFromInterval(1, 802)
  console.log(`New random request with number: ${rand}, from user: ${ctx.from.username}`)
  P.getPokemonByName(rand) // with Promise
    .then(function (response) {
      // console.log(response)
      return ctx.replyWithPhoto(`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${rand}.png`, {caption: response.name.replace(/^(.)|\s+(.)/g, function ($1) { return $1.toUpperCase() })})
    })
    .catch(function (error) {
      console.log('There was an ERROR: ', error)
      return ctx.reply('There was an ERROR, sorry Trainer')
    })
}

