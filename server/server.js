let config = require('config')
let tmi = require('tmi.js')
let _ = require('lodash')

let handlers = require('./handlers')
let initialChannel = config.get('initialChannel')


//defualt options for tmi.js
let options = {
	options: {
		debug: true
	},
	connections: {
		cluster: "aws",
		reconnect: true
	},
	identity: {
		username: config.get('botName'),
		password: config.get('password')
	},
	channels: [initialChannel]
}

//connect to irc
let client = new tmi.client(options)
client.connect()

 
client.on('chat',function(channel,user,message,self) {
  let command = message.split(' ')
  console.log('command: ', command)
  if (command[0] in handlers) {
    // the output is an array because twitch does not allow newlines in the same message
    // map through that array and output each line
    handlers[command[0]].run(user, command).map((line) => {
      client.action(initialChannel, line)
    })
  }
})

client.on('connected',function(address, port) {
  console.log('Connected to Twitch IRC')
})
