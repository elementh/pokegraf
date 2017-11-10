class Chat {
  constructor (id, title, type, firstName, lastName, username, photoSmall, photoBig) {
    this.id = id
    this.title = title
    this.type = type
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
