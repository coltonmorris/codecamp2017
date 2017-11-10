let config = require('config')
let tmi = require('tmi.js')

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
	channels: [config.get('initialChannel')]
}

//connect to irc
let client = new tmi.client(options)
client.connect()

let initialChannel = config.get('initialChannel')

//change the color of username, doesnt work??
client.color("HotPink")

client.on('chat',function(channel,user,message,self) {
	if (message === "!joke") {
		//after !joke is said, switches channel bot is talking in to partyinpink
		client.action(initialChannel, "You are the joke.")
		// client.part(initialChannel)
		// initialChannel = "partyinpink"
		// client.join("partyinpink")
		// client.action(initialChannel, user['display-name'] + " hi there whats up dawgg.")
	}
	else{
		client.action(initialChannel, user['display-name'] + " hi there whats up dawgg.")
	}
})

client.on('connected',function(address, port) {
	client.action(initialChannel, "hello im a cool guy")
})




