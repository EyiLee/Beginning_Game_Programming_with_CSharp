﻿using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaCards;

namespace ProgrammingAssignment6
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        // max valid blockjuck score for a hand
        const int MaxHandValue = 21;

        // deck and hands
        Deck deck;
        List<Card> dealerHand = new List<Card>();
        List<Card> playerHand = new List<Card>();

        // hand placement
        const int TopCardOffset = 100;
        const int HorizontalCardOffset = 150;
        const int VerticalCardSpacing = 125;

        // messages
        SpriteFont messageFont;
        const string ScoreMessagePrefix = "Score: ";
        Message playerScoreMessage;
        Message dealerScoreMessage;
        Message winnerMessage;
        List<Message> messages = new List<Message>();

        // message placement
        const int ScoreMessageTopOffset = 25;
        const int HorizontalMessageOffset = HorizontalCardOffset;
        Vector2 winnerMessageLocation = new Vector2(WindowWidth / 2,
            WindowHeight / 2);

        // menu buttons
        Texture2D quitButtonSprite;
        Texture2D hitButtonSprite;
        Texture2D standButtonSprite;
        List<MenuButton> menuButtons = new List<MenuButton>();

        // menu button placement
        const int TopMenuButtonOffset = TopCardOffset;
        const int QuitMenuButtonOffset = WindowHeight - TopCardOffset;
        const int HorizontalMenuButtonOffset = WindowWidth / 2;
        const int VerticalMenuButtonSpacing = 125;

        // use to detect hand over when player and dealer didn't hit
        bool playerHit = false;
        bool dealerHit = false;
        bool gameOver = false;

        // game state tracking
        static GameState currentState = GameState.WaitingForPlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set resolution and show mouse
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create and shuffle deck
            deck = new Deck(Content, WindowWidth / 2, WindowHeight / 2);
            deck.Shuffle();

            // first player card
            Card firstPlayerCard = deck.TakeTopCard();
            firstPlayerCard.X = HorizontalCardOffset;
            firstPlayerCard.Y = TopCardOffset;
            firstPlayerCard.FlipOver();
            playerHand.Add(firstPlayerCard);

            // first dealer card
            Card firstDealerCard = deck.TakeTopCard();
            firstDealerCard.X = WindowWidth - HorizontalCardOffset;
            firstDealerCard.Y = TopCardOffset;
            //firstDealerCard.FlipOver();
            dealerHand.Add(firstDealerCard);

            // second player card
            Card secondPlayerCard = deck.TakeTopCard();
            secondPlayerCard.X = HorizontalCardOffset;
            secondPlayerCard.Y = TopCardOffset + VerticalCardSpacing;
            secondPlayerCard.FlipOver();
            playerHand.Add(secondPlayerCard);

            // second dealer card
            Card secondDealerCard = deck.TakeTopCard();
            secondDealerCard.X = WindowWidth - HorizontalCardOffset;
            secondDealerCard.Y = TopCardOffset + VerticalCardSpacing;
            secondDealerCard.FlipOver();
            dealerHand.Add(secondDealerCard);

            // load sprite font, create message for player score and add to list
            messageFont = Content.Load<SpriteFont>(@"fonts\Arial24");
            playerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(playerHand).ToString(),
                messageFont,
                new Vector2(HorizontalMessageOffset, ScoreMessageTopOffset));
            messages.Add(playerScoreMessage);

            // load quit button sprite for later use
            quitButtonSprite = Content.Load<Texture2D>(@"graphics\quitbutton");
            MenuButton quitButton = new MenuButton(quitButtonSprite, new Vector2(HorizontalMenuButtonOffset, QuitMenuButtonOffset),
                GameState.Exiting);
            menuButtons.Add(quitButton);

            // create hit button and add to list
            hitButtonSprite = Content.Load<Texture2D>(@"graphics\hitbutton");
            MenuButton hitButton = new MenuButton(hitButtonSprite, new Vector2(HorizontalMenuButtonOffset, TopMenuButtonOffset),
                GameState.PlayerHitting);
            menuButtons.Add(hitButton);

            // create stand button and add to list
            standButtonSprite = Content.Load<Texture2D>(@"graphics\standbutton");
            MenuButton standButton = new MenuButton(standButtonSprite,
                new Vector2(HorizontalMenuButtonOffset, TopMenuButtonOffset + VerticalMenuButtonSpacing),
                GameState.WaitingForDealer);
            menuButtons.Add(standButton);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();
            // update menu buttons as appropriate
            foreach (MenuButton menuButton in menuButtons)
            {
                if (currentState == GameState.WaitingForPlayer ||
                    currentState == GameState.DisplayingHandResults)
                {
                    menuButton.Update(mouse);
                }
            }

            // game state-specific processing
            switch (currentState)
            {
                case GameState.CheckingHandOver:
                    bool playerBusted = GetBlockjuckScore(playerHand) > MaxHandValue;
                    bool dealerBusted = GetBlockjuckScore(dealerHand) > MaxHandValue;
                    if (playerBusted || dealerBusted)
                    {
                        if (playerBusted && dealerBusted)
                        {
                            winnerMessage = new Message("Tie!", messageFont, winnerMessageLocation);
                        }
                        else if (playerBusted)
                        {
                            winnerMessage = new Message("Dealer Won!", messageFont, winnerMessageLocation);
                        }
                        else
                        {
                            winnerMessage = new Message("Player Won!", messageFont, winnerMessageLocation);
                        }
                        messages.Add(winnerMessage);

                        dealerHand[0].FlipOver();
                        dealerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(dealerHand).ToString(),
                            messageFont,
                            new Vector2(WindowWidth - HorizontalMessageOffset, ScoreMessageTopOffset));
                        messages.Add(dealerScoreMessage);

                        menuButtons.RemoveRange(1, 2);

                        gameOver = true;

                        ChangeState(GameState.DisplayingHandResults);
                    }
                    else if (!playerHit && !dealerHit)
                    {
                        ChangeState(GameState.DisplayingHandResults);
                    }
                    else
                    {
                        playerHit = false;
                        dealerHit = false;
                        ChangeState(GameState.WaitingForPlayer);
                    }
                    break;
                case GameState.DealerHitting:
                    dealerHit = true;
                    Card addDealerCard = deck.TakeTopCard();
                    addDealerCard.X = WindowWidth - HorizontalCardOffset;
                    addDealerCard.Y = TopCardOffset + VerticalCardSpacing * dealerHand.Count;
                    addDealerCard.FlipOver();
                    dealerHand.Add(addDealerCard);
                    ChangeState(GameState.CheckingHandOver);
                    break;
                case GameState.DisplayingHandResults:
                    int playerScore = GetBlockjuckScore(playerHand);
                    int dealerScore = GetBlockjuckScore(dealerHand);
                    if (playerScore <= MaxHandValue &&
                        dealerScore <= MaxHandValue &&
                        !gameOver)
                    {
                        if (playerScore > dealerScore)
                        {
                            winnerMessage = new Message("Player Won!", messageFont, winnerMessageLocation);
                        }
                        else if (playerScore < dealerScore)
                        {
                            winnerMessage = new Message("Dealer Won!", messageFont, winnerMessageLocation);
                        }
                        else
                        {
                            winnerMessage = new Message("Tie!", messageFont, winnerMessageLocation);
                        }
                        messages.Add(winnerMessage);

                        dealerHand[0].FlipOver();
                        dealerScoreMessage = new Message(ScoreMessagePrefix + GetBlockjuckScore(dealerHand).ToString(),
                            messageFont,
                            new Vector2(WindowWidth - HorizontalMessageOffset, ScoreMessageTopOffset));
                        messages.Add(dealerScoreMessage);

                        gameOver = true;

                        menuButtons.RemoveRange(1, 2);
                    }
                    break;
                case GameState.Exiting:
                    Exit();
                    break;
                case GameState.PlayerHitting:
                    playerHit = true;
                    Card addPlayerCard = deck.TakeTopCard();
                    addPlayerCard.X = HorizontalCardOffset;
                    addPlayerCard.Y = TopCardOffset + VerticalCardSpacing * playerHand.Count;
                    addPlayerCard.FlipOver();
                    playerHand.Add(addPlayerCard);
                    messages[0].Text = ScoreMessagePrefix + GetBlockjuckScore(playerHand).ToString();
                    ChangeState(GameState.WaitingForDealer);
                    break;
                case GameState.WaitingForDealer:
                    if (GetBlockjuckScore(dealerHand) < 17)
                    {
                        ChangeState(GameState.DealerHitting);
                    }
                    else
                    {
                        ChangeState(GameState.CheckingHandOver);
                    }
                    break;
                case GameState.WaitingForPlayer:
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Goldenrod);

            spriteBatch.Begin();

            // draw hands
            foreach (Card card in playerHand)
            {
                card.Draw(spriteBatch);
            }

            foreach (Card card in dealerHand)
            {
                card.Draw(spriteBatch);
            }

            // draw messages
            foreach (Message message in messages)
            {
                message.Draw(spriteBatch);
            }

            // draw menu buttons
            foreach (MenuButton menuButton in menuButtons)
            {
                menuButton.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Calculates the Blockjuck score for the given hand
        /// </summary>
        /// <param name="hand">the hand</param>
        /// <returns>the Blockjuck score for the hand</returns>
        private int GetBlockjuckScore(List<Card> hand)
        {
            // add up score excluding Aces
            int numAces = 0;
            int score = 0;
            foreach (Card card in hand)
            {
                if (card.Rank != Rank.Ace)
                {
                    score += GetBlockjuckCardValue(card);
                }
                else
                {
                    numAces++;
                }
            }

            // if more than one ace, only one should ever be counted as 11
            if (numAces > 1)
            {
                // make all but the first ace count as 1
                score += numAces - 1;
                numAces = 1;
            }

            // if there's an Ace, score it the best way possible
            if (numAces > 0)
            {
                if (score + 11 <= MaxHandValue)
                {
                    // counting Ace as 11 doesn't bust
                    score += 11;
                }
                else
                {
                    // count Ace as 1
                    score++;
                }
            }

            return score;
        }

        /// <summary>
        /// Gets the Blockjuck value for the given card
        /// </summary>
        /// <param name="card">the card</param>
        /// <returns>the Blockjuck value for the card</returns>
        private int GetBlockjuckCardValue(Card card)
        {
            switch (card.Rank)
            {
                case Rank.Ace:
                    return 11;
                case Rank.King:
                case Rank.Queen:
                case Rank.Jack:
                case Rank.Ten:
                    return 10;
                case Rank.Nine:
                    return 9;
                case Rank.Eight:
                    return 8;
                case Rank.Seven:
                    return 7;
                case Rank.Six:
                    return 6;
                case Rank.Five:
                    return 5;
                case Rank.Four:
                    return 4;
                case Rank.Three:
                    return 3;
                case Rank.Two:
                    return 2;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="newState">the new game state</param>
        public static void ChangeState(GameState newState)
        {
            currentState = newState;
        }
    }
}
