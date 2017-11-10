'use strict'

const User = require('./model/User').User
const Chat = require('./model/Chat').Chat
const connection = require('typeorm').getConnection

module.exports = {
  middleware: middleware
}

function middleware () {
  return async(ctx, next) => {
    if (ctx.state.command) {
      await addUser(ctx.message.from)
      await addChat(ctx)
      await addCommand(ctx.state.command)
    }
    next()
  }
}

async function addUser (userData) {
  let userRepository = await connection().getRepository('User')
  userRepository.findOneById(userData.id).then((response) => {
    if (response === undefined) {
      let user = new User(userData.id, userData.isBot, userData.first_name, userData.last_name, userData.username, userData.language_code)

      userRepository.save(user)
    }
  })
}

async function addChat (ctx) {
  ctx.getChat().then(async(chatData) => {
    let chatRepository = await connection().getRepository('Chat')
    chatRepository.findOneById(chatData.id).then((response) => {
      if (response === undefined) {
        let chat = new Chat(chatData.id, chatData.title, chatData.type, chatData.first_name, chatData.last_name, chatData.username, chatData.photo.small_file_id, chatData.photo.big_file_id)

        chatRepository.save(chat)
      }
    })
  })
}

async function addCommand (command) {
  switch (command.command) {
    case 'fusion':
      await addFusion(command)
      break
    case 'pokemon':
    case 'pkm':
      await addPokemon(command)
      break
    default:
      break
  }
}

async function addFusion (command) {

}
async function addPokemon (command) {
}
