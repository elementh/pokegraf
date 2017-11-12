'use strict'

const { start, about, random, fusion, help, pokemon } = require('./commands')

const { Extra } = require('telegraf')

const Telegraf = require('telegraf')
const commandParts = require('telegraf-command-parts')
const stats = require('./stats').middleware

const pokegraf = new Telegraf(process.env.BOT_TOKEN)
const markup = Extra.markdown()

pokegraf.telegram.getMe().then((botInfo) => {
  pokegraf.options.username = botInfo.username
})

pokegraf.use(commandParts())
pokegraf.use(stats())

pokegraf.catch(err => {
  console.log('Oops', err)
})

pokegraf.action((text) => {
  return text.startsWith('pokemon')
}, (ctx) => {
  let actions = ctx.update.callback_query.data.split(/\s+/)
  let pokemonRequested = actions[1]
  pokemon(ctx, markup, pokemonRequested)
})

pokegraf.action((text) => {
  return text.startsWith('stats')
}, (ctx) => {
  let actions = ctx.update.callback_query.data.split(/\s+/)
  let pokemonRequested = actions[1]
  pokemon(ctx, markup, pokemonRequested, 'stats')
})
// COMMANDS

// Start
pokegraf.command('start', (ctx) => start(ctx, markup))

// About
pokegraf.command('about', (ctx) => about(ctx, markup))

// Random
pokegraf.command('random', (ctx) => random(ctx, markup))

// help
pokegraf.command('help', (ctx) => help(ctx, markup))

// Fusion
pokegraf.command('fusion', (ctx) => fusion(ctx, markup))

pokegraf.command('pokemon', (ctx) => pokemon(ctx, markup))

pokegraf.command('pkm', (ctx) => pokemon(ctx, markup))

pokegraf.command('test', (ctx) => {
  // ctx.getChat().then((chat) => {
  //   // console.info(chat)
  // })
  // // console.log(ctx.from)
})
module.exports = pokegraf
