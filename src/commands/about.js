'use strict'

module.exports = function about (ctx, markup) {
  return ctx.reply(aboutText, markup)
}

const aboutText = `[pokegraf](https://github.com/elementh/pokegraf) is a bot made with ❤️ by [Lucas Maximiliano Marino](http://lucasmarino.me/) with the help of [contributors](https://github.com/elementh/pokegraf/blob/master/CONTRIBUTORS.md)!.  

It uses the [PokeAPI](https://github.com/PokeAPI/pokeapi), [Pokemon Fusion](http://pokemon.alexonsager.net/) and [Telegraf](https://github.com/telegraf/telegraf/).  

Pokémon ©1995 [pokémon](http://www.pokemon.com/), [nintendo](http://www.nintendo.com/), [game freak](http://www.gamefreak.co.jp/), [creatures](http://www.creatures.co.jp/html/en/).`
