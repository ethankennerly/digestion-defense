using Finegamedesign.Utils;
using System;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public static class ReceiverUtils
    {
        private const int kEmpty = -1;

        public static bool IsEmpty(ReceiverComponent receiver)
        {
            return receiver.occupantId == kEmpty;
        }

        public static bool AllFull(GameContext context, int[] receiverIds)
        {
            foreach (int receiverId in receiverIds)
            {
                GameEntity receiver = context.GetEntityWithId(receiverId);
                if (ReceiverUtils.IsEmpty(receiver.receiver))
                    return false;
            }
            return true;
        }

        /// <returns>
        /// If receiver is empty and accepts a component of the given name.
        /// </returns>
        public static bool Filter(ReceiverComponent receiver, int componentIndex)
        {
            DebugUtil.Assert(componentIndex >= 0 && componentIndex < GameComponentsLookup.componentNames.Length,
                "ReceiverUtils.Filter: Expects component index " + componentIndex +
                " is in range of game component names " +
                DataUtil.ToString(GameComponentsLookup.componentNames));

            if (!IsEmpty(receiver))
                return false;

            HashSet<int> filterIndexes = receiver.filterComponentIndexes;
            if (filterIndexes == null)
            {
                DebugUtil.Assert(filterIndexes != null,
                    "ReceiverUtils.Filter: Expects filter component indexes are defined on " + receiver +
                    ". componentIndex='" + componentIndex + "'");
                return false;
            }

            return filterIndexes.Contains(componentIndex);
        }

        /// <returns>
        /// If receiver is empty and accepts a component of the given name.
        /// </returns>
        public static bool Filter(ReceiverComponent receiver, string componentName)
        {
            if (!IsEmpty(receiver))
                return false;

            HashSet<int> filterIndexes = receiver.filterComponentIndexes;
            if (filterIndexes == null)
            {
                DebugUtil.Assert(filterIndexes != null,
                    "ReceiverUtils.Filter: Expects filter component indexes are defined on " + receiver +
                    ". componentName='" + componentName + "'");
                return false;
            }

            int componentIndex = ToComponentIndex(componentName);

            return filterIndexes.Contains(componentIndex);
        }

        /// <remarks>
        /// Iterating would scale better if filter names were:
        ///     (A) a bitfield instead of a hash set.  Or,
        ///     (B) an array instead of a hash set.
        /// </remarks>
        /// <returns>
        /// If receiver is empty and candidate has any of the receiver's filter components.
        /// </returns>
        public static bool Filter(ReceiverComponent receiver, GameEntity candidate)
        {
            if (!IsEmpty(receiver))
                return false;

            HashSet<int> filterIndexes = receiver.filterComponentIndexes;
            if (filterIndexes == null)
            {
                DebugUtil.Assert(filterIndexes != null,
                    "ReceiverUtils.Filter: Expects filter component indexes are defined on " + receiver);
                return false;
            }

            foreach (int filterIndex in filterIndexes)
            {
                if (candidate.HasComponent(filterIndex))
                    return true;
            }

            return false;
        }

        public static int ToComponentIndex(string componentName)
        {
            string[] componentNames = GameComponentsLookup.componentNames;
            int componentIndex = Array.IndexOf(componentNames, componentName);
            DebugUtil.Assert(componentIndex >= 0,
                "ReceiverUtils.Filter: Expects component '" + componentName + "' in game component names " +
                DataUtil.ToString(componentNames));
            return componentIndex;
        }

        public static void ReplaceOccupant(GameEntity receiver, GameEntity occupant)
        {
            receiver.ReplaceReceiver(
                receiver.receiver.filterComponentIndexes,
                occupant.id.value
            );
        }

        public static void Transfer(GameEntity giver, GameEntity receiver)
        {
            receiver.ReplaceReceiver(
                receiver.receiver.filterComponentIndexes,
                giver.receiver.occupantId
            );

            SetEmpty(giver);
        }

        public static void SetEmpty(GameEntity receiver)
        {
            receiver.ReplaceReceiver(
                receiver.receiver.filterComponentIndexes,
                kEmpty
            );
        }

        public static void SetEmpty(GameContext context, int[] receiverIds)
        {
            foreach (int receiverId in receiverIds)
            {
                GameEntity receiver = context.GetEntityWithId(receiverId);
                SetEmpty(receiver);
            }
        }

        /// <param name="giverIds">
        /// Selects first acceptable giver or gift in that giver's receiver.  Sets that giver ID to empty if selected.
        /// </param>
        public static void OccupyIfEmpty(GameContext context, int[] receiverIds, int[] giverIds)
        {
            foreach (int receiverId in receiverIds)
            {
                GameEntity receiver = context.GetEntityWithId(receiverId);
                if (!IsEmpty(receiver.receiver))
                    continue;

                GameEntity[] giverEntities = GameLinkUtils.GetEntitiesWithIds(context, giverIds);
                foreach (GameEntity giverEntity in giverEntities)
                {
                    if (Filter(receiver.receiver, giverEntity))
                    {
                        ReplaceOccupant(receiver, giverEntity);
                        return;
                    }

                    int giftId = giverEntity.receiver.occupantId;
                    if (giftId == kEmpty)
                        continue;

                    GameEntity gift = context.GetEntityWithId(giftId);
                    if (gift == null)
                    {
                        DebugUtil.Assert(gift != null,
                            "ReceiverUtils.OccupyIfEmpty: Expected gift was not null. Gift ID=" + giftId);
                        continue;
                    }

                    if (Filter(receiver.receiver, gift))
                    {
                        Transfer(giverEntity, receiver);
                        return;
                    }
                }

                DebugUtil.Assert(false,
                    "ReceiverUtils.OccupyIfEmpty: Expected some filter to match. " +
                    "giverEntities=" + DataUtil.ToString(giverEntities) + " " +
                    "receiver=" + receiver
                );
            }
        }

        /// <summary>
        /// TODO
        /// Receiver might be an input or an output of a transmitter.
        /// As in a Petri Net, only transmits if all receivers are occupied.
        /// Unlike a Petri Net, only transmits if to empty outputs.
        /// Also unlike a Petri Net, preserves the input entity if it would be a suitable output.
        ///
        /// To deposit into one of many alternatives, output to a receiver selector.
        ///
        /// Expects each input and output has a receiver, or else Entitas throws an exception.
        /// </summary>
        public static void TryTransmit(GameContext context, GameEntity receiver, int[] inputIds, int[] outputIds)
        {
            if (!AllFull(context, inputIds))
                return;

            OccupyIfEmpty(context, outputIds, inputIds);

            SetEmpty(context, inputIds);
        }
    }
}
