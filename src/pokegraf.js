'use strict'

const { start, about, random, fusion } = require('./commands')

const Pokedex = require('pokedex-promise-v2')
const P = new Pokedex()

const Telegraf = require('telegraf')
const { Extra, Markup } = require('telegraf')
const pokegraf = new Telegraf(process.env.BOT_TOKEN)

// P.getPokemonsList().then(function (response) {
//   console.log('ALL DATA CACHED')
// })

// Start
pokegraf.command('start', (ctx) => start(ctx))

// About
pokegraf.command('about', (ctx) => about(ctx))

// Random
pokegraf.command('random', (ctx) => random(ctx))

// Fusion
pokegraf.command('fusion', (ctx) => fusion(ctx))

module.exports = pokegraf
