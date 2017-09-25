'use strict'

const mysql = require('nodejs-mysql').default

const db = mysql.getInstance({
  host: process.env.DB_HOST,
  port: process.env.DB_PORT,
  user: process.env.DB_USER,
  password: process.env.DB_PASS,
  database: process.env.DB_NAME
})

const TABLE_STATS_COMMANDS = 'stats_commands'
const TABLE_STATS_FUSION = 'stats_fusion'
const TABLE_STATS_POKEMON = 'stats_pokemon'
const TABLE_STATS_USERS = 'stats_users'

module.exports = db

db.checkTables = function () {
  db.exec('SHOW TABLES')
  .then(rows => {
    if (rows[3] &&
        rows[0].Tables_in_pokegraf === TABLE_STATS_COMMANDS &&
        rows[1].Tables_in_pokegraf === TABLE_STATS_FUSION &&
        rows[2].Tables_in_pokegraf === TABLE_STATS_POKEMON &&
        rows[3].Tables_in_pokegraf === TABLE_STATS_USERS) {
      console.log('Found existing database.')
    } else {
      console.log('Database not found. Creating one...')
      db.generateTables()
    }
  })
  .catch(err => {
    console.log(err)
  })
}

db.generateTables = function () {
  db.exec('CREATE OR REPLACE TABLE `'+TABLE_STATS_COMMANDS+'` \
        (`command` varchar(45) NOT NULL, \
        `times_used` int(11) DEFAULT NULL, \
        PRIMARY KEY (`command`)) ENGINE=InnoDB DEFAULT CHARSET=utf8;')
  .then(rows => {
    console.log('...stats_commands created...')
  })
  .catch(err => {
    console.log(err)
  })
  db.exec('CREATE OR REPLACE TABLE `'+TABLE_STATS_FUSION+'` \
        (`id_pkmn_1` int(11) NOT NULL, \
        `id_pkmn_2` int(11) NOT NULL, \
        `times_used` varchar(45) DEFAULT NULL, \
        PRIMARY KEY (`id_pkmn_1`,`id_pkmn_2`)) \
        ENGINE=InnoDB DEFAULT CHARSET=utf8')
  .then(rows => {
    console.log('...stats_fusion created...')
  })
  .catch(err => {
    console.log(err)
  })
  db.exec('CREATE OR REPLACE TABLE `'+TABLE_STATS_POKEMON+'` \
        (`id_pkmn` int(11) NOT NULL, \
        `times_used` int(11) DEFAULT NULL, \
        PRIMARY KEY (`id_pkmn`)) ENGINE=InnoDB DEFAULT CHARSET=utf8;')
  .then(rows => {
    console.log('...stats_pokemon created...')
  })
  .catch(err => {
    console.log(err)
  })
  db.exec('CREATE TABLE `'+TABLE_STATS_USERS+'` \
        (`type` varchar(10) NOT NULL, \
        `number` int(11) DEFAULT \'0\', \
        PRIMARY KEY (`type`)) ENGINE=InnoDB DEFAULT CHARSET=utf8;')
  .then(() => {
    return db.exec('INSERT INTO `'+TABLE_STATS_USERS+'` (`type`,`number`) VALUES(\'groups\',0);')
  })
  .then(() => {
    return db.exec('INSERT INTO `'+TABLE_STATS_USERS+'` (`type`,`number`) VALUES(\'users\',0);')
  })
  .then(rows => {
    console.log('...stats_users created...')
  })
  .catch(err => {
    console.log(err)
  })
}

db.addUser = function () {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}

db.addGroup = function () {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}

db.addPokemon = function (id) {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}

db.addFusion = function (fId, sId) {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}

db.addCommand = function (command) {
  db.exec('INSERT IGNORE INTO `'+TABLE_STATS_COMMANDS+'` (command, times_used) VALUES ("'+command+'", 0) ')
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}


/**
 * Check if a command exist.
 * If not exist it will be created
 * @param {string} command
 */
db.checkCommand = function (command) {
  db.exec('SELECT command FROM `'+TABLE_STATS_COMMANDS+'` WHERE command = "'+command+'" ')
  .then(rows => {
      console.log("Command found: ")
      console.log(rows)
      
      if (rows.fieldCount == 0) {
          db.insertCommand(command)
      }
  })
  .catch(err => {
    console.log(err)
  })
}

/**
 * Increment the times a command is used
 * @param {string} command
 */
db.increaseCommand = function (command) {
    db.checkCommand(command)
    
  db.exec('UPDATE `'+TABLE_STATS_COMMANDS+'` SET times_used = times_used + 1 WHERE command = "'+command+'" ')
  .then(rows => {
      console.log("Command " + command + " increased")
      console.log(rows)
  })
  .catch(err => {
    console.log(err)
  })
}
