﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using Clkd.Assets;
using Clkd.Main;
using Clkd.Managers;
using Clkd.State;

using CloakedTetris.Shapes;
using CloakedTetris.State;
using Clkd.GUI;
using Clkd.GUI.Layouts;

namespace CloakedTetris.Initializers
{
    public static class GameInitializer
    {
        public static void InitializeGame(ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {

            //TODO pull rendering manager out and make it top level????


            ContextManager contextManager = new ContextManager();
            // Create game state object.
            ContextualGameState GameState = new ContextualGameState("global", "start");

            contextManager.AddNewContext(newId: "global")
                .AddComponent(new RenderableManager(graphicsDeviceManager, new Camera2D()))
                .AddComponent(GameState);

            GuiPane pane = new GuiPane(new GuiContainer(new GuiGridLayout(2, 2)));
            pane.RootContainer.TopPadding = 20f;
            pane.RootContainer.BottomPadding = 20f;
            pane.RootContainer.RightPadding = 20f;
            pane.RootContainer.LeftPadding = 20f;
            pane.RootContainer.GetLayout<GuiGridLayout>().HorizontalGutter = 5;
            pane.RootContainer.GetLayout<GuiGridLayout>().VerticalGutter = 20;
            pane.RootContainer.GetLayout<GuiGridLayout>().AddComponent(0, 0, new ColoredRectangle(100, 100, Color.CornflowerBlue));
            pane.RootContainer.GetLayout<GuiGridLayout>().AddComponent(0, 1, new ColoredRectangle(100, 100, Color.Red));
            pane.RootContainer.GetLayout<GuiGridLayout>().AddComponent(1, 0, new ColoredRectangle(100, 100, Color.Green));
            pane.RootContainer.GetLayout<GuiGridLayout>().AddComponent(1, 1, new ColoredRectangle(100, 100, Color.Purple));


            // // Create the first context your game will operate in.
            contextManager.AddShallowCopyOf(id: "global", newId: "start")
                .AddComponent(pane);

            // set up the context 
            InitializeStartContext(contextManager.GetContext(id: "start"));

            contextManager.GetContext("global").GetComponent<RenderableManager>()
                .SetWindowSize(320, 640)
                .SetCameraView(new Rectangle(0, 0, 320, 640));

            // Initialize CloakedGame.
            Cloaked.Initialize(content, graphicsDeviceManager, contextManager);
            Cloaked.ContextManager.ActivateContext("start");
            pane.Initialize();
            Cloaked.Ready = true;
        }

        private static void InitializeStartContext(GameContext context)
        {
            context.AddComponent(new KeyboardInputManager()
            // Register a keybinding that will start the game when any key is pressed.
            // The trigger will push the in-game game context to take over.
                .RegisterKeyMapping(
                    KeyMapping.GetMappingToAnyKey("pressAnyKey"),
                    new GenericInputTrigger<KeyStatus>(
                        (status) =>
                        {
                            return status.KeyTyped();
                        },
                        (KeyStatus status) =>
                        {
                            SetUpNewGame(Level.GetLevel(1));
                        }
                    )
                )
            );

            context.AddComponent(new MouseInputManager()
                .RegisterMouseMapping(
                    MouseMapping.GetMappingToAnyButton("clickAnyWhere"),
                    new GenericInputTrigger<MouseStatus>(
                        (status) =>
                        {
                            return status.Clicked();
                        },
                        (MouseStatus status) =>
                        {
                            SetUpNewGame(Level.GetLevel(1));
                        }
                    )
                )
            );
        }

        private static void InitializeGameContext(GameContext context)
        {
            context.AddComponent(new KeyboardInputManager())
            .AddComponent(new TriggerManager())
            .AddComponent(new ValueGameState());

            context.GetComponent<KeyboardInputManager>().RegisterKeyMapping(
                new KeyMapping("down", Keys.Down),
                new GenericInputTrigger<KeyStatus>(
                    (status) => { return status.KeyTyped() || (status.KeyHeld() && status.DurationSinceLastExecute.TotalMilliseconds > 100); },
                    (KeyStatus status) =>
                    {
                        var state = Cloaked.GetComponent<ContextualGameState>();
                        state.GetComponent<AbstractShape>("game", "activeShape")
                        .Drop(state.GetComponent<Level>("game", "level").Grid);
                        Cloaked.GetComponent<ValueGameState>().SetBoolValue("preventTimedDrop", true);
                    }
                )
            )

            .RegisterKeyMapping(
                new KeyMapping("down", Keys.Down),
                new GenericInputTrigger<KeyStatus>(
                    (status) => { return status.KeyReleased(); },
                    (KeyStatus status) =>
                    {
                        Cloaked.GetComponent<ValueGameState>().SetBoolValue("preventTimedDrop", false);
                    }
                )
            )

            .RegisterKeyMapping(
                new KeyMapping("left", Keys.Left),
                new GenericInputTrigger<KeyStatus>(
                    (status) => { return status.KeyTyped() || (status.KeyHeld() && status.DurationSinceLastExecute.TotalMilliseconds > 200); },
                    (KeyStatus status) =>
                    {
                        var state = Cloaked.GetComponent<ContextualGameState>();
                        state.GetComponent<AbstractShape>("game", "activeShape")
                        .MoveLeft(state.GetComponent<Level>("game", "level").Grid);
                    }
                )
            )

            .RegisterKeyMapping(
                new KeyMapping("space", Keys.Up),
                new GenericInputTrigger<KeyStatus>(
                    (status) => { return status.KeyTyped() || (status.KeyHeld() && status.DurationSinceLastExecute.TotalMilliseconds > 200); },
                    (KeyStatus status) =>
                    {
                        var state = Cloaked.GetComponent<ContextualGameState>();
                        state.GetComponent<AbstractShape>("game", "activeShape")
                        .Rotate(state.GetComponent<Level>("game", "level").Grid);
                    }
                )
            )
            .RegisterKeyMapping(
                new KeyMapping("right", Keys.Right),
                new GenericInputTrigger<KeyStatus>(
                    (status) => { return status.KeyTyped() || (status.KeyHeld() && status.DurationSinceLastExecute.TotalMilliseconds > 200); },
                    (KeyStatus status) =>
                    {
                        var state = Cloaked.GetComponent<ContextualGameState>();
                        state.GetComponent<AbstractShape>("game", "activeShape")
                        .MoveRight(state.GetComponent<Level>("game", "level").Grid);
                    }
                )
            );
        }

        public static void SetUpNewGame(Level level)
        {
            GameContext context = Cloaked.ContextManager.AddShallowCopyOf("global", "game");
            InitializeGameContext(context);
            var GameStateContext = context.GetComponent<ContextualGameState>().InitializeContext("game");

            GameStateContext.Add("level", level);
            GameStateContext.Add("activeShape", ShapeManager.NextShape);

            var effect = new TimedAction(
                (gameTime, actionState) =>
                {
                    if (!Cloaked.GetComponent<ValueGameState>().GetBoolValue("preventTimedDrop"))
                    {
                        ContextualGameState gs = Cloaked.GetComponent<ContextualGameState>();
                        gs.GetComponent<AbstractShape>("game", "activeShape").Drop(gs.GetComponent<Level>("game", "level").Grid);
                    }

                }, new TimeSpan(0, 0, 1));

            context.AddComponent("drop_every_1000_ms_state", new TimedExecutionState(effect));

            var collision_trigger = new Trigger("collision_trigger",
                () =>
                {
                    ContextualGameState gs = Cloaked.GetComponent<ContextualGameState>();
                    return gs.GetComponent<AbstractShape>("game", "activeShape").Collided;
                },
                () =>
                {
                    var gs = Cloaked.GetComponent<ContextualGameState>();
                    gs.SetComponent("game", "activeShape", ShapeManager.NextShape);
                },
                3,
                false,
                false
            );

            var line_trigger = new Trigger("line_trigger",
                () =>
                {
                    ContextualGameState gs = Cloaked.GetComponent<ContextualGameState>();
                    return gs.GetComponent<AbstractShape>("game", "activeShape").Collided;
                },
                () =>
                {
                    ContextualGameState gs = Cloaked.GetComponent<ContextualGameState>();
                    Level lvl = gs.GetComponent<Level>("game", "level");
                    lvl.CheckRows();
                },
                4,
                false,
                false
            );

            context.GetComponent<TriggerManager>().Subscribe("drop", collision_trigger);
            context.GetComponent<TriggerManager>().Subscribe("drop", line_trigger);

            Cloaked.ContextManager.ActivateContext("game");
        }
    }
}
