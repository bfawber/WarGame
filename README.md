# War Game Simulator

## Overview

This project was created to simulate the card game 'War'. The concept of war is quite simple:

1. Each player starts with half the deck of cards.
2. Each 'Battle' is played by each player drawing the top card of their deck and placing it on the table.
3. Whoever's card is higher in rank take's the cards played and adds them to the bottom of their deck
4. If the cards are equal in rank, both players put three cards face down on the table and play a fourth face up.
5. They repeat this process until someone's card is a higher rank. The player who plays the card with the higher rank wins all the cards on the table.
6. When a player wins all 52 cards, they win the game.

## Running the program

To run the program, you can use 2 methods:

1. Run the WarGame.exe found in the Distributable  folder.
2. If you have the dotnet core cli installed, you can also run a `dotnet ".\src\WarGame\bin\Debug\netcoreapp2.1\WarGame.dll"` command after building the project.

By default, one game will be played without any interaction from the user.

## Command Line Options

The following command line options can be added to modify how the simulation is run:

1. `--interactive` - Specify if the game wait for input between each battle.
2. `--games <number>` - Specify how many games should be played.
3. `--compound-stats` - Specify whether the stats in each game should be combined, or reset between each game

Examples:

1. `WarGame.exe --interactive --games 2` - Wait between each battle for a key press, play two games, each game resets the stats.
2. `WarGame.exe --game 10 --compound-stats` - Play 10 games, don't reset the stats between games.