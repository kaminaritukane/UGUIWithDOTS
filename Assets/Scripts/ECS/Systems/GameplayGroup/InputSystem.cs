using System;
using System.Text;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace INF.GamePlay
{
    [DisableAutoCreation]
    [UpdateInGroup(typeof(GamePlaySystemGroup))]
    public class InputSystem : SystemBase
    {
        public enum Button : uint
        {
            None = 0,
            MoveFowards = 1 << 0,
            MoveBackwards = 1 << 1,
            MoveRight = 1 << 2,
            MoveLeft = 1 << 3,
        }

        public struct ButtonBitField
        {
            public uint flags;
            public uint changed;

            public void GetButtonFlags()
            {
                changed = 0;

                // forward
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Set(Button.MoveFowards, true);
                }
                else if (Input.GetKeyUp(KeyCode.W))
                {
                    Set(Button.MoveFowards, false);
                }

                // backward
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Set(Button.MoveBackwards, true);
                }
                else if (Input.GetKeyUp(KeyCode.S))
                {
                    Set(Button.MoveBackwards, false);
                }

                // right
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Set(Button.MoveRight, true);
                }
                else if (Input.GetKeyUp(KeyCode.D))
                {
                    Set(Button.MoveRight, false);
                }

                // left
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Set(Button.MoveLeft, true);
                }
                else if (Input.GetKeyUp(KeyCode.A))
                {
                    Set(Button.MoveLeft, false);
                }
            }

            public bool HasChanges()
            {
                return changed > 0;
            }

            // from up to down
            public bool IsDown(Button button)
            {
                return (changed & (uint)button) > 0
                    && IsSet(button);
            }

            // from down to up
            public bool IsUp(Button button)
            {
                return (changed & (uint)button) > 0
                    && !IsSet(button);
            }

            // check only the flags
            public bool IsSet(Button button)
            {
                return (flags & (uint)button) > 0;
            }

            private void Set(Button button, bool val)
            {
                if (IsSet(button) == val)
                {
                    return;
                }

                changed |= (uint)button;

                if (val)
                {
                    flags = flags | (uint)button;
                }
                else
                {
                    flags = flags & ~(uint)button;
                }
            }

            public override string ToString()
            {
                var stringBuilder = new StringBuilder();
                var names = Enum.GetNames(typeof(Button));
                var values = Enum.GetValues(typeof(Button));
                stringBuilder.Append("<");
                for (int i = 0; i < names.Length; i++)
                {
                    var value = (uint)values.GetValue(i);
                    if ((flags & value) == 0)
                        continue;

                    stringBuilder.Append("," + names[i]);
                }
                stringBuilder.Append(">");
                return stringBuilder.ToString();
            }
        }

        private ButtonBitField _buttons = new ButtonBitField();

        private EndSimulationEntityCommandBufferSystem _ecb;

        protected override void OnCreate()
        {
            base.OnCreate();

            _ecb = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            _buttons.GetButtonFlags();
            if (!_buttons.HasChanges())
            {
                return;
            }

            var ecb = _ecb.CreateCommandBuffer().ToConcurrent();

            NativeArray<ButtonBitField> buttonsArray = new NativeArray<ButtonBitField>(1, Allocator.TempJob);
            buttonsArray[0] = _buttons;

            Entities.ForEach((int entityInQueryIndex,
                Entity entity,
                ref InputReceiver iptReceiver,
                ref DynamicBuffer<UnitAction> actions,
                in Player player) =>
            {
                var buttons = buttonsArray[0];

                // forward
                if (buttons.IsDown(Button.MoveFowards))
                {
                    ecb.AppendToBuffer(entityInQueryIndex, entity, new UnitAction
                    {
                        action = UnitAction.eUnitAction.MoveForward,
                        parameter = 1.0f
                    });
                }
                else if (buttons.IsUp(Button.MoveFowards))
                {
                    ecb.AppendToBuffer(entityInQueryIndex, entity, new UnitAction
                    {
                        action = UnitAction.eUnitAction.StopMoveForward,
                        parameter = 1.0f
                    });
                }


                // backward
                if (buttons.IsDown(Button.MoveBackwards))
                {
                    ecb.AppendToBuffer(entityInQueryIndex, entity, new UnitAction
                    {
                        action = UnitAction.eUnitAction.MoveForward,
                        parameter = -1.0f
                    });
                }
                else if (buttons.IsUp(Button.MoveBackwards))
                {
                    ecb.AppendToBuffer(entityInQueryIndex, entity, new UnitAction
                    {
                        action = UnitAction.eUnitAction.StopMoveForward,
                        parameter = -1.0f
                    });
                }

                // right
                if (buttons.IsDown(Button.MoveRight))
                {
                    ecb.AppendToBuffer(entityInQueryIndex, entity, new UnitAction
                    {
                        action = UnitAction.eUnitAction.MoveRight,
                        parameter = 1.0f
                    });
                }
                else if (buttons.IsUp(Button.MoveRight))
                {
                    ecb.AppendToBuffer(entityInQueryIndex, entity, new UnitAction
                    {
                        action = UnitAction.eUnitAction.StopMoveRight,
                        parameter = 1.0f
                    });
                }

                // left
                if (buttons.IsDown(Button.MoveLeft))
                {
                    ecb.AppendToBuffer(entityInQueryIndex, entity, new UnitAction
                    {
                        action = UnitAction.eUnitAction.MoveRight,
                        parameter = -1.0f
                    });
                }
                else if (buttons.IsUp(Button.MoveLeft))
                {
                    ecb.AppendToBuffer(entityInQueryIndex, entity, new UnitAction
                    {
                        action = UnitAction.eUnitAction.StopMoveRight,
                        parameter = -1.0f
                    });
                }
            })
            .WithDeallocateOnJobCompletion(buttonsArray)
            .Schedule();

            _ecb.AddJobHandleForProducer(this.Dependency);
        }
    }
}