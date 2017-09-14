'use strict'

const { capitalize, pokemonList } = require('../helpers')
const { Extra, Markup } = require('telegraf')

module.exports = function pokemon (ctx, markup, P) {
  console.log('new pokemon request from: ', ctx.from.username) // this will dissapear in final version, it's for testing
  let pokemonRequested = ctx.state.command.splitArgs[0]

  if (pokemonRequested > 0 && pokemonRequested < 722) {
    replyPokemonById(ctx, markup, P)
  }

  if (pokemonList.includes(capitalize(pokemonRequested))) {
    replyPokemonByName(ctx, markup, P)
  }
}

function replyPokemonById (ctx, markup, P) {
  let pokemonRequested = ctx.state.command.splitArgs[0]

  return pokemonById(pokemonRequested, ctx, markup, P)
}

function replyPokemonByName (ctx, markup, P) {
  let pokemonRequested = ctx.state.command.splitArgs[0]
  pokemonRequested = pokemonList.indexOf(capitalize(pokemonRequested)) + 1

  return pokemonById(pokemonRequested, ctx, markup, P)
}

function pokemonById (pokemonRequested, ctx, markup, P) {
  P.getPokemonByName(pokemonRequested)
  .then(response => {
    return ctx.telegram.sendPhoto(ctx.update.message.chat.id, `https://veekun.com/dex/media/pokemon/global-link/${response.id}.png`, {caption: `${capitalize(response.species.name)}`})
  }).then(() => {
    return P.getPokemonSpeciesByName(pokemonRequested)
  }).then(response => {
    let description = response.flavor_text_entries[1].flavor_text
    const replyOptions = Markup.inlineKeyboard([
      Markup.urlButton('Stats', 'http://telegraf.js.org') // TODO
       // Markup.callbackButton('Delele', 'delete')
    ]).extra()
    ctx.telegram.sendMessage(ctx.update.message.chat.id, `${description.replace(/(\r\n|\n|\r)/gm, ' ')}`, replyOptions)
  })
  .catch(function (err) {
    console.error(err)
  })
}
