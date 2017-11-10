let config = require('config')
let firebase = require('firebase')

firebase.initializeApp(config.get('firebase'))
let initialChannel = config.get('initialChannel')
let commands = {}


// Listener for counter
let counter 
firebase.app().database().ref().child('counter').on('value', snap => {
  counter = snap.val()
})

commands['!dec'] = {
  'info': '!dec: This decrements our counter',
  'run': (user) => {
    counter--
    firebase.app().database().ref('counter/').set(counter)
    return [counter]
  },
}

commands['!inc'] = {
  'info': '!inc: This increments our counter',
  'run': (user) => {
    counter++
    firebase.app().database().ref('counter/').set(counter)
    return [counter]
  },
}

commands['!object'] = {
  'info': '!object: This creates a object with an x and z coordinate. Must be between 0-50. Example usage: "!object 20 30".',
  'run': (user) => {
    let output = 'Created Object with coordinates: '

    let x = Math.floor((Math.random() * 50))
    let z = Math.floor((Math.random() * 50))
    output += parseInt(x) + ', ' + parseInt(z)

    firebase.app().database().ref('Objects/').push({
      x: x,
      z: z,
      user: user.username
    })

    return [output]
  },
}

commands['!help'] = {
  'info': '!help: This command shows the help message',
  'run': (user) => {
    let output = ['Available Commands:']

    Object.keys(commands).map((command) => {
      output.push(commands[command].info)
    })

    return output
  },
}

module.exports = commands
