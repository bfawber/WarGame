using System;
using System.Collections.Generic;
using WarGame.Core;
using WarGame.Core.HashSetDeck;
using WarGame.Model;

namespace WarGame
{
	class Program
	{
		static void Main(string[] args)
		{
			GameManager gameManager = new GameManager(new ConsoleDisplay(), new WarStatCollector());
			gameManager.StartSimulation();
		}		
	}
}
