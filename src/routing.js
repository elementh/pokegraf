'use strict'

const { capitalize, pokemon, pokemonList } = require('./helpers')
const { Extra, Markup } = require('telegraf')

module.exports = function routing (ctx, markup, P) {
  let message = ctx.message.text
  if (message > 0 && message < 803) {
    P.getPokemonByName(message)
    .then(function (response) {
      ctx.telegram.sendPhoto(ctx.update.message.chat.id, response.sprites.front_default, {caption: `${pokemon.getName(message)}`})
      return P.getPokemonSpeciesByName(message)
    }).then(response => {
      let description = response.flavor_text_entries[1].flavor_text
      const replyOptions = Markup.inlineKeyboard([
        // Markup.callbackButton('Stats', (ctx) => { ctx.reply('ass') })
        // Markup.callbackButton('Delele', 'delete')
      ]).extra()

      ctx.reply(`${description.replace(/(\r\n|\n|\r)/gm, ' ')}`)
    })
    // .then(() => {
    //   ctx.reply()
    // })
    .catch(function (err) {
      console.error(err)
    })
  }
  if (pokemonList.indexOf(capitalize(message)) != -1) {
    ctx.replyWithPhoto(`https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/${pokemonList.indexOf(capitalize(message)) + 1}.png`, {caption: capitalize(message)})
  }
}

// {caption: pokemon.getName(message)},
