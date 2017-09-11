module.exports = {
  randomIntFromInterval: randomIntFromInterval,
  capitalize: capitalize,
  pokemon: require('pokemon'),
  pokemonList: require('pokemon').all()

}

function randomIntFromInterval (min, max) {
  return Math.floor(Math.random() * (max - min + 1) + min)
}

function capitalize (text) {
  return text.replace(/^(.)|\s+(.)/g, function ($1) { return $1.toUpperCase() })
}
