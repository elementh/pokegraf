class User {
  constructor (id, isBot, firstName, lastName, username, languageCode) {
    this.id = id
    this.isBot = isBot
    this.firstName = firstName
    this.lastName = lastName
    this.username = username
    this.languageCode = languageCode
  }
}

module.exports = {
  User: User
}
