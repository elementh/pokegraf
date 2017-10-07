class Chat {
  constructor (id, type, title, firstName, lastName, username, photoSmall, photoBig) {
    this.id = id
    this.type = type
    this.title = title
    this.firstName = firstName
    this.lastName = lastName
    this.username = username
    this.photoSmall = photoSmall
    this.photoBig = photoBig
  }
}

module.exports = {
  Chat: Chat
}
