const Chance = require('chance')
const chance = new Chance()

let database = {
  posts: [
    {
      guid: '56638a2a-a8ae-5ebc-81a8-967de018e2c0',
      name: 'Top 10 ES6 Features every Web Developer must know',
      url: 'https://webapplog.com/es6',
      text: 'This essay will give you a quick introduction to ES6. If you don’t know what is ES6, it’s a new JavaScript implementation.',
      comments: [
        {
          text: 'Cruel…..var { house, mouse} = No type optimization at all',
          timeStamp: '2017-12-16 21:16:08'
        },
        {
          text: 'I think you’re undervaluing the benefit of ‘let’ and ‘const’.',
          timeStamp: '2017-12-16 21:16:08'
        },
        {
          text: '(p1,p2)=>{ … } ,i understand this ,thank you !',
          timeStamp: '2017-12-16 21:16:08'
        }
      ]
    }
  ]
}

class Post {
  constructor(name, url, text) {
    this.guid = chance.guid()
    this.name = name
    this.url = url
    this.text = text
  }
}

class Comment {
  constructor(text) {
    this.text = text
    this.timeStamp = this.newTimestamp()
  }

  newTimestamp(){
    const date = new Date().toISOString().substr(0, 10)
    const time = new Date().toISOString().substr(11, 8)
    return date + ' ' + time
  }
}

module.exports.database = database
module.exports.Post = Post
module.exports.Comment = Comment
