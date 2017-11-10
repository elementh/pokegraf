const Chat = require('../model/Chat').Chat

module.exports = {
  target: Chat,
  columns: {
    id: {
      primary: true,
      type: 'int'
    },
    title: {
      type: 'varchar'
    },
    type: {
      type: 'varchar'
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
    photoSmall: {
      type: 'varchar'
    },
    photoBig: {
      type: 'varchar'
    }
  }
}
