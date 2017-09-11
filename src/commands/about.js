'use strict'

module.exports = function about (ctx, markup) {
  return ctx.reply(aboutText, markup)
}

const aboutText = `[Pokegraf](https://github.com/elementh/pokegraf) is a bot made with ❤️ by [Lucas Maximiliano Marino](http://lucasmarino.me/).  

It uses the [PokeAPI](https://github.com/PokeAPI/pokeapi), [Pokemon Fusion](http://pokemon.alexonsager.net/) and [Telegraf](https://github.com/telegraf/telegraf/).  

Pokémon and their names are property of Nintendo, Creatures and Game Freak.`
