'use strict'

module.exports = function help (ctx, markup) {
  console.log('new help from: ', ctx.from.username)
  return ctx.reply(helpText, markup)
}

const helpText = `Basic usage:
  /random
  /fusion
  /about
  /help
`
