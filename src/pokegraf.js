'use strict'

const { start, about, random, fusion } = require('./commands')
const routing = require('./routing')

// const Pokedex = require('pokedex-promise-v2')
// const P = new Pokedex()

const Telegraf = require('telegraf')
const { Extra, Markup } = require('telegraf')

const pokegraf = new Telegraf(process.env.BOT_TOKEN)
const markup = Extra.markdown()

pokegraf.telegram.getMe().then((botInfo) => {
  pokegraf.options.username = botInfo.username
})

pokegraf.catch(err => {
  console.log('Oops')
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

pokegraf.on('message', (ctx) => routing(ctx, markup))

module.exports = pokegraf

// P.getPokemonsList().then(function (response) {
//   console.log('ALL DATA CACHED')
// })
