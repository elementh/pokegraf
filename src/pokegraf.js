const Pokedex = require('pokedex-promise-v2')
const Telegraf = require('telegraf')
const { Extra, Markup } = require('telegraf')
const { start, about, random, fusion } = require('./commands')

const P = new Pokedex()

P.getPokemonsList().then(function (response) {
  console.log('ALL DATA CACHED')
})

const pokegraf = new Telegraf(process.env.BOT_TOKEN)

module.exports = pokegraf

pokegraf.command('start', (ctx) => start(ctx))

pokegraf.command('about', (ctx) => about(ctx))

pokegraf.command('random', (ctx) => random(ctx))

pokegraf.command('fusion', (ctx) => fusion(ctx))

function randomIntFromInterval (min, max) {
  return Math.floor(Math.random() * (max - min + 1) + min)
}

