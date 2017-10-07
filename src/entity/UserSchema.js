const User = require('../model/User').User

module.exports = {
  target: User,
  columns: {
    id: {
      primary: true,
      type: 'int'
    },
    isBot: {
      type: 'boolean'
    },
    firstName: {
      type: 'varchar'
    },
    lastName: {
      type: 'varchar'
    },
    username: {
      type: 'varchar'
    },
    languageCode: {
      type: 'varchar'
    }
  }
}
