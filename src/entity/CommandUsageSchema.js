const CommandUsage = require('../model/CommandUsage').CommandUsage

module.exports = {
  target: CommandUsage,
  columns: {
    command: {
      type: 'varchar'
    },
    timesUsed: {
      type: 'int'
    }
  }
}
