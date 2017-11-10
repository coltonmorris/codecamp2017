# Server
This portion of our code is a node.js webserver that is connected to twitch's IRC. It updates a firebase db with new commands said in the chat.

### To Run
---
``` bash
npm start
```

### TODO
---
* have a handler file with all available commands
* help command
* connect to firebase

### Example Config File
---
Add a file ```config/default.json``` and have it in this structure (this is just an example, it will not work):
``` javascript
{
  "botName": "codecamp2017",
  "password": "oauth:asdfsdf",

  "initialChannel": "codecamp2017",

  "twitchClientId": "asdfasdfasd",
  "twitchClientSecret": "asdfasdfasd",

  "firebase" : {
    "apiKey": "asdfasdfasd",
    "authDomain": "testing-a2aa2.firebaseapp.com",
    "databaseURL": "https://testing-a2aa2.firebaseio.com",
    "projectId": "testing-a2aa2",
    "storageBucket": "testing-a2aa2.appspot.com",
    "messagingSenderId": "12341234"
  }
}
```
