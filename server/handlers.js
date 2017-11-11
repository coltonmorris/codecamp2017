let config = require('config')
let firebase = require('firebase')
let _ = require('lodash')

firebase.initializeApp(config.get('firebase'))
let initialChannel = config.get('initialChannel')
let commands = {}


// Listener for counter
let counter 
firebase.app().database().ref().child('counter').on('value', snap => {
  counter = snap.val()
})

commands['!dec'] = {
  'info': '!dec: This decrements our counter, it has no use.',
  'run': (user, message) => {
    counter--
    firebase.app().database().ref('counter/').set(counter)
    return [counter]
  },
}

commands['!inc'] = {
  'info': '!inc: This increments our counter, it has no use.',
  'run': (user, message) => {
    counter++
    firebase.app().database().ref('counter/').set(counter)
    return [counter]
  },
}

commands['!object'] = {
  'info': '!object: This creates a object with an x and z coordinate. Must be between -50 to 50 (can\'t be too close though). Example usage: "!object 20 30".',
  'run': (user, message) => {
    let max = 50
    let min = -50

    if (message.length != 3) {
      return ['POOR USAGE. It should look like this: "!object <num> <num>"']
    }

    let x = parseInt(message[1]) || min -1
    let z = parseInt(message[2]) || min -1
    // parseInt doesn't work with 0... haha KMS
    if (message[1] == '0') x = 0
    if (message[2] == '0') z = 0

    // check for NaN and for too large
    if (x < min || x > max || z < min || z > max) return ['POOR USAGE. It should be between -50 to 50. Example: "!object -10 20"']

    // Make sure we do not spawn an object right on the player...
    if (x < 20 && x > 0) x = 20
    if (x > -20 && x < 0) x = -20
    if (z < 20 && z > 0) z = 20
    if (z > -20 && z < 0) z = -20

    firebase.app().database().ref('badguys/').push({
      x: x,
      z: z,
      user: user.username
    })

    return ['Created Object with coordinates: (' + message[1] + ', ' + message[2] + ')']
  },
}

commands['!help'] = {
  'info': '!help: This command shows the help message',
  'run': (user, message) => {
    let output = ['Available Commands:']

    Object.keys(commands).map((command) => {
      output.push(commands[command].info)
    })

    return output
  },
}

module.exports = commands
