'use strict'

require('dotenv-safe').load()

const Pokedex = require('pokedex-promise-v2')
const Telegraf = require('telegraf')
const { Extra, Markup } = require('telegraf')

const P = new Pokedex()
const pokegraf = new Telegraf(process.env.BOT_TOKEN)

P.getPokemonsList()
  .then(function (response) {
    console.log('ALL DATA CACHED')
  })

pokegraf.command('start', ({ from, reply }) => {
  console.log('start', from)
  return reply(
  `Hello there Pokémon Trainer! Welcome to *pokegraf*.\n\nHere is some basic usage:\n
    /random
    /fusion
    /about`)
})
pokegraf.command('about', ({ from, reply }) => {
  return reply('ABOUT')
})

pokegraf.command('random', (ctx) => {
  let rand = randomIntFromInterval(1, 802)
  console.log(`NEW REQUEST WITH RAND NUMBER: ${rand} AND DATE: ${Date.now()}`)
  P.getPokemonByName(rand) // with Promise
    .then(function (response) {
      // console.log(response)
      ctx.replyWithPhoto(`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${rand}.png`, {caption: response.name.replace(/^(.)|\s+(.)/g, function ($1) { return $1.toUpperCase() })})
    })
    .catch(function (error) {
      console.log('There was an ERROR: ', error)
      ctx.reply('There was an ERROR, SORRY TRAINER')
    })
})

pokegraf.command('fusion', (ctx) => {
  let firstPokemon = randomIntFromInterval(1, 151)
  let secondPokemon = randomIntFromInterval(1, 151)

  while (firstPokemon === secondPokemon) {
    secondPokemon = randomIntFromInterval(1, 151)
  }

  ctx.replyWithPhoto(`http://images.alexonsager.net/pokemon/fused/${firstPokemon}/${firstPokemon}.${secondPokemon}.png`)
})
// pokegraf.command('start', ({ from, reply }) => {
//   console.log('start', from)
//   return reply(`Welcome ${from.first_name} to Pokegraf.\n\nPlease choose your language:`, Markup
//     .keyboard([['🇪🇸 Spanish'], ['🇬🇧 English']])
//     .oneTime()
//     .resize()
//     .extra()
//   )
// })

pokegraf.startPolling()

function randomIntFromInterval (min, max) {
  return Math.floor(Math.random() * (max - min + 1) + min)
}
