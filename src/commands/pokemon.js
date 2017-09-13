'use strict'

const { capitalize, pokemonList } = require('../helpers')
const { Extra, Markup } = require('telegraf')

module.exports = function pokemon (ctx, markup, P) {
  console.log('new pokemon request from: ', ctx.from.username) // this will dissapear in final version, it's for testing
  let pokemonRequested = ctx.state.command.splitArgs[0]
  console.log(pokemonRequested)

  if (pokemonRequested > 0 && pokemonRequested < 722) {
    pokemonById(ctx, markup, P)
  }

  if (pokemonList.indexOf(capitalize(pokemonRequested)) != -1) {
    pokemonByName(ctx, markup, P)
  }
}

function pokemonById (ctx, markup, P) {
  let pokemonRequested = ctx.state.command.splitArgs[0]

  P.getPokemonByName(pokemonRequested)
  .then(response => {
    return ctx.telegram.sendPhoto(ctx.update.message.chat.id, `https://veekun.com/dex/media/pokemon/global-link/${response.id}.png`, {caption: `${capitalize(response.species.name)}`})
  }).then(() => {
    return P.getPokemonSpeciesByName(pokemonRequested)
  }).then(response => {
    let description = response.flavor_text_entries[1].flavor_text
    const replyOptions = Markup.inlineKeyboard([
      Markup.urlButton('Stats', 'http://telegraf.js.org')
       // Markup.callbackButton('Delele', 'delete')
    ]).extra()

    return ctx.telegram.sendMessage(ctx.update.message.chat.id, `${description.replace(/(\r\n|\n|\r)/gm, ' ')}`, replyOptions)
  })
  // .then(() => {
  //   ctx.reply()
  // })
  .catch(function (err) {
    console.error(err)
  })
}

function pokemonByName (ctx, markup, P) {
  let pokemonRequested = ctx.state.command.splitArgs[0]

  ctx.replyWithPhoto(`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${pokemonList.indexOf(capitalize(pokemonRequested)) + 1}.png`, {caption: capitalize(pokemonRequested)})
}
