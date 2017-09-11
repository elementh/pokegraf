'use strict'

module.exports = function start (ctx, markup) {
  console.log('new start from: ', ctx.from.username)
  return ctx.reply(greetingText, markup)
}

const greetingText = `Hello there Pokémon Trainer! Welcome to *pokegraf*.

Basic usage:
  /random
  /fusion
  /about
`
