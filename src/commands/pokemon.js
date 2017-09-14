'use strict'

const { capitalize, pokemonList, P } = require('../helpers')
const { Extra, Markup } = require('telegraf')

module.exports = function pokemon (ctx, markup, request = false) {
  console.log('new pokemon request from: ', ctx.from.username) // this will dissapear in final version, it's for testing
  if (request) {
    return pokemonById(request, ctx, markup)
  }

  let pokemonRequested = ctx.state.command.splitArgs[0]

  if (pokemonRequested > 0 && pokemonRequested < 722) {
    return replyPokemonById(ctx, markup)
  }

  if (pokemonList.includes(capitalize(pokemonRequested))) {
    return replyPokemonByName(ctx, markup)
  }
}

function replyPokemonById (ctx, markup) {
  let pokemonRequested = ctx.state.command.splitArgs[0]

  return pokemonById(pokemonRequested, ctx, markup)
}

function replyPokemonByName (ctx, markup) {
  let pokemonRequested = ctx.state.command.splitArgs[0]
  pokemonRequested = pokemonList.indexOf(capitalize(pokemonRequested)) + 1

  return pokemonById(pokemonRequested, ctx, markup)
}

function pokemonById (pokemonRequested, ctx, markup) {
  let chatId
  ctx.getChat()
  .then(response => {
    chatId = response.id
    return P.getPokemonByName(pokemonRequested)
  })
  // let chatId = ctx.update.message.chat.id ? ctx.update.message.chat.id : ctx.update.call
  .then(response => {
    return ctx.telegram.sendPhoto(chatId, `https://veekun.com/dex/media/pokemon/global-link/${response.id}.png`, {caption: `${capitalize(response.species.name)}`})
  }).then(() => {
    return P.getPokemonSpeciesByName(pokemonRequested)
  }).then(response => {
    let description = response.flavor_text_entries[1].flavor_text
    const replyOptions = Markup.inlineKeyboard([
      [
        Markup.callbackButton('Stats', 'stats'),
        Markup.callbackButton('Delele', 'stats')
      ],
      [
        Markup.callbackButton(`⬅️ ${pokemonList[previousPokemon(pokemonRequested)]}`, `pokemon ${previousPokemon(pokemonRequested) + 1}`),
        Markup.callbackButton(`${pokemonList[actualPokemon(pokemonRequested)]}`, `pokemon ${actualPokemon(pokemonRequested) + 1}`),
        Markup.callbackButton(`${pokemonList[nextPokemon(pokemonRequested)]} ➡️`, `pokemon ${nextPokemon(pokemonRequested) + 1}`)
      ]
    ]).extra()
    ctx.telegram.sendMessage(chatId, `${description.replace(/(\r\n|\n|\r)/gm, ' ')}`, replyOptions)
  })
  .catch(function (err) {
    console.error(err)
  })
}

function previousPokemon (pokemonRequested) {
  if (pokemonRequested == 1) {
    return 720
  } else {
    pokemonRequested = --pokemonRequested
    return --pokemonRequested
  }
}

function actualPokemon (pokemonRequested) {
  pokemonRequested = --pokemonRequested
  return pokemonRequested
}

function nextPokemon (pokemonRequested) {
  if (pokemonRequested == 721) {
    return 1
  } else {
    pokemonRequested = --pokemonRequested
    return ++pokemonRequested
  }
}
