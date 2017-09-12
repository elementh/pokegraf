'use strict'

const { start, about, random, fusion } = require('./commands')
const routing = require('./routing')

const Pokedex = require('pokedex-promise-v2')
const P = new Pokedex()

const Telegraf = require('telegraf')
const { Extra, Markup } = require('telegraf')

const pokegraf = new Telegraf(process.env.BOT_TOKEN)
const markup = Extra.markdown()

pokegraf.telegram.getMe().then((botInfo) => {
  pokegraf.options.username = botInfo.username
})

pokegraf.catch(err => {
  console.log('Oops', err)
})

// COMMANDS
// Start
pokegraf.command('start', (ctx) => start(ctx, markup))

// About
pokegraf.command('about', (ctx) => about(ctx, markup))

// Random
pokegraf.command('random', (ctx) => random(ctx, markup))

// Fusion
pokegraf.command('fusion', (ctx) => fusion(ctx, markup))

pokegraf.on('message', (ctx) => routing(ctx, markup, P))

pokegraf.action('stats', (ctx) => {
  ctx.reply('ASDAS')
})

module.exports = pokegraf

pokegraf.catchThemAll = function () {
  console.log('Fetching pokémon data...')
  // TODO: DOWNLOAD with get all and then go one by one with promises. somehow
  for (var i = 1; i < 20; i++) {
    let j = i
    P.getPokemonByName(i + 1) // with Promise
      .then(function (response) {
        console.log(`Catched pokémon with pokedex entry: ${j}`)
      })
      .catch(function (err) {
        console.error(`There was an error while catching this pokémon: ${j}`)
      })
  }
}

pokegraf.pikachu = function () {
  P.getPokemonByName('pikachu')
  .then(function (response) {
    console.log(`Catched pokémon with pokedex entry: ${25}`)
  })
  .catch(function (err) {
    console.error(`There was an error while catching this pokémon: ${25}`)
  })
  P.getPokemonSpeciesByName('pikachu')
  .then(function (response) {
    console.log(`Catched species with pokedex entry: ${25}`)
  })
  .catch(function (err) {
    console.error(`There was an error while catching this species: ${25}`)
  })
}
