'use strict'

const {
  capitalize,
  pokemonList,
  P
} = require('../helpers')
const {
  Markup
} = require('telegraf')

module.exports = function pokemon (ctx, markup, request = false, mode = 'description') {
  console.log('new pokemon request from: ', ctx.from.username, mode) // this will dissapear in final version, it's for testing
  if (request && mode === 'description') {
    return pokemonById(request, ctx, markup)
  } else if (mode === 'description') {
    let pokemonRequested = ctx.state.command.splitArgs[0]

    if (pokemonRequested > 0 && pokemonRequested < 722) {
      return replyPokemonById(ctx, markup)
    }

    if (pokemonList.includes(capitalize(pokemonRequested))) {
      return replyPokemonByName(ctx, markup)
    }
  } else if (mode === 'stats') {
    return statsById(request, ctx, markup)
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
    }).then(response => {
      return ctx.telegram.sendPhoto(chatId, `https://veekun.com/dex/media/pokemon/global-link/${response.id}.png`, {
        caption: `${capitalize(response.species.name)}`
      })
    }).then(() => {
      return P.getPokemonSpeciesByName(pokemonRequested)
    }).then(response => {
      let description = response.flavor_text_entries[1].flavor_text
      const replyOptions = Markup.inlineKeyboard([
        [
          // Markup.callbackButton('WIP', 'wip'),
          Markup.callbackButton('Stats', `stats ${pokemonRequested}`)
        ],
        [
          Markup.callbackButton(`⬅️ ${pokemonList[previousPokemon(pokemonRequested)]}`, `pokemon ${previousPokemon(pokemonRequested) + 1}`),
          Markup.callbackButton(`${pokemonRequested}`, `pokemon ${pokemonRequested}`),
          Markup.callbackButton(`${pokemonList[nextPokemon(pokemonRequested)]} ➡️`, `pokemon ${nextPokemon(pokemonRequested) + 1}`)
        ]
      ]).extra()
      ctx.telegram.sendMessage(chatId, `${description.replace(/(\r\n|\n|\r)/gm, ' ')}`, replyOptions)
    })
    .catch(function (err) {
      console.error(err)
    })
}

function statsById (pokemonRequested, ctx, markup) {
  let chatId
  ctx.getChat()
    .then(response => {
      chatId = response.id
      return P.getPokemonByName(pokemonRequested)
    }).then(response => {
      return ctx.telegram.sendPhoto(chatId, `https://veekun.com/dex/media/pokemon/global-link/${response.id}.png`, {
        caption: `${capitalize(response.species.name)}`
      })
    }).then(() => {
      return P.getPokemonByName(pokemonRequested)
    }).then(response => {
      let stats = formatStats(response.stats)
      const replyOptions = Markup.inlineKeyboard([
        [
          // Markup.callbackButton('WIP', 'wip'),
          Markup.callbackButton('Description', `pokemon ${pokemonRequested}`)
        ],
        [
          Markup.callbackButton(`⬅️ ${pokemonList[previousPokemon(pokemonRequested)]}`, `pokemon ${previousPokemon(pokemonRequested) + 1}`),
          Markup.callbackButton(`${pokemonRequested}`, `pokemon ${pokemonRequested}`),
          Markup.callbackButton(`${pokemonList[nextPokemon(pokemonRequested)]} ➡️`, `pokemon ${nextPokemon(pokemonRequested) + 1}`)
        ]
      ]).extra()
      ctx.telegram.sendMessage(chatId, stats, replyOptions)
    })
    .catch(function (err) {
      console.error(err)
    })
}

function previousPokemon (pokemonRequested) {
  if (pokemonRequested === 1) {
    return 720
  } else {
    pokemonRequested = --pokemonRequested
    return --pokemonRequested
  }
}

// function actualPokemon (pokemonRequested) {
//   pokemonRequested = --pokemonRequested
//   return pokemonRequested
// }

function nextPokemon (pokemonRequested) {
  if (pokemonRequested === 721) {
    return 1
  } else {
    pokemonRequested = --pokemonRequested
    return ++pokemonRequested
  }
}

function formatStats (stats) {
  return `HP 💗 ${stats[5].base_stat}
Attack 💥 ${stats[4].base_stat}
Defense 🛡 ${stats[3].base_stat}
Special Attack 🌟 ${stats[2].base_stat}
Special Defense 🔰 ${stats[1].base_stat}
Speed 👟 ${stats[0].base_stat}
`
}
