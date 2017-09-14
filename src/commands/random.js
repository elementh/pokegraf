'use strict'

const { randomIntFromInterval} = require('../helpers')
const pokemon = require('./pokemon')

module.exports = function random (ctx, markup) {
  let rand = randomIntFromInterval(1, 802)
  return pokemon(ctx, markup, rand)
}

