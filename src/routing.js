'use strict'

const { capitalize, pokemon, pokemonList } = require('./helpers')

module.exports = function routing (ctx, markup) {
  let message = ctx.update.message.text

  if (message > 0 && message < 803) {
    ctx.replyWithPhoto(`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${message}.png`, {caption: pokemon.getName(message)})
  }
  if (pokemonList.indexOf(capitalize(message)) != -1) {
    ctx.replyWithPhoto(`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${pokemonList.indexOf(capitalize(message)) + 1}.png`, {caption: capitalize(message)})
  }
}
