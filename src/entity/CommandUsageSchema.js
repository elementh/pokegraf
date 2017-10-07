const CommandUsage = require('../model/CommandUsage').CommandUsage

module.exports = {
  target: CommandUsage,
  columns: {
    command: {
      primary: true,
      type: 'varchar'
    },
    timesUsed: {
      type: 'int'
    }
  }
}
