
# Message Ticker UI v1.0
This is a very simple little UI element for displaying text in a live scrolling little window similar to a console window. I created this for [the prototype of the Arena game](https://www.youtube.com/watch?v=2f8a_a6mVxE) and noticed that it was very self-contained and should be easy add to other projects. I can see myself re-using this in the future, so I figured I'd make an easy to import package out of it.

It's meant to be a very straightforward and simple package so that it can be easily dropped into any project and customized with new features from there.

![screenshot](Screenshots/console.gif)

This project was built in **Unity 2017.3.1f1**

## Download
You can either clone the repository or [download the asset package](https://github.com/brihernandez/MessageTickerUI/raw/master/MessageTickerUI.unitypackage) located in the root.

## Features

- Automatic scrolling of passed in messages
- Max number of messages, after which old messages are deleted
- Optional coloring of messages
- Messages can be set to fade out after a time or remain permanently

## How to use

Keep in mind is that only one Ticker is supported at a time. If multiple tickers are present, they will all display the same information.

1. Import package
    - If you're interested only in the ticker itself, do not import the `Demo` folder.
2. Drag the `Ticker` prefab onto your UI canvas.
3. Call `Ticker.AddItem(text)` to create a message on the ticker.
    - Alternatively, `Ticker.AddItem(text, lifespan, color)` can be used to customize the message.

# Changelog

### 1.0 (May 14 2019)

- Released
