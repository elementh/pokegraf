'use strict'

const connection = require('typeorm').getConnection

class User {
  constructor (id, isBot, firstName, lastName, username, languageCode) {
    this.id = id
    this.isBot = isBot
    this.firstName = firstName
    this.lastName = lastName
    this.username = username
    this.languageCode = languageCode
  }
  async findOne (id) {
    let userRepository = await connection().getRepository(User)
    userRepository.find(id).then((response) => {
      if (response.length) {
        return response[0]
      } else {
        return false
      }
    })
  }
  async add (user) {
    let userRepository = await connection().getRepository(User)
    await userRepository.save(new User(
      user.id,
      user.is_bot,
      user.first_name,
      user.last_name,
      user.username,
      user.language_code
    ))
  }
}

module.exports = {
  User: User
}
