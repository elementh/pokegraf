'use strict'
require('dotenv-safe').load()

const Telegraf = require('telegraf')
const { Extra, Markup } = require('telegraf')

const pokegraf = new Telegraf(process.env.BOT_TOKEN)

pokegraf.command('start', ({ from, reply }) => {
  console.log('start', from)
  return reply(`Welcome ${from.first_name} to Pokegraf.\n\nPlease choose your language:`, Markup
    .keyboard([['🇪🇸 Spanish'], ['🇬🇧 English']])
    .oneTime()
    .resize()
    .extra()
  )
})

pokegraf.startPolling()
